public class BreathingActivity : MindfulnessActivity
{
    public BreathingActivity() 
        : base("Breathing Activity", "This activity will help you relax by walking you through breathing in and out slowly.") { }

    public void Run()
    {
        DisplayStartingMessage();
        int duration = GetDuration();
        int interval = 5;

        for (int i = 0; i < duration; i += interval * 2)
        {
            Console.Write("Breathe in...");
            Countdown(interval);
            Console.Write("Breathe out...");
            Countdown(interval);
        }

        DisplayEndingMessage();
    }

    private void Countdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write($" {i}");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }
}
