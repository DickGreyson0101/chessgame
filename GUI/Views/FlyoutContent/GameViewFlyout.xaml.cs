using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Data.IO;
using Microsoft.Win32;

namespace GUI.Views.FlyoutContent
{
    /// <summary>
    /// Interaction logic for GameViewFlyout.xaml
    /// </summary>
    public partial class GameViewFlyout
    {
        private GameView _gameView;
        public GameViewFlyout(GameView gameView)
        {
            InitializeComponent();
            _gameView = gameView;
        }

        private void TileSave_OnClick(object sender, RoutedEventArgs e)
        {
            ISaver saver = new BinarySaver();
            string directorySaveName = "Save";
            string fullSavePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" +
                                  directorySaveName;
            Console.WriteLine(fullSavePath);
            if (Directory.Exists(fullSavePath) == false) Directory.CreateDirectory(fullSavePath);
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = saver.Filter(),
                InitialDirectory = fullSavePath
            };
            if (saveFileDialog.ShowDialog() == true) saver.Save(_gameView.Game.Container, saveFileDialog.FileName);
        }

        private async void TileQuit_OnClick(object sender, RoutedEventArgs e)
        {
            await _gameView.Quit();
        }
    }
}
