using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Media;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Input;
using System.Diagnostics.Contracts;

namespace Pairs
{
    public abstract class GameController
    {
       
        private string iconSet;
        private int gameSizeRow;
        private int gameSizeCol;

        protected GameState state = GameState.Running;

        protected List<Card> gameCards;

        private Stack<KeyValuePair<Card, Rectangle>> candidateStack = new Stack<KeyValuePair<Card, Rectangle>>();

        protected Random random = new Random(DateTime.Now.Millisecond);

        protected Grid gameGrid;

        protected SoundController soundController = new SoundController();

        public GameController(Grid gameGrid, GameOptions gameOptions)
        {
            Contract.Requires(gameGrid != null, "gameGrid cannot be null.");
            Contract.Requires(gameOptions != null, "gameOptions cannot be null.");
            Contract.Requires(!String.IsNullOrEmpty(gameOptions.SelectedIconSet), "iconSet cannot be null or empty.");

            this.gameGrid = gameGrid;
            this.iconSet = gameOptions.SelectedIconSet;
            this.gameSizeRow = gameOptions.SelectedGameRowSize;
            this.gameSizeCol = gameOptions.SelectedGameColSize;
        }

       
        protected void PushCardOnCandidateStack(Rectangle cardRectangle)
        {
            candidateStack.Push(new KeyValuePair<Card, Rectangle>((Card)cardRectangle.DataContext, cardRectangle));
        }

        protected int CardsOnStack
        {
            get
            {
                return candidateStack.Count;
            }
        }

        private List<Card> AssignCardsToGameGrid(Grid gameGrid, List<Card> initialCardCollection)
        {
            Contract.Requires(gameGrid != null, "empty game grid not allowed");
            Contract.Requires(initialCardCollection.Count == this.gameSizeCol * this.gameSizeRow, "Must be stacked now");

            List<Card> gameCardCollection = new List<Card>();

            gameGrid.Children.Clear();
            gameGrid.RowDefinitions.Clear();
            gameGrid.ColumnDefinitions.Clear();

            Style style = Application.Current.Resources["RectangleStyle"] as Style;

            for (int row = 0; row < this.gameSizeRow; row++)
            {
                gameGrid.RowDefinitions.Add(new RowDefinition());

                for (int col = 0; col < this.gameSizeCol; col++)
                {
                    if (row == 0)
                    {
                        gameGrid.ColumnDefinitions.Add(new ColumnDefinition());
                    }
                    var rectangle = new Rectangle();
                    rectangle.SetBinding(Rectangle.FillProperty, "ActiveImage");
                    rectangle.Style = style;
                    Grid.SetColumn(rectangle, col);
                    Grid.SetRow(rectangle, row);


                    int randomCardNumber = random.Next(0, initialCardCollection.Count);

                    Card card = initialCardCollection[randomCardNumber];

                    gameCardCollection.Add(card);
                    gameGrid.Children.Add(rectangle);
                    rectangle.DataContext = card;
                    rectangle.MouseLeftButtonUp += rectangleLeftMouseButtonUp;

                    initialCardCollection.RemoveAt(randomCardNumber);
                    Wait(200);
                }
            }

            Contract.Ensures(initialCardCollection.Count == 0, "Must be empty now");

            return gameCardCollection;
        }

        private void rectangleLeftMouseButtonUp(object sender, MouseButtonEventArgs e)
        {

            Rectangle cardRectangle = e.Source as Rectangle;
            if (cardRectangle == null) return;

            PickCard(cardRectangle);
        }

        public void PickCard(Rectangle cardRectangle)
        {

            if (state == GameState.GameOver) return;

            Card card = cardRectangle.DataContext as Card;

            // Check if this card can still be played.
            if (card.Status != CardState.Covered)
            {
                return;
            }

            //Inform 
            OnCardPicked(card);

            soundController.Play(SoundType.Flip);
            FlipCard(cardRectangle);

            PushCardOnCandidateStack(cardRectangle);


            if (CardsOnStack == 2)
            {
                string cardName = candidateStack.Peek().Key.Name;
                bool isMatch = CheckIfCardsOnCandiateStackMatch();

                if (isMatch)
                {
                    OnMatch(cardName);
                }
                else
                {
                    OnMiss();
                }
            }


            if (!gameCards.Exists(c => c.Status == CardState.Covered))
            {
                state = GameState.GameOver;
            }
        }

