using System;

public class ChecklistGoal : Goal
{
    private int _amountCompleted;
    private int _targetAmount;
    private int _bonusPoints;

    public ChecklistGoal(string name, string description, int points, int targetAmount, int bonusPoints, int amountCompleted = 0)
        : base(name, description, points)
    {
        _targetAmount = targetAmount;
        _bonusPoints = bonusPoints;
        _amountCompleted = amountCompleted;
    }

    public override int RecordEvent()
    {
        _amountCompleted++;
        int earnedPoints = _points;
        if (_amountCompleted == _targetAmount)
        {
            earnedPoints += _bonusPoints;
        }
        return earnedPoints;
    }

    public override string GetStatusString()
    {
        return $"[{(IsComplete() ? "X" : " ")}] {_name} ({_description}) -- Currently completed: {_amountCompleted}/{_targetAmount}";
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal:{_name},{_description},{_points},{_targetAmount},{_bonusPoints},{_amountCompleted}";
    }

    public bool IsComplete()
    {
        return _amountCompleted >= _targetAmount;
    }
}