using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.WebSockets;
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

namespace Test2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = winCount;
        }
        Logic _Logic = new Logic();
        WinCount winCount = new WinCount(){};
        public class WinCount : INotifyPropertyChanged
        {
            private int playerXCount = 0;
            public int PlayerXCount
            {
                get { return playerXCount; }
                set
                {
                    playerXCount = value;
                    OnPropertyChanged(nameof(PlayerXCount));
                }
            }

            private int playerOCount = 0;
            public int PlayerOCount
            {
                get { return playerOCount; }
                set
                { 
                    playerOCount = value;
                    OnPropertyChanged(nameof(PlayerOCount));
                }
            }
            public event PropertyChangedEventHandler PropertyChanged;
            private void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private void PlayerClickSpace(object sender, RoutedEventArgs e)
        {
            var space = (Button)sender;
            if(!String.IsNullOrWhiteSpace(space.Content?.ToString())) return;
            space.Content = _Logic.CurrentPlayer;

            var coodinates = space.Tag?.ToString().Split(',');
            var xValue = int.Parse(coodinates[0]);
            var yValue = int.Parse(coodinates[1]);


            var buttonPosition = new Position() { x = xValue, y = yValue }; 
            _Logic.UpdateBoard(buttonPosition,_Logic.CurrentPlayer);
            if (_Logic.PlayerWin())
            {
                MessageBox.Show($"PLAYER {_Logic.CurrentPlayer} WINS!!!");
                if (_Logic.CurrentPlayer == "X")
                {
                    winCount.PlayerXCount++;
                }
                else
                {
                    winCount.PlayerOCount++;
                }
                NewGame();
                _Logic.SetNextPlayer();


            }
            else if(_Logic.PlayerDraw()) 
            {
                MessageBox.Show("DRAW");
                NewGame();
                _Logic.SetNextPlayer();
            }
            _Logic.SetNextPlayer();


        }
        public void btnResetClick(object sender, RoutedEventArgs e)
        {
            NewGame();
            winCount.PlayerXCount = 0;
            winCount.PlayerOCount = 0;
        }
        public void NewGame()
        {
            foreach (var control in gridBoard.Children)
            {
                if (control is Button)
                {
                    ((Button)control).Content = string.Empty;
                }
            }
            _Logic = new Logic();
        }
    }

}
