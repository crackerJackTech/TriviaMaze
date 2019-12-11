using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using EntertainmentMaze;
using EntertainmentMaze.Database;
using EntertainmentMaze.maze;

namespace EntertainmentMazeTests
{
    [TestClass]
    public class QuestionTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Question_CannotMakeQuestion_BadQuestionID()
        {
            Question question = new Question(-1, 1, 1, "question", "answer");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Question_CannotMakeQuestion_BadAnswerID()
        {
            Question question = new Question(1, -1, 1, "question", "answer");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Question_CannotMakeQuestion_BadTypeID()
        {
            Question question = new Question(1, 1, -1, "question", "answer");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Question_CannotMakeQuestion_BadQuestionString()
        {
            Question question = new Question(1, 1, 1, null, "answer");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Question_CannotMakeQuestion_BadAnswerString()
        {
            Question question = new Question(1, 1, 1, "question", null);
        }
    }
}
