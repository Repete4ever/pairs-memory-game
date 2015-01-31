using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows;

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
            DoubleAnimation daValueProperty = new DoubleAnimation();
            daValueProperty.From = 200;
            daValueProperty.To = 0;
            daValueProperty.Duration = TimeSpan.FromSeconds(200);
            daValueProperty.SetValue(Storyboard.TargetNameProperty, "progressBarTimeLeft");
            daValueProperty.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath("Value"));

            ColorAnimation caForegroundProperty = new ColorAnimation();
            caForegroundProperty.To = Colors.Red;
            caForegroundProperty.BeginTime = TimeSpan.FromSeconds(170);
            caForegroundProperty.SetValue(Storyboard.TargetNameProperty, "progressBarTimeLeft");
            caForegroundProperty.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath("Foreground.Color"));

            DoubleAnimation daOpacityProperty = new DoubleAnimation();
            daOpacityProperty.From = 1;
            daOpacityProperty.To = 0;
            daOpacityProperty.Duration = TimeSpan.FromMilliseconds(900);
            daOpacityProperty.AutoReverse = true;
            daOpacityProperty.RepeatBehavior = RepeatBehavior.Forever;
            daOpacityProperty.BeginTime = TimeSpan.FromSeconds(185);
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
