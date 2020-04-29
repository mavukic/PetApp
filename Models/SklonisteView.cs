using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetApp.Models
{
    public class SklonisteView
    {
        public IEnumerable<Skloniste> Sklonista { get; set; }
        public IEnumerable<PostSklonista> PostSklonista { get; set; }
    }
}
