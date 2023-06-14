using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Data.IO;
using Data.Model;
using Microsoft.Win32;

namespace GUI.Views
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home
    {
        private MainWindow _mainWindow;
        public Home(MainWindow mw)
        {
            InitializeComponent();
            _mainWindow = mw;

        }

        private void CreateNewGameButton_OnClick(object sender, RoutedEventArgs e)
        {
            _mainWindow.MainControl.Content = new GameModeSelection(new Container(), _mainWindow);
        }

        private void UseSaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            ILoader loader = new BinaryLoader();

            const string directorySaveName = "Save";
            string fullSavePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" +
                                  directorySaveName;
            if (!Directory.Exists(fullSavePath))
                Directory.CreateDirectory(fullSavePath);

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = loader.Filter(),
                InitialDirectory = fullSavePath
            };

            if (openFileDialog.ShowDialog() != true) return;

            Container container = loader.Load(openFileDialog.FileName);

            _mainWindow.MainControl.Content = new GameModeSelection(container, _mainWindow);
        }

        private void ContributeButton_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start("cmd", "/c start http://www.google.com");
        }
    }
}
