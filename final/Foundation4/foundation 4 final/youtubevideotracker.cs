using System;
using System.Collections.Generic;

class Comment
{
    public string Name { get; set; }
    public string Text { get; set; }

    public Comment(string name, string text)
    {
        Name = name;
        Text = text;
    }
}

class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int LengthSeconds { get; set; }
    public List<Comment> Comments { get; } = new List<Comment>();

    public Video(string title, string author, int lengthSeconds)
    {
        Title = title;
        Author = author;
        LengthSeconds = lengthSeconds;
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Title: {Title}\nAuthor: {Author}\nLength: {LengthSeconds} seconds");
        Console.WriteLine($"Comments ({Comments.Count}):");
        foreach (var comment in Comments)
        {
            Console.WriteLine($"- {comment.Name}: {comment.Text}");
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        List<Video> videos = new List<Video>();

        var video1 = new Video("Intro to C#", "Patrick", 300);
        video1.AddComment(new Comment("Alice", "Great video!"));
        video1.AddComment(new Comment("Bob", "Very helpful, thanks!"));

        var video2 = new Video("Advanced LINQ", "Patrick", 480);
        video2.AddComment(new Comment("Sarah", "This was hard to follow."));
        video2.AddComment(new Comment("Jake", "I finally understand LINQ now!"));

        var video3 = new Video("How to Center a Div", "Patrick", 120);
        video3.AddComment(new Comment("Mia", "Short and sweet!"));

        videos.Add(video1);
        videos.Add(video2);
        videos.Add(video3);

        foreach (var video in videos)
        {
            video.DisplayInfo();
        }
    }
}
