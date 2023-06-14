using Data.Model.Piece;
using Logic.Engine.Rules;

namespace Logic.Engine.RuleManager
{
    public class KingRuleGroup : RuleGroup
    {
        public KingRuleGroup()
        {
            Rules.Add(new KingMovementRule());
            Rules.Add(new CanOnlyTakeEnemyRule());
            Rules.Add(new WillNotMakeCheck());
            Rules.Add(new CastlingRule());
        }

        protected override Type Type => Type.King;
    }
}