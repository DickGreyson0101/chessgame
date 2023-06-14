using System.Collections.Generic;
using System.Linq;
using Data.Model;
using Data.Model.Piece;

namespace Logic.Engine.Rules
{
    public class CanOnlyTakeEnemyRule : IRule
    {
        public bool IsMoveValid(Move move, Board board) =>
            move.PieceColor != board.PieceAt(move.TargetCoordinate)?.Color;

        public List<Square> PossibleMoves(Pieces piece)
        {
            return piece.Square.Board.Squares.OfType<Square>().ToList().FindAll(x => piece.Color != x?.Piece?.Color);
        }
    }
}