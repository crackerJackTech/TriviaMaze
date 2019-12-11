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
        [DataMember]
        private List<Coordinate> path;
        private bool[,] traversed;
        [DataMember]
        private List<(int,int)> shifts = new List<(int, int)>(5)
        {
            (-1,0),
            (0,1),
            (1,0),
            (0,-1)
        };

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

        public Room[,] GetRooms()
        {
            return _Rooms;
        }

        internal List<Coordinate> IsSolvable()
        {
            path = new List<Coordinate>();
            traversed = new bool[5,5];
            Coordinate end = new Coordinate(4, 4);
            Coordinate playerLocation = new Coordinate(SameRow(), SameColumn());
            traversed[0, 0] = true;
            path.Add(playerLocation);

            if (!FindPath(traversed, playerLocation, end, path))
            {
                path.RemoveAt(path.Count-1);
            }
            return path;
        }

        private bool FindPath(bool[,] traversed, Coordinate playerLocation, Coordinate end, List<Coordinate> path)
        {
            if(playerLocation.row > 4)
            {
                playerLocation.row = 4;
            }
            if(playerLocation.col > 4)
            {
                playerLocation.col = 4;
            }
            if (playerLocation.row < 0)
            {
                playerLocation.row = 0;
            }
            if (playerLocation.col < 0)
            {
                playerLocation.col = 0;
            }
            if (playerLocation.row == end.row && playerLocation.col == end.col)
            {
                return true;
            }
            int x = 0;

            foreach((int r,int c) s in shifts)
            {
                Coordinate shift = new Coordinate(s.r, s.c);

                if (canTraverse(shift, playerLocation, traversed))
                {
                    traversed[playerLocation.row, playerLocation.col] = true;
                    path.Add(new Coordinate(playerLocation.row + s.r, playerLocation.col + s.c));
                    playerLocation = new Coordinate(playerLocation.row + s.r, playerLocation.col + s.c);

                    if (FindPath(traversed, playerLocation, end, path))
                    {
                        return true;
                    }
                    path.RemoveAt(path.Count-1);
                }
                x++;
            }
            return false;
        }

        private bool canTraverse(Coordinate shift, Coordinate roomLocation, bool[,] traversed)
        {
            if (shift.row == -1 && shift.col == 0)
            {
                return ((!(_Rooms[roomLocation.row, roomLocation.col].NorthDoor is null)) && (_Rooms[roomLocation.row, roomLocation.col].NorthDoor.GetDoorStatus() is false) && (traversed[roomLocation.row + shift.row, roomLocation.col + shift.col] == false));
            }
            if (shift.row == 0 && shift.col == 1)
            {
                return ((!(_Rooms[roomLocation.row, roomLocation.col].EastDoor is null)) && (_Rooms[roomLocation.row, roomLocation.col].EastDoor.GetDoorStatus() is false) && (traversed[roomLocation.row + shift.row, roomLocation.col + shift.col] == false));
            }
            if (shift.row == 1 && shift.col == 0)
            {
                return ((!(_Rooms[roomLocation.row, roomLocation.col].SouthDoor is null)) && (_Rooms[roomLocation.row, roomLocation.col].SouthDoor.GetDoorStatus() is false) && (traversed[roomLocation.row + shift.row, roomLocation.col + shift.col] == false));
            }
            if (shift.row == 0 && shift.col == -1)
            {
                return ((!(_Rooms[roomLocation.row, roomLocation.col].WestDoor is null)) && (_Rooms[roomLocation.row, roomLocation.col].WestDoor.GetDoorStatus() is false) && (traversed[roomLocation.row + shift.row, roomLocation.col + shift.col] == false));
            }
            return false;
        }

        public void SetCheatLocation()
        {
            _Rooms[PlayerLocation[0], PlayerLocation[1]].RemovePreviousPlayerLocation();
            PlayerLocation[0] = 4;
            PlayerLocation[1] = 3;
            _Rooms[PlayerLocation[0], PlayerLocation[1]].SetPlayerInRoom();
        }

        public Room GetExitLocationOfMaze() => ExitLocationOfMaze();

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

    [DataContract]
    public class Coordinate
    {
        [DataMember]
        public int row, col;

        public Coordinate(int row, int col)
        {
            this.row = row;
            this.col = col;
        }
    }
}
