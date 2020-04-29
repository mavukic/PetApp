using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PetApp.Models
{
    public class Posvajatelj
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Molimo unesite prezime")]
        [MaxLength(30, ErrorMessage = "Previše zankova")]
        public String Ime { get; set; }

        [Required(ErrorMessage = "Molimo unesite ime")]
        [MaxLength(50, ErrorMessage = "Previše zankova")]
        public String Prezime { get; set; }

        [Required(ErrorMessage = "Molimo unesite mjesto boravišta")]
        [MaxLength(30, ErrorMessage = "Previše zankova")]
        public String Mjesto { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Datum prijave")]
        public DateTime Datum { get; set; }

        [DisplayName("Broj mobitela")]
        [Required(ErrorMessage = "Molimo unesite broj mobitela (ili email) kako bi vas mogli kontaktirati")]
        [System.ComponentModel.DataAnnotations.RegularExpression("^[0-9]+$", ErrorMessage = "Broj mobitela/telefona mora sadržavati samo brojeve")]
        public String BrMob { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Upišite email")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Molimo odaberite životinju koju želite posvojiti")]
        public int LjubimacId { get; set; }
        public Ljubimac Ljubimac { get; set; }
    }
}
