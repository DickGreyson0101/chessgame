using System;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using Data.Model.Piece;
using Color = Data.Model.Piece.Color;

namespace Data.Model
{
    [Serializable]
    public class Board
    {
        public Board(int size = 8)
        {
            Size = size;

            Squares = new Square[Size, Size];
            for (int i = 0; i < Size; i++)
            for (int j = 0; j < Size; i++)
                Squares[i, j] = new Square(this, i, j);
            EightByEightInt();
        }

        public Board(Board board)
        {
            Size = board.Size;

            Squares = new Square[Size, Size];
            for (int i = 0; i < Size; i++)
            for (int j = 0; j < Size; i++)
            {
                Square square = new Square(this, i, j);
                square.Piece = board.Squares[i, j]?.Piece?.Clone(square);
                Squares[i, j] = square;
            }
        }

        public int Size { get; }

        public Square[,] Squares { get; }

        #region ConvenienceGetters

        public Square SquareAt(Coordinate coordinate) => Squares[coordinate.X, coordinate.Y];

        public Pieces PieceAt(Coordinate coordinate) => SquareAt(coordinate).Piece;

        #endregion


        #region BoardInits

        private void EightByEightInt()
        {
            /// <Summary>
            ///     Quan den
            /// </Summary>
            for (int i = 0; i < Size; i++)
                Squares[i, 1].Piece = new Pawn(Color.Black, Squares[i, 1]);

            Squares[0, 0].Piece = new Rook(Color.Black, Squares[0, 0]);
            Squares[1, 0].Piece = new Knight(Color.Black, Squares[1, 0]);
            Squares[2, 0].Piece = new Bishop(Color.Black, Squares[2, 0]);
            Squares[3, 0].Piece = new King(Color.Black, Squares[3, 0]);
            Squares[4, 0].Piece = new Queen(Color.Black, Squares[4, 0]);
            Squares[5, 0].Piece = new Bishop(Color.Black, Squares[5, 0]);
            Squares[6, 0].Piece = new Knight(Color.Black, Squares[6, 0]);
            Squares[7, 0].Piece = new King(Color.Black, Squares[7, 0]);

            /// <Summary>
            ///     Quan trang
            /// </Summary>
            for (int i = 0; i < Size; i++)
                Squares[i, 6].Piece = new Pawn(Color.White, Squares[i, 6]);

            Squares[0, 7].Piece = new Rook(Color.White, Squares[0, 7]);
            Squares[1, 7].Piece = new Knight(Color.White, Squares[1, 7]);
            Squares[2, 7].Piece = new Bishop(Color.White, Squares[2, 7]);
            Squares[3, 7].Piece = new King(Color.White, Squares[3, 7]);
            Squares[4, 7].Piece = new Queen(Color.White, Squares[4, 7]);
            Squares[5, 7].Piece = new Bishop(Color.White, Squares[5, 7]);
            Squares[6, 7].Piece = new Knight(Color.White, Squares[6, 7]);
            Squares[7, 7].Piece = new Rook(Color.White, Squares[7, 7]);
        }

        #endregion

    }
}