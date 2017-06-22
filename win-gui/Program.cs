using System;
using System.Windows.Forms;

namespace Synthesia
{
   static class Program
   {
      [STAThread]
      static void Main(string[] args)
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new MetadataEditor(args.Length == 1 ? args[0] : null));
      }
   }
}
