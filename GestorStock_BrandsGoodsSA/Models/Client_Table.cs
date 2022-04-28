using System;
using System.Collections.Generic;

#nullable disable

namespace GestorStock_BrandsGoodsSA.Models
{
    public partial class Client
    {
        public Client()
        {
            Requests = new HashSet<Request>();
        }

        public int ClientId { get; set; }
        public string ClientCode { get; set; }
        public string ClientName { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}
