using Data.Model.Piece;
using Logic.Engine.Rules;

namespace Logic.Engine.RuleManager
{
    public class BishopRuleGroup : RuleGroup
    {
        public BishopRuleGroup()
        {
            Rules.Add(new BishopMovementRule());
            Rules.Add(new CanOnlyTakeEnemyRule());
            Rules.Add(new WillNotMakeCheck());
        }

        protected override Type Type => Type.Bishop;
    }
}