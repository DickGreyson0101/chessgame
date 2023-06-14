using System;
using Data.Model;
using Data.Model.Piece;
using Type = Data.Model.Piece.Type;

namespace Data.Command
{
    [Serializable]
    public class MoveCommand : ICompensableCommand
    {
        private Board _board;
        private bool _hasChangedState;
        private Pieces _piece;
        private Pieces _removedPiece;
        private Square _startSquare;
        private Square _targetSquare;


        public MoveCommand(Move move, Board board)
        {
            Move = move;
            _board = board;

            TakePiece = board.PieceAt(Move.TargetCoordinate) != null;
        }

        public MoveCommand(MoveCommand command, Board board)
        {
            _board = board;
            Move = command.Move;

            TakePiece = board.PieceAt(Move.TargetCoordinate) != null;
        }

        public void Execute()
        {
            _targetSquare = _board.SquareAt(Move.TargetCoordinate);
            _startSquare = _board.SquareAt(Move.StartCoordinate);
            _piece = _startSquare.Piece;

            if (!_piece.HasMoved)
            {
                _piece.HasMoved = true;
                _hasChangedState = true;
            }

            if (_targetSquare.Piece == null)
            {
                _startSquare.Piece = null;
                _piece.Square = _targetSquare;
                _targetSquare.Piece = _piece;
            }
            else
            {
                _removedPiece = _targetSquare.Piece;
                _targetSquare.Piece = null;
                _piece.Square.Piece = null;
                _piece.Square = _targetSquare;
                _targetSquare.Piece = _piece;
            }
        }

        public void Compensate()
        {
            if (_hasChangedState)
            {
                _piece.HasMoved = false;
            }

            _targetSquare.Piece = _removedPiece;
            _startSquare.Piece = _piece;
            _piece.Square = _startSquare;
        }

        public bool TakePiece { get; }

        public Move Move { get; }

        public Type PieceType => Move.PieceType;

        public Color PieceColor => Move.PieceColor;

        public ICompensableCommand Copy(Board board) => new MoveCommand(this, board);

        public override string ToString() =>
            _piece + " of " + Move.StartCoordinate + " toward " + Move.TargetCoordinate;
    }
}