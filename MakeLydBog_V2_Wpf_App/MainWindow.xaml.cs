using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Text;
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
        GetContentFromEpub_V2 epub_V2;
        Fungtions fun;
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

        }
        private void CreateFilePaths()
        {
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
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            //string TextFieldUserName = Request.Form["TextFieldUserName"].ToString();


            CreateFilePaths();

        }

        private void btnEpubPath_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.Filter = "Epub files (*.epub)|*.epub";//.epub
            openFileDialog.InitialDirectory = @"C:\Users\mads3170\OneDrive\Books\SoundBooks\Epub";
            //openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog.ShowDialog();
            string epubFilePath = openFileDialog.FileName;
            

        }
    }
}
