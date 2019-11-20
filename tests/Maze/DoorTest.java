package Maze;

import org.junit.Assert;
import org.junit.Test;

import static org.junit.Assert.*;

public class DoorTest
{
    @Test
    public void MakeDoorGoodValues_Success()
    {
        Question question = new Question("Question", "Answer");
        Door door = new Door(question, false);

        Assert.assertEquals("Question Answer false", door.toString());
    }

    @Test(expected = IllegalArgumentException.class)
    public void MakeDoorBadValues_Fail()
    {
        Door door = new Door(null,null);
    }
}