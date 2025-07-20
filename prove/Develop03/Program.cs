// Program.cs
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Exceeding Requirements: Storing a library of scriptures.
        // For simplicity, we'll start with one scripture and then expand.
        // You could load these from a file as another stretch goal.
        List<Scripture> scriptures = new List<Scripture>
        {
            new Scripture(new ScriptureReference("John", 3, 16), "For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life."),
            new Scripture(new ScriptureReference("Proverbs", 3, 5, 6), "Trust in the Lord with all thine heart; and lean not unto thine own understanding. In all thy ways acknowledge him, and he shall direct thy paths.")
        };

        // Select a scripture to memorize (for now, just the first one)
        // Exceeding Requirements: Randomly selecting a scripture from the library.
        Random random = new Random();
        Scripture currentScripture = scriptures[random.Next(scriptures.Count)];

        // Main program loop
        while (!currentScripture.IsCompletelyHidden())
        {
            Console.Clear(); // Clear the console screen
            Console.WriteLine(currentScripture.GetDisplayText()); // Display the scripture

            Console.WriteLine("\nPress Enter to hide words, or type 'quit' to exit.");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
            {
                break; // Exit the loop if user types 'quit'
            }

            // Hide a few random words (e.g., 3-5 words at a time)
            currentScripture.HideRandomWords(random.Next(3, 6)); // Hide 3 to 5 words
        }

        Console.Clear();
        Console.WriteLine(currentScripture.GetDisplayText()); // Display the fully hidden scripture
        Console.WriteLine("\nAll words are hidden. Good job! Or you quit. Program ending.");
        Console.WriteLine("🚀 If you see this, you finally overwrote Program.cs correctly!");
    }
}

// Scripture.cs
class Scripture
{
    private ScriptureReference _reference;
    private List<Word> _words;

    // Constructor to initialize the scripture with a reference and text
    public Scripture(ScriptureReference reference, string text)
    {
        _reference = reference;
        _words = new List<Word>();

        // Split the text into words and create Word objects
        string[] wordArray = text.Split(new char[] { ' ', ',', '.', ';', ':', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string wordText in wordArray)
        {
            _words.Add(new Word(wordText));
        }
    }

    // Hides a specified number of random words that are not already hidden
    public void HideRandomWords(int count)
    {
        // Exceeding Requirements: Randomly selecting from only unhidden words.
        List<Word> unhiddenWords = _words.Where(word => !word.IsHidden()).ToList();
        Random random = new Random();

        // If there are fewer unhidden words than 'count', hide all remaining unhidden words
        int wordsToHide = Math.Min(count, unhiddenWords.Count);

        for (int i = 0; i < wordsToHide; i++)
        {
            int indexToHide = random.Next(unhiddenWords.Count);
            unhiddenWords[indexToHide].Hide();
            unhiddenWords.RemoveAt(indexToHide); // Remove to avoid hiding the same word twice in one turn
        }
    }

    // Gets the full display text of the scripture, including reference and hidden words
    public string GetDisplayText()
    {
        string scriptureText = string.Join(" ", _words.Select(word => word.GetDisplayText()));
        return $"{_reference.GetDisplayText()} {scriptureText}";
    }

    // Checks if all words in the scripture are hidden
    public bool IsCompletelyHidden()
    {
        return _words.All(word => word.IsHidden());
    }
}

// ScriptureReference.cs
class ScriptureReference
{
    private string _book;
    private int _chapter;
    private int _startVerse;
    private int _endVerse; // Optional, for verse ranges

    // Constructor for a single verse scripture (e.g., John 3:16)
    public ScriptureReference(string book, int chapter, int verse)
    {
        _book = book;
        _chapter = chapter;
        _startVerse = verse;
        _endVerse = verse; // For single verse, start and end are the same
    }

    // Constructor for a scripture with a verse range (e.g., Proverbs 3:5-6)
    public ScriptureReference(string book, int chapter, int startVerse, int endVerse)
    {
        _book = book;
        _chapter = chapter;
        _startVerse = startVerse;
        _endVerse = endVerse;
    }

    // Gets the formatted display text for the scripture reference
    public string GetDisplayText()
    {
        if (_startVerse == _endVerse)
        {
            return $"{_book} {_chapter}:{_startVerse}";
        }
        else
        {
            return $"{_book} {_chapter}:{_startVerse}-{_endVerse}";
        }
    }
}

// Word.cs
class Word
{
    private string _text;
    private bool _isHidden;

    // Constructor to initialize a word
    public Word(string text)
    {
        _text = text;
        _isHidden = false; // Initially, words are not hidden
    }

    // Hides the word
    public void Hide()
    {
        _isHidden = true;
    }

    // Shows the word (unhides it) - not used in this program but good for completeness
    public void Show()
    {
        _isHidden = false;
    }

    // Checks if the word is currently hidden
    public bool IsHidden()
    {
        return _isHidden;
    }

    // Gets the display text for the word (either the word itself or underscores)
    public string GetDisplayText()
    {
        if (_isHidden)
        {
            // Replace the word with underscores of the same length
            return new string('_', _text.Length);
        }
        else
        {
            return _text;
        }
    }
}
