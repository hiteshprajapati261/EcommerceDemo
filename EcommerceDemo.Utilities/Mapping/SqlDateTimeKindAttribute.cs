namespace EcommerceDemo.Utilities.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Specifies the <see cref="DateTimeKind"/> of a <see cref="DateTime"/> property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SqlDateTimeKindAttribute : Attribute
    {
        public DateTimeKind Kind { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kind">DateTimeKind to use</param>
        public SqlDateTimeKindAttribute(DateTimeKind kind)
        {
            Kind = kind;
        }
    }
}
