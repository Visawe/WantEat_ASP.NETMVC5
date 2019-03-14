using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eat.Models
{
    
    public class GoogleRestaurantDetails
    {
        public GoogleRestaurantDetails()
        {
            GoogleReviews = new HashSet<GoogleReview>();
        }
   
        [Key, JsonIgnore]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("place_id")]
        public string Place_id { get; set; }
        [JsonProperty("rating")]
        public double Rating { get; set; }
        [JsonIgnore]
        public DateTime? DateTimeLastUpdate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonProperty("reviews")]
        public virtual ICollection<GoogleReview> GoogleReviews { get; set; }
        [JsonIgnore]
        public virtual Restaurant Restaurant { get; set; }

        public static explicit operator GoogleRestaurantDetails(JToken v)
        {
            throw new NotImplementedException();
        }
    }
}