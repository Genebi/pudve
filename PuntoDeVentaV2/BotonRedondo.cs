using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace PuntoDeVentaV2
{
    public class BotonRedondo : Button
    {
        /// <summary>
        /// Variables privada para personalizar el boton
        /// </summary>
        #region Variables privadas
        private int borderSize = 0;
        private int borderRadius = 40;
        private Color borderColor = Color.PaleVioletRed;
        #endregion

        /// <summary>
        /// Get y Set para las propiedades de presonalizar el Boton
        /// </summary>
        #region Propiedades
        [Category("SIFO Controls")]
        public int BroderSize
        {
            get
            {
                return borderSize;
            }
            set
            {
                borderSize = value;
                this.Invalidate();
            }
        }

        [Category("SIFO Controls")]
        public int BorderRadius
        {
            get
            {
                return borderRadius;
            }
            set
            {
                if (value <= this.Height)
                {
                    borderRadius = value;
                }
                else
                {
                    borderRadius = this.Height;
                }
                this.Invalidate();
            }
        }

        [Category("SIFO Controls")]
        public Color BorderColor
        {
            get
            {
                return borderColor;
            }
            set
            {
                borderColor = value;
                this.Invalidate();
            }
        }

        [Category("SIFO Controls")]
        public Color BackGroundColor
        {
            get
            {
                return this.BackColor;
            }
            set
            {
                this.BackColor = value;
            }
        }

        [Category("SIFO Controls")]
        public Color TextColor
        {
            get
            {
                return this.ForeColor;
            }
            set
            {
                this.ForeColor = value;
            }
        }
        #endregion

        /// <summary>
        /// Constructor de botón redondo
        /// </summary>
        #region Constructor 
        public BotonRedondo()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Size = new Size(150, 40);
            this.BackColor = Color.MediumSlateBlue;
            this.ForeColor = Color.White;
            this.Resize += new EventHandler(Button_Resize);
        }
        #endregion
    }
}
