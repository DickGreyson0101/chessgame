using System;

namespace Data.Model.Piece
{
    [Serializable]
    public class Pawn : Pieces
    {
        public Pawn(Color color, Square square) : base(color, square)
        {
            Type = Type.Pawn;
        }

        public Pawn(Color color) : base(color)
        {
            Type = Type.Pawn;
        }

        public bool EnPassant { get; set; }

        public override Pieces Clone(Square square) => new Pawn(Color, square);

        public override string ToString() => "Pawn";
    }
}