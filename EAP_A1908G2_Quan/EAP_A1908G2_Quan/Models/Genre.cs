using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EAP_A1908G2_Quan.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string GenreName { get; set; }
        public virtual ICollection<Album> Albums { get; set; }

        public Genre()
        {
            this.Albums = new HashSet<Album>();
        }
    }
}