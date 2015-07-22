using System;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Pairs
{
    public class Card : INotifyPropertyChanged
    {

        private readonly BitmapImage frontImage;
        private readonly BitmapImage backImage;


        public Card (string name, BitmapImage frontImage, BitmapImage backImage)
	    {
            this.Name = name;
            this.frontImage = frontImage;
            this.backImage = backImage;

            this.Status = CardState.Covered;
	    }

        public string Name { get; }

        public CardState Status { get; set; }

        public Brush ActiveImage
        {
            get
            {
                if (this.Status == CardState.Covered)
                {
                    var brush = new ImageBrush(backImage) {Stretch = Stretch.Uniform};
                    return brush;
                }
                
                if (this.Status == CardState.Uncovered)
                {
                    var brush = new ImageBrush(frontImage) {Stretch = Stretch.Uniform};
                    return brush;
                }

                if (this.Status == CardState.Matched)
                {
                    return new SolidColorBrush(Colors.LightBlue);
                }

                throw new InvalidOperationException("Invalid Card State.");
            }
        }

        public void Uncover()
        {
            this.Status = CardState.Uncovered;
            RaiseNotifyChanged("ActiveImage");
        }

        public void Cover()
        {
            this.Status = CardState.Covered;
            RaiseNotifyChanged("ActiveImage");
        }

        public void Match()
        {
            this.Status = CardState.Matched;
            RaiseNotifyChanged("ActiveImage");
        }

        private void RaiseNotifyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public enum CardState
    {
        Covered,
        Uncovered,
        Matched
    }
}
