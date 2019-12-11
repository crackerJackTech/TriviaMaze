using EntertainmentMaze.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Runtime.Serialization;

namespace EntertainmentMaze.maze
{
    [DataContract]
    public class Door
    {
        [DataMember]
        private Question Question { get; set; }
        [DataMember]
        private bool IsLocked { get; set; }
        [DataMember]
        private bool IsOpened { get; set; }
        [DataMember]
        private string QuestionString { get; set; }
        [DataMember]
        private string AnswerString { get; set; }

        public static Door CreateDoor() => new Door();
        public Question GetQuestion() => Question;
        public bool GetDoorStatus() => IsLocked;
        public bool GetDoorOpenedStatus() => IsOpened;
        public string GetQuestionString() => QuestionString;
        public string GetAnswerString() => AnswerString;

        public Door()
        {
            Question = GetRandomQuestion();
            QuestionString = Question.CurrentQuestion;
            AnswerString = Question.Answer;
            IsLocked = false;
        }

        private Question GetRandomQuestion()
        {
            Random rnd = new Random();
            List<Question> listOfQuestions = DatabaseListRetrieval.GetListOfQuestions();
            int r = rnd.Next(listOfQuestions.Count);
            Question question = listOfQuestions.ElementAt(r);

            return question;
        }



        public void OpenDoor()
        {
            IsOpened = true;
        }
        public void LockDoor()
        {
            IsLocked = true;
        }

        public void UnlockDoor()
        {
            IsLocked = false;
        }

        public string DisplayQuestion()
        {
            return ($"{Question.CurrentQuestion.ToString()}");
        }

        public string DisplayAnswer()
        {
            return ($"{Question.Answer.ToString()}");
        }
    }
}
