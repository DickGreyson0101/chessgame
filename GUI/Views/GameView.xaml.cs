using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using Logic.Engine;
using GUI.ModelView;
using Data.Model.Piece;
using GUI.Views.FlyoutContent;
using GUI.Views.Widgets;

namespace GUI.Views
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView
    {
        private BoardView _boardView;
        private MainWindow _mainWindow;
        public GameView(MainWindow mainWindow, Logic.Core.Game game, BoardView boardView)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _boardView = boardView;
            Game = game;

            game.PlayerDisconnectedEvent += GameOnPlayerDisconnectedEvent;

            game.StateChanged += _boardView.GameStateChanged;

            game.StateChanged += state =>
            {
                switch (state)
                {
                    case BoardState.BlackCheckMate:
                        _mainWindow.ShowMessageAsync("Game over", "The black player is checkmate",
                            MessageDialogStyle.AffirmativeAndNegative);
                        break;
                    case BoardState.WhiteCheckMate:
                        _mainWindow.ShowMessageAsync("Game over", "The white player is checkmate.",
                            MessageDialogStyle.AffirmativeAndNegative);
                        break;
                    case BoardState.BlackPat:
                        _mainWindow.ShowMessageAsync("Draw", "The black player is dead.",
                            MessageDialogStyle.AffirmativeAndNegative);
                        break;
                    case BoardState.WhitePat:
                        _mainWindow.ShowMessageAsync("Draw", "The white player is dead.",
                            MessageDialogStyle.AffirmativeAndNegative);
                        break;
                }
            };

            GameViewFlyout gameViewFlyout = new GameViewFlyout(this);
            _mainWindow.Flyout.Content = gameViewFlyout.Content;
            UcBoardView.Content = boardView;

            game.Container.MoveDone += move =>
            {
                LabelPlayerTurn.Content = move.PieceColor == Color.Black ? "White" : "Black";
            };

            game.Container.MoveUndone += move =>
            {
                LabelPlayerTurn.Content = move.PieceColor == Color.Black ? "White" : "Black";
            };

            HistoryView.Content = new HistoryView(this);
        }

        private void GameOnPlayerDisconnectedEvent(string message)
        {
            _mainWindow.ShowMessageAsync("Error : Disconnecting a player", message);
        }

        public Logic.Core.Game Game { get; set; }

        private void ButtonUndo_OnClick(object sender, RoutedEventArgs e) => Game.Undo();
        private void ButtonRedo_OnClick(object sender, RoutedEventArgs e) => Game.Redo();

        #region Flyout
        private void Grid_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!_mainWindow.Flyout.IsOpen) return;
            _mainWindow.Flyout.IsOpen = false;
        }

        private void ButtonMenu_OnClick(object sender, RoutedEventArgs e)
        {
            if (_mainWindow.Flyout.IsOpen) return;
            _mainWindow.Flyout.IsOpen = true;
        }

        public async Task Quit()
        {
            _mainWindow.Flyout.IsOpen = false;

            var result =
                await
                    _mainWindow.ShowMessageAsync("Leave the game",
                        "Do you really want to leave the game ? If your game is not saved, it will be lost...",
                        MessageDialogStyle.AffirmativeAndNegative);
            if (result == MessageDialogResult.Affirmative)
            {
                _mainWindow.Flyout.Content = null;
                _mainWindow.MainControl.Content = new Home(_mainWindow);
            }
        }

        #endregion
    }
}
