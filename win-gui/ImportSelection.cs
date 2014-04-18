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
    public partial class ImportSelection : Form
    {
        public bool ImportFingerHints { get { return CheckFingerHints.Checked; } }
        public bool ImportHandParts { get { return CheckHandParts.Checked; } }

        public ImportSelection()
        {
            InitializeComponent();
        }

        private void CheckChanged(object sender, EventArgs e)
        {
            _okButton.Enabled = CheckFingerHints.Checked || CheckHandParts.Checked;
        }

        private void ImportSelection_Load(object sender, EventArgs e)
        {
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }
    }
}
