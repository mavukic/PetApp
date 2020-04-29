using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetApp.Models
{
    public class UdrugaView
    {
        public IEnumerable<Udruga> Udruge { get; set; }
        public IEnumerable<PostUdruge> PostUdruge { get; set; }
    }
}
