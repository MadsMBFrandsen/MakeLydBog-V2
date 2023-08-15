using MakeLydBog_V2;


string StoryName = "Making Waves";
StoryName = "Azarinth Healer - Rhaegar";
StoryName = "Naruto Tamer of the XAntibody";
StoryName = "The Game Must Go On";
//Nanomancer Reborn - I've Become A Snow Girl
//Epic of Caterpillar
//Itai no wa Iya nanode Bōgyo-Ryoku ni Kyokufuri Shitai to Omoimasu
//Isekai’d Shoggoth
//Invincible
//I Really Am Not The Lord of Demon
//Hybrid Hive Eat Shard
//Haze Gray
//For Want of an Outfit
//Distance Learning for Fun and Profit
//Death March kara Hajimaru Isekai Kyousoukyoku
//As Long as You Have the [Shop] Skill

string EpubToExtratFileName = "Making_Waves_by_Vimesenthusiast-7JFqWVfu";
EpubToExtratFileName = "Azarinth Healer - Rhaegar";
EpubToExtratFileName = "Naruto_Tamer_of_the_XAntibody_by_KitsuneDragon-8qkewete";
EpubToExtratFileName = "The_Game_Must_Go_On_by_crossedge-byiu1SNR";

//Am_I_Smart_by_Penguin-sa-oKVKU2pf

List<string> StoryNameList = new List<string>();
List<string> EpubToExtratFileNameList = new List<string>();

StoryNameList.Add("Isekaid Shoggoth");
EpubToExtratFileNameList.Add("Isekaid_Shoggoth");
//StoryNameList.Add("A Mother Goo in a Grimdark World");
//EpubToExtratFileNameList.Add("A Mother Goo in a Grimdark World");
//StoryNameList.Add("Death March");
//EpubToExtratFileNameList.Add("Death_March_kara_Hajimaru_Isekai_Kyousoukyoku__Sousetsuka");



//Paths
//-----MainPath
string StartPath = @"D:\Books\";
//-----SubPaths
string EpubFilePath = StartPath + @"Epub\";
string ExtraktionPath = StartPath + @"EpubExtrated\";
//string TxtExtratedFolder = StartPath + @"TextFilles\";
string TextFilePath = StartPath + @"TextFilles\";
string LydBogPath = StartPath + @"SoundBooks\";


//Instanses
GetContentFromEpub getContentFromEpub =
    new GetContentFromEpub();
//----------------
GetContentFromEpub_V2 getContentFromEpub2 =
    new GetContentFromEpub_V2();
//----------------

CreateTxtFiles createTxtFiles =
    new CreateTxtFiles();

CreateLydFile createLydFile =
    new CreateLydFile();

Fungtions fungtions =
    new Fungtions();
//Funktions


//lav det til en metode så der bare skal kaldes på den i en fungtions.CreateStuff(Storyname,
//EpubToExtratFileNameList)

//while (StoryNameList.Count > 0)
//{
//    StoryName = StoryNameList[0];
//    StoryNameList.RemoveAt(0);
//    EpubToExtratFileName = EpubToExtratFileNameList[0];
//    EpubToExtratFileNameList.RemoveAt(0);

    Console.WriteLine("***");
    Console.WriteLine("StoryName =" + StoryName);
    Console.WriteLine("***");


    //List<Chapter> ListOfChapters = getContentFromEpub.GetContentFromEpubMetode(EpubFilePath, EpubToExtratFileName);
    List<Chapter> ListOfChapters = getContentFromEpub2.GetContentFromEpub_V2Metode(EpubFilePath, EpubToExtratFileName);

int ListOfChaptersCount = 0;
    foreach (Chapter chapter in ListOfChapters)
    {
        ListOfChaptersCount = ListOfChaptersCount + chapter.Content.Length;
    }

    foreach (Chapter item in ListOfChapters)
    {
        createTxtFiles.CreateFile(item, TextFilePath, StoryName);
    }

    int number = ListOfChapters.Count;
    int number1 = ListOfChapters.Count;
    DateTime dateTime = DateTime.Now;
    Console.WriteLine("Start Tid    =" + dateTime);
    Console.WriteLine("---------------------------");
    foreach (Chapter item in ListOfChapters)
    {
        //DateTime DT = DateTime.Now;
        //TimeSpan DT1 = DT - dateTime;
        //int TimeInSeconds = DT1.Seconds;
        //fungtions.GetTimeLeft(TimeInSeconds);
        Console.WriteLine("Title    " + item.Title);
        Console.WriteLine("Start Number      " + number);
        number1--;
        Console.WriteLine("Er nu             " + number1 + " Tilbage");
        ListOfChaptersCount = ListOfChaptersCount - item.Content.Length;
        Console.WriteLine("Time Left");
        fungtions.GetTimeLeft(ListOfChaptersCount / 1600);//1500

        if (!File.Exists(LydBogPath + StoryName + @"\" + item.Title + ".mp3"))
        {
            createLydFile.CreateSoundFile(LydBogPath, item, StoryName, LydBogPath);
        }

        Console.WriteLine("---------------------------");
    }

    DateTime dateTime1 = DateTime.Now;
    TimeSpan time = dateTime1 - dateTime;
    int TimeInSeconds = time.Seconds;
    fungtions.GetTimeLeft(TimeInSeconds);
    
    Console.WriteLine("Gæt Tid      =" + dateTime); //To Do Fix
    Console.WriteLine("Slut Tid     =" + dateTime1);
    Console.WriteLine("Tid taget    =" + (dateTime1 - dateTime));

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

//}