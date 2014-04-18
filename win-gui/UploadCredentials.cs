using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Synthesia
{
    public partial class UploadCredentials : Form
    {
        public string SiteKey { get { return SiteKeyBox.Text; } }

        public UploadCredentials()
        {
            InitializeComponent();
        }

        private void SiteKeyBox_TextChanged(object sender, EventArgs e)
        {
            _okButton.Enabled = !string.IsNullOrWhiteSpace(SiteKeyBox.Text);
        }
    }
}
