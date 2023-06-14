using Data.Model.Piece;
using Logic.Engine.Rules;

namespace Logic.Engine.RuleManager
{
    public class PawnRuleGroup : RuleGroup
    {
        public PawnRuleGroup()
        {
            Rules.Add(new PawnMovementRule());
            Rules.Add(new CanOnlyTakeEnemyRule());
            Rules.Add(new WillNotMakeCheck());
        }

        protected override Type Type => Type.Pawn;
    }
}