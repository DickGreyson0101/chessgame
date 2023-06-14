using System;
using System.Collections.Generic;
using System.Linq;
using Data.Model;
using Data.Model.Piece;
using Type = Data.Model.Piece.Type;


namespace Logic.Engine.Rules
{
    public class KingMovementRule : IRule
    {
        public bool IsMoveValid(Move move, Board board)
        {
            if (((board.PieceAt(move.TargetCoordinate)?.Color == move.PieceColor) &&
                 (board.PieceAt(move.TargetCoordinate)?.Type == Type.Rook)) ||
                ((Math.Abs(move.TargetCoordinate.X - move.StartCoordinate.X) == 2) &&
                 (move.TargetCoordinate.Y == move.StartCoordinate.Y)))
                return true;

            return (Math.Abs(move.StartCoordinate.X - move.TargetCoordinate.X) <= 1) &&
                   (Math.Abs(move.StartCoordinate.Y - move.TargetCoordinate.Y) <= 1);
        }

        public List<Square> PossibleMoves(Pieces piece)
        {
            return piece.Square.Board.Squares.OfType<Square>()
                .ToList()
                .FindAll(x => IsMoveValid(new Move(piece, x), piece.Square.Board));
        }
    }
}