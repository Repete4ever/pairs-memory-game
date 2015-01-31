using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Pairs
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameOptions gameOptions = new GameOptions();
        private GameController gameController;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void rectangleLeftMouseButtonUp(object sender, MouseButtonEventArgs e)
        {

            Rectangle cardRectangle = e.Source as Rectangle;
            if (cardRectangle == null) return;

            gameController.PickCard(cardRectangle);
            

        }

        private void OptionsCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OptionWindow optionWindow = new OptionWindow();
            optionWindow.DataContext = gameOptions;
            optionWindow.ShowDialog();
        }

        private void NewGameSinglePlayerCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            borderPlayer1.DataContext = null;
            borderPlayer2.DataContext = null;
            var gameController = new SinglePlayerGameController(gameGrid, gameOptions);
            this.gameController = gameController;
            gameController.StartGame();
        }

        private void NewGameTwoPlayerCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TwoPlayerGameController gameController = new TwoPlayerGameController(gameGrid, gameOptions);
            borderPlayer1.DataContext = gameController.Player1;
            borderPlayer2.DataContext = gameController.Player2;
            this.gameController = gameController;
            gameController.StartGame();
        }

        private void NewGameAgainstComputerCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AgainstComputerGameController gameController = new AgainstComputerGameController(gameGrid, gameOptions);
            borderPlayer1.DataContext = gameController.Player1;
            borderPlayer2.DataContext = gameController.Player2;
            this.gameController = gameController;
            gameController.StartGame();
        }
    }
}
