using System;
using System.Collections.Generic;
using System.Text;

namespace EntertainmentMaze.maze
{
    public class MazeBuilder
    {
        private bool FinishedBuild = false;
        internal Maze Maze = new Maze();

        public Maze Build()
        {
            Maze.CompleteBuild();
            FinishedBuild = true;
            return Maze;
        }

        public MazeBuilder SetRows(int rows)
        {
            if(rows<=0)
            {
                throw new ArgumentOutOfRangeException(nameof(rows));
            }

            Check();
            Maze.Rows = rows;
            return this;
        }

        public MazeBuilder SetColumns(int columns)
        {
            if (columns <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(columns));
            }

            Check();
            Maze.Columns = columns;
            return this;
        }

        public MazeBuilder SetPlayer(Player player)
        {
            if(player is null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            Check();
            Maze._Player = player;
            return this;
        }


        private void Check()
        {
            if(FinishedBuild)
            {
                throw new ArgumentException("Do use other builder to create new instance");
            }
        }
    }
}
