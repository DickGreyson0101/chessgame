using Data.Model;
using Data.Model.Piece;
using GUI.ModelView;
using Logic.Core;
using Logic.Engine;

namespace GUI.Game
{
    public class LocalGameCreator : GameCreator
    {
        public override Mode Mode => Mode.Local;

        public override Logic.Core.Game CreateGame(Container container, BoardView boardView, Color color, GameCreatorParameters parameters)
        {
            IEngine engine = new RealEngine(container);
            PlayerControler whitePlayerController = new BoardViewPlayerController(boardView);
            PlayerControler blackPlayerController = new BoardViewPlayerController(boardView);
            Player whitePlayer = new Player(Color.White, whitePlayerController);
            Player blackPlayer = new Player(Color.Black, blackPlayerController);

            Logic.Core.Game game = new Logic.Core.Game(engine, whitePlayer, blackPlayer, container, true);

            whitePlayer.Game = game;
            blackPlayer.Game = game;

            whitePlayerController.Player = whitePlayer;
            blackPlayerController.Player = blackPlayer;

            boardView.BoardViewPlayerControllers.Add((BoardViewPlayerController)whitePlayerController);
            boardView.BoardViewPlayerControllers.Add((BoardViewPlayerController)blackPlayerController);

            //TODO Remvoe the logger
            //SMTPLogger smtpLogger = new SMTPLogger(game);
            return game;
        }
    }
}