namespace EcommerceDemo.Utilities.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// This member is ignored by SqlMapper.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SqlIgnoreAttribute : Attribute
    {
    }
}
