using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVentaV2
{
    public class DatosTotalesReporte
    {
        public decimal TotalCantidad { get; set; }
        public decimal TotalEfectivo { get; set; }
        public decimal TotalTarjeta { get; set; }
        public decimal TotalVales { get; set; }
        public decimal TotalCheque { get; set; }
        public decimal TotalTransferencia { get; set; }
        public decimal TotalCredito { get; set; }
        public decimal TotalAnticipo { get; set; }
    }
}
