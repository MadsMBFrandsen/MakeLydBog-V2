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
    class Fungtions
    {
        public void GetTimeLeft(int totalSeconds)
        {

            //totalSeconds = totalSeconds;
            int hours = totalSeconds / 3600;
            int minutes = (totalSeconds % 3600) / 60;
            int seconds = (totalSeconds % 3600) % 60;

            Console.WriteLine($"         Hours: {hours}, Minutes: {minutes}, Seconds: {seconds}");
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

        public void MakeTxtFile(List<Chapter> ListOfChapters, string TextFilePath, string StoryName)
        {
            CreateTxtFiler createTxtFiles = new CreateTxtFiler();
            foreach (Chapter item in ListOfChapters)
            {
                createTxtFiles.CreateFile(item, TextFilePath, StoryName);
            }
        }
        public void MakeMp3File(List<Chapter> ListOfChapters, int ListOfChaptersCount, string LydBogPath, string StoryName)
        {
            CreateLydFiler createLydFile = new CreateLydFiler();

            int number = ListOfChapters.Count;
            int number1 = ListOfChapters.Count;
            DateTime dateTime = DateTime.Now;
            Console.WriteLine("         Start Tid    =" + dateTime);
            Console.WriteLine("         ---------------------------");
            foreach (Chapter item in ListOfChapters)
            {
                Console.WriteLine("         Title    " + item.Title);
                Console.WriteLine("         Start Number      " + number);
                number1--;
                Console.WriteLine("         Er nu             " + number1 + " Tilbage");
                ListOfChaptersCount = ListOfChaptersCount - item.Content.Length;
                Console.WriteLine("         Time Left");
                GetTimeLeft(ListOfChaptersCount / 1600);//1500

                if (!File.Exists(LydBogPath + StoryName + @"\" + item.Title + ".mp3"))
                {
                    createLydFile.CreateSoundFile(item, StoryName, LydBogPath);
                }

                Console.WriteLine("         ---------------------------");
            }

            DateTime dateTime1 = DateTime.Now;
            TimeSpan time = dateTime1 - dateTime;
            int TimeInSeconds = time.Seconds;
            GetTimeLeft(TimeInSeconds);

            Console.WriteLine("         Gæt Tid      =" + dateTime); //To Do Fix
            Console.WriteLine("         Slut Tid     =" + dateTime1);
            Console.WriteLine("         Tid taget    =" + (dateTime1 - dateTime));

        }
        public bool ListOfEpubs(List<Epub> epubs, string EpubFilePath, string TextFilePath, string LydBogPath)
        {
            GetContentFromEpub_V2 getContentFromEpub2 = new GetContentFromEpub_V2();

            foreach (Epub item in epubs)
            {
                Start(item.StoryName);
                List<Chapter> ListOfChapters = getContentFromEpub2.GetContentFromEpub_V2Metode(EpubFilePath, item.EpubToExtratFileName);
                int ListOfChaptersCount = ListOfChaptersCountMetode(ListOfChapters);
                MakeTxtFile(ListOfChapters, TextFilePath, item.StoryName);
                MakeMp3File(ListOfChapters, ListOfChaptersCount, LydBogPath, item.StoryName);
                End();
            }
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
