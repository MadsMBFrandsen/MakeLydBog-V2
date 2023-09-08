using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VersOne.Epub;
using MakeLydBog_V2_Wpf_App.Models;

namespace MakeLydBog_V2_Wpf_App
{
    class GetContentFromEpub_V2
    {
        public GetContentFromEpub_V2()
        {

        }
        public List<Chapter> GetContentFromEpub_V2Metode(string EpubFilename, bool NeedsANumber, int StartOnNumber)
        {
            List<List<string>> contentList = new List<List<string>>();
            //string epubFilePath = BaseEpubFilePath + EpubFilename + ".Epub";          
            string epubFilePath = EpubFilename;
            EpubBook epubBook = EpubReader.ReadBook(epubFilePath);

            List<Chapter> chapters = new List<Chapter>();

            Console.WriteLine($"         Title: {epubBook.Title}");
            Console.WriteLine($"         Author: {epubBook.Author}");

            int Number = 0;
            bool IsTrue = false;
            int count = 1; //hvis chapter Titlen ikke inde holder et number
            string key = string.Empty;
            foreach (EpubLocalTextContentFile item in epubBook.ReadingOrder)
            {
                Console.WriteLine("         Title");
                string Title = GetTitleFromEpub(item.Key);


                string titletemp = Title.ToLower();

                if (ForbiddenTitles(titletemp))
                {
                    if (NeedsANumber)
                    {
                        Title = "Chapter_ " + count + " " + Title.Substring(StartOnNumber);
                        count++;
                    }


                   

                    Console.WriteLine("         " + Title);
                    Console.WriteLine("         Content");
                    string Content = GetContentFromEpub(item.Content);

                    Chapter chapter = new Chapter();

                    chapter.Title = Title;
                    chapter.Content = Content;
                    chapters.Add(chapter);
                }
                else
                {
                    Console.WriteLine(Title);
                    Console.WriteLine("         Chapter is not a chapter");
                }
            }


            Console.WriteLine("         Done With Reading Epub");

            return chapters;
        }
        internal bool ForbiddenTitles(string titletemp)
        {
            bool istrue = true;
            if (titletemp.Contains("cover"))
            {
                istrue = false;
            }
            if (titletemp.Contains("information"))
            {
                istrue = false;
            }
            if (titletemp.Contains("stylesheet"))
            {
                istrue = false;
            }
            if (titletemp.Contains("title_page"))
            {
                istrue = false;
            }
            if (titletemp.Contains("nav"))
            {
                istrue = false;
            }
            if (titletemp.Contains("introduction"))
            {
                istrue = false;
            }

            return istrue;
        }

        internal string GetTitleFromEpub(string EpubTitle)
        {
            string title = string.Empty;
            List<string> EpubTitlelist = EpubTitle.Split("/").ToList();
            title = EpubTitlelist.Last().Trim();
            title = title.Replace(".xhtml", "");
            return title;
        }
        internal string GetContentFromEpub(string EpubContent)
        { //
            string content = string.Empty;
            content = CleanHtmlTags(EpubContent);
            List<string> tempcontentList = Regex.Split(content, @"(?<=[.])").ToList();
            List<string> tempcontentList2 = new List<string>();
            foreach (string item in tempcontentList)
            {
                if (item.Length >= 2)
                {
                    string st = item.Trim(); ;
                    tempcontentList2.Add(item);
                }
            }
            string str = String.Join("\n", tempcontentList2);
            content = str;
            return content;
        }

        static string CleanHtmlTags(string html)
        {
            // Remove HTML tags using regex
            string cleanedText = Regex.Replace(html, "<.*?>", string.Empty);

            // Convert special characters/entities to their plain text equivalents
            cleanedText = System.Net.WebUtility.HtmlDecode(cleanedText);

            return cleanedText;
        }
        static int GetWordCount(string text)
        {
            // Count the number of words in the text
            string[] words = text.Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            return words.Length;
        }

        static string RemoveSpecialCharacters(string input)
        {
            // Remove non-alphanumeric characters using regular expression
            string pattern = "[^a-zA-Z0-9 .,]";
            string result = Regex.Replace(input, pattern, "");

            return result;
        }
    }
}
