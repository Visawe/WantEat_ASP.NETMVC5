using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eat.Models
{
    public class GoogleReview
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public int? GoogleRestaurantDetailsId { get; set; }
        [JsonIgnore]
        public virtual GoogleRestaurantDetails GoogleRestaurantDetails { get; set; }
        [JsonProperty("author_name")]
        public string Author_name { get; set; }
        [JsonProperty("author_url")]
        public string Author_url { get; set; }
        [JsonProperty("language")]
        public string Language { get; set; }
        [JsonProperty("profile_photo_url")]
        public string Profile_photo_url { get; set; }
        [JsonProperty("rating")]
        public int? Rating { get; set; }
        [JsonProperty("relative_time_description")]
        public string Relative_time_description { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("time")]
        public int? Time { get; set; }

        public bool Equals(GoogleReview p)
        {
            // If parameter is null return false:
            if (p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (GoogleRestaurantDetailsId == p.GoogleRestaurantDetailsId) 
                && (Author_name == p.Author_name)
                      && (Rating == p.Rating)
                          && (Text == p.Text)
                            && (Time == p.Time);
        }
    }
}