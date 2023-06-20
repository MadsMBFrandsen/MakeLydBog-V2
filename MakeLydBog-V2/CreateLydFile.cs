using NAudio.Lame;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Speech.Synthesis;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MakeLydBog_V2
{
    public class CreateLydFile
    {

        public bool CreateSoundFile(string LydBogPath, Chapter chapter, string Storyname, string path)
        {
            Directory.CreateDirectory(path + Storyname);
            bool b = false;
            using (SpeechSynthesizer reader = new SpeechSynthesizer())
            {
                //set some settings
                reader.Volume = 100;
                reader.Rate = 0; //medium


                MemoryStream ms = new MemoryStream();
                reader.SetOutputToWaveStream(ms);


                reader.Speak(chapter.Content);
                if (!File.Exists(LydBogPath+Storyname + @"\" + chapter.Title + ".mp3"))
                {
                    ConvertWavStreamToMp3File(ref ms, path + Storyname + @"\" + chapter.Title + ".mp3");
                    b = true;
                }




            }

            return b;
        }
        public static void ConvertWavStreamToMp3File(ref MemoryStream ms, string savetofilename)
        {
            //rewind to beginning of stream
            ms.Seek(0, SeekOrigin.Begin);

            using (var retMs = new MemoryStream())
            using (var rdr = new WaveFileReader(ms))
            using (var wtr = new LameMP3FileWriter(savetofilename, rdr.WaveFormat, LAMEPreset.VBR_90))
            {
                rdr.CopyTo(wtr);
            }
        }
    }
}
