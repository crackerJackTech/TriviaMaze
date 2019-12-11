using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using EntertainmentMaze.maze;
using EntertainmentMaze.Database;

namespace EntertainmentMazeTests
{
    [TestClass]
    public class DoorTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            DatabaseListRetrieval.InitializeList();
        }
      
        [TestMethod]
        public void Constructor_CreatesDoorWithValidInput_Success()
        {
            //Arrange
            var testDoor = Door.CreateDoor();
            //Act
            var testQuestion = testDoor.DisplayQuestion();
            var testAnswer = testDoor.DisplayAnswer();
            var expectedToBeLocked = false;
            //Assert
            Assert.IsTrue(testQuestion.Length > 0);
            Assert.IsTrue(testAnswer.Length > 0);
            Assert.AreEqual<bool>(expectedToBeLocked, testDoor.GetDoorStatus());
        }

        [TestMethod]
        public void DisplayQuestion_QuestionIsValidFormat_Success()
        {
            //Arrange
            var testDoor = Door.CreateDoor();
            //Act
            var expectedResult = testDoor.GetQuestionString();
            //Assert
            Assert.AreEqual<string>(expectedResult, testDoor.GetQuestion().ToString());
        }
      
        [TestMethod]
        public void DisplayAnswer_AnswerIsValidFormat_Success()
        {
            //Arrange
            var testDoor = Door.CreateDoor();
            //Act
            var expectedResult = testDoor.GetAnswerString();
            //Assert
            Assert.AreEqual<string>(expectedResult, testDoor.GetAnswerString().ToString());
        }

    }
}
