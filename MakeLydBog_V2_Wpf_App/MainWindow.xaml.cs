using MakeLydBog_V2_Wpf_App.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace MakeLydBog_V2_Wpf_App
{
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        GetContentFromEpub_V2 epub_V2;
        Fungtions fun;
        FungtionsV2 funV2;
        string epubFilePath = string.Empty;
        string txtFilePath = string.Empty;
        string soundFilePath = string.Empty;
        string Storyname = string.Empty;
        List<Epub> epubList;
        List<Chapter> ListOfChapters;


        int StartNumber = 0;
        int RealNumber = 0;
        string ChapterName = "";
        string TimeLeft = "";
        public MainWindow()
        {
            InitializeComponent();
            Start();

        }

        private void Start()
        {
            //Instanses
            epub_V2 = new GetContentFromEpub_V2();
            fun = new Fungtions();
            funV2 = new FungtionsV2();
            epubList = new List<Epub>();
        }
        private void CreateFilePaths()
        {
            //If D:\Books Exist vælg denne stig

            //-----Create Folders
            //Directory.CreateDirectory(epubFilePath);
            Directory.CreateDirectory(txtFilePath);
            Directory.CreateDirectory(soundFilePath);

        }

        private async void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            if (TBEpubFilePath.Text == "Epub File Path" || TBSoundPath.Text == "Txt File Path" || TBSoundPath.Text == "Sound File Path" || TBStoryName.Text == "Story Name")
            {
                if (TBEpubFilePath.Text == "Epub File Path")
                {
                    LError.Content = "Needs A Epub File Path";
                }
                if (TBTextPath.Text == "Txt File Path")
                {
                    LError.Content = "Needs A Txt File Path";
                }
                if (TBSoundPath.Text == "Sound File Path")
                {
                    LError.Content = "Needs A Sound File Path";
                }
                if (TBStoryName.Text == "Story Name")
                {
                    LError.Content = "Needs A Story Name";
                }
                LError.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            else
            {
                LError.Content = "Errors";
                LError.BorderBrush = System.Windows.Media.Brushes.Black;
                BtnStart.IsEnabled = false;
                BtnEpubPath.IsEnabled = false;
                BtnTxtPath.IsEnabled = false;
                BtnSoundPath.IsEnabled = false;
                BtnAddToList.IsEnabled = false;

                TBEpubFilePath.IsEnabled = false;
                TBTextPath.IsEnabled = false;
                TBSoundPath.IsEnabled = false;
                TBStoryName.IsEnabled = false;
                TBChapterName.IsEnabled = false;

                CBIsAList.IsEnabled = false;
                CBOneChapter.IsEnabled = false;

                CreateFilePaths();
                string NeedANumber = string.Empty;
                string StartOn = string.Empty;

                if (CBNeedANumber.IsChecked == true)
                {

                }
                if (CBStartOn.IsChecked == true)
                {

                }

                if (CBOneChapter.IsChecked == true)
                {
                    string txtcontent = TBContent.Text;
                    string txttitle = TBChapterName.Text;
                    Chapter chapter = new Chapter();
                    chapter.Title = txttitle;
                    chapter.Content = txtcontent;
                    List<Chapter> ListOfChapters = new List<Chapter>
                    {
                        chapter
                    };
                    await funV2.OneChapterAsync(ListOfChapters, txtFilePath, soundFilePath, Storyname);
                }

                if (CBIsAList.IsChecked == true)
                {
                    dispatcherTimer.Tick += new EventHandler(OnTimerEvent);
                    dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
                    dispatcherTimer.Start();

                    await funV2.ListOfEpubsAsync(epubList, epubFilePath, txtFilePath, soundFilePath);
                }
                else
                {
                    Epub epub = new Epub();
                    epub.StoryName = TBStoryName.Text.Trim();
                    epub.EpubToExtratFileName = TBEpubFilePath.Text;

                    dispatcherTimer.Tick += new EventHandler(OnTimerEvent);
                    dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
                    dispatcherTimer.Start();

                    epubList.Clear();
                    epubList.Add(epub);
                    await funV2.ListOfEpubsAsync(epubList, epubFilePath, txtFilePath, soundFilePath);
                }

                dispatcherTimer.Stop();

                BtnStart.IsEnabled = true;
                BtnEpubPath.IsEnabled = true;
                BtnTxtPath.IsEnabled = true;
                BtnSoundPath.IsEnabled = true;
                BtnAddToList.IsEnabled = true;

                TBEpubFilePath.IsEnabled = true;
                TBTextPath.IsEnabled = true;
                TBSoundPath.IsEnabled = true;
                TBStoryName.IsEnabled = true;
                TBChapterName.IsEnabled = true;

                CBIsAList.IsEnabled = true;
                CBOneChapter.IsEnabled = true;
            }
        }

        private async void OnTimerEvent(object? sender, EventArgs e)
        {
            StartNumber = funV2.GetStartNumberAsync();
            RealNumber = funV2.GetRealNumberAsync();
            ChapterName = funV2.GetChapterNameAsync();
            TimeLeft = funV2.GetTimeLeftAsync();

            LStartNummer.Content = (int)StartNumber;
            LRealNummer.Content = (int)RealNumber;
            LChapterName.Content = ChapterName;
            LTimeLeft.Content = TimeLeft;
        }

        private async Task ListOfEpubsMethodeAsync(object sender, EventArgs e)
        {
            await funV2.ListOfEpubsAsync(epubList, epubFilePath, txtFilePath, soundFilePath);
        }

        private void BtnEpubPath_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.Filter = "Epub files (*.epub)|*.epub";
            openFileDialog.InitialDirectory = @"C:\Users\mads3170\OneDrive\Books\SoundBooks\Epub";
            //openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //openFileDialog.ShowDialog();



            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                epubFilePath = openFileDialog.FileName;
                TBEpubFilePath.Text = epubFilePath;
                List<string> tempstringList = epubFilePath.Split(@"\").ToList();
                string tempstringReplace = tempstringList.Last().Replace("_", " ").Trim().Split(".").ToList().First().Split("by ").ToList().First();
                //List<string> tempstringList2 = tempstringReplace.Split(".").ToList();
                //string tempstring2 = tempstringList2.First();
                //tempstring2 = tempstringReplace.Split(@"by").ToList().First();
                TBStoryName.Text = tempstringReplace;
            }
            else
            {
                epubFilePath = string.Empty;
            }


        }

        private void BtnTxtPath_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            fbd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtFilePath = fbd.SelectedPath + @"\TextFilles\";
                TBTextPath.Text = fbd.SelectedPath + @"\TextFilles\";
            }
            else
            {
                txtFilePath = string.Empty;
            }

        }

        private void BtnSoundPath_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            fbd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                soundFilePath = fbd.SelectedPath + @"\SoundBooks\";
                TBSoundPath.Text = fbd.SelectedPath + @"\SoundBooks\";
            }
            else
            {
                soundFilePath = string.Empty;
            }
        }

        private void BtnAddToList_Click(object sender, RoutedEventArgs e)
        {

            if (epubList.Count == 0)
            {
                epubFilePath = TBEpubFilePath.Text.Trim();
                Storyname = TBStoryName.Text.Trim();
                LVEpubFilePath.Items.Add(epubFilePath);
                LVStoryname.Items.Add(Storyname);
                Epub epub = new Epub();
                epub.StoryName = Storyname;
                epub.EpubToExtratFileName = epubFilePath;
                epubList.Add(epub);
            }
            else
            {
                if (epubList.Last().EpubToExtratFileName == TBEpubFilePath.Text || TBEpubFilePath.Text == null || TBEpubFilePath.Text == "" ||
                epubList.Last().StoryName == TBStoryName.Text || TBStoryName.Text == null || TBStoryName.Text == "")
                {
                    //On Punching Gods and Absentee Dads
                }
                else
                {
                    epubFilePath = TBEpubFilePath.Text;
                    Storyname = TBStoryName.Text;
                    LVEpubFilePath.Items.Add(epubFilePath);
                    LVStoryname.Items.Add(Storyname);
                    Epub epub = new Epub();
                    epub.StoryName = Storyname;
                    epub.EpubToExtratFileName = epubFilePath;
                    epubList.Add(epub);
                }

            }


        }
        private void BtnCheckStoryname_Click(object sender, RoutedEventArgs e)
        {
            if (TBEpubFilePath.Text != "Epub File Path")
            {
                GetContentFromEpub_V2 getContentFromEpub2 = new GetContentFromEpub_V2();
                ListOfChapters = getContentFromEpub2.GetContentFromEpub_V2Metode(epubFilePath);
                foreach (Chapter item in ListOfChapters)
                {
                    LVChapterTitlesNames.Items.Add(item.Title);
                }
            }
        }
        private void CBIsAList_Checked(object sender, RoutedEventArgs e)
        {
            CBOneChapter.IsEnabled = false;
            TBEpubFilePath.BorderBrush = System.Windows.Media.Brushes.Red;
            TBChapterName.BorderBrush = System.Windows.Media.Brushes.Red;
            TBStoryName.BorderBrush = System.Windows.Media.Brushes.Red;
            TBTextPath.BorderBrush = System.Windows.Media.Brushes.Red;
            TBSoundPath.BorderBrush = System.Windows.Media.Brushes.Red;

        }

        private void CBIsAList_UnChecked(object sender, RoutedEventArgs e)
        {
            CBOneChapter.IsEnabled = true;
            TBEpubFilePath.BorderBrush = System.Windows.Media.Brushes.Black;
            TBChapterName.BorderBrush = System.Windows.Media.Brushes.Black;
            TBStoryName.BorderBrush = System.Windows.Media.Brushes.Black;
            TBTextPath.BorderBrush = System.Windows.Media.Brushes.Black;
            TBSoundPath.BorderBrush = System.Windows.Media.Brushes.Black;
        }


        private void IsChecked(object sender, RoutedEventArgs e)
        {
            CBIsAList.IsEnabled = false;
            TBEpubFilePath.BorderBrush = System.Windows.Media.Brushes.Red;
            TBChapterName.BorderBrush = System.Windows.Media.Brushes.Red;
            TBStoryName.BorderBrush = System.Windows.Media.Brushes.Red;
            TBTextPath.BorderBrush = System.Windows.Media.Brushes.Red;
            TBSoundPath.BorderBrush = System.Windows.Media.Brushes.Red;

        }

        private void IsUnChecked(object sender, RoutedEventArgs e)
        {
            CBIsAList.IsEnabled = true;
            TBEpubFilePath.BorderBrush = System.Windows.Media.Brushes.Black;
            TBChapterName.BorderBrush = System.Windows.Media.Brushes.Black;
            TBStoryName.BorderBrush = System.Windows.Media.Brushes.Black;
            TBTextPath.BorderBrush = System.Windows.Media.Brushes.Black;
            TBSoundPath.BorderBrush = System.Windows.Media.Brushes.Black;

        }


    }
}

