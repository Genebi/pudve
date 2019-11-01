using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVentaV2
{
    class MetodosGenerales
    {
        public string RemoverCaracteres(string nombre)
        {
            nombre = nombre.Replace('Á', 'A')
                           .Replace('É', 'E')
                           .Replace('Í', 'I')
                           .Replace('Ó', 'O')
                           .Replace('Ú', 'U')
                           .Replace('Ñ', 'N');

            return nombre;
        }
    }
}
