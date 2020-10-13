using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;


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
            MoodAnalyserFactory.CreateMoodAnalyserParameterizedObject("MoodAnalyser.MoodAnalyse", "MoodAnalyse", "happy");
            MoodAnalyserFactory.CreateMoodAnalyserDefaultConstructor("MoodAnalyser.MoodAnalyse", "MoodAnalyse");
        }
    }

    public class MoodAnalyse {
        public string message;
        public MoodAnalyse(string message) {
            Console.WriteLine("Parameterized Constructor");
            this.message = message;
        }

        public MoodAnalyse() {
            Console.WriteLine("Default Constructor");
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
            NULL_MESSAGE,
            NO_SUCH_CLASS,
            NO_SUCH_CONSTRUCTOR,
            NO_SUCH_METHOD

        }
        public readonly ExceptionType type;

        public MoodAnalyserCustomExceptions(ExceptionType type, string message) : base(message)
        {
            this.type = type;
        }
    }

    public class MoodAnalyserFactory {

        public static object CreateMoodAnalyserDefaultConstructor(string className, string constructor) {
            Type type = typeof(MoodAnalyse);
            if (type.Name.Equals(className) || type.FullName.Equals(className))
            {
                if (type.Name.Equals(constructor))
                {
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    Type moodAnalyse = assembly.GetType(className);
                    return Activator.CreateInstance(moodAnalyse);
                }
                else
                {
                    throw new MoodAnalyserCustomExceptions(MoodAnalyserCustomExceptions.ExceptionType.NO_SUCH_CONSTRUCTOR, "Constructor not present");
                }
            }
            else {
                throw new MoodAnalyserCustomExceptions(MoodAnalyserCustomExceptions.ExceptionType.NO_SUCH_CLASS, "Class is not present");
            }
        }

        public static object CreateMoodAnalyserParameterizedObject(string className, string constructor, string message)
        {
            Type type = typeof(MoodAnalyse);

            if (type.Name.Equals(className) || type.FullName.Equals(className))
            {
                if (type.Name.Equals(constructor)) 
                {
                    ConstructorInfo construt = type.GetConstructor(new[] { typeof(string) });
                    Object obj = construt.Invoke(new object[] { message });
                    return obj;
                }
                else
                    throw new MoodAnalyserCustomExceptions(MoodAnalyserCustomExceptions.ExceptionType.NO_SUCH_CONSTRUCTOR, "constructor not found");
            }
            else
            {
                throw new MoodAnalyserCustomExceptions(MoodAnalyserCustomExceptions.ExceptionType.NO_SUCH_CLASS, "class not found");
            }
        }
        
    }
}
