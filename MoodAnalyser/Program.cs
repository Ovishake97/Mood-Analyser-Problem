using System;

namespace MoodAnalyser
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hey! How's your mood today?");
            string message = Console.ReadLine();
            MoodAnalyse moodAnalyser = new MoodAnalyse(message);
            Console.WriteLine($"Looks like you are " +moodAnalyser.analyseMood());
        }
    }

    public class MoodAnalyse {
        public string message;
        public MoodAnalyse(string message) {
            this.message = message;
        }
        public string analyseMood() {
            if (message.ToLower().Contains("sad"))
            {
                return "sad";
            }
            else {
                return "happy";
            }
        }
    }
}
