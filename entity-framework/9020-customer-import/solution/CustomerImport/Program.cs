using System;
using System.IO;

namespace CustomerImport
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Check command-line arguments
            if (args.Length != 1)
            {
                Console.WriteLine("Missing argument (file to imort)");
                return;
            }

            if (!File.Exists(args[0]))
            {
                Console.WriteLine("File cannot be found");
                return;
            }
            #endregion

            #region Import data
            using (var db = new CustomerContext())
            {
                // Clean database before importing data
                db.CleanupTargetDatbaseAsync().Wait();

                // Read import file into memory
                var importText = File.ReadAllText(args[0]);

                // Import Customers
                db.ImportAsync(importText).Wait();
            }
            #endregion
        }
    }
}
