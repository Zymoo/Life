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
        public bool Status {
            get { return status; }
            set {
                this.status = value;
                this.NotifyPropertyChanged("Status");
                }
        }
        public Cell()
        {
            Status = false;
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
