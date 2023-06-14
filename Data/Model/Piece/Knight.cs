using System;

namespace Data.Model.Piece
{
    [Serializable]
    public class Knight : Pieces
    {
        public Knight(Color color, Square square) : base(color, square)
        {
            Type = Type.Knight;
        }

        public Knight(Color color) : base(color)
        {
            Type = Type.Knight;
        }

        public override Pieces Clone(Square square) => new Knight(Color, square);

        public override string ToString() => "Knight";
    }
}