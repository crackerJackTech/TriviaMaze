package Maze;

import java.sql.Date;

public class Player
{
	private String name;
	private int age;
	private Date dateOfBirth; //Needed?
	
	public Player(String name, int age)
	{
	    if(name == null)
        {
            throw new IllegalArgumentException("Name cannot be null!");
        }
	    if(age <= 0)
        {
            throw new IllegalArgumentException("Age cannot be less than or equal to zero!");
        }
	    
		this.name = name;
		this.age = age;
	}

	//for testing purposes
	@Override
    public String toString()
    {
        return (name + " " + this.age);
    }

}
