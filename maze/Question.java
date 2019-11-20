package Maze;
import java.sql.*;

import Maze.QuestionTypes;

public class Question
{
	private Question question;
	private String questionString;
	private String answer;
	private QuestionTypes format;
	private String[] questionCollection;

	public Question()
	{
		this.questionCollection = selection();
	}


	public Question(String question, String answer)
	{
		if(question == null)
		{
			throw new IllegalArgumentException("Question cannot be null!");
		}
		if(answer == null)
		{
			throw new IllegalArgumentException("Answer cannot be null!");
		}

		this.questionString = question;
		this.answer = answer;
	}

	public Question(Question question, String answer, QuestionTypes format)
	{
		this.question = question;
		this.answer = answer;
		this.format = format;
	}
	
	//testing purposes
	@Override
	public String toString()
	{
		return (questionString + " " + answer);
	}

	private Connection connect()
	{
		String url = "jdbc:sqlite:TriviaDatabase.db";
		Connection connection = null;
		try
		{
			connection = DriverManager.getConnection(url);
		}
		catch(SQLException e)
		{
			System.out.println(e.getMessage());
		}

		return connection;
	}

	private String[] selection()
	{
		Connection connection = connect();
		Statement statement = null;

		//eventually this will be better kept in a hashset or something, so that answerID's can be paired directly with questions
		String[] qCollection = new String[25];

		try {
			statement = connection.createStatement();
			ResultSet resultSet = statement.executeQuery("SELECT * FROM QUESTION");

			int i =0;


			while(resultSet.next())
			{
				int questionID = resultSet.getInt("QUESTIONID");
				int answerID = resultSet.getInt("ANSWERID");
				int typeID = resultSet.getInt("TYPEID");
				String q = resultSet.getString("QUESTION");

				qCollection[i] = q;
				i++;
			}
			resultSet.close();
			statement.close();
			connection.close();
		}
		catch(SQLException e)
		{
			System.out.println(e.getMessage());
		}

		return qCollection;
	}

}
