using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Pairs
{
    public class AgainstComputerGameController : GameController
    {
        private readonly List<Card> cardMemory = new List<Card>();

        private readonly List<Player> players = new List<Player>();

        public AgainstComputerGameController(Grid gameGrid, GameOptions gameOptions)
            : base(gameGrid, gameOptions)
        {
            players.Clear();
            players.Add(new Player() { Name = "Human" });
            players.Add(new Player() { Name = "Computer"});
            players[0].IsActive = true;
        }

        public Player Player1 => players[0];

        public Player Player2 => players[1];

        protected override void OnCardPicked(Card card)
        {
            if (cardMemory.Contains(card)) return;

            if (cardMemory.Count() > 6)
            {
                cardMemory.RemoveAt(random.Next(0, cardMemory.Count));
            }

            cardMemory.Add(card);
        }

        protected override void OnMatch(string cardName)
        {
            RemoveCardsFromMemory(cardName);
            
            GivePoints();

            if (Player2.IsActive)
            {
                SimulateMove();
            }

        }

        private void RemoveCardsFromMemory(string cardName)
        {
            cardMemory.RemoveAll(card => card.Name == cardName);
           
        }

        protected override void OnMiss()
        {
            SwitchPlayer();

            if (Player2.IsActive)
            {
                SimulateMove();
            }
        }

        private void GivePoints()
        {
            players.Single(player => player.IsActive).Score++;
        }

        private void SwitchPlayer()
        {
            players.ForEach(player => player.IsActive = !player.IsActive);
        }

        private void SimulateMove()
        {
            if (!gameCards.Exists(c => c.Status == CardState.Covered))
            {
                state = GameState.GameOver;
            }

            if (state == GameState.GameOver) return;

            Card cardA;
            Card cardB;
            
            var potentialCollection = gameCards.Where(c => c.Status == CardState.Covered).ToList();

            IGrouping<string, Card> memoryMatch = CheckMemoryForMatch();

            if (memoryMatch == null)
            {
                //
                //Pick random card (CARD A)
                //
                int posA = random.Next(0, potentialCollection.Count());
                cardA = potentialCollection.ElementAt(posA);
                if (!cardMemory.Contains(cardA))
                {
                    cardMemory.Add(cardA);
                }
                memoryMatch = CheckMemoryForMatch();

                if (memoryMatch == null)
                {
                    //
                    //Pick random card (CARD B)
                    //

                    potentialCollection.RemoveAt(posA); //Avoid picking cardA again

                    int posB = random.Next(0, potentialCollection.Count());
                    cardB = potentialCollection.ElementAt(posB);
                }
                else
                {
                    cardA = memoryMatch.ElementAt(0);
                    cardB = memoryMatch.ElementAt(1);
                }

            }
            else
            {
                cardA = memoryMatch.ElementAt(0);
                cardB = memoryMatch.ElementAt(1);
            }

            

            var allRectangles = gameGrid.Children.OfType<Rectangle>().ToList();
            var recA = allRectangles.Single(rec => rec.DataContext == cardA);
            var recB = allRectangles.Single(rec => rec.DataContext == cardB);

            Wait(6800);
            PickCard(recA);
            Wait(4800);
            PickCard(recB);
        }



        private IGrouping<string, Card> CheckMemoryForMatch()
        {
            IGrouping<string,Card> result = cardMemory.GroupBy(c => c.Name).FirstOrDefault(g => g.Count() > 1);

            return result;
        }
    }
}
