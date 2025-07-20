using System;
using System.Collections.Generic;

// The Comment class abstracts the concept of a comment,
// focusing on what a comment IS (name, text) rather than how it's stored internally.
class Comment
{
    // Private fields to encapsulate the data
    private string _name;
    private string _text;

    // Constructor to initialize a new Comment object
    public Comment(string name, string text)
    {
        _name = name;
        _text = text;
    }

    // Public getters to access the encapsulated data
    public string GetName()
    {
        return _name;
    }

    public string GetText()
    {
        return _text;
    }
}

// The Video class abstracts the concept of a YouTube video,
// providing essential properties and behaviors without exposing internal complexities.
class Video
{
    // Private fields for video properties, demonstrating encapsulation
    private string _title;
    private string _author;
    private int _lengthSeconds; // Length of the video in seconds
    private List<Comment> _comments; // A list to hold Comment objects

    // Constructor to initialize a new Video object
    public Video(string title, string author, int lengthSeconds)
    {
        _title = title;
        _author = author;
        _lengthSeconds = lengthSeconds;
        _comments = new List<Comment>(); // Initialize the list of comments
    }

    // Method to add a comment to the video's comment list
    public void AddComment(Comment comment)
    {
        _comments.Add(comment);
    }

    // Method to return the number of comments on the video
    public int GetNumberOfComments()
    {
        return _comments.Count;
    }

    // Method to display all information about the video and its comments
    public void DisplayVideoInfo()
    {
        Console.WriteLine($"Title: {_title}");
        Console.WriteLine($"Author: {_author}");
        Console.WriteLine($"Length: {_lengthSeconds} seconds");
        Console.WriteLine($"Number of Comments: {GetNumberOfComments()}"); // Using the method to get count

        // Iterate through the comments and display each one
        Console.WriteLine("Comments:");
        if (_comments.Count == 0)
        {
            Console.WriteLine("  No comments yet.");
        }
        else
        {
            foreach (Comment comment in _comments)
            {
                Console.WriteLine($"  - {comment.GetName()}: \"{comment.GetText()}\"");
            }
        }
        Console.WriteLine(); // Add a blank line for readability between videos
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create a list to hold Video objects
        List<Video> videos = new List<Video>();

        // --- Video 1 ---
        Video video1 = new Video("C# Basics: Variables and Data Types", "CodeAcademy", 600);
        video1.AddComment(new Comment("Alice Smith", "Great introduction! Very clear explanations."));
        video1.AddComment(new Comment("Bob Johnson", "Helped me understand int vs string. Thanks!"));
        video1.AddComment(new Comment("Charlie Brown", "Could you make a video on loops next?"));
        video1.AddComment(new Comment("Diana Prince", "Perfect for beginners."));
        videos.Add(video1);

        // --- Video 2 ---
        Video video2 = new Video("Advanced LINQ Techniques in C#", "TechGuru", 1200);
        video2.AddComment(new Comment("Eve Adams", "This was a bit too advanced for me, but I'll rewatch."));
        video2.AddComment(new Comment("Frank White", "Finally, a clear explanation of GroupBy!"));
        video2.AddComment(new Comment("Grace Lee", "Awesome content, learned a lot."));
        videos.Add(video2);

        // --- Video 3 ---
        Video video3 = new Video("Building a Simple Web API with ASP.NET Core", "DevJourney", 900);
        video3.AddComment(new Comment("Henry King", "Very practical tutorial."));
        video3.AddComment(new Comment("Ivy Queen", "What about authentication?"));
        video3.AddComment(new Comment("Jack Sparrow", "Exactly what I needed for my project."));
        videos.Add(video3);

        // --- Video 4 ---
        Video video4 = new Video("Understanding Asynchronous Programming in C#", "AsyncMaster", 750);
        video4.AddComment(new Comment("Karen Miller", "The async/await explanation was superb!"));
        video4.AddComment(new Comment("Liam Neeson", "My code is much cleaner now."));
        videos.Add(video4);

        // Iterate through the list of videos and display their information
        Console.WriteLine("--- YouTube Video Tracker Report ---");
        foreach (Video video in videos)
        {
            video.DisplayVideoInfo();
            Console.WriteLine("-------------------------------------"); // Separator for clarity
        }
    }
}