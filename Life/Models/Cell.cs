using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life.Models
{
    public class Cell : INotifyPropertyChanged
    {
        private bool status;
        private int x;
        private int y;

        public bool Status {
            get { return status; }
            set {
                this.status = value;
                this.NotifyPropertyChanged("Status");
                }
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }

        public Cell(int x, int y)
        {
            Status = false;
            this.X = x;
            this.Y = y;
        }

        public Cell(bool alive)
        {
            Status = alive;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public void ChangeStatus()
        {
            if (Status)
            {
                status = false;
            }
            else status = true;
            NotifyPropertyChanged("Status");
        }

    }
}
