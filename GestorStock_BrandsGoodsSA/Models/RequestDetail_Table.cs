using System;
using System.Collections.Generic;

#nullable disable

namespace GestorStock_BrandsGoodsSA.Models
{
    /// <summary>
    /// Classe que faz a ligação com a tabela RequestDetail da base de dados.
    /// </summary>
    public partial class RequestDetail
    {
        public int RequestDetailId { get; set; }
        public int RequestId { get; set; }
        public int ArticleId { get; set; }
        public int ArticleQuantity { get; set; }

        public virtual Article Article { get; set; }
        public virtual Request Request { get; set; }
    }
}
