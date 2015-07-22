using System.ComponentModel;

namespace Pairs
{
    public class Player : INotifyPropertyChanged
    {
        private int score;
        private bool active;

        public string Name { get; set; }


        public int Score
        {
            get { return score; }
            set 
            {
                score = value;
                RaiseNotifyPropertyChanged("Score");
            }
        }

        /// <summary>
        /// Indicates whether this player is currently allowed to draw cards.
        /// </summary>
        public bool IsActive
        {
            get
            {
                return active;
            }

            set
            {
                active = value;
                RaiseNotifyPropertyChanged("IsActive");
            }
            
        }


        private void RaiseNotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
