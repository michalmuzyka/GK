using GK.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GK
{
    /// <summary>
    /// Logika interakcji dla klasy GameBoard.xaml
    /// </summary>
    public partial class GameBoard : Page
    {
        Game game;
        public GameBoard(Game game)
        {
            InitializeComponent();
            this.game = game;
            this.DataContext = game;
        }

        private async void GameBoard_Loaded(object sender, RoutedEventArgs e)
        {
            switch(game.GameMode)
            {
                case GameMode.PlayWithAi:
                    game.PlayWithAi();
                    break;
                case GameMode.WatchAi:
                    await game.WatchAiGameAsync();
                    break;
            }
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (game.GameMode == GameMode.PlayWithAi)
            {
                var number = (GameNumber)((Label)sender).Tag;
                if (number.Clickable)
                    game.SelectNumber(number);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
