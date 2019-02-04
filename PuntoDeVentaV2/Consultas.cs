using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;

namespace PuntoDeVentaV2
{
    class Consultas
    {
        Conexion cn = new Conexion();

        public Consultas()
        {

        }
        public string Productos(int id)
        {
            return "SELECT P.Nombre, P.Stock, P.Precio, P.Categoria, P.ClaveInterna AS 'Clave Interna', P.CodigoBarras AS 'Código de Barras' FROM Productos P INNER JOIN Usuarios U ON P.IDUsuario = U.ID WHERE U.ID = '" + id + "'";
        }

        public string GuardarProducto(string[] datos, int id)
        {
            string consulta = "INSERT INTO Productos(Nombre, Stock, Precio, Categoria, ClaveInterna, CodigoBarras, ClaveProducto, UnidadMedida, TipoDescuento, IDUsuario)";
                   consulta += "VALUES('" + datos[0] + "', '" + datos[1] + "', '" + datos[2] + "', '" + datos[3] + "', '" + datos[4] + "', '" + datos[5] + "', '" + datos[6] + "', '" + datos[7] + "', '"+ datos[8] +"', '" + id + "')";

            return consulta;
        }

        public string GuardarDetallesProducto(string[] datos, int idProducto)
        {
            string consulta = "INSERT INTO DetallesFacturacionProductos (Tipo, Impuesto, TipoFactor, TasaCuota, Definir, Importe, IDProducto)";
                   consulta += "VALUES ('" + datos[0] + "', '" + datos[1] + "', '" + datos[2] + "', '" + datos[3] + "', '" + datos[4] + "', '" + datos[5] + "', '" + idProducto + "')";

            return consulta;
        }

        public string GuardarDescuentoCliente(string[] datos, int idProducto)
        {
            string consulta = "INSERT INTO DescuentoCliente (PrecioProducto, PorcentajeDescuento, PrecioDescuento, Descuento, IDProducto)";
                   consulta += "VALUES ('"+ datos[0] +"', '"+ datos[1] +"', '"+ datos[2] +"', '"+ datos[3] +"', '"+ idProducto +"')";

            return consulta;
        }

        public string GuardarDescuentoMayoreo(string[] datos, int idProducto)
        {
            string consulta = "INSERT INTO DescuentoMayoreo (RangoInicial, RangoFinal, Precio, Checkbox, IDProducto)";
                   consulta += "VALUES ('"+ datos[0] +"', '"+ datos[1] +"', '"+ datos[2] +"', '"+ datos[3] +"', '"+ idProducto +"')";

            return consulta;
        }
    }
}
