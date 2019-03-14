using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eat.Models
{
    public class GeoIPLocation
    {
        [Key]
        [StringLength(150)]
        public string network { get; set; }

        public int? geoname_id { get; set; }

        public int? registered_country_geoname_id { get; set; }

        [StringLength(100)]
        public string represented_country_geoname_id { get; set; }

        public int? is_anonymous_proxy { get; set; }

        public int? is_satellite_provider { get; set; }

        [StringLength(150)]
        public string postal_code { get; set; }

        public double? latitude { get; set; }

        public double? longitude { get; set; }

        public int? accuracy_radius { get; set; }

        public int? networkFrom { get; set; }

        public int? networkTo { get; set; }

        public virtual GeoName GeoName { get; set; }
    }
}