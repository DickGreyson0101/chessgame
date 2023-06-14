using System;
using System.Windows.Media;

namespace Data.Model.Piece
{
    [Serializable]
    public class Queen : Pieces
    {
        public Queen(Color color, Square square) : base(color, square)
        {
            Type = Type.Queen;
        }

        public Queen(Color color) : base(color)
        {
            Type = Type.Queen;
        }

        public override Pieces Clone(Square square) => new Queen(Color, square);

        public override string ToString() => "Queen";
    }
}