using System;
using System.Collections.Generic;

// Base class for all activities
public abstract class Activity
{
    private DateTime _date;
    private int _lengthMinutes; // Length of the activity in minutes

    public Activity(DateTime date, int lengthMinutes)
    {
        _date = date;
        _lengthMinutes = lengthMinutes;
    }

    public DateTime GetDate() => _date;
    public int GetLengthMinutes() => _lengthMinutes;

    // Abstract methods to be implemented by derived classes
    public abstract double GetDistance(); // in miles/km
    public abstract double GetSpeed();    // in mph/kph
    public abstract double GetPace();     // in minutes per mile/km

    // Virtual method to provide a summary that can be overridden
    public virtual string GetSummary()
    {
        return $"{_date.ToShortDateString()} ({_lengthMinutes} min)";
    }
}

// Derived class for Running activity
public class Running : Activity
{
    private double _distanceMiles; // Distance in miles

    public Running(DateTime date, int lengthMinutes, double distanceMiles)
        : base(date, lengthMinutes)
    {
        _distanceMiles = distanceMiles;
    }

    public override double GetDistance() => _distanceMiles;

    // Speed (mph) = (distance / minutes) * 60
    public override double GetSpeed()
    {
        if (GetLengthMinutes() == 0) return 0;
        return (_distanceMiles / GetLengthMinutes()) * 60;
    }

    // Pace (min/mile) = minutes / distance
    public override double GetPace()
    {
        if (_distanceMiles == 0) return 0;
        return GetLengthMinutes() / _distanceMiles;
    }

    public override string GetSummary()
    {
        return $"{base.GetSummary()} - Running: {GetDistance():F2} miles, Speed: {GetSpeed():F2} mph, Pace: {GetPace():F2} min/mile";
    }
}

// Derived class for Cycling activity
public class Cycling : Activity
{
    private double _speedMph; // Speed in mph

    public Cycling(DateTime date, int lengthMinutes, double speedMph)
        : base(date, lengthMinutes)
    {
        _speedMph = speedMph;
    }

    // Distance (miles) = (speed * minutes) / 60
    public override double GetDistance()
    {
        return (_speedMph * GetLengthMinutes()) / 60;
    }

    public override double GetSpeed() => _speedMph;

    // Pace (min/mile) = 60 / speed
    public override double GetPace()
    {
        if (_speedMph == 0) return 0;
        return 60 / _speedMph;
    }

    public override string GetSummary()
    {
        return $"{base.GetSummary()} - Cycling: {GetDistance():F2} miles, Speed: {GetSpeed():F2} mph, Pace: {GetPace():F2} min/mile";
    }
}

// Derived class for Swimming activity
public class Swimming : Activity
{
    private int _laps; // Number of laps

    // Assuming 1 lap = 50 meters, and 1 mile = 1609.34 meters
    private const double METERS_PER_LAP = 50;
    private const double METERS_PER_MILE = 1609.34;

    public Swimming(DateTime date, int lengthMinutes, int laps)
        : base(date, lengthMinutes)
    {
        _laps = laps;
    }

    // Distance (miles) = (laps * meters_per_lap) / meters_per_mile
    public override double GetDistance()
    {
        return (_laps * METERS_PER_LAP) / METERS_PER_MILE;
    }

    // Speed (mph) = (distance / minutes) * 60
    public override double GetSpeed()
    {
        if (GetLengthMinutes() == 0) return 0;
        return (GetDistance() / GetLengthMinutes()) * 60;
    }

    // Pace (min/mile) = minutes / distance
    public override double GetPace()
    {
        if (GetDistance() == 0) return 0;
        return GetLengthMinutes() / GetDistance();
    }

    public override string GetSummary()
    {
        return $"{base.GetSummary()} - Swimming: {GetDistance():F2} miles ({_laps} laps), Speed: {GetSpeed():F2} mph, Pace: {GetPace():F2} min/mile";
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create a list of Activity objects (demonstrating polymorphism)
        List<Activity> activities = new List<Activity>
        {
            new Running(new DateTime(2025, 7, 18), 30, 3.0), // 30 min run, 3 miles
            new Cycling(new DateTime(2025, 7, 19), 45, 15.0), // 45 min cycle, 15 mph
            new Swimming(new DateTime(2025, 7, 20), 20, 40), // 20 min swim, 40 laps
            new Running(new DateTime(2025, 7, 21), 60, 6.2), // 60 min run, 6.2 miles (approx 10k)
            new Cycling(new DateTime(2025, 7, 22), 90, 20.0) // 90 min cycle, 20 mph
        };

        Console.WriteLine("--- Exercise Tracker Summary ---");
        foreach (var activity in activities)
        {
            // The GetSummary method is called on the base class reference,
            // but the overridden method in the derived class is executed.
            // This is the core of polymorphism.
            Console.WriteLine(activity.GetSummary());
        }
    }
}
