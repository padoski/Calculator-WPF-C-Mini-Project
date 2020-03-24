using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private decimal _actualResult = 0;
        private char _operation = ' ';
        private bool _isLastOperation = true;
        private bool _isLastEqual = false;
        private int _counter = 0;
        public MainWindow()
        {
            InitializeComponent();
            var currentCulture = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            var ci = new CultureInfo(currentCulture) { NumberFormat = { NumberDecimalSeparator = "." } };
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
        }
        private void ButtonNumb_Click(object sender, RoutedEventArgs e)
        {
            if (_isLastOperation == true) 
            { 
                ResultDisplay.Text = "";
                _isLastOperation = false; 
            }

            Button tempButton = (Button)sender;
            ResultDisplay.Text += tempButton.Content;

            IsTooLarge();


        }
        private void ButtonOperation_Click(object sender, RoutedEventArgs e)
        {
            
            if(_isLastOperation==false)
            {
                
                _counter++;
                Calculations();
                Button tempButton = (Button)sender;
                _operation = Convert.ToChar(tempButton.Content);
                UpdateComputings();
                if (_counter == 1)
                {
                    ComputingDisplay.Text = ResultDisplay.Text + Convert.ToString(_operation);
                    _actualResult = Convert.ToDecimal(ResultDisplay.Text);
                }
                _isLastOperation = true;
                UpdateResultDisplay();
            }
            if (_isLastEqual == true)
            {
                Button tempButton = (Button)sender;
                _operation = Convert.ToChar(tempButton.Content);
                ComputingDisplay.Text = _actualResult.ToString() + Convert.ToString(_operation);
                _isLastEqual = false;
            }


        }
        private void ButtonC_Click(object sender, RoutedEventArgs e)
        {
            _actualResult = 0;
            ComputingDisplay.Text = "";
            ResultDisplay.Text = "";
            _counter = 0;
        }
        private void ButtonCE_Click(object sender, RoutedEventArgs e)
        {
            ResultDisplay.Text = "";
        }

        private void ButtonBCSP_Click(object sender, RoutedEventArgs e)
        {
            if(ResultDisplay.Text.Length>0 && _isLastOperation==false)
            {
                ResultDisplay.Text = ResultDisplay.Text.Remove(ResultDisplay.Text.Length - 1, 1);
            }
        }
        private void Common_Click(object sender, RoutedEventArgs e)
        {
            bool isAlreadyCommon = ResultDisplay.Text.Contains('.');
            if (isAlreadyCommon==false && ResultDisplay.Text.Length!=0)
            {
                ResultDisplay.Text+='.';
            }
        }
        private void UpdateComputings()
        {
                ComputingDisplay.Text = ComputingDisplay.Text + ResultDisplay.Text + Convert.ToString(_operation);
        }
        private void UpdateResultDisplay()
        {
            ResultDisplay.Text = _actualResult.ToString();
        }

        private void ButtonEqual_Click(object sender, RoutedEventArgs e)
        {
            if (_isLastEqual != true)
            {
                Calculations();
                ComputingDisplay.Text = ComputingDisplay.Text + ResultDisplay.Text + "=";
                ResultDisplay.Text = _actualResult.ToString();
                _isLastEqual = true;
                _isLastOperation = true;
            }
            
        }
        private void Calculations()
        {
            try { 
            switch (_operation)
            {
                case '+':
                    _actualResult += Convert.ToDecimal(ResultDisplay.Text); break;
                case '-':
                    _actualResult -= Convert.ToDecimal(ResultDisplay.Text); break;
                case 'X':
                    _actualResult *= Convert.ToDecimal(ResultDisplay.Text);
                        _actualResult = Math.Round(_actualResult, 10);
                        break;
                case '/':
                        if(Convert.ToDecimal(ResultDisplay.Text)==0)
                        {
                            MessageBox.Show("NIE DZIELIMY PRZEZ ZERO!");
                            ComputingDisplay.Text = "";
                            _actualResult = 0;
                            ResultDisplay.Text = "";
                        }
                        else
                        {
                            _actualResult /= Convert.ToDecimal(ResultDisplay.Text);
                            _actualResult = Math.Round(_actualResult, 10);  
                        }
                        break;
                }
            }
            catch
            {
                ResultDisplay.Text = "";
                _actualResult = 0;
                ComputingDisplay.Text = "";
                MessageBox.Show("Wpisana liczba jest za duża");
            }
           
        }
        
        private void IsTooLarge()
        {
            if (ResultDisplay.Text.Length > 20)
            {
                ResultDisplay.Text = "";
                _actualResult = 0;
                MessageBox.Show("Wpisana liczba jest za duża");
            }
        }

        private void ButtonPosAndNeg_Click(object sender, RoutedEventArgs e)
        {
            if(ResultDisplay.Text!="")
            {
                _actualResult = Convert.ToDecimal(ResultDisplay.Text);
                _actualResult = _actualResult * (-1);
                ResultDisplay.Text = _actualResult.ToString();
            } 
        }
    }
}
