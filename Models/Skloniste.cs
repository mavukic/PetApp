using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PetApp.Models
{
    public class Skloniste
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "Previše zankova")]
        [Required(ErrorMessage = "Unesite naziv skloništa")]
        public String Naziv { get; set; }

        [Required(ErrorMessage = "Unesite adresu skloništa")]
        [MaxLength(70, ErrorMessage = "Previše zankova")]
        public String Adresa { get; set; }

        [MaxLength(30, ErrorMessage = "Previše zankova")]
        [Required(ErrorMessage = "Unesite grad u kojem se nalazi sklonište")]
        public String Grad { get; set; }

        [DisplayName("Broj mobitela ili telefona")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Broj mobitela/telefona mora sadržavati samo brojeve")]
        public String Tel { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Upišite email")]
        public String Email { get; set; }
 [RegularExpression("@(http://)?(www\\.)?\\w+\\.(com|net|edu|org|hr)", ErrorMessage = "Upisite web stranicu, mora počinjati sa http:// ili slično")]
 public String Web { get; set; }
        public ICollection<PostSklonista> PostsSklonista { get; set; }
        public ICollection<Ljubimac> Ljubimac { get; set; }
    }
}
