using EntertainmentMaze.maze;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EntertainmentMazeTests
{
    [TestClass]
    public class PlayerTests
    {

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_FirstNameIsNull_ThrowsException()
        {
            //Arrange
            _ = new Player(null, "Kramer");
            //Act

            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_LastNameIsNull_ThrowsException()
        {
            //Arrange
            _ = new Player("Devin", null);
            //Act

            //Assert
        }

        [TestMethod]
        public void NameCheck_InputNameIsValid_Success()
        {
            //Arrange
            Console.SetIn(new StringReader("Devin"));
            string expectedName = "Devin";
            string actualName = Player.NameCheck();
            //Act

            //Assert
            Assert.AreEqual<string>(expectedName, actualName);

        }

        [TestMethod]
        public void NameCheck_InputNameIsNotVaild_Success()
        {
            //Arrange
            Console.SetIn(new StringReader("123"));
            string expectedName = "Devin";
            string actualName = Player.NameCheck();
            //Act

            //Assert
            Assert.AreNotEqual<string>(expectedName, actualName);
        }
    }
}
