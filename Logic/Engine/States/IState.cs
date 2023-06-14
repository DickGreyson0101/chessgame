using Data.Model;
using Data.Model.Piece;

namespace Logic.Engine.States
{
    public interface IState
    {
        /// <summary>
        ///     Check whether or not a tray is in the state defined by the class
        /// </summary>
        /// <param name="board"></param>
        /// <param name="color"></param>
        /// <returns> True if the tray is in the state defined by the class</returns>
        bool IsInState(Board board, Color color);

        /// <summary>
        ///     Explain the condition in a short sentence
        /// </summary>
        /// <returns></returns>
        string Explain();
    }
}