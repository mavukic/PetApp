using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PetApp.Models
{
    public class Ljubimac
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Molimo unesite ime")]
        [MaxLength(30, ErrorMessage = "Previše zankova")]
        public String Ime { get; set; }

        [MaxLength(150, ErrorMessage = "Previše zankova")]
        public String Opis { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Datum { get; set; }

        [Required(ErrorMessage = "Molimo unesite grad u kojem se nalazi životinja")]
        public String Grad { get; set; }
        public int? SklonisteId { get; set; }
        [DisplayName("Sklonište")]
        public Skloniste Skloniste { get; set; }

        [DisplayName("Lokacija u gradu")]
        [Required(ErrorMessage = "Molimo unesite lokaciju životinje")]
        [MaxLength(30, ErrorMessage = "Previše zankova")]
        public String Mjesto { get; set; }

        public String Vrsta { get; set; }

        [Range(0, 25, ErrorMessage = "Molimo unesite valjane godine")]
        public int Godine { get; set; }
        public string Slika { get; set; }
    }
}
