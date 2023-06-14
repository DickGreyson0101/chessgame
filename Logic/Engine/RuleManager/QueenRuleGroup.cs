using Data.Model.Piece;
using Logic.Engine.Rules;

namespace Logic.Engine.RuleManager
{
    public class QueenRuleGroup : RuleGroup
    {
        public QueenRuleGroup()
        {
            Rules.Add(new QueenMovementRule());
            Rules.Add(new CanOnlyTakeEnemyRule());
            Rules.Add(new WillNotMakeCheck());
        }

        protected override Type Type => Type.Queen;
    }
}