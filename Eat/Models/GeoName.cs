using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eat.Models
{
    public class GeoName
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GeoName()
        {
            GeoIPLocations = new HashSet<GeoIPLocation>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int geoname_id { get; set; }

        [StringLength(150)]
        public string locale_code { get; set; }

        [StringLength(150)]
        public string continent_code { get; set; }

        [StringLength(150)]
        public string continent_name { get; set; }

        [StringLength(150)]
        public string country_iso_code { get; set; }

        [StringLength(150)]
        public string country_name { get; set; }

        [StringLength(150)]
        public string subdivision_1_iso_code { get; set; }

        [StringLength(150)]
        public string subdivision_1_name { get; set; }

        [StringLength(150)]
        public string subdivision_2_iso_code { get; set; }

        [StringLength(150)]
        public string subdivision_2_name { get; set; }

        [StringLength(150)]
        public string city_name { get; set; }

        [StringLength(50)]
        public string metro_code { get; set; }

        [StringLength(150)]
        public string time_zone { get; set; }

        public int? is_in_european_union { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GeoIPLocation> GeoIPLocations { get; set; }
    }
}