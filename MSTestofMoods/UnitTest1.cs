using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoodAnalyser;
using System.Security.Cryptography.X509Certificates;

namespace MSTestofMoods
{
    [TestClass]
   
    public class UnitTest1
    {
        //Initialising mood analyser object

        MoodAnalyser.MoodAnalyse analyse = null;
        [TestInitialize]
        public void SetUp() {
            
            analyse = new MoodAnalyser.MoodAnalyse("sad");
        }
        [TestMethod]
        public void TestCase1()
        {
            //Act 
            string actual = analyse.AnalyseMood();
            //Assert
            Assert.AreEqual("sad", actual);
        }
        [TestMethod]
        public void TestCase2() {
            //Arrange
            analyse = new MoodAnalyser.MoodAnalyse("happy");
            //Act
            string actual = analyse.AnalyseMood();
            //Assert
            Assert.AreEqual("happy", actual);
        }
        [TestMethod]
        public void GivenNullShowingHappy() {
            analyse = new MoodAnalyser.MoodAnalyse(" ");
            string actual = analyse.AnalyseMood();
            Assert.AreEqual("happy", actual);
        }
        [TestMethod]
        public void GivenNullCustomExceptionReturnNull() {

            string actual = " ";
            try
            {
                //Act
                actual = analyse.AnalyseMood();
            }
            catch (MoodAnalyserCustomExceptions execption)
            {
                //Assert
                Assert.AreEqual("Mood should not be null", execption.Message);
            }
        }
        [TestMethod]
        public void GivenEmptyCustomExceptionReturn() {
            string message = "";
            try
            {
                MoodAnalyser.MoodAnalyse mood = new MoodAnalyser.MoodAnalyse(message);
                //Act
                mood.AnalyseMood();
            }
            catch (MoodAnalyserCustomExceptions execption)
            {
                //Assert
                Assert.AreEqual("Message should not be empty", execption.Message);
            }
        }
        [TestMethod]
        public void GivenMoodAnalyserClassReturnMoodAnalyserObject_DefaultConstructor() {

            object expected = new MoodAnalyse();
            object actual = MoodAnalyserFactory.CreateMoodAnalyserDefaultConstructor("MoodAnalyser.MoodAnalyse", "MoodAnalyse");
            expected.Equals(actual); 
        }
        [TestMethod]
        public void GivenWrongClassReturnCustomException_DefaultConstructor() {
            try
            {
                object actual = MoodAnalyserFactory.CreateMoodAnalyserDefaultConstructor("MoodAnalyser.MoodAnalyser", "MoodAnalyse");
            }
            catch (MoodAnalyserCustomExceptions exceptions) {
                Assert.AreEqual("Class is not present", exceptions.Message);
            }
        }
        [TestMethod]
        public void GivenWrongConstructorReturnCustomException_DefaultConstructor()
        {
            try
            {
                object actual = MoodAnalyserFactory.CreateMoodAnalyserDefaultConstructor("MoodAnalyser.MoodAnalyse", "Mood");
            }
            catch (MoodAnalyserCustomExceptions exceptions)
            {
                Assert.AreEqual("Constructor not present", exceptions.Message);
            }
        }

        [TestMethod]
        public void GivenMoodAnalyseClassReturnMoodAnalyseObject_ParameterizedConstructor()
        {
            var obj = new MoodAnalyse("happy");
            object result = MoodAnalyserFactory.CreateMoodAnalyserParameterizedObject("MoodAnalyser.MoodAnalyse", "MoodAnalyse", "happy");
            obj.Equals(result);
        }
        [TestMethod]
        public void GivenWrongClassReturnCustomException_ParameterizedConstructor()
        {
            try
            {
                object actual = MoodAnalyserFactory.CreateMoodAnalyserParameterizedObject("MoodAnalyser.MoodAnalyser", "MoodAnalyse","happy");
            }
            catch (MoodAnalyserCustomExceptions exceptions)
            {
                Assert.AreEqual("class not found", exceptions.Message);
            }
        }
        [TestMethod]
        public void GivenWrongConstructorReturnCustomException_ParameterizedConstructor()
        {
            try
            {
                object actual = MoodAnalyserFactory.CreateMoodAnalyserParameterizedObject("MoodAnalyser.MoodAnalyse", "Mood", "happy");
            }
            catch (MoodAnalyserCustomExceptions exceptions)
            {
                Assert.AreEqual("constructor not found", exceptions.Message);
            }
        }

        [TestMethod]
        public void GivenHappyShouldInvokeHappy()
        {
            string expected = "happy";
            string actual = MoodAnalyser.MoodAnalyserFactory.InvokeAnalyserMethod("happy", "AnalyseMood");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenImproperMethodNameShouldReturnCustomException() {
            string expected = "happy";
            try {
                string actual = MoodAnalyser.MoodAnalyserFactory.InvokeAnalyserMethod(expected, "MoodAnalyse");
            }
            catch (MoodAnalyserCustomExceptions exceptions) {
                Assert.AreEqual("Method not present", exceptions.Message);
                
            }
        }
    }
}
