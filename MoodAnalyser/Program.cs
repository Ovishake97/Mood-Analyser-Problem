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
           Console.WriteLine($"Looks like you are " +moodAnalyser.AnalyseMood());
        }
    }

    public class MoodAnalyse {
        public string message;
        public MoodAnalyse(string message) {
            this.message = message;
        }
        public string AnalyseMood() {
            try
            {
                if (message.ToLower().Contains("sad"))
                {
                    return "sad";
                }
                else
                {
                   return "happy";
                }

            }
            catch (NullReferenceException e) {
                return "happy";


            }
           
        }
    }
}
