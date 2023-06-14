using System;

namespace Data.Model.Piece
{
    [Serializable]
    public class Bishop : Pieces
    {
        public Bishop(Color color, Square square) : base(color, square)
        {
            Type = Type.Bishop;
        }

        public Bishop(Color color) : base(color)
        {
            Type = Type.Bishop;
        }

        public override Pieces Clone(Square square) => new Bishop(Color, square);

        public override string ToString() => "Bishop";
    }
}