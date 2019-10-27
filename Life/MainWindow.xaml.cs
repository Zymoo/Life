using Life.Models;
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

namespace Life
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int width;
        private int height;
        private Game game;

        public MainWindow()
        {
            InitializeComponent();
            SetUpBoard();
            width = 10;
            height = 10;
            game = new Game(width, height);
        }

        private void SetUpBoard()
        {

            for (int i = 0; i < 10; i++)
            {
                BoardGrid.ColumnDefinitions.Add(new ColumnDefinition { });
                BoardGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Button button = new Button
                    {
                        Background = new SolidColorBrush(Colors.AliceBlue),
                        Content = "0"
                    };
                    button.Click += new RoutedEventHandler(ButtonChosen);
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                    BoardGrid.Children.Add(button);
                }
            }
        }

        private void ButtonChosen(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.Content = ChangeFieldState(button.Content.ToString());
            game.ChangeCell(Grid.GetRow(button), Grid.GetColumn(button));
            
        }

        private string ChangeFieldState(string prevState)
        {
            if (prevState.Equals("0"))
            {
                return "1";
            }
            else return "0";
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            game.PlayNextTurn();
            foreach(Button button in BoardGrid.Children)
            {
                button.Content = game.GetCellStatus(Grid.GetRow(button), Grid.GetColumn(button)) ? "1" : "0";
            }
        }
    }
}
