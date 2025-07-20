using System;
using System.Collections.Generic;

// Base class for all events
public class Event
{
    // Private fields for encapsulation
    private string _title;
    private string _description;
    private DateTime _date;
    private TimeSpan _time;
    private Address _address;

    // Constructor to initialize common event properties
    public Event(string title, string description, DateTime date, TimeSpan time, Address address)
    {
        _title = title;
        _description = description;
        _date = date;
        _time = time;
        _address = address;
    }

    // Public accessors (getters) for event details
    public string GetTitle() => _title;
    public string GetDescription() => _description;
    public DateTime GetDate() => _date;
    public TimeSpan GetTime() => _time;
    public Address GetAddress() => _address;

    // Method to get standard event details
    public string GetStandardDetails()
    {
        return $"Title: {_title}\nDescription: {_description}\nDate: {_date.ToShortDateString()}\nTime: {_time}\nAddress:\n{_address.GetFullAddress()}";
    }

    // Virtual method to be overridden by derived classes for full details
    public virtual string GetFullDetails()
    {
        return GetStandardDetails(); // Base implementation just returns standard details
    }

    // Method to get a short description of the event
    public string GetShortDescription()
    {
        return $"Type: Generic Event\nTitle: {_title}\nDate: {_date.ToShortDateString()}";
    }
}

// Derived class for Lectures
public class Lecture : Event
{
    private string _speaker;
    private int _capacity;

    public Lecture(string title, string description, DateTime date, TimeSpan time, Address address, string speaker, int capacity)
        : base(title, description, date, time, address)
    {
        _speaker = speaker;
        _capacity = capacity;
    }

    // Override GetFullDetails to include lecture-specific information
    public override string GetFullDetails()
    {
        return $"{base.GetFullDetails()}\nSpeaker: {_speaker}\nCapacity: {_capacity} attendees";
    }

    // Override GetShortDescription for Lecture type
    public new string GetShortDescription()
    {
        return $"Type: Lecture\nTitle: {GetTitle()}\nDate: {GetDate().ToShortDateString()}";
    }
}

// Derived class for Receptions
public class Reception : Event
{
    private string _rsvpEmail;

    public Reception(string title, string description, DateTime date, TimeSpan time, Address address, string rsvpEmail)
        : base(title, description, date, time, address)
    {
        _rsvpEmail = rsvpEmail;
    }

    // Override GetFullDetails to include reception-specific information
    public override string GetFullDetails()
    {
        return $"{base.GetFullDetails()}\nRSVP Email: {_rsvpEmail}";
    }

    // Override GetShortDescription for Reception type
    public new string GetShortDescription()
    {
        return $"Type: Reception\nTitle: {GetTitle()}\nDate: {GetDate().ToShortDateString()}";
    }
}

// Derived class for Outdoor Gatherings
public class OutdoorGathering : Event
{
    private string _weatherForecast;

    public OutdoorGathering(string title, string description, DateTime date, TimeSpan time, Address address, string weatherForecast)
        : base(title, description, date, time, address)
    {
        _weatherForecast = weatherForecast;
    }

    // Override GetFullDetails to include outdoor gathering-specific information
    public override string GetFullDetails()
    {
        return $"{base.GetFullDetails()}\nWeather Forecast: {_weatherForecast}";
    }

    // Override GetShortDescription for Outdoor Gathering type
    public new string GetShortDescription()
    {
        return $"Type: Outdoor Gathering\nTitle: {GetTitle()}\nDate: {GetDate().ToShortDateString()}";
    }
}

// Class to represent an Address
public class Address
{
    private string _street;
    private string _city;
    private string _stateProvince;
    private string _zipPostalCode;
    private string _country;

    public Address(string street, string city, string stateProvince, string zipPostalCode, string country)
    {
        _street = street;
        _city = city;
        _stateProvince = stateProvince;
        _zipPostalCode = zipPostalCode;
        _country = country;
    }

    public string GetFullAddress()
    {
        return $"{_street}\n{_city}, {_stateProvince} {_zipPostalCode}\n{_country}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create addresses for events
        Address address1 = new Address("123 University Ave", "Provo", "UT", "84604", "USA");
        Address address2 = new Address("456 Grand Blvd", "Salt Lake City", "UT", "84101", "USA");
        Address address3 = new Address("789 Park Lane", "Orem", "UT", "84058", "USA");

        // Create different types of events
        Lecture lecture = new Lecture(
            "Introduction to C# Programming",
            "A beginner-friendly lecture on the fundamentals of C#.",
            new DateTime(2025, 9, 15),
            new TimeSpan(10, 0, 0),
            address1,
            "Dr. Jane Smith",
            150
        );

        Reception reception = new Reception(
            "Annual Alumni Mixer",
            "An evening reception for alumni to network and socialize.",
            new DateTime(2025, 10, 20),
            new TimeSpan(18, 30, 0),
            address2,
            "alumni@example.com"
        );

        OutdoorGathering outdoorGathering = new OutdoorGathering(
            "Community BBQ & Games",
            "A fun outdoor event with food, games, and music for the whole family.",
            new DateTime(2025, 7, 25),
            new TimeSpan(14, 0, 0),
            address3,
            "Sunny with a high of 85°F"
        );

        // Put all events in a list of the base type
        List<Event> events = new List<Event>
        {
            lecture,
            reception,
            outdoorGathering
        };

        Console.WriteLine("--- Event Details ---");
        foreach (var ev in events)
        {
            Console.WriteLine("\nStandard Details:");
            Console.WriteLine(ev.GetStandardDetails()); // Demonstrates base class method

            Console.WriteLine("\nFull Details:");
            Console.WriteLine(ev.GetFullDetails()); // Demonstrates overridden method (polymorphism in action here too!)

            Console.WriteLine("\nShort Description:");
            Console.WriteLine(ev.GetShortDescription()); // Demonstrates new method (hiding base method)
            Console.WriteLine("------------------------------------------");
        }
    }
}
