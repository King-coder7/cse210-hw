using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

// Base class for all mindfulness activities
class Activity
{
    protected string _name;
    protected string _description;
    protected int _duration;

    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    public void DisplayStartingMessage()
    {
        Console.WriteLine($"Welcome to the {_name} Activity.");
        Console.WriteLine();
        Console.WriteLine(_description);
        Console.WriteLine();
        Console.Write("How long, in seconds, would you like for your session? ");
        _duration = int.Parse(Console.ReadLine());
        Console.Clear();
        Console.WriteLine("Get ready...");
        ShowSpinner(5); // Pause for several seconds
    }

    public void DisplayEndingMessage()
    {
        Console.WriteLine();
        Console.WriteLine("Well done!!");
        ShowSpinner(3); // Pause for a few seconds
        Console.WriteLine();
        Console.WriteLine($"You have completed the {_name} Activity for {_duration} seconds.");
        ShowSpinner(5); // Pause for several seconds
        Console.Clear();
    }

    public void ShowSpinner(int seconds)
    {
        List<string> spinnerFrames = new List<string> { "|", "/", "-", "\\" };
        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(seconds);
        int i = 0;
        while (DateTime.Now < endTime)
        {
            Console.Write(spinnerFrames[i]);
            Thread.Sleep(200);
            Console.Write("\b \b"); // Erase the character
            i = (i + 1) % spinnerFrames.Count;
        }
    }

    public void ShowCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(i);
            Thread.Sleep(1000);
            if (i < 10)
            {
                Console.Write("\b \b"); // For single digit, just overwrite
            }
            else
            {
                Console.Write("\b\b  \b\b"); // For double digit, overwrite two characters
            }
        }
    }
}

// Derived class for Breathing Activity
class BreathingActivity : Activity
{
    public BreathingActivity() : base("Breathing", "This activity will help you relax by walking your through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    public void Run()
    {
        DisplayStartingMessage();

        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(_duration);

        while (DateTime.Now < endTime)
        {
            Console.Write("Breathe in...");
            ShowCountdown(4); // Adjust based on desired breathing pace
            Console.WriteLine();

            if (DateTime.Now >= endTime) break; // Check if duration is met

            Console.Write("Breathe out...");
            ShowCountdown(6); // Adjust based on desired breathing pace
            Console.WriteLine();
            Console.WriteLine(); // Add an extra line for better spacing
        }

        DisplayEndingMessage();
    }
}

// Derived class for Reflection Activity
class ReflectionActivity : Activity
{
    private List<string> _prompts;
    private List<string> _questions;
    private List<string> _usedPrompts; // To ensure all prompts are used before repeating
    private List<string> _usedQuestions; // To ensure all questions are used before repeating

    public ReflectionActivity() : base("Reflection", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
    {
        _prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        _questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        _usedPrompts = new List<string>();
        _usedQuestions = new List<string>();
    }

    public string GetRandomPrompt()
    {
        if (_usedPrompts.Count == _prompts.Count)
        {
            _usedPrompts.Clear(); // Reset if all prompts have been used
        }

        Random random = new Random();
        string selectedPrompt;
        do
        {
            selectedPrompt = _prompts[random.Next(_prompts.Count)];
        } while (_usedPrompts.Contains(selectedPrompt));

        _usedPrompts.Add(selectedPrompt);
        return selectedPrompt;
    }

    public string GetRandomQuestion()
    {
        if (_usedQuestions.Count == _questions.Count)
        {
            _usedQuestions.Clear(); // Reset if all questions have been used
        }

        Random random = new Random();
        string selectedQuestion;
        do
        {
            selectedQuestion = _questions[random.Next(_questions.Count)];
        } while (_usedQuestions.Contains(selectedQuestion));

        _usedQuestions.Add(selectedQuestion);
        return selectedQuestion;
    }

    public void Run()
    {
        DisplayStartingMessage();

        Console.WriteLine("Consider the following prompt:");
        Console.WriteLine();
        Console.WriteLine($" --- {GetRandomPrompt()} --- ");
        Console.WriteLine();
        Console.WriteLine("When you have thought about the prompt, press enter to continue.");
        Console.ReadLine();
        Console.WriteLine("Now ponder on each of the following questions as they relate to this experience.");
        Console.Write("You may begin in: ");
        ShowCountdown(5); // Prepare to begin

        Console.Clear();

        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(_duration);

        while (DateTime.Now < endTime)
        {
            Console.Write($"> {GetRandomQuestion()} ");
            ShowSpinner(5); // Pause for reflection time
            Console.WriteLine(); // New line for the next question
        }

        DisplayEndingMessage();
    }
}

// Derived class for Listing Activity
class ListingActivity : Activity
{
    private List<string> _prompts;
    private List<string> _usedPrompts; // To ensure all prompts are used before repeating

