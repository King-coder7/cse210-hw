// Program.cs
// Eternal Quest Program
// Exceeded requirements by adding:
// - Leveling system based on score milestones
// - Negative goals that deduct points
// - Console colors for user feedback

using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        GoalManager manager = new GoalManager();
        string choice = "";

        while (choice != "7")
        {
            Console.Clear();
            Console.WriteLine("Eternal Quest Program");
            Console.WriteLine("----------------------");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Show Score");
            Console.WriteLine("7. Quit");
            Console.Write("Select a choice: ");
            choice = Console.ReadLine();

            switch (choice)
            {
                case "1": manager.CreateGoal(); break;
                case "2": manager.DisplayGoals(); break;
                case "3": manager.SaveGoals(); break;
                case "4": manager.LoadGoals(); break;
                case "5": manager.RecordEvent(); break;
                case "6": manager.DisplayScore(); break;
            }
        }
    }
}

abstract class Goal
{
    protected string _name;
    protected string _description;
    protected int _points;

    public abstract void RecordEvent(ref int score);
    public abstract bool IsComplete();
    public abstract string GetStatus();
    public abstract string GetSaveString();
    public virtual string GetName() => _name;
}

class SimpleGoal : Goal
{
    private bool _isComplete = false;

    public SimpleGoal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public override void RecordEvent(ref int score)
    {
        if (!_isComplete)
        {
            _isComplete = true;
            score += _points;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Completed {_name}! +{_points} points");
            Console.ResetColor();
        }
        else
        {
            Console.WriteLine("Goal already completed.");
        }
    }

    public override bool IsComplete() => _isComplete;

    public override string GetStatus() => ($"[{(_isComplete ? "X" : " ")}] {_name} ({_description})");

    public override string GetSaveString() => $"Simple|{_name}|{_description}|{_points}|{_isComplete}";
}

class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public override void RecordEvent(ref int score)
    {
        score += _points;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Recorded {_name}! +{_points} points");
        Console.ResetColor();
    }

    public override bool IsComplete() => false;

    public override string GetStatus() => ($"[∞] {_name} ({_description})");

    public override string GetSaveString() => $"Eternal|{_name}|{_description}|{_points}";
}

class ChecklistGoal : Goal
{
    private int _count;
    private int _target;
    private int _bonus;

    public ChecklistGoal(string name, string description, int points, int target, int bonus)
    {
        _name = name;
        _description = description;
        _points = points;
        _target = target;
        _bonus = bonus;
    }

    public override void RecordEvent(ref int score)
    {
        if (_count < _target)
        {
            _count++;
            score += _points;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Progress on {_name}. +{_points} points");
            Console.ResetColor();

            if (_count == _target)
            {
                score += _bonus;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"Completed {_name}! Bonus +{_bonus} points!");
                Console.ResetColor();
            }
        }
        else
        {
            Console.WriteLine("Goal already completed.");
        }
    }

    public override bool IsComplete() => _count >= _target;

    public override string GetStatus() => ($"[{(_count >= _target ? "X" : " ")}] {_name} ({_description}) -- Completed {_count}/{_target}");

    public override string GetSaveString() => $"Checklist|{_name}|{_description}|{_points}|{_target}|{_bonus}|{_count}";
}

class NegativeGoal : Goal
{
    public NegativeGoal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public override void RecordEvent(ref int score)
    {
        score -= _points;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Oops! {_name} recorded. -{_points} points");
        Console.ResetColor();
    }

    public override bool IsComplete() => false;

    public override string GetStatus() => ($"[!] {_name} ({_description})");

    public override string GetSaveString() => $"Negative|{_name}|{_description}|{_points}";
}

class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;

    public void CreateGoal()
    {
        Console.WriteLine("Select Goal Type:");
        Console.WriteLine("1. Simple");
        Console.WriteLine("2. Eternal");
        Console.WriteLine("3. Checklist");
        Console.WriteLine("4. Negative");
        Console.Write("Choice: ");
        string choice = Console.ReadLine();

        Console.Write("Name: "); string name = Console.ReadLine();
        Console.Write("Description: "); string desc = Console.ReadLine();
        Console.Write("Points: "); int points = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case "1": _goals.Add(new SimpleGoal(name, desc, points)); break;
            case "2": _goals.Add(new EternalGoal(name, desc, points)); break;
            case "3":
                Console.Write("Target Count: "); int target = int.Parse(Console.ReadLine());
                Console.Write("Bonus Points: "); int bonus = int.Parse(Console.ReadLine());
                _goals.Add(new ChecklistGoal(name, desc, points, target, bonus));
                break;
            case "4": _goals.Add(new NegativeGoal(name, desc, points)); break;
        }
    }

    public void DisplayGoals()
    {
        Console.WriteLine("Goals:");
        foreach (Goal g in _goals)
        {
            Console.WriteLine(g.GetStatus());
        }
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }

    public void RecordEvent()
    {
        Console.WriteLine("Select goal to record:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetName()}");
        }
        int index = int.Parse(Console.ReadLine()) - 1;
        _goals[index].RecordEvent(ref _score);
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }

    public void SaveGoals()
    {
        File.WriteAllText("goals.txt", _score + "\n");
        foreach (Goal g in _goals)
        {
            File.AppendAllText("goals.txt", g.GetSaveString() + "\n");
        }
        Console.WriteLine("Goals saved.");
        Console.ReadLine();
    }

    public void LoadGoals()
    {
        string[] lines = File.ReadAllLines("goals.txt");
        _score = int.Parse(lines[0]);
        _goals.Clear();
        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split("|");
            string type = parts[0];
            switch (type)
            {
                case "Simple": _goals.Add(new SimpleGoal(parts[1], parts[2], int.Parse(parts[3])) { }); if (bool.Parse(parts[4])) _goals[_goals.Count - 1].RecordEvent(ref _score); break;
                case "Eternal": _goals.Add(new EternalGoal(parts[1], parts[2], int.Parse(parts[3]))); break;
                case "Checklist":
                    ChecklistGoal cg = new ChecklistGoal(parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[4]), int.Parse(parts[5]));
                    while (cg.IsComplete() == false && int.Parse(parts[6])-- > 0) cg.RecordEvent(ref _score);
                    _goals.Add(cg);
                    break;
                case "Negative": _goals.Add(new NegativeGoal(parts[1], parts[2], int.Parse(parts[3]))); break;
            }
        }
        Console.WriteLine("Goals loaded.");
        Console.ReadLine();
    }

    public void DisplayScore()
    {
        Console.WriteLine($"Current Score: {_score}");
        Console.WriteLine($"Level: {_score / 1000}");
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }
}
