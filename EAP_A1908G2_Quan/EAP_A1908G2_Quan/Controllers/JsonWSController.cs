using EAP_A1908G2_Quan.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EAP_A1908G2_Quan.Controllers
{
    public class JsonWSController : ApiController
    {
        public DataContext db = new DataContext();
        public JToken GetAlbums()
        {
            return JToken.FromObject(db.Albums);
        }

        // GET: api/JsonWS/5
        [System.Web.Http.Description.ResponseType(typeof(Album))]

        public JToken GetAlbum(int id)
        {
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return JToken.FromObject("Not Found");
            }

            return JToken.FromObject(db.Albums.Where(s => s.AlbumId == id));
        }
    }
}
