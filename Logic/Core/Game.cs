﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Engine;
using Data.Model;
using Data.Model.Piece;

namespace Logic.Core
{
    public class Game
    {
        private Player _currentPlayer;
        private bool _playerMissing;
        private readonly bool _canUndoRedo;
        private Player WhitePlayer { get; }
        private Player BlackPlayer { get; }
        private IEngine Engine { get; }
        public Container Container { get; set; }

        public Game(IEngine engine, Player whitePlayer, Player blackPlayer, Container container, bool canUndoRedo)
        {
            _playerMissing = false;
            _canUndoRedo = canUndoRedo;
            WhitePlayer = whitePlayer;
            BlackPlayer = blackPlayer;
            Engine = engine;
            Container = container;
            WhitePlayer.MoveDone += PlayerMoveHandler;
            BlackPlayer.MoveDone += PlayerMoveHandler;

            _currentPlayer = WhitePlayer;
            OnBoardStateChanged();

            _currentPlayer.Play(null);
        }

        private void PlayerMoveHandler(Player sender, Move move)
        {
            //Si un joueur n'est pas là, on ne peut pas jouer (réseau)
            if (_playerMissing) return;

            if (sender != _currentPlayer)
            {
                sender.Stop();
            }
            else
            {
                if (Engine.DoMove(move))
                {
                    _currentPlayer.Stop();
                    ChangePlayer();
                    OnBoardStateChanged();
                }

                _currentPlayer.Play(move);
            }
        }

        private void ChangePlayer() => _currentPlayer = _currentPlayer == WhitePlayer ? BlackPlayer : WhitePlayer;

        public List<Square> PossibleMoves(Pieces piece) => Engine.PossibleMoves(piece);

        #region Undo Redo

        /// <summary>
        /// Demande au moteur d'annuler le dernier coup joué
        /// </summary>
        public void Undo()
        {
            if (!_canUndoRedo) return;

            Move move = Engine.Undo();
            if (move == null) return;

            _currentPlayer.Stop();
            ChangePlayer();
            OnBoardStateChanged();
            _currentPlayer.Play(null);
        }

        public void Undo(int count)
        {
            if (!_canUndoRedo) return;

            Move lastMove = null;
            _currentPlayer.Stop();
            for (int i = 0; i < count; i++)
            {
                Move move = Engine.Undo();
                if (move != null)
                {
                    ChangePlayer();
                    lastMove = move;
                }
            }
            _currentPlayer.Play(lastMove);
            if (lastMove != null)
                OnBoardStateChanged();
        }

        /// <summary>
        /// Demande au moteur de refaire le dernier coup annulé
        /// </summary>
        public void Redo()
        {
            if (!_canUndoRedo) return;

            Move move = Engine.Redo();
            if (move == null) return;

            _currentPlayer.Stop();
            ChangePlayer();
            StateChanged?.Invoke(Engine.CurrentState());
            _currentPlayer.Play(null);
        }

        #endregion

        public void PlayerLeave(Player player, string reason)
        {
            _playerMissing = true;
            PlayerDisconnectedEvent?.Invoke("Le joueur " + (player.Color == Color.White ? "Blanc" : "Noir") + " s'est déconnecté de la partie, si vous voulez reprendre la partie plus tard vous pouvez l'enregistrer...\n\n(" + reason + ")");
        }


        #region Delegate and Events

        public delegate void StateHandler(BoardState state);
        public event StateHandler StateChanged;
        private void OnBoardStateChanged() => StateChanged?.Invoke(Engine.CurrentState());

        public delegate void PlayerDisconnectedEventHandler(string message);
        public event PlayerDisconnectedEventHandler PlayerDisconnectedEvent;

        #endregion
    }
}
