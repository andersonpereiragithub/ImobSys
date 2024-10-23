using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImobSys.Domain.Entities
{
    namespace ImobSys.Domain
    {
        public class CoordenadasGeograficas
        {
            public double? Latitude { get; set; }  // Latitude (ex: -23.5505)
            public double? Longitude { get; set; } // Longitude (ex: -46.6333)

            // Construtor padrão
            public CoordenadasGeograficas()
            {
                Latitude = null;   // Valor nulo inicialmente, pode ser preenchido depois
                Longitude = null;  // Valor nulo inicialmente
            }

        }
    }
}
