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

        static int errors = 0;

        static void Main(string[] args) 
        {

            Console.Title = "StellarCheck by MikeWe";
            Console.WriteLine("Press any key to start...");
            Console.ReadKey();
            Console.Clear();    
            List<String> mods = GetDirectoryNames(@"C:\Program Files (x86)\Steam\steamapps\workshop\content\281990");

            if (mods == null && Properties.Settings.Default.customPath == string.Empty)
            {            
                Console.WriteLine("Stellaris Mod Directory Not Found...");
                Console.Write("Please enter the path were all your mods are stored:");
                string path = Console.ReadLine();
                Properties.Settings.Default.customPath = path;
                Properties.Settings.Default.Save();
                mods = GetDirectoryNames(path);
            }
            else if (mods == null)
            {
                mods = GetDirectoryNames(Properties.Settings.Default.customPath);
            }
            
            Console.WriteLine("Searching for valid files...");
            List<String> files = new List<String>();
           
            foreach (var mod in mods)
            {            
                files.Add(GetFileName(mod));    
            }

            Console.WriteLine("Searching for incompatibilities...");
            List<String> zippedData = new List<String>();
            int inCompatibilities = 0;
            int duplicates = 0;

            Console.ForegroundColor = ConsoleColor.Red;

            foreach (var zipPath in files)
            {
                try
                {
                    var zip = ZipFile.Read(zipPath);
                    
                    foreach (ZipEntry e in zip.Entries)
                    {
                        if (zippedData.Contains(e.FileName) && e.FileName.Contains(".txt") && e.FileName.Contains("00_"))
                        {                        
                            Console.WriteLine(zipPath);
                            Console.WriteLine("({0})", e.FileName);
                            inCompatibilities += 1;
                        }
                        else if (zippedData.Contains(e.FileName)) duplicates += 1;
                        
                        zippedData.Add(e.FileName);
                    }
                }
                catch
                {
                    errors += 1;
                }            
            }

            Console.ResetColor();
            Console.WriteLine("{0} Incompatibilities found", inCompatibilities);
            Console.WriteLine("{0} Duplicate files found", duplicates + inCompatibilities);
            Console.WriteLine("{0} Errors (For Debug)", errors);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

        }

        static List<String> GetDirectoryNames(string path)
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

        static String GetFileName(string path)
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
