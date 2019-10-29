using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life
{
    public class Status : INotifyPropertyChanged
    {
        private bool running;

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public bool Running
        {
            get { return running; }
            set
            {
                this.running = value;
                this.NotifyPropertyChanged("Running");
            }
        }
        public Status()
        {
        }
    }
}
