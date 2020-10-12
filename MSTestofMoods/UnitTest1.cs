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
            string actual = analyse.analyseMood();
            //Assert
            Assert.AreEqual("sad", actual);
        }
        public void TestCase2() {
            //Arrange
            analyse = new MoodAnalyser.MoodAnalyse("happy");
            //Act
            string actual = analyse.analyseMood();
            //Assert
            Assert.AreEqual("happy", actual);
        }
    }
}
