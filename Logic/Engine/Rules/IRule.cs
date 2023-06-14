using System.Collections.Generic;
using Data.Model;
using Data.Model.Piece;

namespace Logic.Engine.Rules
{
    public interface IRule
    {
        /// <summary>
        ///     Check if a move is correct against a rule
        /// </summary>
        /// <param name="move">Move to check</param>
        /// <param name="board">Board to apply the move on</param>
        /// <returns>False if the move is invalidated by this rule</returns>
        bool IsMoveValid(Move move, Board board);

        /// <summary>
        ///     Collect all squares that meet the rule for the given piece
        /// </summary>
        /// <param name="piece">Part that performs the movement</param>
        /// <returns> List of boxes for which the rule is checked </returns>
        List<Square> PossibleMoves(Pieces piece);
    }
}