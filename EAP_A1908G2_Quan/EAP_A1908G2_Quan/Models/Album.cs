using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EAP_A1908G2_Quan.Models
{
    public class Album
    {
        public int AlbumId { get; set; }
        [Required]
        [MaxLength(32),MinLength(3)]
        public string Title { get; set; }
        [DataType("date")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "DateTime2")]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public string Artist { get; set; }
        [Required]
        [Range(1,15)]
        public double Price { get; set; }
        public int GenreId { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual Genre Genre { get; set; }
    }
}