using System.Collections.Generic;
using System.Linq;
using Data.Model;
using Data.Model.Piece;
using Logic.Engine.Rules;

namespace Logic.Engine.States
{
    public class CheckState : IState
    {
        public bool IsInState(Board board, Color color)
        {
            // Build groups of special rules that do not take into account of checks
            Board tempBoard = new Board(board);
            List<IRule> queenMovementCheckRules =
                new List<IRule> { new QueenMovementRule(), new CanOnlyTakeEnemyRule() };
            List<IRule> pawnMovementCheckRules = new List<IRule> { new PawnMovementRule(), new CanOnlyTakeEnemyRule() };
            List<IRule> kingMovementCheckRules = new List<IRule> { new KingMovementRule(), new CanOnlyTakeEnemyRule(), new CastlingRule() };
            List<IRule> knightMovementCheckRules = new List<IRule>
                { new KnightMovementRule(), new CanOnlyTakeEnemyRule() };
            List<IRule> rookMovementCheckRules = new List<IRule> { new RookMovementRule(), new CanOnlyTakeEnemyRule() };
            List<IRule> bishopMovementCheckRules = new List<IRule>
                { new BishopMovementRule(), new CanOnlyTakeEnemyRule() };

            Dictionary<Type, List<IRule>> rulesGroup = new Dictionary<Type, List<IRule>>
            {
                { Type.Rook, rookMovementCheckRules },
                { Type.Knight, knightMovementCheckRules },
                { Type.Bishop, bishopMovementCheckRules },
                { Type.King, kingMovementCheckRules },
                { Type.Queen, queenMovementCheckRules },
                { Type.Pawn, pawnMovementCheckRules }
            };

            //Looking for the king
            Pieces concernedKing = tempBoard.Squares.OfType<Square>()
                .First(x => (x?.Piece?.Type == Type.King) && (x?.Piece?.Color == color)).Piece;

            bool res = false;
            foreach (KeyValuePair<Type, List<IRule>> rules in rulesGroup)
            {
                List<Square> possibleMoves = new List<Square>();
                concernedKing.Type = rules.Key;
                possibleMoves = possibleMoves.Concat(rules.Value.First().PossibleMoves(concernedKing)).ToList();
                rules.Value.ForEach(x => possibleMoves = possibleMoves.Intersect(x.PossibleMoves(concernedKing)).ToList());

                if (possibleMoves.Any(x => x?.Piece?.Type == rules.Key))
                    //Check if it should be a different color
                    res = true;
            }

            concernedKing.Type = Type.King;
            return res;
        }

        public string Explain() => "The player's king is in check!";

    }
}