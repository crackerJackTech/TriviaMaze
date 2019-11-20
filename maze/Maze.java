package Maze;

public class Maze
{
	private Room[][] rooms;
	private Player player;
	private MazeBuilder builder;

	public Maze(int rows, int columns, String name, int age)
	{
		this.player = new Player(name, age);
		this.rooms = new Room[rows][columns];
	}

	public Maze(int rows, int columns, MazeBuilder builder, String name, int age)
	{
		this.player = new Player(name, age);
		this.rooms = new Room[rows][columns];
		this.builder = builder;
	}

}
