using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using EntertainmentMaze.maze;
using EntertainmentMaze.Database;

namespace EntertainmentMazeTests
{
    [TestClass]
    public class MazeBuilderTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            DatabaseListRetrieval.InitializeList();
        }

        Maze playerMaze;
        MazeBuilder mazeBuilder = new MazeBuilder();

        [TestMethod]
        public void CreateMaze_HasCorrectSizeRooms_Success()
        {
            playerMaze = mazeBuilder
                .SetRows(5)
                .SetColumns(5)
                .Build();

            Assert.IsTrue(playerMaze.Rows == 5);
            Assert.IsTrue(playerMaze.Columns == 5);
        }

        [TestMethod]
        public void CreateMaze_UsingMazeBuilder_SomeRoomIsNotNull()
        {
            Player newPlayer = new Player("FirstName", "LastName");
            playerMaze = mazeBuilder
                .SetRows(5)
                .SetColumns(5)
                .SetPlayer(newPlayer)
                .Build();

            Room roomTest = playerMaze.GetLocation();
            Assert.IsNotNull(roomTest);
        }

        [TestMethod]
        public void MazeBuilder_BuildMethod_ReturnsValidMAzeObject()
        {
            Maze mazeTest = mazeBuilder
                .SetRows(3)
                .SetColumns(3)
                .Build();
            Assert.IsNotNull(mazeTest);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MazeBuilder_CannotMakeMaze_WithNullPlayer()
        {
            Player newPlayer = new Player(null, null);
            playerMaze = mazeBuilder
                .SetRows(5)
                .SetColumns(5)
                .SetPlayer(newPlayer)
                .Build();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void MazeBuilder_CannotMakeMaze_WithBadRows()
        {
            Player newPlayer = new Player("FirstName", "LastName");
            playerMaze = mazeBuilder
                .SetRows(0)
                .SetColumns(5)
                .SetPlayer(newPlayer)
                .Build();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void MazeBuilder_CannotMakeMaze_WithBadColumns()
        {
            Player newPlayer = new Player("FirstName", "LastName");
            playerMaze = mazeBuilder
                .SetRows(5)
                .SetColumns(0)
                .SetPlayer(newPlayer)
                .Build();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MazeBuilder_CannotMakeMaze_WithBadCPlayer()
        {
            Player newPlayer = new Player("FirstName", "LastName");
            playerMaze = mazeBuilder
                .SetRows(5)
                .SetColumns(5)
                .SetPlayer(null)
                .Build();
        }
    }
}
