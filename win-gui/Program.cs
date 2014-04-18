using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace Synthesia
{
   static class Program
   {
      [STAThread]
      static void Main(string[] args)
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);

         MetadataEditor editor = new MetadataEditor();

         if (args.Length == 1 && File.Exists(args[0]))
            editor.ForceOpenFile(args[0]);

         Application.Run(editor);
      }
   }
}
