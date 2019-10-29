using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life.Models
{
    public class BasicCell
    {
        private bool status;
        public bool Status { get => status; set => status = value; }

        public BasicCell()
        {
            Status = false;
        }

        public BasicCell(bool alive)
        {
            Status = alive;
        }

        public void ChangeStatus()
        {
            if (Status)
            {
                status = false;
            }
            else status = true;
        }
    }
}
