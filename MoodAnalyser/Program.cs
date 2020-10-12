using System;
using System.Collections.Generic;


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
                if (this.message.Equals(string.Empty)) {
                    throw new MoodAnalyserCustomExceptions(MoodAnalyserCustomExceptions.ExceptionType.EMPTY_MESSAGE, "Message should not be empty");
                }
                if (message.ToLower().Contains("sad"))
                {
                    return "sad";
                }
                else
                {
                   return "happy";
                }

            }
            catch (NullReferenceException) {
                throw new MoodAnalyserCustomExceptions(MoodAnalyserCustomExceptions.ExceptionType.NULL_MESSAGE, "Mood should not be null");

            }
        }
    }

    public class MoodAnalyserCustomExceptions :Exception {

        public enum ExceptionType
        {
            EMPTY_MESSAGE,
            NULL_MESSAGE
        }
        public readonly ExceptionType type;

        public MoodAnalyserCustomExceptions(ExceptionType type, string message) : base(message)
        {
            this.type = type;
        }
    }
}
