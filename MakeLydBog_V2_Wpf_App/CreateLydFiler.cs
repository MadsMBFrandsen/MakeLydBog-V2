﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MakeLydBog_V2_Wpf_App.Models;
using System.Speech.Synthesis;
using NAudio.Lame;
using NAudio.Wave;

namespace MakeLydBog_V2_Wpf_App
{
    class CreateLydFiler
    {
        public bool CreateSoundFile(Chapter chapter, string Storyname, string path)
        {
            Directory.CreateDirectory(path + Storyname);
            bool b = false;
            using (SpeechSynthesizer reader = new SpeechSynthesizer())
            {
                //set some settings
                reader.Volume = 100;
                reader.Rate = 0; //medium

                /*reader.SelectVoice("");*/

                //foreach (var v in reader.GetInstalledVoices().Select(v => v.VoiceInfo))
                //{
                //    Console.WriteLine("Name:{0}, Gender:{1}, Age:{2}",
                //      v.Description, v.Gender, v.Age);
                //}



                MemoryStream ms = new MemoryStream();
                reader.SetOutputToWaveStream(ms);


                reader.Speak(chapter.Content);
                //if (!File.Exists(LydBogPath+Storyname + @"\" + chapter.Title + ".mp3"))         
                if (!File.Exists(path + Storyname + @"\" + chapter.Title + ".mp3"))
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
