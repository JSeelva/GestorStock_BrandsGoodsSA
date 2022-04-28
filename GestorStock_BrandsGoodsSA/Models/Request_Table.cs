using System;
using System.Collections.Generic;

#nullable disable

namespace GestorStock_BrandsGoodsSA.Models
{
    public partial class Request
    {
        public Request()
        {
            RequestDetails = new HashSet<RequestDetail>();
        }

        public int RequestId { get; set; }
        public int ClientId { get; set; }

        public virtual Client Client { get; set; }
        public virtual ICollection<RequestDetail> RequestDetails { get; set; }


    }
}
