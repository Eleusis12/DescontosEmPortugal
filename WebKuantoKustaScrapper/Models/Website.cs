using System;
using System.Collections.Generic;

namespace WebKuantoKustaScrapper.Models
{
    public partial class Website
    {
        public Website()
        {
            SitesAverificar = new HashSet<SitesAverificar>();
        }

        public int IdWebsite { get; set; }
        public string SiteUrl { get; set; }

        public virtual ICollection<SitesAverificar> SitesAverificar { get; set; }
    }
}
