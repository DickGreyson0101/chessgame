using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls.Primitives;
using Data.Model.Piece;

namespace Data.Model
{
    [Serializable]
    public class Square : INotifyPropertyChanged
    {
        private Pieces _piece;

        public Square(Board parent, int x, int y)
        {
            Board = parent;
            X = x;
            Y = y;
        }

        public Board Board { get; set; }

        public Pieces Piece
        {
            get { return _piece; }
            set
            {
                _piece = value;
                OnPropertyChanged();
            }
        }

        public int X { get; }

        public int Y { get; }

        public Coordinate Coordinate => new Coordinate(X, Y);

        [field: NonSerialized] 
        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString() => (char) ('A' + X) + (8 - Y).ToString();
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
