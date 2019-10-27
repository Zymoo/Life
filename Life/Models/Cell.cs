using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life.Models
{
    public class Cell
    {
        public bool Status { get; set; }
        public Cell()
        {
            Status = false;
        }

        public Cell(bool alive)
        {
            Status = alive;
        }

        public void ChangeStatus()
        {
            if (Status)
            {
                Status = false;
            }
            else Status = true;
        }

    }
}
