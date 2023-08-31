using System;
using System.IO;
using System.Threading.Tasks;
using MakeLydBog_V2_Wpf_App.Models;

namespace MakeLydBog_V2_Wpf_App
{
    class CreateTxtFilerV2
    {
        public async Task<bool> CreateFileAsync(Chapter Chapter, string Path, string StoryName)
        {
            bool b = false;

            Directory.CreateDirectory(Path + StoryName);

            string FullTestFilePath = Path + StoryName + "\\" + Chapter.Title + ".txt";

            if (!File.Exists(FullTestFilePath))
            {
                b = true;

                using (StreamWriter sw = File.CreateText(FullTestFilePath))
                {
                    await sw.WriteAsync(Chapter.Content);
                }
            }

            return b;
        }
    }
}
