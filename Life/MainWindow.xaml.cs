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
        private bool run;
        private Game game;
        private List<Cell> cells;

        public MainWindow()
        {
            InitializeComponent();
            width = 10;
            height = 10;
            TryGivenDimensions();
            SetUpBoard(width, height);
            run = false;
            cells = new List<Cell>();
        }

        private void TryGivenDimensions()
        {
            if (!int.TryParse(widthField.Text, out width) || width > 40)
            {
                width = 10;
                MessageBox.Show("Wrong width! Max width is 40!");
            }
            if (!int.TryParse(heightField.Text, out height) || height > 20)
            {
                height = 10;
                MessageBox.Show("Wrong height! Max height is 20!");
            }
        }

        private void SetUpBoard(int x, int y)
        {
            BoardGrid.ColumnDefinitions.Clear();
            BoardGrid.RowDefinitions.Clear();
            game = new Game(width, height);

            for (int i = 0; i < y; i++)
            {
                BoardGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < x; i++)
            {
                BoardGrid.RowDefinitions.Add(new RowDefinition());
                for (int j = 0; j < y; j++)
                {
                    Button button = new Button
                    {
                       // Background = new SolidColorBrush(Colors.AliceBlue),
                        Content = "0",
                        Margin = new Thickness(0),
                        Style = FindResource("FieldStyle") as Style
                    };
                    button.Click += new RoutedEventHandler(ButtonChosen);
                    Binding binding = new Binding("Background");
                    //binding.Source = cells.
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                    BoardGrid.Children.Add(button);
                }
                
            }
        }

        private void ButtonChosen(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button.Content.ToString().Equals("0"))
            {
                button.Background = new SolidColorBrush(Colors.Blue);
            }
            else button.Background = new SolidColorBrush(Colors.AliceBlue);
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
            NextTurn();
        }

        private void NextTurn()
        {
            game.PlayNextTurn();
            foreach (Button button in BoardGrid.Children)
            {
                if (game.GetCellStatus(Grid.GetRow(button), Grid.GetColumn(button)))
                {
                    button.Content = "1";
                    button.Background = new SolidColorBrush(Colors.Blue);
                }
                else
                {
                    button.Content = "0";
                    button.Background = new SolidColorBrush(Colors.AliceBlue);
                }
            }
        }
        private async void RunButton_Click(object sender, RoutedEventArgs e)
        {
            run = true;
            while (run)
            {
                NextTurn();
                await Task.Delay(500);
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            run = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TryGivenDimensions();
            SetUpBoard(width, height);
        }
    }
}
