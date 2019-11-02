using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVentaV2
{
    class MetodosGenerales
    {
        string[] preposiciones = new string[] {
            "A", "ANTE", "BAJO", "CABE", "CON", "CONTRA", "DE", "DESDE", "DURANTE", "EN",
            "ENTRE", "HACIA", "HASTA", "PARA", "POR", "SEGÚN", "SIN", "SO", "SOBRE", "TRAS",
            "VERSUS", "VÍA", "MEDIANTE"
        };

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

        public string RemoverPreposiciones(string nombre)
        {
            nombre = RemoverCaracteres(nombre);

            List<string> lista = new List<string>();

            var palabras = nombre.Split(' ');

            foreach (string palabra in palabras)
            {
                if (!preposiciones.Contains(palabra))
                {
                    lista.Add(palabra);
                }
            }

            palabras = lista.ToArray();

            return string.Join(" ", palabras);
        }
    }
}
