package Maze;

import org.junit.Assert;
import org.junit.Test;

import static org.junit.Assert.*;

public class RoomTest
{
    @Test
    public void MakeRoomGoodInput_Success()
    {
        Door d1 = new Door(new Question("Q1","A1"),false);
        Door d2 = new Door(new Question("Q2","A2"),false);
        Door d3 = new Door(new Question("Q3","A3"),true);
        Door[] doors = {d1,d2,d3};

        Room room = new Room(doors);

        String expected = "Q1 A1 falseQ2 A2 falseQ3 A3 true";

        Assert.assertEquals(expected, room.toString());
    }

    @Test(expected = IllegalArgumentException.class)
    public void MakeRoomBadInputDoorsIsNull_Fail()
    {
        Room room = new Room(null);
    }

    @Test(expected = IllegalArgumentException.class)
    public void MakeRoomBadInputDoorsIsEmpty_Fail()
    {
        Door[] d = new Door[0];

        Room room = new Room(d);
    }
}