using EntertainmentMaze.Database;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace EntertainmentMaze.maze
{
    [Serializable]
    public class Question
    {

        private int QuestionID { get; }
        private int AnswerID { get; }
        private int TypeID { get; }
        public string CurrentQuestion { get; }
        public string Answer { get; }

        

        public Question(int questionID, int answerID, int typeID, string question, string answer)
        {
            if (questionID < 0) { throw new ArgumentOutOfRangeException(nameof(questionID)); }
            if (answerID < 0) { throw new ArgumentOutOfRangeException(nameof(answerID)); }
            if (typeID < 0) { throw new ArgumentOutOfRangeException(nameof(typeID)); }
            if (question is null || question == "") { throw new ArgumentNullException(nameof(question)); }
            if (answer is null || answer == "") { throw new ArgumentNullException(nameof(answer)); }

            this.QuestionID = questionID;
            this.AnswerID = answerID;
            this.TypeID = typeID;
            this.CurrentQuestion = question ?? throw new ArgumentNullException(nameof(question));
            this.Answer = answer ?? throw new ArgumentNullException(nameof(answer));
        }



        public override string ToString()
        {
            return CurrentQuestion;
        }



    }
}
