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
    public partial class mensajeCopiar : Form
    {
        public mensajeCopiar(string message, string caption, string buttonText, string textToCopy)
        {
            InitializeComponent();

            // Set the message and caption of the message box
            messageLabel.Text = message;
            Text = caption;

            // Set the text to copy to the clipboard
            textToCopyLabel.Text = textToCopy;

            // Set the text of the copy button
            copyButton.Text = buttonText;

            // Add an event handler to the copy button
            copyButton.Click += CopyButton_Click;
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            // Copy the text to the clipboard
            Clipboard.SetText(textToCopyLabel.Text);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
