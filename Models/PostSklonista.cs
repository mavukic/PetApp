using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PetApp.Models
{
    public class PostSklonista
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public String Naziv { get; set; }

        [AllowHtml]
        public string Content { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Datum { get; set; }

        [DisplayName("Sklonište Id")]
        public int SklonisteId { get; set; }

        [DisplayName("Sklonište")]
        public Skloniste Skloniste { get; set; }
    }
}
