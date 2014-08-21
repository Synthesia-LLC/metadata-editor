using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Synthesia
{
   public partial class MetadataMerger : Form
   {
      RawMetadataFile Master { get; set; }

      private bool m_dirty = false;
      private bool Dirty
      {
         get { return m_dirty; }

         set
         {
            m_dirty = value;
            SaveButton.Enabled = Dirty;
         }
      }

      public MetadataMerger()
      {
         InitializeComponent();
         LogText.Select(LogText.TextLength, 0);
      }

      public bool SaveChanges()
      {
         if (Master == null) return false;
         Master.Save();

         Dirty = false;
         return true;
      }

      public bool OkayToProceed()
      {
         if (!Dirty) return true;

         DialogResult r = MessageBox.Show("Would you like to save your changes first?", "Save Changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
         if (r == DialogResult.Cancel) return false;
         if (r == DialogResult.Yes) if (!SaveChanges()) return false;

         return true;
      }

      private void MetadataMerger_DragDrop(object sender, DragEventArgs e)
      {
         if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;

         string[] filenames = e.Data.GetData(DataFormats.FileDrop) as string[];
         if (filenames.Length > 1 && Master == null)
         {
            MessageBox.Show("The first file must be dragged in by itself.");
            return;
         }

         foreach (string f in filenames)
         {
            FileInfo file = new FileInfo(f);
            if (file.Extension.ToLower() == ".lnk") file = new FileInfo(WindowsShell.Shortcut.Resolve(f));

            if (!new List<string> { ".xml", ".synthesia" }.Contains(file.Extension.ToLower()))
            {
               Log(string.Format("SKIPPING file with unknown extension: {0}", file.Name));
               continue;
            }

            RawMetadataFile metadata = new RawMetadataFile(file);
            if (Master == null)
            {
               Master = metadata;
               Log(string.Format("Setting \"{0}\" as master ({1})", file.Name, metadata.Statistics));
            }
            else
            {
               Log(string.Format("Adding contents of \"{0}\" to master ({1})", file.Name, metadata.Statistics));
               if (Master.AddRange(metadata, Log)) Dirty = true;
            }
         }
      }

      private void MetadataMerger_DragEnter(object sender, DragEventArgs e)
      {
         if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;
         e.Effect = DragDropEffects.All;
      }

      private void MetadataMerger_FormClosing(object sender, FormClosingEventArgs e)
      {
         if (!OkayToProceed()) e.Cancel = true;
      }

      private void Log(string text)
      {
         int oldStart = LogText.SelectionStart;
         int oldLength = LogText.SelectionLength;

         if (LogText.TextLength == 0) LogText.Text = text;
         else LogText.AppendText(Environment.NewLine + text);

         LogText.Select(LogText.TextLength, 0);
         LogText.ScrollToCaret();

         LogText.Select(oldStart, oldLength);
      }

      private void SaveButton_Click(object sender, EventArgs e)
      {
         SaveChanges();
      }

   }
}
