using System;
using System.Collections.Generic;

namespace WebKuantoKustaScrapper.Models
{
    public partial class PrecoVariacoes
    {
        public int IdVariacao { get; set; }
        public int IdPreco { get; set; }
        public float Preco { get; set; }
        public DateTime DataAlteracao { get; set; }

        public virtual Preco IdPrecoNavigation { get; set; }
    }
}
