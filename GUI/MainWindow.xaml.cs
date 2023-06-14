using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ControlzEx.Theming;
using MahApps.Metro;
using MahApps.Metro.Controls.Dialogs;
using Data.IO;
using GUI.Views;
using MahApps.Metro.Controls;
using Container = Data.Model.Container;
using GameModeSelection = GUI.Views.GameModeSelection;
using Home = GUI.Views.Home;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void MenuItemQuit_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            //TODO has to do with the logger
            if (File.Exists("log.temp"))
                File.Delete("log.temp");
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (Environment.GetCommandLineArgs().Length != 1)
            {
                ILoader loader = new BinaryLoader();
                Container container = null;

                try
                {
                    container = loader.Load(Environment.GetCommandLineArgs()[1]);
                }
                catch (Exception)
                {
                    this.ShowMessageAsync("Unable to read the selected file",
                        Environment.GetCommandLineArgs()[1]);
                }

                if (container != null)
                    MainControl.Content = new GameModeSelection(container, this);
                else
                    MainControl.Content = new Home(this);
            }
            else
                MainControl.Content = new Home(this);
        }
    }
}


public class SimpleCommand : ICommand
{
    public Predicate<object> CanExecuteDelegate { get; set; }
    public Action<object> ExecuteDelegate { get; set; }

    public bool CanExecute(object parameter)
    {
        if (CanExecuteDelegate != null)
            return CanExecuteDelegate(parameter);
        return true;
    }

    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public void Execute(object parameter)
    {
        ExecuteDelegate?.Invoke(parameter);
    }
}
