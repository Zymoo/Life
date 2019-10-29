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
        private const int maxWidth = 40;
        private const int maxHeight = 30;
        private int width = 20;
        private int height = 20;
        private bool run;
        private Game game;
        private Cell[,] cells;

        public MainWindow()
        {
            InitializeComponent();
            SetUpBoard(width, height);
            run = false;
        }

        private void TryGivenDimensions()
        {
            if (!int.TryParse(widthField.Text, out width) || width > maxWidth)
            {
                width = maxWidth;
                MessageBox.Show("Wrong width!");
            }
            if (!int.TryParse(heightField.Text, out height) || height > maxHeight)
            {
                height = maxHeight;
                MessageBox.Show("Wrong height!");
            }
        }

        private void SetUpBoard(int x, int y)
        {
            cells = new Cell[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    cells[i, j] = new Cell();
                }
            }
            game = new Game(cells);

            BoardGrid.ColumnDefinitions.Clear();
            BoardGrid.RowDefinitions.Clear();

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
                        Margin = new Thickness(0),
                        Style = FindResource("FieldStyle") as Style
                    };
                    button.Click += new RoutedEventHandler(ButtonChosen);
                    Binding binding = new Binding("Status");
                    binding.Source = cells[i, j];
                    binding.Converter = new MyBackgroundConverter();
                    binding.Mode = BindingMode.TwoWay;
                    button.SetBinding(Button.BackgroundProperty, binding);
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                    BoardGrid.Children.Add(button);
                }
            }
        }

        private void ButtonChosen(object sender, EventArgs e)
        {
            Button button = sender as Button;
            /*
            if (button.Background.Equals(Colors.AliceBlue))
            {
                button.Background = new SolidColorBrush(Colors.Blue);
            }
            else button.Background = new SolidColorBrush(Colors.AliceBlue);
            */
            //button.Content = ChangeFieldState(button.Content.ToString());
            game.ChangeCell(Grid.GetRow(button), Grid.GetColumn(button));
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            NextTurn();
        }

        private void NextTurn()
        {
            game.PlayNextTurn();
            /*
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
            */
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
