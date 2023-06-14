using System.Collections.Generic;
using System.Linq;
using Data.Model;
using Data.Model.Piece;

namespace Logic.Engine.Rules
{
    internal class CanOnlyTakeKingEnemy : IRule
    {
        public bool IsMoveValid(Move move, Board board)
        {
            if (move.PieceColor == board.PieceAt(move.TargetCoordinate)?.Color)
                return board.PieceAt(move.TargetCoordinate).Type == Type.Rook;

            return true;
        }

        public List<Square> PossibleMoves(Pieces piece)
        {
            return piece.Square.Board.Squares.OfType<Square>().ToList()
                .FindAll(x => IsMoveValid(new Move(piece, x), piece.Square.Board));
        }
    }
}