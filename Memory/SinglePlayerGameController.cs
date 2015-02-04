using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Pairs
{
    public class SinglePlayerGameController : GameController
    {


        public SinglePlayerGameController(Grid gameGrid, GameOptions gameOptions)
            : base(gameGrid, gameOptions)
        {
           
        }

        protected override void OnGameStart()
        {
            
            StartTimer();
        }

        private void StartTimer()
        {
            DoubleAnimation daValueProperty = new DoubleAnimation
            {
                From = 200,
                To = 0,
                Duration = TimeSpan.FromSeconds(200)
            };
            daValueProperty.SetValue(Storyboard.TargetNameProperty, "progressBarTimeLeft");
            daValueProperty.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath("Value"));

            ColorAnimation caForegroundProperty = new ColorAnimation
            {
                To = Colors.Red,
                BeginTime = TimeSpan.FromSeconds(170)
            };
            caForegroundProperty.SetValue(Storyboard.TargetNameProperty, "progressBarTimeLeft");
            caForegroundProperty.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath("Foreground.Color"));

            DoubleAnimation daOpacityProperty = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(900),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever,
                BeginTime = TimeSpan.FromSeconds(185)
            };
            daOpacityProperty.SetValue(Storyboard.TargetNameProperty, "progressBarTimeLeft");
            daOpacityProperty.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath("Opacity"));


            App.TimerStoryboard = new Storyboard();
            App.TimerStoryboard.Children.Add(daValueProperty);
            App.TimerStoryboard.Children.Add(caForegroundProperty);
            App.TimerStoryboard.Children.Add(daOpacityProperty);
            App.TimerStoryboard.Duration = TimeSpan.FromSeconds(200);

            //sb.Completed += new EventHandler(GameOver);

            App.TimerStoryboard.Begin(gameGrid, true);

        }
    }
}
