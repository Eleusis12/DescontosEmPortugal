using System;
using System.Collections.Generic;

namespace DescontosEmPortugal.Web.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            Product = new HashSet<Product>();
            SitesAverificar = new HashSet<SitesAverificar>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<Product> Product { get; set; }
        public virtual ICollection<SitesAverificar> SitesAverificar { get; set; }
    }
}
