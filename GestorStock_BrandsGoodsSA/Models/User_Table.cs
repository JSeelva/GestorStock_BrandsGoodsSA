using System;
using System.Collections.Generic;

#nullable disable

namespace GestorStock_BrandsGoodsSA.Models
{
    partial class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int UserNumber { get; set; }
        public string PassWord { get; set; }
        public bool Admin { get; set; }
    }
}
