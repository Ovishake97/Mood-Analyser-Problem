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
    }
}
