using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PetApp.Models
{
    public class Udruga
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Unesite naziv udruge")]
        [MaxLength(50, ErrorMessage = "Previše zankova")]
        public String Naziv { get; set; }

        [Required(ErrorMessage = "Unesite adresu udruge")]
        [MaxLength(70, ErrorMessage = "Previše zankova")]
        public String Adresa { get; set; }

        [Required(ErrorMessage = "Unesite grad u kojem se nalazi udruga")]
        [MaxLength(30, ErrorMessage = "Previše zankova")]
        public String Grad { get; set; }

        [DisplayName("Broj mobitela ili telefona")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Broj mobitela/telefona mora sadržavati samo brojeve")]
        public String Tel { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.EmailAddress, ErrorMessage = "Upišite email")]
        public String Email { get; set; }

        [Url(ErrorMessage = "Unesite valjanu web stranicu, web stranica mora poceti sa http:// ili https:// ili ftp://")]
        public String Web { get; set; }
        public ICollection<PostUdruge> PostsUdruge { get; set; }
    }
}
