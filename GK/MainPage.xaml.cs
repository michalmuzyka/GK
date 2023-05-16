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
    /// Logika interakcji dla klasy MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public int X { get; set; } = Consts.DefaultX;
        public int K { get; set; } = Consts.DefaultK;

        public MainPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void watchAiButton_Click(object sender, RoutedEventArgs e)
        {
            StartGame(new Game(GameMode.WatchAi, X, K));
        }

        private void playWithAiButton_Click(object sender, RoutedEventArgs e)
        {
            StartGame(new Game(GameMode.PlayWithAi, X, K));
        }

        private void StartGame(Game game)
        {
            this.NavigationService.Navigate(new GameBoard(game));
        }
    }
}
