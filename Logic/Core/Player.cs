using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Model;
using Data.Model.Piece;

namespace Logic.Core
{
    public class Player
    {
        public Color Color { get; internal set; }

        private PlayerControler _playerControler;

        public Game Game { get; set; }

        public Player(Color color, PlayerControler playerControler)
        {
            Color = color;
            _playerControler = playerControler;
        }

        public void Play(Move move) => _playerControler.Play(move);

        public void Stop() => _playerControler.Stop();

        public List<Square> PossibleMoves(Pieces piece) => Game.PossibleMoves(piece);

        public void Move(Move move)
        {
            MoveDone?.Invoke(this, move);
        }

        public void LeaveGame(string reason)
        {
            Game.PlayerLeave(this, reason);
        }

        public delegate void MoveHandler(Player sender, Move move);
        public event MoveHandler MoveDone;
    }
}
