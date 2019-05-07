using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVentaV2
{
    public class ItemsProductoComboBox
    {
        string _text;
        string _value;

        public ItemsProductoComboBox()
        {
            _text = string.Empty;
            _value = string.Empty;
        }

        public ItemsProductoComboBox(string text, string value)
        {
            _text = text;
            _value = value;
        }

        public string text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public override string ToString()
        {
            return _value;
        }
    }
}
