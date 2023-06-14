namespace Data.Model.Piece
{
    using System;
    [Serializable]
    public class King : Pieces
    {
        public King(Color color, Square square) : base(color, square)
        {
            Type = Type.King;
        }

        public King(Color color) : base(color)
        {
            Type = Type.King;
        }
        public override Pieces Clone(Square square) => new King(Color, square);

        public override string ToString() => "King";
    }
}