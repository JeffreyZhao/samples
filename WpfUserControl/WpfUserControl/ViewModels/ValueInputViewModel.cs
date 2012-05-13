using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WpfUserControl.ViewModels
{
    public class ValueInputViewModel : INotifyPropertyChanged
    {
        public ValueInputViewModel()
        {
            this.Text = "";
            this.Number = 0;
        }

        private string _text;

        public string Text
        {
            get
            {
                return this._text;
            }
            set
            {
                this._text = value;
                this.NotifyPropertyChanged("Text");
            }
        }

        private int _number;

        public int Number
        {
            get
            {
                return this._number;
            }
            set
            {
                this._number = value;
                this.NotifyPropertyChanged("Number");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            var propertyChanged = this.PropertyChanged;
            if (propertyChanged != null)
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
