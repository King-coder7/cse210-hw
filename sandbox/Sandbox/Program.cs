// This program exceeds requirements by:
// - Including a Mood tracker for each journal entry
// - Saving and loading using JSON instead of plain text
// - Allowing users to search for entries by mood

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;

// Entry class with mood
public class Entry
{
    public string Date { get; set; }
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Mood { get; set; }

    public Entry(string prompt, string response, string mood)
    {
        Date = DateTime.Now.ToString("yyyy-MM-dd");
        Prompt = prompt;
        Response = response;
        Mood = mood;
    }

    public override string ToString()
    {
        return $"Date: {Date}\nMood: {Mood}\nPrompt: {Prompt}\nResponse: {Response}\n";
    }
}

// Journal class with JSON saving/loading and mood search
public class Journal
{
    private List<Entry> _entries = new List<Entry>();

    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    public void DisplayEntries()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("No journal entries found.");
            return;
        }

        foreach (var entry in _entries)
        {
            Console.WriteLine(entry.ToString());
        }
    }

    public void SaveToJson(string filename)
    {
        string json = JsonSerializer.Serialize(_entries, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filename, json);
        Console.WriteLine($"Journal saved to {filename}");
    }

    public void LoadFromJson(string filename)
    {
        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            return;
        }

        string json = File.ReadAllText(filename);
        _entries = JsonSerializer.Deserialize<List<Entry>>(json);
        Console.WriteLine($"Journal loaded from {filename}");
    }

    public void SearchByMood(string mood)
    {
        var filtered = _entries.Where(e => e.Mood.Equals(mood, StringComparison.OrdinalIgnoreCase)).ToList();
        if (filtered.Count == 0)
        {
            Console.WriteLine($"No entries found for mood: {mood}");
            return;
        }

        Console.WriteLine($"\nEntries with mood '{mood}':");
        foreach (var entry in filtered)
        {
            Console.WriteLine(entry.ToString());
        }
    }
}

// Prompt generator
public class PromptGenerator
{
    private List<string> _prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    private Random _random = new Random();

    public string GetRandomPrompt()
    {
        int index = _random.Next(_prompts.Count);
        return _prompts[index];
    }
}

// MAIN PROGRAM
class Program
{
    static void Main()
    {
        Journal journal = new Journal();
        PromptGenerator generator = new PromptGenerator();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\nJournal Menu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display all entries");
            Console.WriteLine("3. Save journal to file (JSON)");
            Console.WriteLine("4. Load journal from file (JSON)");
            Console.WriteLine("5. Search entries by mood");
            Console.WriteLine("6. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    string prompt = generator.GetRandomPrompt();
                    Console.WriteLine($"\nPrompt: {prompt}");
                    Console.Write("Your response: ");
                    string response = Console.ReadLine();
                    Console.Write("Mood (e.g., Happy, Sad, Grateful, Stressed): ");
                    string mood = Console.ReadLine();
                    journal.AddEntry(new Entry(prompt, response, mood));
                    break;

                case "2":
                    journal.DisplayEntries();
                    break;

                case "3":
                    Console.Write("Enter filename to save (e.g., journal.json): ");
                    string saveFile = Console.ReadLine();
                    journal.SaveToJson(saveFile);
                    break;

                case "4":
                    Console.Write("Enter filename to load (e.g., journal.json): ");
                    string loadFile = Console.ReadLine();
                    journal.LoadFromJson(loadFile);
                    break;

                case "5":
                    Console.Write("Enter mood to search for: ");
                    string searchMood = Console.ReadLine();
                    journal.SearchByMood(searchMood);
                    break;

                case "6":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }
}
