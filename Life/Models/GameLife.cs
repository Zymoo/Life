using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life.Models
{   
    class GameLife
    {

        private List<Cell> cells;
        private readonly int rows;
        private readonly int columns;
        private int resurection = 3;

        public int Resurection { get => resurection; set => resurection = value; }

        public GameLife(List<Cell> c, int rows, int columns)
        {
            this.cells = c;
            this.rows = rows;
            this.columns = columns;
        }

        public void PlayNextTurn()
        {
            cells = GetNextState();
        }

        public void ChangeCell(int x, int y)
        {
            cells.Find(c => (c.X == x && c.Y == y)).ChangeStatus();
        }


        public bool GetCellStatus(int i)
        {
            return cells.ElementAt(i).Status;
        }

        public List<Cell> GetNextState()
        {
            List<Cell> nextCells = new List<Cell>();
            for (int i = 0; i < cells.Count(); i++)
            {
                nextCells.Add(new Cell(Survive(i)));
            }
            for (int i = 0; i < cells.Count(); i++)
            {
                cells.ElementAt(i).Status = nextCells.ElementAt(i).Status;
            }
            return cells;
        }

        private bool Survive(int index)
        {
            int startX = cells.ElementAt(index).X - 1;
            int startY = cells.ElementAt(index).Y - 1;
            int aliveNeighbours = 0;

            for (int x = startX; x <= startX + 2; x++)
            {
                for (int y = startY; y <= startY + 2; y++)
                {
                    if (x >= 0 && y >= 0 && x < rows && y < columns && (x != startX + 1 || y != startY + 1))
                    {
                        if (cells.Find(c => (c.X == x && c.Y == y)).Status)
                            aliveNeighbours++;
                    }
                }
            }
            if (cells.ElementAt(index).Status)
            {
                return (aliveNeighbours == 3 || aliveNeighbours == 2);
            }
            else return aliveNeighbours == resurection;
        }

        public void Glider()
        {
            foreach (Cell c in cells)
            {
                if((c.X == 0 && c.Y == 2) || (c.X == 1 && c.Y == 2) || (c.X == 2 && c.Y == 2) || (c.X == 1 && c.Y == 0) || (c.X == 2 && c.Y == 1)){
                    c.Status = true;
                }
            }
        }

        public void SpaceShip()
        {
            foreach (Cell c in cells)
            {
                if ((c.X == 1 && c.Y == 2) || (c.X == 1 && c.Y == 3) || 
                    (c.X == 2 && c.Y == 0) || (c.X == 2 && c.Y == 1) || (c.X == 2 && c.Y == 3) || (c.X == 2 && c.Y == 4) ||
                    (c.X == 3 && c.Y == 0) || (c.X == 3 && c.Y == 1) || (c.X == 3 && c.Y == 2) || (c.X == 3 && c.Y == 3) ||
                    (c.X == 4 && c.Y == 1) || (c.X == 4 && c.Y == 2))
                {
                    c.Status = true;
                }
            }
        }

    }
}
