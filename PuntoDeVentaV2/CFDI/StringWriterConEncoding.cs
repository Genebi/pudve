using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVentaV2.CFDI
{
    public class StringWriterConEncoding: StringWriter
    {
        private readonly Encoding encoding;
        public StringWriterConEncoding(Encoding encoding): base()
        {
            this.encoding = encoding;
        }

        public override Encoding Encoding
        {
            get
            {
                return this.encoding;
            }
        }
    }
}
