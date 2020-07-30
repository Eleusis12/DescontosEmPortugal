using System;
using System.Collections.Generic;

namespace WebKuantoKustaScrapper.Models
{
    public partial class Product
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Marca { get; set; }
        public int IdCategoria { get; set; }
        public string Imagem { get; set; }
        public string Website { get; set; }
        public int IdPreco { get; set; }
        public int? IdPesquisa { get; set; }
        public int Popularidade { get; set; }

        public virtual Categoria IdCategoriaNavigation { get; set; }
        public virtual SitesAverificar IdPesquisaNavigation { get; set; }
        public virtual Preco IdPrecoNavigation { get; set; }
    }
}
