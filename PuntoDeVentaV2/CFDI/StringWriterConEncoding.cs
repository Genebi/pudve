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
        
        public StringWriterConEncoding(Encoding encoding): base()
        {
            this.m_encoding = encoding;
        }
        private readonly Encoding m_encoding;
        public override Encoding Encoding
        {
            get
            {
                return this.m_encoding;
            }
        }
    }
}
