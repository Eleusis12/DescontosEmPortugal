using System;
using System.Collections.Generic;

namespace DescontosEmPortugal.Web.Models
{
    public partial class SitesAverificar
    {
        public SitesAverificar()
        {
            Product = new HashSet<Product>();
        }

        public int IdPesquisa { get; set; }
        public int IdWebsite { get; set; }
        public int IdCategoria { get; set; }

        public virtual Categoria IdCategoriaNavigation { get; set; }
        public virtual Website IdWebsiteNavigation { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
