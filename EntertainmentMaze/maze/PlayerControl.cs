using System;
using System.Collections.Generic;
using System.Text;

namespace EntertainmentMaze.maze
{
    public class PlayerControl
    {
        public static void MovementAttempt(Maze playerMaze, string movementDirection)
        {
            Room[,] rooms = playerMaze.GetRooms();

            if(playerMaze is null)
            {
                throw new ArgumentNullException(nameof(playerMaze));
            }
            if(movementDirection == "" || movementDirection is null)
            {
                throw new ArgumentNullException(nameof(movementDirection));
            }

            switch (movementDirection)
            {
                case "N":
                    if (playerMaze.GetLocation().NorthDoor is null || playerMaze.GetLocation().NorthDoor.GetDoorStatus())
                    {
                        Console.WriteLine("This Door is locked! You cannot go through here.");
                        return;
                    }
                    else if (playerMaze.GetLocation().NorthDoor.GetDoorOpenedStatus() is true)
                    {
                        playerMaze.MoveHero("N");
                    }
                    else
                    {
                        if (QuestionAttempt(playerMaze.GetLocation().NorthDoor.DisplayQuestion(), playerMaze.GetLocation().NorthDoor.DisplayAnswer()) is true)
                        {
                            playerMaze.GetLocation().NorthDoor.OpenDoor();
                            playerMaze.MoveHero("N");
                            playerMaze.GetLocation().SouthDoor.OpenDoor();
                        }
                        else
                        {
                            playerMaze.GetLocation().NorthDoor.LockDoor();
                            if (!(rooms[playerMaze.GetLocation().RowLocation - 1, playerMaze.GetLocation().ColumnLocation] is null))
                            {
                                rooms[playerMaze.GetLocation().RowLocation - 1, playerMaze.GetLocation().ColumnLocation].SouthDoor.LockDoor();
                            }
                        }
                    }
                    return;
                case "E":
                    if (playerMaze.GetLocation().EastDoor is null || playerMaze.GetLocation().EastDoor.GetDoorStatus())
                    {
                        Console.WriteLine("This Door is locked! You cannot go through here.");
                        return;
                    }
                    else if (playerMaze.GetLocation().EastDoor.GetDoorOpenedStatus() is true)
                    {
                        playerMaze.MoveHero("E");
                    }
                    else
                    {
                        if (QuestionAttempt(playerMaze.GetLocation().EastDoor.DisplayQuestion(), playerMaze.GetLocation().EastDoor.DisplayAnswer()) is true)
                        {
                            playerMaze.GetLocation().EastDoor.OpenDoor();
                            playerMaze.MoveHero("E");
                            playerMaze.GetLocation().WestDoor.OpenDoor();
                        }
                        else
                        {
                            playerMaze.GetLocation().EastDoor.LockDoor();
                            if(!(rooms[playerMaze.GetLocation().RowLocation, playerMaze.GetLocation().ColumnLocation + 1] is null))
                            {
                                rooms[playerMaze.GetLocation().RowLocation, playerMaze.GetLocation().ColumnLocation + 1].WestDoor.LockDoor();
                            }
                        }
                    }
                    return;
                case "S":
                    if (playerMaze.GetLocation().SouthDoor is null || playerMaze.GetLocation().SouthDoor.GetDoorStatus() is true)
                    {
                        Console.WriteLine("This Door is locked! You cannot go through here.");
                        return;
                    }
                    else if (playerMaze.GetLocation().SouthDoor.GetDoorOpenedStatus() is true)
                    {
                        playerMaze.MoveHero("S");
                    }
                    else
                    {
                        if (QuestionAttempt(playerMaze.GetLocation().SouthDoor.DisplayQuestion(), playerMaze.GetLocation().SouthDoor.DisplayAnswer()) is true)
                        {
                            playerMaze.GetLocation().SouthDoor.OpenDoor();
                            playerMaze.MoveHero("S");
                            playerMaze.GetLocation().NorthDoor.OpenDoor();
                        }
                        else
                        {
                            playerMaze.GetLocation().SouthDoor.LockDoor();
                            if (!(rooms[playerMaze.GetLocation().RowLocation + 1, playerMaze.GetLocation().ColumnLocation] is null))
                            {
                                rooms[playerMaze.GetLocation().RowLocation + 1, playerMaze.GetLocation().ColumnLocation].NorthDoor.LockDoor();
                            }
                        }
                    }
                    return;
                case "W":
                    if (playerMaze.GetLocation().WestDoor is null || playerMaze.GetLocation().WestDoor.GetDoorStatus() is true)
                    {
                        Console.WriteLine("This Door is locked! You cannot go through here.");
                        return;
                    }
                    else if(playerMaze.GetLocation().WestDoor.GetDoorOpenedStatus() is true)
                    {
                        playerMaze.MoveHero("W");
                    }
                    else
                    {
                        if (QuestionAttempt(playerMaze.GetLocation().WestDoor.DisplayQuestion(), playerMaze.GetLocation().WestDoor.DisplayAnswer()) is true)
                        {
                            playerMaze.GetLocation().WestDoor.OpenDoor();
                            playerMaze.MoveHero("W");
                            playerMaze.GetLocation().EastDoor.OpenDoor();
                        }
                        else
                        {
                            playerMaze.GetLocation().WestDoor.LockDoor();
                            if (!(rooms[playerMaze.GetLocation().RowLocation, playerMaze.GetLocation().ColumnLocation - 1] is null))
                            {
                                rooms[playerMaze.GetLocation().RowLocation, playerMaze.GetLocation().ColumnLocation - 1].EastDoor.LockDoor();
                            }
                        }
                    }
                    return;
            }
        }

        public static bool QuestionAttempt(string question, string answer)
        {
            if(question is null || question == "")
            {
                throw new ArgumentNullException(nameof(question));
            }
            if (answer is null || answer == "")
            {
                throw new ArgumentNullException(nameof(answer));
            }

            string attemptedAnswer = "";
            Console.WriteLine("Here is the question to open the door:\n");
            Console.WriteLine(question);

            attemptedAnswer = Console.ReadLine();

            if (answer.ToLowerInvariant().Equals(attemptedAnswer.ToLowerInvariant()))
            {
                Console.WriteLine("Correct Answer! You move to the next room...");
                return true;
            }
            else
            {
                Console.WriteLine("Incorrect! The Door locks...");
                return false;
            }
        }
    }
}
