using System; 

public abstract class Goal
{
    protected string _name;
    protected string _description;
    protected int _points;

    public Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public string GetName() { return _name; }
    public string GetDescription() { return _description; }
    public int GetPoints() { return _points; }

    public abstract int RecordEvent();
    public abstract string GetStatusString();
    public abstract string GetStringRepresentation();
}