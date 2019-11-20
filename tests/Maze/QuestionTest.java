package Maze;

import org.junit.Assert;
import org.junit.Test;

import static org.junit.Assert.*;

public class QuestionTest
{
    @Test
    public void MakeQuestionGoodValues_Success()
    {
        Question question = new Question("How many licks does it take to get to the center of the tootsie pop?","The world may never know");
        Assert.assertEquals("How many licks does it take to get to the center of the tootsie pop? The world may never know", question.toString());
    }

    @Test(expected = IllegalArgumentException.class)
    public void MakeQuestionBadValues_Fail()
    {
        Question question = new Question(null, null);
    }

    @Test
    public void MakeQuestionDefault_FromDatabase()
    {
        Question question = new Question();
    }
}