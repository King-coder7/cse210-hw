using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class GoalManager
{
    private List<Goal> _goals;
    private int _score;

    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
    }

    public int GetScore()
    {
        return _score;
    }

    public void AddGoal(Goal goal)
    {
        _goals.Add(goal);
    }

    public void DisplayGoals()
    {
        if (!_goals.Any())
        {
            Console.WriteLine("No goals to display. Start by creating a new goal!");
            return;
        }

        Console.WriteLine("\nYour Goals:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetStatusString()}");
        }
    }

    public void RecordEvent()
    {
        DisplayGoals();
        if (!_goals.Any())
        {
            Console.WriteLine("You need to create goals before you can record an event.");
            return;
        }

        Console.Write("Which goal did you accomplish? ");
        if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= _goals.Count)
        {
            int index = choice - 1;
            Goal selectedGoal = _goals[index];
            
            if (selectedGoal is SimpleGoal simpleGoal && simpleGoal.IsComplete())
            {
                Console.WriteLine("This simple goal has already been completed.");
                return;
            }

            int pointsEarned = selectedGoal.RecordEvent();
            _score += pointsEarned;
            Console.WriteLine($"Congratulations! You have earned {pointsEarned} points!");
            Console.WriteLine($"You now have {_score} points.\n");
        }
        else
        {
            Console.WriteLine("Invalid goal selection.");
        }
    }

    public void SaveGoals(string filename)
    {
        try
        {
            using (StreamWriter outputFile = new StreamWriter(filename))
            {
                outputFile.WriteLine(_score);
                foreach (Goal goal in _goals)
                {
                    outputFile.WriteLine(goal.GetStringRepresentation());
                }
            }
            Console.WriteLine($"Goals saved to {filename}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving goals: {ex.Message}");
        }
    }

    public void LoadGoals(string filename)
    {
        if (!File.Exists(filename))
        {
            Console.WriteLine("No saved goals found. Starting with a new quest!");
            _score = 0;
            _goals.Clear();
            return;
        }

        try
        {
            string[] lines = File.ReadAllLines(filename);
            if (lines.Length > 0 && int.TryParse(lines[0], out int loadedScore))
            {
                _score = loadedScore;
            } else {
                _score = 0;
            }

            _goals.Clear();
            for (int i = 1; i < lines.Length; i++) 
            {
                string line = lines[i];
                string[] parts = line.Split(':');
                if (parts.Length < 2) continue;

                string goalType = parts[0];
                string[] details = parts[1].Split(',');

                switch (goalType)
                {
                    case "SimpleGoal":
                        if (details.Length >= 4)
                            AddGoal(new SimpleGoal(details[0], details[1], int.Parse(details[2]), bool.Parse(details[3])));
                        break;
                    case "EternalGoal":
                        if (details.Length >= 3)
                            AddGoal(new EternalGoal(details[0], details[1], int.Parse(details[2])));
                        break;
                    case "ChecklistGoal":
                        if (details.Length >= 6)
                            AddGoal(new ChecklistGoal(details[0], details[1], int.Parse(details[2]), int.Parse(details[3]), int.Parse(details[4]), int.Parse(details[5])));
                        break;
                    default:
                        Console.WriteLine($"Unknown goal type '{goalType}' encountered during loading. Skipping line.");
                        break;
                }
            }
            Console.WriteLine($"Goals loaded from {filename}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading goals: {ex.Message}");
            _score = 0;
            _goals.Clear();
        }
    }
}