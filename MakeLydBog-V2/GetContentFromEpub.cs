using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using VersOne.Epub;

namespace MakeLydBog_V2
{
    public class GetContentFromEpub
    {
        public GetContentFromEpub()
        {

        }
        public List<Chapter> GetContentFromEpubMetode(string BaseEpubFilePath, string EpubFilename)
        {
            List<List<string>> contentList = new List<List<string>>();
            string epubFilePath = BaseEpubFilePath + EpubFilename + ".Epub";
            EpubBook epubBook = EpubReader.ReadBook(epubFilePath);

            List<Chapter> chapters = new List<Chapter>();
            //List<string> chapters = new List<string>();

            // Add number til title
            int TitleCountNumber = 1;

            foreach (EpubLocalTextContentFile textContentFile in epubBook.ReadingOrder)
            {
                string filePath = textContentFile.FilePath;
                string fileName = Path.GetFileName(filePath);

                string content = textContentFile.Content;
                string ContentType = textContentFile.ContentType.ToString();
                bool b = !IsExcludedFile(fileName, textContentFile.Content);
                //Console.WriteLine(b);
                string chapterTitle = ExtractChapterTitle(textContentFile.Content);

                //Title is "" if Epub Is From sufficientvelocity
                if (chapterTitle == "")
                {
                    HtmlDocument htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(content);

                    HtmlNode h1Node = htmlDoc.DocumentNode.SelectSingleNode("//h1");
                    chapterTitle = h1Node?.InnerHtml;
                    //<h1>Negima - Chapter 1</h1>
                    //find first instans of chapter 
                }
                bool ContainsANumber = false;
                string chapterTitleLower = "patreon";
                try
                {
                    ContainsANumber = Regex.IsMatch(chapterTitle, @"\d");
                    chapterTitleLower = chapterTitle.ToLower();
                }
                catch (Exception)
                {

                    Console.WriteLine("Error on " + chapterTitle);
                }


                if (ContainsANumber)
                {
                    if (!chapterTitleLower.Contains("amazon kindel") || !chapterTitleLower.Contains("not a chapter") ||
                        !chapterTitleLower.Contains("patreon") || !chapterTitleLower.Contains("salvos book") ||
                        !chapterTitleLower.Contains("is now available") || !chapterTitleLower.Contains("last chance to read all"))//
                    {
                        //chapterTitle = ExtractChapterTitle(textContentFile.Content);

                        //chapterTitle = RemoveSpecialCharacters(chapterTitle);
                        //chapterTitle = Regex.Replace(chapterTitle, @"[?!%&]", "");
                        string chapterContent = CleanHtmlTags(textContentFile.Content);
                        chapterContent = chapterContent.Trim();

                        Console.WriteLine(chapterTitle);

                        if (chapterTitle != "")
                        {
                            string result = chapterContent.Replace(chapterTitle, string.Empty, StringComparison.OrdinalIgnoreCase);
                        }


                        //int chapterTitleNumber1 = chapterTitle.Length;
                        //string chapterTitlestring = chapterContent.Substring(0, chapterTitleNumber1);


                        chapterTitle = RemoveSpecialCharacters(chapterTitle);


                        //if (chapterTitle == chapterTitlestring)
                        //{
                        //    //if there is multibil titels in the files
                        //    //chapterContent = chapterTitle + result;
                        //}


                        // Add chapter content to the list
                        Chapter chapters1 = new Chapter();
                        chapters1.Title = "Nr " + TitleCountNumber + ": " + chapterTitle;
                        chapters1.Content = chapterContent;
                        TitleCountNumber++;
                        chapters.Add(chapters1);
                        //chapters.Add(chapterContent);
                    }
                }
            }


            return chapters;
        }

        //static bool IsExcludedFile(string fileName)
        //{
        //    string[] excludedFiles = { "cover.xhtml", "title_page.xhtml", "Information.xhtml", "stylesheet.css" };
        //    return Array.Exists(excludedFiles, f => f.Equals(fileName, StringComparison.OrdinalIgnoreCase));
        //}

        static List<string> GetChapters(string input, string title)
        {
            List<string> chapters = new List<string>();

            string pattern = $@"(?<={Regex.Escape(title)})\s*[\s\S]*?(?=\r?\n\r?\n)";

            Match match = Regex.Match(input, pattern);

            if (match.Success)
            {
                string chapter = match.Value.Trim();
                chapters.Add(chapter);
            }

            return chapters;
        }


        static bool IsExcludedFile(string fileName, string content)
        {
            // Exclude files with titles containing specific keywords
            string[] excludedKeywords = { "KINDLE", "Amazon", "Poll", "Poll:", "Art Gallery", "Announcement", "cover", "title_page", "Information", "stylesheet" };
            foreach (string keyword in excludedKeywords)
            {
                if (fileName.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            // Exclude files with more than 1000 words
            int wordCount = GetWordCount(content);
            if (wordCount < 500)
            {
                return true;
            }
            return false;
        }

        static string ExtractChapterTitle(string html)
        {
            // Extract the chapter title from the content using regex or any suitable method
            // Here's an example using a simple regex pattern
            Match match = Regex.Match(html, @"<title>(.*?)<\/title>");
            return match.Success ? match.Groups[1].Value : string.Empty;
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

