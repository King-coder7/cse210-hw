public class ListingActivity : MindfulnessActivity
{
    private List<string> _prompts = new()
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "Who are your personal heroes?"
    };

    public ListingActivity() 
        : base("Listing Activity", "This activity helps you reflect on the good things in your life by listing items.") { }

    public void Run()
    {
        DisplayStartingMessage();
        Random rand = new Random();
        Console.WriteLine(_prompts[rand.Next(_prompts.Count)]);
        ShowSpinner(3);

        int duration = GetDuration();
        DateTime endTime = DateTime.Now.AddSeconds(duration);
        int count = 0;

        Console.WriteLine("Start listing items:");

        while (DateTime.Now < endTime)
        {
            Console.Write("> ");
            Console.ReadLine();
            count++;
        }

        Console.WriteLine($"\nYou listed {count} items!");
        DisplayEndingMessage();
    }
}
