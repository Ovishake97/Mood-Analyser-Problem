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
           Console.WriteLine(MoodAnalyserFactory.SetField("Happy", "message"));
        }
    }
    
    public class MoodAnalyse {
        public string message;
        //Parameterised constructor of the class
        public MoodAnalyse(string message) {
            Console.WriteLine("Parameterized Constructor");
            this.message = message;
        }
        //Default constructor of the class
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

    //Declaring custom exceptions

    public class MoodAnalyserCustomExceptions :Exception {

        public enum ExceptionType
        {
            EMPTY_MESSAGE,
            NULL_MESSAGE,
            NO_SUCH_CLASS,
            NO_SUCH_CONSTRUCTOR,
            NO_SUCH_METHOD,
            NO_SUCH_FIELD

        }
        public readonly ExceptionType type;

        public MoodAnalyserCustomExceptions(ExceptionType type, string message) : base(message)
        {
            this.type = type;
        }
    }


    //Implementating Reflection
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
        public static string InvokeAnalyserMethod(string message, string methodName)
        {
            try
            {
                Type type = Type.GetType("MoodAnalyser.MoodAnalyse");
                MethodInfo methodInfo = type.GetMethod(methodName);
                object method = CreateMoodAnalyserParameterizedObject("MoodAnalyser.MoodAnalyse", "MoodAnalyse", "Happy");
                object info = methodInfo.Invoke(method, null);
                return info.ToString();
            }
            catch (NullReferenceException)
            {
                throw new MoodAnalyserCustomExceptions(MoodAnalyserCustomExceptions.ExceptionType.NO_SUCH_METHOD, "Method not present");
            }
        }

        public static string SetField(string message, string fieldName) {
            try {
                if (message == null) {
                    throw new MoodAnalyserCustomExceptions(MoodAnalyserCustomExceptions.ExceptionType.EMPTY_MESSAGE, "No message found");
                }
                MoodAnalyse moodAnalyse = new MoodAnalyse();
                Type type = typeof(MoodAnalyse);
                FieldInfo fieldInfo = type.GetField(fieldName, BindingFlags.Public|BindingFlags.Instance);
                fieldInfo.SetValue(moodAnalyse,message);
                return moodAnalyse.message;

            }
            catch (NullReferenceException) {
                throw new MoodAnalyserCustomExceptions(MoodAnalyserCustomExceptions.ExceptionType.NO_SUCH_FIELD, "Field not found");

            }
        }
    }
}
