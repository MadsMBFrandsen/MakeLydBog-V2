using MakeLydBog_V2_Wpf_App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VersOne.Epub;
using MakeLydBog_V2_Wpf_App.Models;
using System.IO;

namespace MakeLydBog_V2_Wpf_App
{




    class FungtionsV2
    {
        public int StartNumber;
        public int RealNumber;
        public string ChapterName;
        public string TimeLeft;

        public void GetTimeLeft(int totalSeconds)
        {

            //totalSeconds = totalSeconds;
            int hours = totalSeconds / 3600;
            int minutes = (totalSeconds % 3600) / 60;
            int seconds = (totalSeconds % 3600) % 60;

            Console.WriteLine($"         Hours: {hours}, Minutes: {minutes}, Seconds: {seconds}");
            string tempstring = $"         Hours: {hours}, Minutes: {minutes}, Seconds: {seconds}";
            TimeLeft = tempstring;
        }

        public void Start(string StoryName)
        {
            Console.WriteLine("         ***");
            Console.WriteLine("         StoryName = " + StoryName);
            Console.WriteLine("         ***");
        }
        public int ListOfChaptersCountMetode(List<Chapter> ListOfChapters)
        {
            int ListOfChaptersCount = 0;
            foreach (Chapter chapter in ListOfChapters)
            {
                ListOfChaptersCount = ListOfChaptersCount + chapter.Content.Length;
            }
            return ListOfChaptersCount;
        }

        public async Task MakeTxtFileAsync(List<Chapter> ListOfChapters, string TextFilePath, string StoryName)
        {
            CreateTxtFilerV2 createTxtFilesV2 = new CreateTxtFilerV2();
            foreach (Chapter item in ListOfChapters)
            {
                await createTxtFilesV2.CreateFileAsync(item, TextFilePath, StoryName);
            }
        }

        public int GetStartNumberAsync()
        {
            return StartNumber;
        }

        public int GetRealNumberAsync()
        {
            return RealNumber;
        }
        public string GetChapterNameAsync()
        {
            return ChapterName;
        }
        public string GetTimeLeftAsync()
        {
            return TimeLeft;
        }
        public async Task<bool> MakeMp3FileAsync(List<Chapter> ListOfChapters, int ListOfChaptersCount, string LydBogPath, string StoryName)
        {
            CreateLydFilerV2 createLydFileV2 = new CreateLydFilerV2();

            int number = ListOfChapters.Count;
            int number1 = ListOfChapters.Count;
            StartNumber = number;
            RealNumber = number1;

            DateTime dateTime = DateTime.Now;
            Console.WriteLine("Start Tid    =" + dateTime);
            Console.WriteLine("---------------------------");

            foreach (Chapter item in ListOfChapters)
            {
                Console.WriteLine("Title    " + item.Title);
                ChapterName = item.Title;
                Console.WriteLine("Start Number      " + number);
                number1--;
                RealNumber = number1;
                Console.WriteLine("Er nu             " + number1 + " Tilbage");
                ListOfChaptersCount = ListOfChaptersCount - item.Content.Length;
                Console.WriteLine("Time Left");
                GetTimeLeft(ListOfChaptersCount / 1600); //1500

                string filePath = Path.Combine(LydBogPath, StoryName, item.Title + ".mp3");
                if (!File.Exists(LydBogPath + StoryName + @"\" + item.Title + ".mp3"))
                {
                    await createLydFileV2.CreateSoundFileAsync(item, StoryName, LydBogPath);
                }

                Console.WriteLine("---------------------------");
            }

            DateTime dateTime1 = DateTime.Now;
            TimeSpan time = dateTime1 - dateTime;
            int TimeInSeconds = time.Seconds;
            GetTimeLeft(TimeInSeconds);

            Console.WriteLine("Gæt Tid      =" + dateTime); //To Do Fix
            Console.WriteLine("Slut Tid     =" + dateTime1);
            Console.WriteLine("Tid taget    =" + (dateTime1 - dateTime));
            return true;
        }

        public async Task<bool> ListOfEpubsAsync(List<Epub> epubs, string EpubFilePath, string TextFilePath, string LydBogPath)
        {
            GetContentFromEpub_V2 getContentFromEpub2 = new GetContentFromEpub_V2();

            foreach (Epub item in epubs)
            {
                Start(item.StoryName);
                List<Chapter> ListOfChapters = getContentFromEpub2.GetContentFromEpub_V2Metode(/*EpubFilePath,*/ item.EpubToExtratFileName);
                int ListOfChaptersCount = ListOfChaptersCountMetode(ListOfChapters);
                await MakeTxtFileAsync(ListOfChapters, TextFilePath, item.StoryName);
                await MakeMp3FileAsync(ListOfChapters, ListOfChaptersCount, LydBogPath, item.StoryName);
                End();
            }
            return true;
        }

        public async Task<bool> OneChapterAsync(List<Chapter> ListOfChapters, string TextFilePath, string LydBogPath, string StoryName)
        {
            int ListOfChaptersCount = ListOfChaptersCountMetode(ListOfChapters);
            await MakeTxtFileAsync(ListOfChapters, TextFilePath, StoryName);
            await MakeMp3FileAsync(ListOfChapters, ListOfChaptersCount, LydBogPath, StoryName);
            End();
            return true;
        }

        public void End()
        {

            Console.WriteLine("                           *          ");
            Console.WriteLine("                          ***         ");
            Console.WriteLine("                         *****        ");
            Console.WriteLine("                        *******       ");
            Console.WriteLine("                       *********      ");
            Console.WriteLine("                      ***********     ");
            Console.WriteLine("                     *************    ");
            Console.WriteLine("                    ***************   ");
            Console.WriteLine("                   *****************  ");
            Console.WriteLine("                  ******************* ");
            Console.WriteLine("                         *****        ");
            Console.WriteLine("                         *****        ");
            Console.WriteLine("                         *****        ");
            Console.WriteLine("                         *****        ");
            Console.WriteLine("                         *****        ");
            Console.WriteLine("                         *****        ");
            Console.WriteLine("                         *****        ");
        }
    }
}