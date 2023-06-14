using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace GUI.ModelView
{
    /// <summary>
    /// Interaction logic for SquareView.xaml
    /// </summary>
    public partial class SquareView : UserControl
    {
        public SquareView(Square square)
        {
            InitializeComponent();
            Square = square;
            DataContext = this;
            Square.PropertyChanged += SquarePropertyChangeHandler;

            if (square.Piece != null)
                PieceView = new PieceView(square.Piece);

            SetResourceReference(BackgroundProperty,
                (square.X + square.Y) % 2 == 0 ? "MahApps.Brushes.Accent" : "MahApps.Brushes.Accent4");

            Grid.SetColumn(this, square.X);
            Grid.SetRow(this, square.Y);
        }

        public PieceView PieceView
        {
            get { return UcPieceView.Content as PieceView; }
            set { UcPieceView.Content = value; }
        }

        public Square Square { get; set; }

        private void SquarePropertyChangeHandler(object sender, PropertyChangedEventArgs e)
        {
            PieceView = Square.Piece != null ? new PieceView(Square.Piece) : null;
            UcPieceView.Content = PieceView;
        }

        public override string ToString() => Square.ToString();
    }
}