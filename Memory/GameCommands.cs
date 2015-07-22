using System.Windows.Input;

namespace Pairs
{
    public static class GameCommands
    {
        private static RoutedUICommand newGameSinglePlayer;
        private static RoutedUICommand newGameTwoPlayer;
        private static RoutedUICommand newGameAgainstComputer;
        private static RoutedUICommand options;

        static GameCommands()
        {
            CreateNewGameSinglePlayerCommand();

            CreateNewGameTwoPlayerCommand();

            CreateNewGameAgainstComputerCommand();

            CreateOptionsCommand();
        }

        private static void CreateNewGameAgainstComputerCommand()
        {
            InputGestureCollection inputs = new InputGestureCollection {new KeyGesture(Key.F4)};
            newGameAgainstComputer = new RoutedUICommand("New Game (Against Computer)", "NewGameAgainstComputer", typeof(GameCommands), inputs);
        }

        private static void CreateOptionsCommand()
        {
            InputGestureCollection inputs = new InputGestureCollection {new KeyGesture(Key.F5)};
            options = new RoutedUICommand("Options", "Options", typeof(GameCommands), inputs);
        }

        private static void CreateNewGameTwoPlayerCommand()
        {
            InputGestureCollection inputs = new InputGestureCollection {new KeyGesture(Key.F2)};
            newGameSinglePlayer = new RoutedUICommand("New Game (Single Player)", "NewGameSinglePlayer", typeof(GameCommands), inputs);
        }

        private static void CreateNewGameSinglePlayerCommand()
        {
            InputGestureCollection inputs = new InputGestureCollection {new KeyGesture(Key.F3)};
            newGameTwoPlayer = new RoutedUICommand("New Game (Two Player)", "NewGameTwoPlayer", typeof(GameCommands), inputs);
        }

        public static RoutedUICommand Options => options;

        public static RoutedUICommand NewGameSinglePlayer => newGameSinglePlayer;

        public static RoutedUICommand NewGameTwoPlayer => newGameTwoPlayer;

        public static RoutedUICommand NewGameAgainstComputer => newGameAgainstComputer;
    }
}
