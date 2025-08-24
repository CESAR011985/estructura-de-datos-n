using Ciudadanos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ciudadanos.Services
{
    public class CiudadanoService
    {
        private readonly IEnumerable<Ciudadano> _ciudadanos;

        public CiudadanoService(IEnumerable<Ciudadano> ciudadanos)
        {
            _ciudadanos = ciudadanos;
        }

        // Ciudadanos no vacunados
        public IEnumerable<Ciudadano> ObtenerNoVacunados()
        {
            return _ciudadanos.Where(c =>
                !c.Vacunado &&
                string.IsNullOrEmpty(c.Dosis1) &&
                string.IsNullOrEmpty(c.Dosis2));
        }

        // Ciudadanos con ambas dosis
        public IEnumerable<Ciudadano> ObtenerConAmbasDosis()
        {
            return _ciudadanos.Where(c =>
                !string.IsNullOrEmpty(c.Dosis1) &&
                !string.IsNullOrEmpty(c.Dosis2));
        }

        // Ciudadanos que solo recibieron Pfizer (una o ambas dosis)
        public IEnumerable<Ciudadano> ObtenerSoloPfizer()
        {
            return _ciudadanos.Where(c =>
                (c.Dosis1 == "Pfizer" || c.Dosis2 == "Pfizer") &&
                c.Dosis1 != "AstraZeneca" &&
                c.Dosis2 != "AstraZeneca");
        }

        // Ciudadanos que solo recibieron AstraZeneca (una o ambas dosis)
        public IEnumerable<Ciudadano> ObtenerSoloAstraZeneca()
        {
            return _ciudadanos.Where(c =>
                (c.Dosis1 == "AstraZeneca" || c.Dosis2 == "AstraZeneca") &&
                c.Dosis1 != "Pfizer" &&
                c.Dosis2 != "Pfizer");
        }

        // Ciudadanos que recibieron ambas vacunas (mezcla)
        public IEnumerable<Ciudadano> ObtenerMezclaVacunas()
        {
            return _ciudadanos.Where(c =>
                (c.Dosis1 == "Pfizer" && c.Dosis2 == "AstraZeneca") ||
                (c.Dosis1 == "AstraZeneca" && c.Dosis2 == "Pfizer"));
        }

        public IEnumerable<Ciudadano> ObtenerConSolaUnaDosis()
        {
            return _ciudadanos.Where(c =>
                (!string.IsNullOrEmpty(c.Dosis1) && string.IsNullOrEmpty(c.Dosis2)) ||
                (string.IsNullOrEmpty(c.Dosis1) && !string.IsNullOrEmpty(c.Dosis2)));
        }
    }
}
