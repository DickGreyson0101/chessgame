﻿using System.Collections.Generic;
using System.Linq;
using Data.Model;
using Data.Model.Piece;
using Logic.Engine.States;
using Data.Command;


namespace Logic.Engine.Rules
{
    public class WillNotMakeCheck : IRule
    {
        public bool IsMoveValid(Move move, Board board)
        {
            IState checkState = new CheckState();
            Board tempBoard = new Board(board);

            bool castling = new CastlingRule().IsMoveValid(move, board) && (move.PieceType == Type.King) &&
                            (tempBoard.PieceAt(move.TargetCoordinate)?.Type == Type.Rook) &&
                            (move.PieceColor == tempBoard.PieceAt(move.TargetCoordinate)?.Color);

            if (!castling)
                if (move.PieceColor == tempBoard.PieceAt(move.TargetCoordinate)?.Color)
                    return true;
            ICompensableCommand command = castling
                ? new CastlingCommand(move, tempBoard)
                : new MoveCommand(move, tempBoard) as ICompensableCommand;

            command.Execute();

            return !checkState.IsInState(tempBoard, move.PieceColor);
        }

        public List<Square> PossibleMoves(Pieces piece)
        {
            return
                piece.Square.Board.Squares.OfType<Square>()
                    .ToList()
                    .FindAll(x => IsMoveValid(new Move(piece, x), piece.Square.Board));
        }
    }
}