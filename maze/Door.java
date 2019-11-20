package Maze;

public class Door
{
	private Question question;
	private Boolean isLocked;
	
	public Door(Question question, Boolean isLocked)
	{
	    if(question == null)
        {
            throw new IllegalArgumentException("Question cannot be null!");
        }
	    if(isLocked == null) {
            throw new IllegalArgumentException("Passed in boolean cannot be null!");
        }
		this.question = question;
		this.isLocked = isLocked;
	}

	//Testing purposes
    @Override
    public String toString()
    {
        return (this.question.toString() + " " + this.isLocked.toString());
    }
}
