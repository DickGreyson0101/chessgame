using System.Collections.Generic;
using Data.Model;
using Data.Model.Piece;
using GUI.ModelView;
using Logic.Core;

namespace GUI.Game
{
    public class BoardViewPlayerController : PlayerControler
    {
        private BoardView _boardView;

        public BoardViewPlayerController(BoardView boardView)
        {
            _boardView = boardView;
        }

        public bool IsPlayable { get; set; }

        public override void Play(Move move)
        {
            IsPlayable = true;
        }

        public override void Move(Move move)
        {
            IsPlayable = false;
            Player.Move(move);
        }

        public override void InvalidMove(List<string> reasonsList)
        {
            IsPlayable = true;
            //TODO 
        }

        public override List<Square> PossibleMoves(Pieces piece)
        {
            return Player.PossibleMoves(piece);
        }

        public override void Stop()
        {
            IsPlayable = false;
        }
    }
}