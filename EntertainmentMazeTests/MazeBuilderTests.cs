using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using EntertainmentMaze.maze;

namespace EntertainmentMazeTests
{
    [TestClass]
    public class MazeBuilderTests
    {
        [TestMethod]
        [DataRow(1, 1)]
        [DataRow(2, 2)]
        [DataRow(10, 10)]
        [DataRow(2, 7)]
        public void MazeBuilder_CreatesMazeWithCorrectInput_Success(int rows, int columns)
        {
            //Arrange
            MazeBuilder mazeBuilder = new MazeBuilder();
            //Act
            Maze actualMaze = mazeBuilder.SetRows(rows).SetColumns(columns).SetPlayer(null).Build();

            //Assert
            Assert.AreEqual<int>(expectedMaze.Rows, actualMaze.Rows);
            Assert.AreEqual<int>(expectedMaze.Columns, actualMaze.Columns);
        }
    }
}
