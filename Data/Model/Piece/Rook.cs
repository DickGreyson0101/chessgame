using System;

namespace Data.Model.Piece
{
    [Serializable]
    public class Rook : Pieces
    {
        public Rook(Color color, Square square) : base(color, square)
        {
            Type = Type.Rook;
        }

        public Rook(Color color) : base(color)
        {
            Type = Type.Rook;
        }

        public override Pieces Clone(Square square) => new Rook(Color, square);

        public override string ToString() => "Rook";
    }
}