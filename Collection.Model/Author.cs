using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Collection.Model
{
    public class Author : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public int AuthorId { get; set; }

        private string firstName;
        public string FirstName {
            get { return firstName; }
            set
            {
                if (SetField(ref firstName, value))
                {
                    OnPropertyChanged(nameof(FullName));
                    OnPropertyChanged(nameof(IsValid));
                }
            }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                if (SetField(ref lastName, value))
                {
                    OnPropertyChanged(nameof(FullName));
                    OnPropertyChanged(nameof(IsValid));
                }
            }
        }

        public string FullName { get => $"{FirstName} {LastName}"; }

        public virtual List<Piece> Pieces { get; set; }

        public bool IsValid { get => !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName); }
    }
}
