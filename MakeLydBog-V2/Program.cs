using MakeLydBog_V2;


string StoryName = "Salvos";


string EpubToExtratFileName = "Salvos (Last chance to read all - MelasDelta";


//Paths
//-----MainPath
string StartPath = @"D:\Books\";
StartPath = @"C:\Users\mads3170\OneDrive\Books\SoundBooks";
//-----SubPaths
string EpubFilePath = StartPath + @"Epub\";
//string ExtraktionPath = StartPath + @"EpubExtrated\";
string TextFilePath = StartPath + @"TextFilles\";
string LydBogPath = StartPath + @"SoundBooks\";

//-----Create Folders
Directory.CreateDirectory(StartPath);
Directory.CreateDirectory(EpubFilePath);
Directory.CreateDirectory(TextFilePath);
Directory.CreateDirectory(LydBogPath);


//Instanses
GetContentFromEpub getContentFromEpub =
    new GetContentFromEpub();

CreateTxtFiles createTxtFiles =
    new CreateTxtFiles();

CreateLydFile createLydFile =
    new CreateLydFile();

Fungtions fungtions = 
    new Fungtions();
//Funktions

List<Chapter> ListOfChapters = getContentFromEpub.GetContentFromEpubMetode(EpubFilePath, EpubToExtratFileName);

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
    number1--;
    ListOfChaptersCount = ListOfChaptersCount - item.Content.Length;
    Console.WriteLine("Time Left");
    fungtions.GetTimeLeft(ListOfChaptersCount);
    Console.WriteLine("Start Number      " + number);
    Console.WriteLine("Er nu             " + number1 + " Tilbage");
    Console.WriteLine("Title    " + item.Title);
    if (!File.Exists(LydBogPath + StoryName + @"\" + item.Title + ".mp3"))
    {
        createLydFile.CreateSoundFile(LydBogPath, item, StoryName, LydBogPath);
    }

    Console.WriteLine("---------------------------");
}

DateTime dateTime1 = DateTime.Now;
Console.WriteLine("Slut Tid    =" + dateTime1);
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