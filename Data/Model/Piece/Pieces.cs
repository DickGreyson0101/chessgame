using System;
using System.Runtime.Serialization;

namespace Data.Model.Piece
{
    [Serializable]
    public abstract class Pieces
    {
        protected Pieces(Color color, Square square)
        {
            Color = color;
            Square = square;
        }

        protected Pieces(Color color)
        {
            Color = color;
            Square = null;
        }

        public Color Color { get; }

        public Square Square { get; set; }

        public Type Type { get; set; }

        public bool HasMoved { get; set; } = false;

        public abstract Pieces Clone(Square square);
    }

    [Serializable]
    public enum Type
    {
        [EnumMember]
        Bishop,
        [EnumMember]
        King,
        [EnumMember]
        Queen,
        [EnumMember]
        Pawn,
        [EnumMember]
        Knight,
        [EnumMember]
        Rook
    }

    [Serializable]
    public enum Color
    {
        [EnumMember]
        White,
        [EnumMember]
        Black
    }
}
