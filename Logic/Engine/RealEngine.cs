using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Data.Model;
using Data.Command;
using Data.Model.Piece;
using Logic.Engine.RuleManager;
using Logic.Engine.States;
using Type = Data.Model.Piece.Type;

namespace Logic.Engine
{
    public class RealEngine : IEngine
    {
        private Container _container;
        private CompensableConvo _convo;
        private Pawn _enPassantPawnBlack;
        private Pawn _enPassantPawnWhite;
        private ObservableCollection<ICompensableCommand> _moves;
        private RuleGroup _ruleGroups;

        /// <summary>
        ///     RealEngine constructor
        /// </summary>
        /// <param name="container"></param>
        public RealEngine(Container container)
        {
            Board = container.Board;
            _container = container;
            _moves = container.Moves;

            _convo = new CompensableConvo(container.Moves);

            _ruleGroups = new PawnRuleGroup();
            _ruleGroups.AddGroup(new RookRuleGroup());
            _ruleGroups.AddGroup(new KnightRuleGroup());
            _ruleGroups.AddGroup(new BishopRuleGroup());
            _ruleGroups.AddGroup(new KingRuleGroup());
            _ruleGroups.AddGroup(new QueenRuleGroup());
        }

        public Board Board { get; }

        public bool DoMove(Move move)
        {
            //No need to move if it's the same square
            if (move.StartCoordinate == move.TargetCoordinate)
                return false;

            Pieces piece = Board.PieceAt(move.StartCoordinate);
            Pieces targetPiece = Board.PieceAt(move.TargetCoordinate);

            //TODO handle exception
            if (_ruleGroups.Handle(move, Board))
            {
                ICompensableCommand command;
                if ((move.PieceType == Type.King) &&
                    (((targetPiece?.Type == Type.Rook) && (move.PieceColor == targetPiece.Color))
                     || (Math.Abs(move.TargetCoordinate.X - move.StartCoordinate.X) == 2)))
                    command = new CastlingCommand(move, Board);
                else if ((move.PieceType == Type.Pawn) && (targetPiece == null) &&
                         (move.StartCoordinate.X != move.TargetCoordinate.X))
                    command = new EnPassantCommand(move, Board);
                else if ((move.PieceType == Type.Pawn) &&
                         (move.TargetCoordinate.Y == (move.PieceColor == Color.White ? 0 : 7)))
                    command = new PromoteCommand(move, Board);
                else
                    command = new MoveCommand(move, Board);

                //En passant
                if (move.PieceColor == Color.White)
                {
                    if (_enPassantPawnWhite != null)
                    {
                        _enPassantPawnWhite.EnPassant = false;
                        _enPassantPawnWhite = null;
                    }
                }
                else
                {
                    if (_enPassantPawnBlack != null)
                    {
                        _enPassantPawnBlack.EnPassant = false;
                        _enPassantPawnBlack = null;
                    }
                }
                if ((move.PieceType == Type.Pawn) && (Math.Abs(move.StartCoordinate.Y - move.TargetCoordinate.Y) == 2))
                    if (move.PieceColor == Color.White)
                    {
                        _enPassantPawnWhite = (Pawn)piece;
                        _enPassantPawnWhite.EnPassant = true;
                    }
                    else
                    {
                        _enPassantPawnBlack = (Pawn)piece;
                        _enPassantPawnBlack.EnPassant = true;
                    }

                //Number of moves since last capture
                if (Board.PieceAt(move.TargetCoordinate) == null)
                    _container.HalfMoveSinceLastCapture++;
                else
                    _container.HalfMoveSinceLastCapture = 0;

                _convo.Execute(command);
                _moves.Add(command);

                return true;
            }

            return false;
        }
        public BoardState CurrentState()
        {
            IState checkState = new CheckState();
            IState patState = new PatState();


            Color color = _moves.Count == 0 ? Color.White : _moves[_moves.Count - 1].PieceColor;

            bool check = checkState.IsInState(Board, color == Color.White ? Color.Black : Color.White);

            bool pat = patState.IsInState(Board, color == Color.White ? Color.Black : Color.White);

            if (pat && check)
                return color == Color.Black ? BoardState.WhiteCheckMate : BoardState.BlackCheckMate;
            if (pat)
                return color == Color.Black ? BoardState.WhitePat : BoardState.BlackPat;
            if (check)
                return color == Color.Black ? BoardState.WhiteCheck : BoardState.BlackCheck;

            return BoardState.Normal;
        }

        public List<Square> PossibleMoves(Pieces piece)
        {
            return _ruleGroups.PossibleMoves(piece);
        }

        /// <summary>
        ///     Undo the last command that has been done
        /// </summary>
        /// <returns>True if anything has been done</returns>
        public Move Undo()
        {
            ICompensableCommand command = _convo.Undo();
            if (command == null) return null;

            if (_container.HalfMoveSinceLastCapture != 0)
                _container.HalfMoveSinceLastCapture--;
            else
            {
                int count = 0;
                for (int i = _moves.Count - 1; i > 0; i--)
                    if (!_moves[i].TakePiece)
                        count++;
                    else
                        break;
                _container.HalfMoveSinceLastCapture = count;
            }

            _moves.Remove(command);
            return command.Move;
        }

        /// <summary>
        ///     Redo the last command that has been undone
        /// </summary>
        /// <returns>True if anything has been done</returns>
        public Move Redo()
        {
            ICompensableCommand command = _convo.Redo();
            if (command == null) return null;

            //Number of moves since last capture
            if (!command.TakePiece)
                _container.HalfMoveSinceLastCapture++;
            else
                _container.HalfMoveSinceLastCapture = 0;

            _moves.Add(command);
            return command.Move;
        }
    }
}