using System.Data;
using Data.Model.Piece;
using Logic.Engine.Rules;

namespace Logic.Engine.RuleManager
{
    public class KnightRuleGroup : RuleGroup
    {
        public KnightRuleGroup()
        {
            Rules.Add(new KnightMovementRule());
            Rules.Add(new CanOnlyTakeEnemyRule());
            Rules.Add(new WillNotMakeCheck());
        }

        protected override Type Type => Type.Knight;
    }
}