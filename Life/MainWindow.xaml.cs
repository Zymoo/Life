using Life.Models;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
        private GameLife game;
        private List<Cell> cells;
        private Status status;

        public MainWindow()
        {
            InitializeComponent();
            SetUpBoard(columns, rows);
            status = new Status();
            statusLabel.Content = status;
            stopButton.IsEnabled = false;
        }

        private void TryGivenDimensions()
        {
            if (!int.TryParse(dimensionControl.widthText.Text, out columns) || columns > maxWidth || columns < 5)
            {
                columns = maxWidth;
                MessageBox.Show("Wrong width!");
            }
            if (!int.TryParse(dimensionControl.heightText.Text, out rows) || rows > maxHeight || rows < 5)
            {
                rows = maxHeight;
                MessageBox.Show("Wrong height!");
            }
        }

        private void SetUpBoard(int rows, int columns)
        {
            cells = new List<Cell>();
            for (int i = 0; i < this.columns; i++)
            {
                for (int j = 0; j < this.rows; j++)
                {
                    cells.Add(new Cell(i, j));
                }
            }
            game = new GameLife(cells, this.rows, this.columns);
            CreateBoardGrid(rows, columns);
        }

        private void SetUpBoard(int rows, int columns, List<Cell> cells)
        {
            this.cells = cells;
            this.rows = rows;
            this.columns = columns;
            game = new GameLife(cells, columns, rows);
            CreateBoardGrid(rows, columns);
        }

        private void CreateBoardGrid(int rows, int columns)
        {
            BoardGrid.ColumnDefinitions.Clear();
            BoardGrid.RowDefinitions.Clear();

            for (int i = 0; i < rows; i++)
            {
                BoardGrid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < columns; i++)
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
                Binding binding = new Binding("Status")
                {
                    Source = cell,
                    Converter = new MyBackgroundConverter(),
                    Mode = BindingMode.TwoWay
                };
                button.SetBinding(Button.BackgroundProperty, binding);
                Grid.SetRow(button, cell.X);
                Grid.SetColumn(button, cell.Y);
                BoardGrid.Children.Add(button);

            }
        }

        private void ButtonChosen(object sender, EventArgs e)
        {
            Button button = sender as Button;
            game.ChangeCell(Grid.GetRow(button), Grid.GetColumn(button));
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            game.PlayNextTurn();
        }

        private async void RunButton_Click(object sender, RoutedEventArgs e)
        {
            DisableButtons();
            stopButton.IsEnabled = true;
            status.Running = true;
            statusLabel.Content = status;
            while (status.Running)
            {
                game.PlayNextTurn();
                await Task.Delay(500);
            }
            statusLabel.Content = status;
        }

        private void DisableButtons()
        {
            nextButton.IsEnabled = false;
            saveButton.IsEnabled = false;
            loadButton.IsEnabled = false;
            runButton.IsEnabled = false;
            reviveCellBox.IsEnabled = false;
        }

        private void EnableButtons()
        {
            nextButton.IsEnabled = true;
            saveButton.IsEnabled = true;
            loadButton.IsEnabled = true;
            runButton.IsEnabled = true;
            reviveCellBox.IsEnabled = true;
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            status.Running = false;
            EnableButtons();
            stopButton.IsEnabled = false;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string json = JsonConvert.SerializeObject(cells);
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                using(FileStream stream = File.Create(saveFileDialog.FileName))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.WriteLine(this.rows);
                        writer.WriteLine(this.columns);
                        writer.WriteLine(json);
                    }
                }
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string json = "";
            List<Cell> newCells = new List<Cell>();
            int newRows = 0;
            int newColumns = 0;
            if (openFileDialog.ShowDialog() == true) {
                using (FileStream stream = File.OpenRead(openFileDialog.FileName))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        newRows = int.Parse(reader.ReadLine());
                        newColumns = int.Parse(reader.ReadLine());
                        json = reader.ReadLine();
                    }
                }
            }
            newCells = JsonConvert.DeserializeObject<List<Cell>>(json);
            SetUpBoard(newRows, newColumns, newCells);
        }

        private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (status.Running)
            {
                e.CanExecute = false;
            }
            else e.CanExecute = true;
        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            TryGivenDimensions();
            SetUpBoard(rows, columns);
        }

        private void ReviveCellBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            game.Resurection = reviveCellBox.SelectedIndex + 1;
        }

        private void GliderButton_Click(object sender, RoutedEventArgs e)
        {
            game.Glider();
        }

        private void SpaceButton_Click(object sender, RoutedEventArgs e)
        {
            game.SpaceShip();
        }
    }
}
