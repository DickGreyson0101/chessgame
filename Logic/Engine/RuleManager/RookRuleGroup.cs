using Logic.Engine.Rules;
using Data.Model.Piece;

namespace Logic.Engine.RuleManager
{
    public class RookRuleGroup : RuleGroup
    {
        public RookRuleGroup()
        {
            Rules.Add(new RookMovementRule());
            Rules.Add(new CanOnlyTakeEnemyRule());
            Rules.Add(new WillNotMakeCheck());
        }

        protected override Type Type => Type.Rook;
    }
}