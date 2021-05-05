using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class InputBoxMessageBoxToDelete : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        string  titleWindow = string.Empty,
                strDefaultResponse = string.Empty;

        // Create an image list that will be used for the combobox icons.
        readonly static ImageList imageListConceptToDelete = new ImageList();

        // Create an List that will be used to concept names from the data table.
        public static readonly List<string> conceptToDeleteList = new List<string>();

        // Mouse index used to reference the position of the cursor when navigating up and down the combobox list.
        private readonly int _MouseIndex = -1;

        public string retornoNombreConcepto = string.Empty;
        
        private void cargarValores()
        {
            this.Text = titleWindow;
            // Used to create the list of User Groups & User Accounts when the form opens.
            PopulateConceptsList();
        }

        private void PopulateConceptsList()
        {
            // Add the image to the image list, image is loaded from the properties resources folder.
            // This image will be used as icon to display in the combobox.
            imageListConceptToDelete.Images.Add((Image)(new Bitmap(Properties.Resources.trash1)));

            // Add a default string to the list for the combobox default value, prompting the user to make their selection
            // before the open up the dropdown combobox.
            conceptToDeleteList.Add(strDefaultResponse);

            using (DataTable dtConceptoDinamicos = cn.CargarDatos(cs.VerificarContenidoDinamico(FormPrincipal.userID)))
            {
                if (!dtConceptoDinamicos.Rows.Count.Equals(0))
                {
                    foreach(DataRow drConcepto in dtConceptoDinamicos.Rows)
                    {
                        conceptToDeleteList.Add(drConcepto["concepto"].ToString());
                    }
                }
            }

            // Run the method below that will populate the combobox with the User Groups & User Accounts from the list.
            AddConceptsComboboxPopulate();
        }

        private void AddConceptsComboboxPopulate()
        {
            // Clear the comobox before re-populating.
            cbConceptos.Items.Clear();

            // Map the combobox's data source to the 'conceptToDeleteList' source.
            cbConceptos.DataSource = conceptToDeleteList;
            // Set the combobox to owner draw mode - otherwise you cannot change the output style to your own format.
            cbConceptos.DrawMode = DrawMode.OwnerDrawFixed;
            // Instruct the combobox to draw itself using the 'comboBox1_DrawItem' method below.
            cbConceptos.DrawItem += cbConceptos_DrawItem;
            // Set the default height for each of the items displayed in the combobox.
            cbConceptos.ItemHeight = 20;
        }

        private void cbConceptos_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Set the textBrush color to Windows Text.
            Brush textBrush = SystemBrushes.WindowText;

            if (e.Index > 1)
            {
                // Highlight the combobox item when the mouse cursor hovers over the item in the dropdown list.
                if (e.Index == _MouseIndex)
                {
                    e.Graphics.FillRectangle(SystemBrushes.HotTrack, e.Bounds);
                    textBrush = SystemBrushes.HighlightText;
                }
                else
                {
                    // Highlight the combobox item when slected in the dropdown list.
                    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                    {
                        e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds);
                        textBrush = SystemBrushes.HighlightText;
                    }
                    else
                    {
                        // Restore background colour to deafult when the mouse leaves the item.
                        e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);
                    }
                }

                // Draw the string i.e populate the combobox with the list of the text names
                // for the Concept
                e.Graphics.DrawString(conceptToDeleteList[e.Index].ToString(), e.Font, textBrush, e.Bounds.Left + 20, e.Bounds.Top);
                e.Graphics.DrawImage(imageListConceptToDelete.Images[0], e.Bounds.Left, e.Bounds.Top);
            }
        }

        public InputBoxMessageBoxToDelete(string _Title, string _DefaultResponse)
        {
            InitializeComponent();
            this.titleWindow = _Title;
            this.strDefaultResponse = _DefaultResponse;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string textoDefaultResponse = string.Empty,
                    auxComparacionAgregar = string.Empty;

            textoDefaultResponse = cbConceptos.Text;
            auxComparacionAgregar = strDefaultResponse;
            if (cbConceptos.Text.Equals(string.Empty))
            {
                retornoNombreConcepto = string.Empty;
            }
            else if (!textoDefaultResponse.Contains(auxComparacionAgregar))
            {
                retornoNombreConcepto = cbConceptos.Text;
                retornoNombreConcepto.Trim();
            }

            this.Close();
        }

        private void InputBoxMessageBoxToDelete_Load(object sender, EventArgs e)
        {
            cargarValores();
        }
    }
}
