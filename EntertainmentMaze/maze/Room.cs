using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EntertainmentMaze.maze
{
    [DataContract]
    public class Room
    {
        [DataMember]
        internal Door NorthDoor { get; set; } //= new Door();
        [DataMember]
        internal Door EastDoor { get; set; } //= new Door();
        [DataMember]
        internal Door SouthDoor { get; set; } //= new Door();
        [DataMember]
        internal Door WestDoor { get; set; } //= new Door();

        public int RowLocation { get; }
        public int ColumnLocation { get; }
        [DataMember]
        private bool IsPlayerInRoom { get; set; } = false;

        public Room(int rowLocation, int columnLocation, int numberOfTotalRows, int numberOfTotalColumns)
        {
            RowLocation = rowLocation;
            ColumnLocation = columnLocation;

            NorthDoor = new Door();
            EastDoor = new Door();
            SouthDoor = new Door();
            WestDoor = new Door();

            CreateRoomDescription(numberOfTotalRows, numberOfTotalColumns);
        }

        [JsonConstructor]
        public Room(Door nDoor, Door eDoor, Door sDoor, Door wDoor)
        {
            NorthDoor = nDoor;
            EastDoor = eDoor;
            SouthDoor = sDoor;
            WestDoor = wDoor;
        }

        public void SetPlayerInRoom()
        {
            IsPlayerInRoom = true;
        }

        public void RemovePreviousPlayerLocation()
        {
            IsPlayerInRoom = false;
        }

        private void CreateRoomDescription(int numberOfTotalRows, int numberOfTotalColumns)
        {
            //Room in the first row
            if (RowLocation == 0)
            {
                NorthDoor = null;
                if (ColumnLocation == 0)
                {
                    WestDoor = null;
                }
                else if (ColumnLocation == (numberOfTotalRows - 1))
                {
                    EastDoor = null;
                }
            }

            //Room in the first column
            else if (ColumnLocation == 0)
            {
                WestDoor = null;
                if (RowLocation == (numberOfTotalColumns - 1))
                {
                    SouthDoor = null;
                }
            }

            //Room in the last row
            else if (RowLocation == (numberOfTotalRows - 1))
            {
                SouthDoor = null;
                if (ColumnLocation == (numberOfTotalRows - 1))
                {
                    EastDoor = null;
                }
            }

            //Room in the last column
            else if (ColumnLocation == (numberOfTotalColumns - 1))
            {
                EastDoor = null;
            }
        }

        public String GetTopOfRoom()
        {
            String top = "*";
            if (!(NorthDoor is null))
            {
                top += "-";
            }
            else
            {
                top += "*";
            }

            top += "*";
            return top;

        }

        public String GetMiddleOfRoom()
        {
            String middle = "";
            if (!(WestDoor is null))
            {
                middle += "|";
            }
            else
            {
                middle += "*";
            }
            if(IsPlayerInRoom)

            {
                middle += "P";
            }

            else
            {
                middle += " ";
            }

            if (!(EastDoor is null))
            {
                middle += "|";
            }
            else
            {
                middle += "*";
            }
            return middle;
        }

        public String GetBottomOfRoom()
        {
            String bottom = "*";

            if (!(SouthDoor is null))
            {
                bottom += "-";
            }
            else
            {
                bottom += "*";
            }

            bottom += "*";

            return bottom;
        }

        public String PrintRoom()
        {
            String stringRoom = "";

            stringRoom += GetTopOfRoom() + "\n";
            stringRoom += GetMiddleOfRoom() + "\n";
            stringRoom += GetBottomOfRoom();

            return stringRoom;
        }
    }
}