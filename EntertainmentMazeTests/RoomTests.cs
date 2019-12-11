using EntertainmentMaze.Database;
using EntertainmentMaze.maze;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntertainmentMazeTests
{
    [TestClass]
    public class RoomTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Room_BadRowLocations_Fails()
        {
            Room roomTest = new Room(-1, 1, 1, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Room_BadColumnLocations_Fails()
        {
            Room roomTest = new Room(1, -1, 1, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Room_BadTotalRowLocations_Fails()
        {
            Room roomTest = new Room(1, 1, -1, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Room_BadTotalColumnsLocations_Fails()
        {
            Room roomTest = new Room(1, 1, 1, -1);
        }

        [TestMethod]
        public void Room_PlayerIsNotInExitRoom()
        {
            DatabaseListRetrieval.InitializeList();
            Maze playerMaze;
            MazeBuilder mazeBuilder = new MazeBuilder();
            Player newPlayer = new Player("FirstName", "LastName");
            playerMaze = mazeBuilder
                .SetRows(5)
                    .SetColumns(5)
                    .SetPlayer(newPlayer)
                    .Build();

            Assert.AreNotEqual(playerMaze.GetExitLocationOfMaze(), playerMaze.GetLocation());
        }
    }
}
