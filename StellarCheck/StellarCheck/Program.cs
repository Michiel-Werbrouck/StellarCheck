using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Ionic.Zip;

namespace StellarCheck
{

    /*
     * Made by MikeWe
    */

    class Program
    {

        int errors = 0;

        static void Main(string[] args) 
        {
            Console.Title = "StellarCheck by MikeWe";
            Console.WriteLine("Press any key to start...");
            Console.ReadKey();
            int inCompatibilities = 0;
            int possibleInCompatibilities = 0;
            Program prgm = new Program();
            List<String> mods = prgm.GetDirectoryNames(@"C:\Program Files (x86)\Steam\steamapps\workshop\content\281990");     
            Console.WriteLine("Searching for valid files...");
            List<String> files = new List<String>();
           
            foreach (var mod in mods)
            {
              
                files.Add(prgm.GetFileName(mod));
        
            }

            Console.WriteLine("Searching for incompatibilities...");
            List<String> zippedData = new List<String>();
            

            Console.ForegroundColor = ConsoleColor.Red;



            foreach (var zipPath in files)
            {
                try
                {
                    var zip = ZipFile.Read(zipPath);
                   

                    foreach (ZipEntry e in zip.Entries)
                    {
                        //string eed = e.FileName.Substring(e.FileName.LastIndexOf("/"));

                        if (zippedData.Contains(e.FileName) && e.FileName.Contains("00_"))
                        {
                            Console.WriteLine(zipPath);
                            Console.WriteLine("({0})", e.FileName);
                            inCompatibilities += 1;
                        }
                        else if (zippedData.Contains(e.FileName)) possibleInCompatibilities += 1;

                        zippedData.Add(e.FileName);


                    }
                }
                catch
                {
                    prgm.errors += 1;
                }
               
            }

            Console.ResetColor();

            Console.WriteLine("{0} Incompatibilities found", inCompatibilities);
            Console.WriteLine("{0} Duplicate files found", possibleInCompatibilities);
            Console.WriteLine("{0} Errors (For Debug)", prgm.errors);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

        }

        List<String> GetDirectoryNames(string path)
        {

            try
            {
                string dirPath = path;

                var dirs = from dir in
                         Directory.EnumerateDirectories(dirPath)
                           select dir;
             
                Console.WriteLine("{0} mod(s) found.",
                    dirs.Count().ToString());

                List<string> workDirs = new List<string>(dirs);
                return workDirs;
            }
            catch 
            {
                errors += 1;
                return null;
            }
        }

        String GetFileName(string path)
        {
            try
            {
                var files = from file in
                    Directory.EnumerateFiles(path)
                            where file.ToLower().Contains("zip")
                            select file;

                Console.WriteLine(@"(\{0})>{1} valid file(s) found.", path.Substring(path.LastIndexOf("\\") + 1)
                    , files.Count().ToString());
              
                    return files.ElementAt(0);                                               
            }
            catch 
            {
                errors += 1;
                return null;
            }
          
        }

    }
}
