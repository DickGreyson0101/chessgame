using System.Collections.Generic;
using System.Linq;
using Data.Model;
using Data.Model.Piece;
using Logic.Engine.RuleManager;

namespace Logic.Engine.States
{
    public class PatState : IState
    {
        public bool IsInState(Board board, Color color)
        {
            RuleGroup ruleGroup = new PawnRuleGroup();
            ruleGroup.AddGroup(new RookRuleGroup());
            ruleGroup.AddGroup(new KnightRuleGroup());
            ruleGroup.AddGroup(new BishopRuleGroup());
            ruleGroup.AddGroup(new KingRuleGroup());
            ruleGroup.AddGroup(new QueenRuleGroup());

            List<Square> possibleSquares = new List<Square>();
            foreach (Square square in board.Squares.OfType<Square>().Where(x => x?.Piece?.Color == color))
                if (square.Piece != null)
                    possibleSquares = possibleSquares.Concat(ruleGroup.PossibleMoves(square.Piece)).ToList();
            return possibleSquares.Count == 0;
        }

        public string Explain() => "We're all dead!";
    }
}