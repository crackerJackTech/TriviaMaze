using EntertainmentMaze.Database;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EntertainmentMaze.maze
{
    [DataContract]
    public class Maze
    {
        private enum Location
        {
            Row = 0,
            Column = 1
        }
        //[DataMember]
        private Room[,] _Rooms;
        [DataMember]
        public Player _Player { get; set; }
        [DataMember]
        private int[] PlayerLocation = new int[2];
        [DataMember]
        public int Rows { get; set; }
        [DataMember]
        public int Columns { get; set; }
        [DataMember]
        private Room[][] surrogateArray;

        private Room GetHeroLocation() => (_Rooms[PlayerLocation[(int)Location.Row], PlayerLocation[(int)Location.Column]]);
        private Room ExitLocationOfMaze() => (_Rooms[Rows - 1, Columns - 1]);

        private int MoveRowUp() => PlayerLocation[(int)Location.Row] - 1;
        private int MoveRowDown() => PlayerLocation[(int)Location.Row] + 1;
        private int MoveColumnLeft() => PlayerLocation[(int)Location.Column] - 1;
        private int MoveColumnRight() => PlayerLocation[(int)Location.Column] + 1;
        private int SameRow() => PlayerLocation[(int)Location.Row];
        private int SameColumn() => PlayerLocation[(int)Location.Column];

        public Maze() { }

        public void CompleteBuild()
        {
            BuildRooms();
            SetHeroLocation(0, 0);
        }

        private void BuildRooms()
        {
            _Rooms = new Room[Rows, Columns];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    _Rooms[i, j] = new Room(i, j, Rows, Columns);
                }
            }
        }

        public Room GetLocation()
        {
            return GetHeroLocation();
        }

        internal Room GetExitLocationOfMaze() => ExitLocationOfMaze();

        internal bool IsSolvable()
        {
            return FindPath(SameRow(), SameColumn());
        }

        private bool FindPath(int playerRowLocation, int playerColumnLocation)
        {
            if (_Rooms[playerRowLocation, playerColumnLocation] is null) return false;
            if (_Rooms[playerRowLocation, playerColumnLocation] == ExitLocationOfMaze()) return true;
            if (FindPath(playerRowLocation + 1, playerColumnLocation)) return true;
            if (FindPath(SameRow(), MoveColumnRight())) return true;
            if (FindPath(playerRowLocation - 1, SameColumn())) return true;
            if (FindPath(SameRow(), MoveColumnLeft())) return true;
            return false;

        }


        private void SetHeroLocation(int rowLocation, int columnLocation)
        {
            PlayerLocation[(int)Location.Row] = rowLocation;
            PlayerLocation[(int)Location.Column] = columnLocation;
            GetHeroLocation().SetPlayerInRoom();
        }
        
        public void MoveHero(String direction)
        {

            switch (direction)
            {
                case "N":
                    if (!(GetHeroLocation().NorthDoor is null))
                    {
                        GetHeroLocation().RemovePreviousPlayerLocation();
                        SetHeroLocation(MoveRowUp(), SameColumn());
                    }
                    return;
                case "E":
                    if (!(GetHeroLocation().EastDoor is null))
                    {
                        GetHeroLocation().RemovePreviousPlayerLocation();
                        SetHeroLocation(SameRow(), MoveColumnRight());
                    }
                    return;
                case "S":
                    if (!(GetHeroLocation().SouthDoor is null))
                    {
                        GetHeroLocation().RemovePreviousPlayerLocation();
                        SetHeroLocation(MoveRowDown(), SameColumn());
                    }
                    return;
                case "W":
                    if (!(GetHeroLocation().WestDoor is null))
                    {
                        GetHeroLocation().RemovePreviousPlayerLocation();
                        SetHeroLocation(SameRow(), MoveColumnLeft());
                    }
                    return;
                default:
                    return;
            }
        }

        internal void DisplayHeroLocation()
        {
            Console.WriteLine($"{(PlayerLocation[(int)Location.Row] + 1).ToString()}, {(PlayerLocation[(int)Location.Column] + 1).ToString()}");
        }

        public string PrintMaze()
        {
            String entireDungeon = "";
            int j;

            for (int i = 0; i < Rows; i++)
            {

                for (j = 0; j < Columns; j++)
                {
                    entireDungeon += _Rooms[i, j].GetTopOfRoom();
                }

                entireDungeon += "\n";

                for (j = 0; j < Columns; j++)
                {
                    entireDungeon += _Rooms[i, j].GetMiddleOfRoom();
                }

                entireDungeon += "\n";

                for (j = 0; j < Columns; j++)
                {
                    entireDungeon += _Rooms[i, j].GetBottomOfRoom();
                }

                entireDungeon += "\n";

            }

            return entireDungeon;
        }

        //BeforeSerializing() and AfterSerializing() from 
        //https://social.msdn.microsoft.com/Forums/vstudio/en-US/ff233917-eabf-47a3-8127-55fac4188b94/define-double-as-datamember?forum=wcf

        [OnSerializing]
        public void BeforeSerializing(StreamingContext ctx)
        {
            int rows = _Rooms.GetLength(0);
            int cols = _Rooms.GetLength(1);
            this.surrogateArray = new Room[rows][];
            for (int i = 0; i < rows; i++)
            {
                this.surrogateArray[i] = new Room[cols];
                for (int j = 0; j < cols; j++)
                {
                    this.surrogateArray[i][j] = _Rooms[i, j];
                }
            }
        }

        [OnDeserialized]
        public void AfterDeserializing(StreamingContext ctx)
        {
            if (this.surrogateArray == null)
            {
                _Rooms = null;
            }
            else
            {
                int rows = this.surrogateArray.Length;
                if (rows == 0)
                {
                    _Rooms = new Room[0, 0];
                }
                else
                {
                    int cols = this.surrogateArray[0].Length;
                    for (int i = 1; i < rows; i++)
                    {
                        if (this.surrogateArray[i].Length != cols)
                        {
                            throw new InvalidOperationException("Surrogate array does not correspond to the original");
                        }
                    }

                    _Rooms = new Room[rows, cols];
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < cols; j++)
                        {
                            _Rooms[i, j] = this.surrogateArray[i][j];
                        }
                    }
                }
            }
        }
    }
}
