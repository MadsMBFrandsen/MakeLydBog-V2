﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VersOne.Epub;
using HtmlAgilityPack;
using System.Text.RegularExpressions;


namespace MakeLydBog_V2
{
    public class GetContentFromEpub_V2
    {
        public GetContentFromEpub_V2()
        {

        }
        public List<Chapter> GetContentFromEpub_V2Metode(string BaseEpubFilePath, string EpubFilename)
        {
            List<List<string>> contentList = new List<List<string>>();
            string epubFilePath = BaseEpubFilePath + EpubFilename + ".Epub";
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
                    if (Number == 0 && IsTrue == false)
                    {
                        Console.WriteLine("         " + Title);
                        Console.WriteLine("         Skriv fra hvor Title begynder i tal");
                        //While title is not a number (Char.IsDigit())
                        try
                        {
                            Number = Convert.ToInt32(Console.ReadLine());
                            IsTrue = true;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("         Error on Title select");
                            throw;
                        }
                    }

                    Title = Title.Substring(Number);
                    Console.WriteLine("         Title = " + Title);
                    try
                    {

                        if (key == "")
                        {
                            Console.WriteLine("         Does the title need a number (y/n)");
                            key = Console.ReadLine();
                        }

                        if (key == "y")
                        {
                            //chapters1.Title = "Nr " + TitleCountNumber + ": " + chapterTitle;
                            Title = "Chapter_" + count + " " + Title;
                            count++;
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    Console.WriteLine("         " + Title);
                    Console.WriteLine("         Content");
                    string Content = GetContentFromEpub(item.Content);
                    //Console.WriteLine(Content);

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
                if (item.Length >=2)
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
