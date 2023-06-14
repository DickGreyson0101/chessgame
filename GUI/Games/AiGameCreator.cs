using Logic.Core;
using Logic.Engine;
using Logic.IA;
using Data.Model.Piece;
using Data.Model;
using GUI.ModelView;

namespace GUI.Game
{
    public class AiGameCreator : GameCreator
    {
        public override Mode Mode => Mode.AI;

        public override Logic.Core.Game CreateGame(Container container, BoardView boardView, Color color, GameCreatorParameters parameters)
        {
            IEngine engine = new RealEngine(container);
            PlayerControler whitePlayerController = new BoardViewPlayerController(boardView);
            PlayerControler blackPlayerController = new UciProcessController(container, parameters.AiSearchType, parameters.AiSkillLevel, parameters.AiSearchValue);
            Player whitePlayer = new Player(Color.White, whitePlayerController);
            Player blackPlayer = new Player(Color.Black, blackPlayerController);

            Logic.Core.Game game = new Logic.Core.Game(engine, whitePlayer, blackPlayer, container, true);

            whitePlayer.Game = game;
            blackPlayer.Game = game;

            whitePlayerController.Player = whitePlayer;
            blackPlayerController.Player = blackPlayer;

            boardView.BoardViewPlayerControllers.Add((BoardViewPlayerController)whitePlayerController);

            //TODO Remvoe the logger
            //SMTPLogger smtpLogger = new SMTPLogger(game);
            return game;
        }
    }
}