        protected virtual void OnGameStart()
        {
            if (App.TimerStoryboard != null)
            {
                App.TimerStoryboard.Stop(gameGrid);
               
            }
            //Nothing else to do here. A child can override this.
        }

        protected virtual void OnCardPicked(Card card)
        {
            //Nothing to do here. A child can override this.
        }

        protected virtual void OnMatch(string cardName)
        {
            //Nothing to do here. A child can override this.
        }

        protected virtual void OnMiss()
        {
            //Nothing to do here. A child can override this.
        }

        protected bool CheckIfCardsOnCandiateStackMatch()
        {
            var evalResult = EvalCards();

            if (evalResult.Key) // It is a match
            {
                soundController.Play(SoundType.Match);
                MatchCard(evalResult.Value[0]);
                MatchCard(evalResult.Value[1]);
                return true;
            }
            else // No match
            {
                soundController.Play(SoundType.Flip);
                FlipCard(evalResult.Value[0]);
                FlipCard(evalResult.Value[1]);
                return false;
            }
        }

        protected void Wait(long milliseconds)
        {

            long dtEnd = DateTime.Now.AddTicks(milliseconds * 1000).Ticks;

            while (DateTime.Now.Ticks < dtEnd)
            {
                Grid g = new Grid();
                g.Dispatcher.Invoke(DispatcherPriority.Background, (DispatcherOperationCallback)(unused => null), null);
            }

        }

        protected KeyValuePair<bool, List<Rectangle>> EvalCards()
        {
            var a = candidateStack.Pop();
            var b = candidateStack.Pop();

            if (a.Key.Name == b.Key.Name) // Match Found
            {
                Wait(3500);
                
                return new KeyValuePair<bool, List<Rectangle>>(true, new List<Rectangle>() { a.Value, b.Value });
            }
            else // No Match
            {
                Wait(6000);

                return new KeyValuePair<bool, List<Rectangle>>(false, new List<Rectangle>() { a.Value, b.Value });
            }

        }


        protected void FlipCard(Rectangle cardRectangle)
        {
            Card card = cardRectangle.DataContext as Card;

            FlipCardRectangle(cardRectangle, 1, 0);

            if (card.Status == CardState.Covered)
            {
                card.Uncover();
            }
            else
            {
                card.Cover();
            }

            FlipCardRectangle(cardRectangle, 0, 1);
        }

        protected List<Card> CreateCards()
        {
            List<Card> cards = new List<Card>();

            BitmapImage backgroundImage = GetImage("images/background.jpg");

            for (int x = 1; x <= this.gameSizeRow * this.gameSizeCol / 2; x++)
            {
                BitmapImage frontImage = GetImage(String.Format("images/{1}/R{0}.png", x,iconSet));
                Card a = new Card(x.ToString(), frontImage, backgroundImage);

                frontImage = GetImage(String.Format("images/{1}/R{0}.png", x, iconSet));
                Card b = new Card(x.ToString(), frontImage, backgroundImage);

                cards.Add(a);
                cards.Add(b);
            }

            return cards;
        }


        protected void MatchCard(Rectangle rectangle)
        {
            Card card = rectangle.DataContext as Card;
            card.Match();
        }

        public void StartGame()
        {
            gameGrid.Children.OfType<Rectangle>().ToList().ForEach(rec => rec.DataContext = null);
            soundController.Play(SoundType.Pop);
            List<Card> initialCards = CreateCards();
            gameCards = AssignCardsToGameGrid(gameGrid, initialCards);
            state = GameState.Running;
            //Inform, that game has started.
            OnGameStart();
        }

        private BitmapImage GetImage(string path)
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = Application.GetResourceStream(new Uri(path, UriKind.Relative)).Stream;
            image.EndInit();
            return image;
        }

        private void FlipCardRectangle(Rectangle cardRectangle, int from, int to)
        {
            cardRectangle.RenderTransformOrigin = new Point(0.5, 0.5);
            ScaleTransform flipTrans = new ScaleTransform();
            flipTrans.ScaleY = 1;
            cardRectangle.RenderTransform = flipTrans;

            DoubleAnimation da = new DoubleAnimation();
            da.From = from;
            da.To = to;
            da.Duration = TimeSpan.FromMilliseconds(200);

            flipTrans.BeginAnimation(ScaleTransform.ScaleYProperty, da);
        }
    }
}
