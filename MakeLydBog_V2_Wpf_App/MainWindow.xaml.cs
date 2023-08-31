﻿using MakeLydBog_V2_Wpf_App.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;


namespace MakeLydBog_V2_Wpf_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        GetContentFromEpub_V2 epub_V2;
        Fungtions fun;
        string epubFilePath;
        string txtFilePath;
        string soundFilePath;
        string Storyname;
        List<Epub> epubList;

        int StartNumber = 0;
        int RealNumber = 0;
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
            epubList = new List<Epub>();




        }
        private void CreateFilePaths()
        {
            //-----Create Folders
            //Directory.CreateDirectory(epubFilePath);
            Directory.CreateDirectory(txtFilePath);
            Directory.CreateDirectory(soundFilePath);
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
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

            //string TextFieldUserName = Request.Form["TextFieldUserName"].ToString();
            if (CBOneChapter.IsChecked == true)
            {
                string txtcontent = TBContent.Text;
                string txttitle = TBChapterName.Text;
                Chapter chapter = new Chapter();
                chapter.Title = txttitle;
                chapter.Content = txtcontent;
                List<Chapter> ListOfChapters = new List<Chapter>();
                ListOfChapters.Add(chapter);
                fun.OneChapter(ListOfChapters, txtFilePath, soundFilePath, Storyname);
            }
            if (CBIsAList.IsChecked == true)
            {
                dispatcherTimer.Tick += new EventHandler(OnTimerEvent);
                dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
                dispatcherTimer.Start();

                fun.ListOfEpubs(epubList, epubFilePath, txtFilePath, soundFilePath);

                //dispatcherTimer.Stop();
            }
            else
            {
                Epub epub = new Epub();
                epub.StoryName = TBStoryName.Text;
                epub.EpubToExtratFileName = TBEpubFilePath.Text;

                dispatcherTimer.Tick += new EventHandler(OnTimerEvent);
                dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
                dispatcherTimer.Start();
                                

                epubList.Clear();
                epubList.Add(epub);
                fun.ListOfEpubs(epubList, epubFilePath, txtFilePath, soundFilePath);
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
        public async void Threadtemp(object? sender, EventArgs e)
        {
            StartNumber = await fun.GetStartNumberAsync();
            RealNumber = await fun.GetRealNumberAsync();
            //StartNumber = fun.StartNumber;
            //RealNumber = fun.RealNumber;
            LStartNummer.Content = StartNumber;
            LRealNummer.Content = RealNumber;
        }
        private async void OnTimerEvent(object? sender, EventArgs e)
        {


            StartNumber = await fun.GetStartNumberAsync();
            RealNumber = await fun.GetRealNumberAsync();
            //StartNumber = fun.StartNumber;
            //RealNumber = fun.RealNumber;
            LStartNummer.Content = (int)StartNumber;
            LRealNummer.Content = (int)RealNumber;
        }

        public void ListOfEpubsMethode(object sender, EventArgs e)
        {
            fun.ListOfEpubs(epubList, epubFilePath, txtFilePath, soundFilePath);
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
            //bool isTrue = false;
            if (epubList.Count == 0)
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
