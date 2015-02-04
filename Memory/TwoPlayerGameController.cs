using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Pairs
{
    public class TwoPlayerGameController : GameController
    {
        private readonly List<Player> players = new List<Player>();

        public TwoPlayerGameController(Grid gameGrid, GameOptions gameOptions)
            : base(gameGrid, gameOptions)
        {
            players.Clear();
            players.Add(new Player() { Name = "Player 1" });
            players.Add(new Player() { Name = "Player 2" });
            players[0].IsActive = true;
        }

        public Player Player1
        {
            get
            {
                return players[0];
            }
        }

        public Player Player2
        {
            get
            {
                return players[1];
            }
        }

        protected override void OnMatch(string cardName)
        {
            GivePoints();
        }

        protected override void OnMiss()
        {
            SwitchPlayer();
        }

        private void GivePoints()
        {
            players.Single(player => player.IsActive).Score++;
        }

        private void SwitchPlayer()
        {
            players.ForEach(player => player.IsActive = !player.IsActive);
        }


    }
}
