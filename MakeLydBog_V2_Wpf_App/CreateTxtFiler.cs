using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MakeLydBog_V2_Wpf_App.Models;

namespace MakeLydBog_V2_Wpf_App
{
    class CreateTxtFiler
    {
        public bool CreateFile(Chapter Chapter, string Path, string StoryName)
        {
            bool b = false;

            Directory.CreateDirectory(Path + StoryName);

            string FullTestFilePath = Path + StoryName + "\\" + Chapter.Title + ".txt";


            if (!File.Exists(FullTestFilePath))
            {
                b = true;
                // Create a file to write to.

                using (StreamWriter sw = File.CreateText(FullTestFilePath))
                {
                    sw.WriteLine(Chapter.Content);
                }
            }
            return b;
        }
    }
}
