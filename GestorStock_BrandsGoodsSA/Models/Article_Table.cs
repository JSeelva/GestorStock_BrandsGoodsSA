using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

//#nullable disable

namespace GestorStock_BrandsGoodsSA.Models
{
    [Serializable, DataContract]
    public partial class Article
    {
        [IgnoreDataMember]
        private int _articleCode;
        [IgnoreDataMember]
        private string _articleName;
        [IgnoreDataMember]
        private double _articlePrice;
        [IgnoreDataMember]
        private int _articleAmount;

        public Article()
        {
            RequestDetails = new HashSet<RequestDetail>();
        }

        public Article(int articleCode, string articleName, double articlePrice, int articleAmount)
        {
            ArticleCode = articleCode;
            ArticleName = articleName;
            ArticlePrice = articlePrice;
            ArticleAmount = articleAmount;
        }

        [IgnoreDataMember]
        public int ArticleId { get ; set; }

        [DataMember]
        public int ArticleCode { get => _articleCode; set => _articleCode = value; }

        [DataMember]
        public string ArticleName { get => _articleName; set => _articleName = value; }

        [DataMember]
        public double ArticlePrice { get => _articlePrice; set => _articlePrice = value; }

        [DataMember]
        public int ArticleAmount { get => _articleAmount; set => _articleAmount = value; }

        [IgnoreDataMember]
        public bool ArticleState { get; set; }

        [IgnoreDataMember]
        public virtual ICollection<RequestDetail> RequestDetails { get; set; }

        public override string ToString()
        {
            return $"Código: {_articleCode}, Nome: {_articleName}, Preço: {_articlePrice}, Stock: {_articleAmount}";
        }

        public static implicit operator List<object>(Article a)
        {
            throw new NotImplementedException();
        }

    }
}
