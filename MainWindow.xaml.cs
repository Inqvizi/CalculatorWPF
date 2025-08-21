using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculatorWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double firstNumber = 0;
        private double secondNumber = 0;
        private string operation = "";
        private bool isOperationClicked = false;
        private bool isNewNumber = true;
        private bool isDarkTheme = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnNumber_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string number = button.Content.ToString();

            if (isNewNumber)
            {
                DisplayTextBox.Text = "";
                isNewNumber = false;
            }

            if (DisplayTextBox.Text == "0")
            {
                DisplayTextBox.Text = number;
            }
            else
            {
                DisplayTextBox.Text += number;
            }

            HistoryTextBlock.Text += number;
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            firstNumber = 0;
            secondNumber = 0;
            operation = "";
            isOperationClicked = false;
            isNewNumber = true;
            DisplayTextBox.Text = "0";
            HistoryTextBlock.Text = "";
        }

        private void BtnOperation_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            HistoryTextBlock.Text += " " + button.Content.ToString() + " ";

            if (!isOperationClicked)
            {
                firstNumber = Convert.ToDouble(DisplayTextBox.Text);
            }
            else
            {
                BtnEquals_Click(sender, e);
                firstNumber = Convert.ToDouble(DisplayTextBox.Text);
             
                HistoryTextBlock.Text = firstNumber.ToString() + " " + button.Content.ToString() + " ";
            }   
            

            operation = button.Tag.ToString();
            isOperationClicked = true;
            isNewNumber = true;
        }

        private void BtnEquals_Click(object sender, RoutedEventArgs e)
        {
            secondNumber = Convert.ToDouble(DisplayTextBox.Text);
            double result = 0;

            switch (operation)
            {
                case "+":
                    result = firstNumber + secondNumber;
                    break;
                case "-":
                    result = firstNumber - secondNumber;
                    break;
                case "*":
                    result = firstNumber * secondNumber;
                    break;
                case "/":
                    if (secondNumber != 0)
                    {
                        result = firstNumber / secondNumber;
                    }
                    else
                    {
                        MessageBox.Show("You can't divide by 0!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    break;
            }
            
            
            HistoryTextBlock.Text += " " + "=";
            DisplayTextBox.Text = result.ToString();
        }

        private void BtnDot_Click(object sender, RoutedEventArgs e)
        {
            if (isNewNumber)
            {
                DisplayTextBox.Text = "0.";
                isNewNumber = false;
            }
            else
            {
                if (!DisplayTextBox.Text.Contains("."))
                {
                    DisplayTextBox.Text += ".";
                }
            }
        }

        private void BtnPercent_Click(object sender, RoutedEventArgs e)
        {
            double number = Convert.ToDouble(DisplayTextBox.Text);

            if (isOperationClicked)
            {
                number = firstNumber * number / 100;
                HistoryTextBlock.Text = firstNumber + " " + operation +  " " + number;
                DisplayTextBox.Text = number.ToString();
            }
            else
            {
                number = number / 100;
                HistoryTextBlock.Text = number.ToString();
                DisplayTextBox.Text = number.ToString();
            }

            isNewNumber = true;
        }

        private void BtnThemeToggle_Click(object sender, RoutedEventArgs e)
        {
            this.Resources.MergedDictionaries.Clear();

            ResourceDictionary newTheme = new ResourceDictionary();

            if (isDarkTheme)
            {
                newTheme.Source = new Uri("Themes/LightTheme.xaml", UriKind.Relative);
                BtnThemeToggle.Content = "☀️";
                isDarkTheme = false;
            }
            else
            {
                newTheme.Source = new Uri("Themes/DarkTheme.xaml", UriKind.Relative);
                BtnThemeToggle.Content = "🌙";
                isDarkTheme = true;
            }

            this.Resources.MergedDictionaries.Add(newTheme);
        }

        private void BtnPlusMinus_Click(object sender, RoutedEventArgs e)
        {
            if(DisplayTextBox.Text == "0")
            {
                MessageBox.Show("You cannot add a minus to 0!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (double.TryParse(DisplayTextBox.Text, out double currentValue))
            {
                currentValue = -currentValue;
                
                DisplayTextBox.Text = currentValue.ToString();
            }
        }
    }
}