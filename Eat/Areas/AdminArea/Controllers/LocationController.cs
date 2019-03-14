using Eat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Eat.Areas.AdminArea.Controllers
{
    public class LocationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: AdminArea/Location
        public ActionResult Index()
        {
            return View();
        }

        static long ToInt(string addr)
        {
            // careful of sign extension: convert to uint first;
            // unsigned NetworkToHostOrder ought to be provided.
            return (long)(uint)IPAddress.NetworkToHostOrder(
                 (int)IPAddress.Parse(addr).Address);
        }

        static string ToAddr(long address)
        {
            return IPAddress.Parse(address.ToString()).ToString();
            // This also works:
            // return new IPAddress((uint) IPAddress.HostToNetworkOrder(
            //    (int) address)).ToString();
        }

        static long getIntOffset(string offset)
        {
            int number = Convert.ToInt32(offset);


            return 0;
        }

        public ActionResult StartManageGeoIPLocation()
        {
            List<GeoIPLocation> geoIPLocations = db.GeoIPLocations.ToList();
            foreach (GeoIPLocation item in geoIPLocations)
            {
                string[] words = item.network.Split(new char[] { '/' });
                long adrInt = ToInt(words[0]);


            }
            return View();
        }
    }
}
     