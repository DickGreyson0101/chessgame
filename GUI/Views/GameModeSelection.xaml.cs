using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Data.Model;
using Data.Model.Piece;
using GUI.Game;
using GUI.ModelView;
using MahApps.Metro.Controls;
using Color = Data.Model.Piece.Color;

namespace GUI.Views
{
    /// <summary>
    /// Interaction logic for GameModeSelection.xaml
    /// </summary>
    public partial class GameModeSelection
    {
        private MainWindow _mainWindow;
        private Container _container;
        public GameModeSelection(Container container, MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _container = container;
        }

        private void TileAiPlay_OnClick(object sender, RoutedEventArgs e)
        {
            _mainWindow.MainControl.Content = new AiOptionSelection(_mainWindow, _container);
        }

        private void TileNetworkPlay_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void LocalGameButton_OnClick(object sender, RoutedEventArgs e)
        {
            GameFactory gameFactory = new GameFactory();
            BoardView boardView = new BoardView(_container);
            Logic.Core.Game game = gameFactory.CreateGame(Mode.Local, _container, boardView, Color.White, null);

            _mainWindow.MainControl.Content = new GameView(_mainWindow, game, boardView);
        }
    }
}
