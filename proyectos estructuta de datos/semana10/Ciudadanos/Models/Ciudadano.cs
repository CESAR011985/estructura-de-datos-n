using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ciudadanos.Models
{
    public class Ciudadano
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Ci { get; set; }
        public bool Vacunado { get; set; }
        public string? Dosis1 { get; set; }
        public string? Dosis2 { get; set; }
    }
}
