package Maze;

public class Room
{
	private Door[] doorCollection;
	
	public Room(Door[] doors)
	{
		if(doors == null)
		{
			throw new IllegalArgumentException("Doors cannot be null!");
		}
		if(doors.length <= 0)
		{
			throw new IllegalArgumentException("Door array passed in is too small!");
		}

		this.doorCollection = doors;
	}

	//testing purposes
	@Override
	public String toString()
	{
		String returnString = "";

		for (Door d : this.doorCollection)
		{
			returnString += d.toString();
		}

		return returnString;
	}
}
