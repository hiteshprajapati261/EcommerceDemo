namespace EcommerceDemo.Utilities.Mapping.Internal
{
    using System;
    using System.Collections.Concurrent;
    using System.Data;
    using System.Globalization;
    using System.Reflection;

    internal class ClassBinding
    {
        public Type Type { get; private set; }

        public ConstructorInfo Constructor { get; private set; }

        private readonly ConcurrentDictionary<string, ColumnPropertyBinding> _bindings = new ConcurrentDictionary<string, ColumnPropertyBinding>();

        public ClassBinding(Type type, ConstructorInfo constructor)
        {
            Type = type;
            Constructor = constructor;
        }

        public void AddPropertyBinding(ColumnPropertyBinding binding)
        {
            if (_bindings.ContainsKey(binding.ColumnName))
                throw new ArgumentException($"The property binding for {binding.PropertyInfo.Name} (Column: {binding.ColumnName}) already exists in class {Type.FullName}.");

            _bindings[binding.ColumnName] = binding;
        }

        public object Map(IDataRecord record)
        {
            var result = Constructor.Invoke(new object[] { });

            for (int i = 0; i < record.FieldCount; ++i)
            {
                var name = record.GetName(i);

                ColumnPropertyBinding binding;
                if (_bindings.TryGetValue(name, out binding))
                {
                    var propertyInfo = binding.PropertyInfo;
                    var propertyType = binding.PropertyInfo.PropertyType;

                    if (!record.IsDBNull(i))
                    {
                        var recordValue = record.GetValue(i);

                        if (propertyType.IsArray &&
                            recordValue is string)
                        {
                            var elementType = propertyType.GetElementType();
                            var data = TextArrayPacker.Unpack(elementType, (string)recordValue);

                            propertyInfo.SetValue(result, data);
                        }
                        else
                        {
                            // resolve enum's
                            if (propertyType.IsEnum)
                                recordValue = Enum.ToObject(propertyType, recordValue);

                            if (!propertyType.IsGenericType ||
                                propertyType.GetGenericTypeDefinition() != typeof(Nullable<>))
                            {
                                recordValue = Convert.ChangeType(
                                    recordValue,
                                    propertyType,
                                    CultureInfo.InvariantCulture);
                            }

                            if (recordValue is DateTime)
                            {
                                //var dateTimeKindAttribute = propertyInfo.GetCustomAttribute<SqlDateTimeKindAttribute>();
                                //if (dateTimeKindAttribute == null)
                                //    throw new SqlModelException($"The property '{propertyInfo.Name}' in {Type.FullName} does not specify [SqlDateTimeKind].");

                                //recordValue = DateTime.SpecifyKind((DateTime)recordValue, dateTimeKindAttribute.Kind);
                            }

                            propertyInfo.SetValue(result, recordValue);
                        }
                    }
                    else
                        propertyInfo.SetValue(result, null);
                }
            }

            return result;
        }
    }
}
