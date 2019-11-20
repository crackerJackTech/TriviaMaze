package Maze;

import org.junit.Assert;
import org.junit.Test;

import static org.junit.Assert.*;

public class PlayerTest
{
    @Test
    public void MakePlayerGoodInput_Success()
    {
        Player player = new Player("Tester", 23);
        Assert.assertEquals("Tester 23", player.toString());
    }

    @Test(expected = IllegalArgumentException.class)
    public void MakePlayerBadInput_Fail()
    {
        Player player = new Player(null,  -1);
    }


}