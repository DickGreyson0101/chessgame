﻿using System;
using Data.Model;
using Data.Model.Piece;
using Type = Data.Model.Piece.Type;

namespace Data.Command
{
    [Serializable]
    public class CastlingCommand : ICompensableCommand
    {
        private ICompensableCommand _kingCommand;
        private ICompensableCommand _rookCommand;

        public CastlingCommand(Move move, Board board)
        {
            Move = move;

            bool isLeftCastling = move.TargetCoordinate.X < move.StartCoordinate.X;

            _kingCommand =
                new MoveCommand(
                    new Move(board.PieceAt(move.StartCoordinate),
                        board.Squares[isLeftCastling ? 2 : 6, move.StartCoordinate.Y]), board);
            _rookCommand =
                new MoveCommand(
                    new Move(board.PieceAt(move.StartCoordinate),
                        board.Squares[isLeftCastling ? 3 : 5, move.StartCoordinate.Y]), board);
        }

        private CastlingCommand(CastlingCommand command, Board board)
        {
            Move = command.Move;

            _rookCommand = command._rookCommand.Copy(board);
            _kingCommand = command._kingCommand.Copy(board);
        }

        public void Execute()
        {
            _rookCommand.Execute();
            _kingCommand.Execute();
        }

        public void Compensate()
        {
            _kingCommand.Compensate();
            _rookCommand.Compensate();
        }

        public bool TakePiece => false;

        public Move Move { get; }

        public Type PieceType => Move.PieceType;

        public Color PieceColor => Move.PieceColor;

        public ICompensableCommand Copy(Board board) => new CastlingCommand(this, board);

        public override string ToString() => "Roc vers tour " + Move.TargetCoordinate;
    }
}