using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    class MetodosGenerales
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

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

        public string quitarTildesYÑ(string cadena)
        {
            var result = cadena;

            if (cadena.Contains("Á"))
            {
                result = cadena.Replace("Á","A");
            }
            else if (cadena.Contains("É"))
            {
                result = cadena.Replace("É", "E");
            }
            else if (cadena.Contains("Í"))
            {
                result = cadena.Replace("Í", "I");
            }
            else if (cadena.Contains("Ó"))
            {
                result = cadena.Replace("Ó", "O");
            }
            else if (cadena.Contains("Ú"))
            {
                result = cadena.Replace("Ú", "U");
            }
            else if (cadena.Contains("Ñ")) 
            {
                result = cadena.Replace("Ñ", "N");
            }

            return result;
        }

        public bool EliminarFiltros()
        {
            bool respuesta = true;

            cn.EjecutarConsulta(cs.EliminarFiltrosProductos(FormPrincipal.userID, FormPrincipal.userNickName, 1));
            cn.EjecutarConsulta(cs.EliminarFiltrosProductos(FormPrincipal.userID, FormPrincipal.userNickName, 2));

            return respuesta;
        }
    }
}
