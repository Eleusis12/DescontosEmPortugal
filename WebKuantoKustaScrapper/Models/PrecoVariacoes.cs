using System;
using System.Collections.Generic;

namespace WebKuantoKustaScrapper.Models
{
    public partial class PrecoVariacoes
    {
        public int IdPreco { get; set; }
        public int Preco { get; set; }
        public DateTime DataAlteracao { get; set; }

        public virtual Preco IdPrecoNavigation { get; set; }
    }
}
