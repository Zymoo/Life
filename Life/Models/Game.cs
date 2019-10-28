using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life.Models
{
    class Game
    {

        private Cell[,] cells;

        public Game(int x, int y)
        {
            cells = new Cell[x, y];
            for(int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    cells[i, j] = new Cell();
                }
            }
        }

        public Game(Cell [,] c)
        {
            cells = c;
        }

        public void PlayNextTurn()
        {
            cells = GetNextState();
        }

        public void ChangeCell(int x, int y)
        {
            cells[x, y].ChangeStatus();
        }


        public bool GetCellStatus(int x, int y)
        {
            return cells[x, y].Status;
        }

        public Cell[,] GetNextState()
        {
            Cell[,] nextCells = new Cell[cells.GetLength(0), cells.GetLength(1)];

            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; j < cells.GetLength(1); j++)
                {
                    nextCells[i, j] = new Cell(Survive(i, j));
                }
            }
            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; j < cells.GetLength(1); j++)
                {
                    cells[i, j].Status = nextCells[i, j].Status;
                }
            }
            return cells;
        }

        private bool Survive(int i, int j)
        {
            int startX = i - 1;
            int startY = j - 1;
            int aliveNeighbours = 0;

            for (int x = startX; x <= startX + 2; x++)
            {
                for (int y = startY; y <= startY + 2; y++)
                {
                    if( x >= 0 && y >= 0 && x < cells.GetLength(0) && y < cells.GetLength(1) && (x!= i || y != j))
                    {
                        if (cells[x, y].Status)
                            aliveNeighbours++;
                    }
                }
            }
            if (cells[i, j].Status)
            {
                return (aliveNeighbours == 3 || aliveNeighbours == 2);
            }
            else return aliveNeighbours == 3;


        }
    }
}
