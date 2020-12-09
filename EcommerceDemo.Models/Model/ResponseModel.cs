using System.Collections.Generic;
using System.Net;

namespace EcommerceDemo.Models.Model
{
    public class ResponseModel
    {
        public HttpStatusCode StatusCode;

        public string Message { get; set; }

        public bool Status { get; set; }

        public string Id { get; set; }

        public int Count { get; set; }

        public object SingleObject { get; set; }

        public IEnumerable<object> ListObject { get; set; }
    }
}
