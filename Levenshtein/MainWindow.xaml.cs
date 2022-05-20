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

namespace Levenshtein
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            StringDistance stringDistance = new StringDistance(sourceInput.Text, targetInput.Text);
            Tuple<int, int[,]> result = stringDistance.levenshtein();
            outputDistance.Content = result.Item1;
            outputMatrix.Children.Clear();
            FillTable(result.Item2);
        }

        private void FillTable(int[,] matrix)
        {
            Button[,] cells = new Button[matrix.GetLength(0) + 2, matrix.GetLength(1) + 2];

            int top = 0;
            int left = 0;

            for (int i = 0; i < cells.GetUpperBound(0); i++) // satır
            {
                for (int j = 0; j < cells.GetUpperBound(1); j++) // sütun
                {
                    cells[i, j] = new Button();
                    cells[i, j].Height = 40;
                    cells[i, j].Width = 40;
                    cells[i, j].Margin = new Thickness(left, top, -left, -top);
                    cells[i, j].HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    cells[i, j].VerticalAlignment = System.Windows.VerticalAlignment.Top;

                    if (i == 0 && j == 0) cells[i, j].Background = new SolidColorBrush(Colors.White);
                    else if (i == 1 || j == 1) cells[i, j].Background = new SolidColorBrush(Colors.LightGreen);
                    else if (i == cells.GetUpperBound(0) - 1 && j == cells.GetUpperBound(1) - 1) cells[i, j].Background = new SolidColorBrush(Colors.IndianRed);
                    else if ((i + j) % 2 == 0) cells[i, j].Background = new SolidColorBrush(Colors.LightGray);

                    if ((i == 0 && j < 2) || (j == 0 && i < 2)) cells[i, j].Content = "";
                    else if (j == 0 && i > 1) cells[i, j].Content = sourceInput.Text[i - 2];
                    else if (i == 0 && j > 1) cells[i, j].Content = targetInput.Text[j - 2];
                    else if (i > 0 && j > 0) cells[i, j].Content = matrix[i - 1, j - 1];

                    outputMatrix.Children.Add(cells[i, j]);
                    left += 40;
                }
                top += 40;
                if (i == 0) outputMatrix.Width = left;
                left = 0;
            }
            outputMatrix.Height = top;
        }
    }
}