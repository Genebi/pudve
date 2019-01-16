﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVentaV2
{
    class Consultas
    {
        public Consultas()
        {

        }

        public string Productos(int id)
        {
            return "SELECT P.Nombre, P.Stock, P.Precio, P.Categoria, P.ClaveInterna AS 'Clave Interna', P.CodigoBarras AS 'Código de Barras' FROM Productos P INNER JOIN Usuarios U ON P.IDUsuario = U.ID WHERE U.ID = '" + id + "'";
        }
    }
}
