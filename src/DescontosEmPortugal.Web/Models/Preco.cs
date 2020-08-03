using System;
using System.Collections.Generic;

namespace DescontosEmPortugal.Web.Models
{
    public partial class Preco
    {
        public Preco()
        {
            PrecoVariacoes = new HashSet<PrecoVariacoes>();
            Product = new HashSet<Product>();
        }

        public int IdPreco { get; set; }
        public float PrecoAtual { get; set; }
        public float? PrecoMaisBaixo { get; set; }
        public bool PrecoMaisBaixoFlag { get; set; }
        public bool? NewProduct { get; set; }
        public DateTime DataPrecoMaisBaixo { get; set; }
        public float Soma { get; set; }
        public int Contador { get; set; }

        public virtual ICollection<PrecoVariacoes> PrecoVariacoes { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
