using System;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness Program");
            Console.WriteLine("1. Start Breathing Activity");
            Console.WriteLine("2. Start Reflection Activity");
            Console.WriteLine("3. Start Listing Activity");
            Console.WriteLine("4. Quit");
            Console.Write("Choose an option: ");
            
            string choice = Console.ReadLine();

            if (choice == "1") new BreathingActivity().Run();
            else if (choice == "2") new ReflectionActivity().Run();
            else if (choice == "3") new ListingActivity().Run();
            else if (choice == "4") break;
        }
    }
}
