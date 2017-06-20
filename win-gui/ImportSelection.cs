using System;
using System.Drawing;
using System.Windows.Forms;

namespace Synthesia
{
   public partial class ImportSelection : Form
   {
      public bool ImportFingerHints { get { return CheckFingerHints.Checked; } }
      public bool ImportHandParts { get { return CheckHandParts.Checked; } }
      public bool ImportParts { get { return CheckParts.Checked; } }

      public bool ImportFromStandard { get { return ComboImportFrom.SelectedIndex == 0; } }

      public ImportSelection()
      {
         InitializeComponent();
      }

      private void CheckChanged(object sender, EventArgs e)
      {
         ButtonOK.Enabled = CheckFingerHints.Checked || CheckHandParts.Checked || CheckParts.Checked;
      }

      private void ImportSelection_Load(object sender, EventArgs e)
      {
         Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

         ComboImportFrom.SelectedIndex = 0;
         ButtonOK.Select();
      }
   }
}
