using System;
using Data.Model;
using Data.Model.Piece;
using Type = Data.Model.Piece.Type;

namespace Data.Command
{
    [Serializable]
    public class PromoteCommand : ICompensableCommand
    {
        private Board _board;
        private ICompensableCommand _moveCommand;
        private Pieces _oldPawn;
        public PromoteCommand(Move move, Board board)
        {
            if (move?.PromotePieceType == null)
                throw new NullReferenceException("Can't build a promote command with null Move.PromotedPriceType");
            _board = board;
            Move = move;

            _moveCommand = new MoveCommand(move, board);
            _oldPawn = board.PieceAt(move.StartCoordinate);
        }

        private PromoteCommand(PromoteCommand promoteCommand, Board board)
        {
            Move = promoteCommand.Move;
            _board = board;
            _moveCommand = promoteCommand._moveCommand.Copy(board);
            _oldPawn = board.PieceAt(Move.StartCoordinate);
        }

        public void Execute()
        {
            _moveCommand.Execute();

            Square square = _board.SquareAt(Move.TargetCoordinate);
            Pieces pieces;
            switch (Move.PromotePieceType)
            {
                case Type.Rook:
                    pieces = new Rook(Move.PieceColor, square);
                    break;
                case Type.Knight:
                    pieces = new Knight(Move.PieceColor, square);
                    break;
                case Type.Bishop:
                    pieces = new Bishop(Move.PieceColor, square);
                    break;
                case Type.King:
                    pieces = new King(Move.PieceColor, square);
                    break;
                case Type.Queen:
                    pieces = new Queen(Move.PieceColor, square);
                    break;
                case Type.Pawn:
                    pieces = new Pawn(Move.PieceColor, square);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            square.Piece = pieces;
        }

        public void Compensate()
        {
            _board.SquareAt(Move.TargetCoordinate).Piece = _oldPawn;
            _moveCommand.Compensate();
        }

        public bool TakePiece => true;
        
        public Move Move { get; }

        public Type PieceType => Move.PieceType;

        public Color PieceColor => Move.PieceColor;

        public ICompensableCommand Copy(Board board) => new PromoteCommand(this, board);

        public override string ToString() =>
            "Promotion to " + Move.PromotePieceType + " from " + Move.TargetCoordinate;
    }
}