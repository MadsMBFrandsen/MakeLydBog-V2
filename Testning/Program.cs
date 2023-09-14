using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        int StartNumber = 5; // Change this to your desired StartNumber
        int EndNumber = 10; // Change this to your desired EndNumber

        List<Chapter> sourceChapters = new List<Chapter>
        {
            new Chapter { Title = "Chapter 1", Content = "Content 1", Number = 1 },
            new Chapter { Title = "Chapter 2", Content = "Content 2", Number = 5 },
            new Chapter { Title = "Chapter 3", Content = "Content 3", Number = 8 },
            new Chapter { Title = "Chapter 4", Content = "Content 4", Number = 12 },
        };

        List<Chapter> filteredChapters = FilterChapters(sourceChapters, StartNumber, EndNumber);

        foreach (Chapter chapter in filteredChapters)
        {
            Console.WriteLine($"Title: {chapter.Title}, Content: {chapter.Content}, Number: {chapter.Number}");
        }
    }

    public static List<Chapter> FilterChapters(List<Chapter> sourceChapters, int startNumber, int endNumber)
    {
        if (startNumber == 0 && endNumber == 0)
        {
            // If both startNumber and endNumber are 0, return all chapters.
            return sourceChapters;
        }
        else
        {
            // Filter chapters based on the given range.
            List<Chapter> filteredChapters = new List<Chapter>();
            foreach (Chapter chapter in sourceChapters)
            {
                if (chapter.Number >= startNumber && chapter.Number <= endNumber)
                {
                    filteredChapters.Add(chapter);
                }
            }
            return filteredChapters;
        }
    }
}

class Chapter
{
    public string Title { get; set; }
    public string Content { get; set; }
    public int Number { get; set; }
}
