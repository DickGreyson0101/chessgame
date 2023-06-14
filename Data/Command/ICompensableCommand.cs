using Data.Model;
using Data.Model.Piece;

namespace Data.Command
{
    public interface ICompensableCommand
    {
        bool TakePiece { get; }

        Move Move { get; }

        Type PieceType { get; }

        Color PieceColor { get; }
        void Execute();

        void Compensate();

        ICompensableCommand Copy(Board board);

        string ToString();
    }
}