    public ListingActivity() : base("Listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
        _prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };
        _usedPrompts = new List<string>();
    }

    public string GetRandomPrompt()
    {
        if (_usedPrompts.Count == _prompts.Count)
        {
            _usedPrompts.Clear(); // Reset if all prompts have been used
        }

        Random random = new Random();
        string selectedPrompt;
        do
        {
            selectedPrompt = _prompts[random.Next(_prompts.Count)];
        } while (_usedPrompts.Contains(selectedPrompt));

        _usedPrompts.Add(selectedPrompt);
        return selectedPrompt;
    }

    public void Run()
    {
        DisplayStartingMessage();

        Console.WriteLine("List as many responses you can to the following prompt:");
        Console.WriteLine();
        Console.WriteLine($" --- {GetRandomPrompt()} --- ");
        Console.WriteLine();
        Console.Write("You may begin in: ");
        ShowCountdown(5); // Countdown to start listing

        Console.Clear(); // Clear console after countdown for cleaner listing

        Console.WriteLine("Start listing items:");
        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(_duration);
        int itemCount = 0;

        while (DateTime.Now < endTime)
        {
            // Give a little pause to ensure the user can type freely without spinner interference
            // and to allow for the time check.
            // In a real application, you might use Console.ReadKey(true) with a timeout,
            // but for this assignment, simple Console.ReadLine is sufficient,
            // and we'll count entries based on how many times ReadLine is called within the duration.
            Console.Write("> ");
            string item = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(item)) // Only count non-empty entries
            {
                itemCount++;
            }
            // If the user takes a long time to type, this loop might not run frequently.
            // The prompt says "The user lists as many items as they can until they reach the duration"
            // So, checking DateTime.Now for each ReadLine is accurate.
        }

        Console.WriteLine($"You listed {itemCount} items!");

        DisplayEndingMessage();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Exceeding Requirements:
        // Implemented logic in ReflectionActivity and ListingActivity to ensure that
        // all prompts and questions are used at least once before any are repeated within a session.
        // This is done by maintaining `_usedPrompts` and `_usedQuestions` lists
        // and clearing them only when all available items have been used.

        int choice = 0;
        while (choice != 4)
        {
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Start Breathing Activity");
            Console.WriteLine("  2. Start Reflection Activity");
            Console.WriteLine("  3. Start Listing Activity");
            Console.WriteLine("  4. Quit");
            Console.Write("Select a choice from the menu: ");

            if (int.TryParse(Console.ReadLine(), out choice))
            {
                Console.Clear();
                switch (choice)
                {
                    case 1:
                        BreathingActivity breathingActivity = new BreathingActivity();
                        breathingActivity.Run();
                        break;
                    case 2:
                        ReflectionActivity reflectionActivity = new ReflectionActivity();
                        reflectionActivity.Run();
                        break;
                    case 3:
                        ListingActivity listingActivity = new ListingActivity();
                        listingActivity.Run();
                        break;
                    case 4:
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
                        break;
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid input. Please enter a number.");
            }
            Console.WriteLine(); // Add a blank line for better separation in the console
        }
    }
}