using System;
using System.Windows.Media;
using Data.Model.Piece;
using Color = Data.Model.Piece.Color;
using Type = Data.Model.Piece.Type;

namespace GUI.ModelView
{
    /// <summary>
    /// Interaction logic for PieceView.xaml
    /// </summary>
    public partial class PieceView
    {
        private Pieces _piece;
        public PieceView(Pieces piece)
        {
            InitializeComponent();
            Piece = piece;
        }

        public Pieces Piece
        {
            get { return _piece; }
            set
            {
                _piece = value;
                switch (Piece.Type)
                {
                    case Type.Bishop:
                        Image.Source =
                            (Piece.Color == Color.Black ? FindResource("BlackBishop") : FindResource("WhiteBishop")) as
                                ImageSource;
                        break;
                    case Type.King:
                        Image.Source =
                            (Piece.Color == Color.Black ? FindResource("BlackKing") : FindResource("WhiteKing")) as
                                ImageSource;
                        break;
                    case Type.Queen:
                        Image.Source =
                            (Piece.Color == Color.Black ? FindResource("BlackQueen") : FindResource("WhiteQueen")) as
                                ImageSource;
                        break;
                    case Type.Pawn:
                        Image.Source =
                            (Piece.Color == Color.Black ? FindResource("BlackPawn") : FindResource("WhitePawn")) as
                                ImageSource;
                        break;
                    case Type.Knight:
                        Image.Source =
                            (Piece.Color == Color.Black ? FindResource("BlackKnight") : FindResource("WhiteKnight")) as
                                ImageSource;
                        break;
                    case Type.Rook:
                        Image.Source =
                            (Piece.Color == Color.Black ? FindResource("BlackRook") : FindResource("WhiteRook")) as
                                ImageSource;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
