using MakeLydBog_V2;
using MakeLydBog_V2.Models;
using System.Collections.Generic;


List<Epub> epubs = new List<Epub>();
Epub epub = new Epub();
epub.StoryName = ("The Dungeon Without a System");
epub.EpubToExtratFileName = ("The_Dungeon_Without_a_System");
epubs.Add(epub);


//Paths
//-----MainPath
string StartPath = @"D:\Books\";
StartPath = @"C:\Users\mads3170\OneDrive\Books\SoundBooks\";
//-----SubPaths
string EpubFilePath = StartPath + @"Epub\";
string TextFilePath = StartPath + @"TextFilles\";
string LydBogPath = StartPath + @"SoundBooks\";

//-----Create Folders
Directory.CreateDirectory(StartPath);
Directory.CreateDirectory(EpubFilePath);
Directory.CreateDirectory(TextFilePath);
Directory.CreateDirectory(LydBogPath);


//Instanses
GetContentFromEpub_V2 getContentFromEpub2 =
    new GetContentFromEpub_V2();
//----------------
Fungtions fungtions =
    new Fungtions();
//----------------

//Funktions
bool islist = false;


if (islist)
{
    fungtions.ListOfEpubs(epubs, EpubFilePath, TextFilePath, LydBogPath);
}
else
{
    fungtions.Start(epubs[0].StoryName);
    List<Chapter> ListOfChapters = getContentFromEpub2.GetContentFromEpub_V2Metode(EpubFilePath, epubs[0].EpubToExtratFileName);
    //List<Chapter> ListOfChapters = getContentFromEpub2.GetContentFromEpub_V2Metode(EpubFilePath, EpubToExtratFileName);
    int ListOfChaptersCount = fungtions.ListOfChaptersCountMetode(ListOfChapters);
    fungtions.MakeTxtFile(ListOfChapters, TextFilePath, epubs[0].StoryName);
    //fungtions.MakeTxtFile(ListOfChapters, TextFilePath, StoryName);
    fungtions.MakeMp3File(ListOfChapters, ListOfChaptersCount, LydBogPath, epubs[0].StoryName);
    //fungtions.MakeMp3File(ListOfChapters, ListOfChaptersCount, LydBogPath, StoryName);
    fungtions.End();
}