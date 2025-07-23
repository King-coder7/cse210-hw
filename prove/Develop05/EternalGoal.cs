using System;

public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points)
    {
    }

    public override int RecordEvent()
    {
        return _points; // Always awards points.
    }

    public override string GetStatusString()
    {
        return $"[ ] {_name} ({_description})"; // Always shows as incomplete.
    }

    public override string GetStringRepresentation()
    {
        return $"EternalGoal:{_name},{_description},{_points}";
    }
}