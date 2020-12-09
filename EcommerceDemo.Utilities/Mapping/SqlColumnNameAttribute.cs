namespace EcommerceDemo.Utilities.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Binds the property value to another column name, instead of using the name of the property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SqlColumnNameAttribute : Attribute
    {
        public string Column { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="column">The name of the column in SqlDataReader</param>
        public SqlColumnNameAttribute(string column)
        {
            Column = column;
        }
    }
}
