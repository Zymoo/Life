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
        private int columns = 20;
        private int rows = 20;
        private bool run;
        private GameLife game;
        private List<Cell> cells;

        public MainWindow()
        {
            InitializeComponent();
            SetUpBoard(columns, rows);
            run = false;
        }

        private void TryGivenDimensions()
        {
            if (!int.TryParse(widthField.Text, out columns) || columns > maxWidth)
            {
                columns = maxWidth;
                MessageBox.Show("Wrong width!");
            }
            if (!int.TryParse(heightField.Text, out rows) || rows > maxHeight)
            {
                rows = maxHeight;
                MessageBox.Show("Wrong height!");
            }
        }

        private void SetUpBoard(int x, int y)
        {
            cells = new List<Cell>();
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    cells.Add(new Cell(i, j));
                }
            }
            game = new GameLife(cells, columns, rows);
            CreateBoardGrid(x, y);
        }

        private void CreateBoardGrid(int x, int y)
        {
            BoardGrid.ColumnDefinitions.Clear();
            BoardGrid.RowDefinitions.Clear();

            for (int i = 0; i < x; i++)
            {
                BoardGrid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < y; i++)
            {
                BoardGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            foreach (Cell cell in cells)
            {
                Button button = new Button
                {
                    Margin = new Thickness(0),
                    Style = FindResource("FieldStyle") as Style
                };
                button.Click += new RoutedEventHandler(ButtonChosen);
                Binding binding = new Binding("Status");
                binding.Source = cell;
                binding.Converter = new MyBackgroundConverter();
                binding.Mode = BindingMode.TwoWay;
                button.SetBinding(Button.BackgroundProperty, binding);
                Grid.SetRow(button, cell.X);
                Grid.SetColumn(button, cell.Y);
                BoardGrid.Children.Add(button);

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
            SetUpBoard(columns, rows);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
