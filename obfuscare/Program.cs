﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace obfuscare
{
    class Program
    {
        static void Main(string[] args)
        {
            var solutionPath = GetSolutionPath(args);

            Console.WriteLine(solutionPath);
            var csFilePathes = GetCsFilePathes(solutionPath);

            foreach (var item in csFilePathes)
            {
                Console.WriteLine(item);
            }

            ObfuscareService.Obfuscare(csFilePathes);

            Console.ReadLine();            
        }

        static string GetSolutionPath(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string solutionPath = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

            if (args.Length > 0 && Directory.Exists(args[0]))
            {
                solutionPath = args[0];
            }

            return solutionPath;
        }

        static IEnumerable<string> GetCsFilePathes(string solutionPath)
        {
            var csFiles = new List<string>();
            csFiles.AddRange(Directory.GetFiles(solutionPath, "*.cs"));
            var subfolders = Directory.GetDirectories(solutionPath);

            if (subfolders.Any())
            {
                foreach (var subfolder in subfolders)
                {
                    if(subfolder.Contains("bin\\") || subfolder.Contains("obj\\"))
                    {
                        continue;
                    }

                    csFiles.AddRange(GetCsFilePathes(subfolder));
                }
            }

            return csFiles;
        }
    }
}
