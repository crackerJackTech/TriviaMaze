using EntertainmentMaze.Database;
using EntertainmentMaze.maze;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EntertainmentMazeTests
{
    [TestClass]
    public class PlayerControlTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PlayerControl_MazePassedInIsNull_Fails()
        {
            PlayerControl.MovementAttempt(null,"Direction");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PlayerControl_DirectionPassedInIsNull_Fails()
        {
            DatabaseListRetrieval.InitializeList();
            MazeBuilder mazeBuilder = new MazeBuilder();
            Maze playerMaze = mazeBuilder
                .SetRows(5)
                .SetColumns(5)
                .Build();

            PlayerControl.MovementAttempt(playerMaze, "");
        }

        [TestMethod]
        public void PlayerControl_MovementAttemptGetsGoodInput_Success()
        {
            DatabaseListRetrieval.InitializeList();
            MazeBuilder mazeBuilder = new MazeBuilder();
            Maze playerMaze = mazeBuilder
                .SetRows(5)
                .SetColumns(5)
                .Build();

            PlayerControl.MovementAttempt(playerMaze, "N");

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PlayerControl_QuestionAttempt_NullQuestion_Fails()
        {
            PlayerControl.QuestionAttempt(null, "Answer");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PlayerControl_QuestionAttempt_EmptyStringQuestion_Fails()
        {
            PlayerControl.QuestionAttempt("", "Answer");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PlayerControl_QuestionAttempt_NullAnswer_Fails()
        {
            PlayerControl.QuestionAttempt("question", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PlayerControl_QuestionAttempt_EmptyStringAnswer_Fails()
        {
            PlayerControl.QuestionAttempt("question", "");
        }

        [TestMethod]
        public void PlayerControl_QuestionAttempt_ReturnsTrue_Success()
        {
            Console.SetIn(new StringReader("Correct Answer"));
            Assert.IsTrue(PlayerControl.QuestionAttempt("question", "Correct Answer"));
        }
    }
}
