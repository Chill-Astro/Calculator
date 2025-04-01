using Calculator.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Windows.System;
using Microsoft.UI.Xaml.Automation.Peers;
using System.Collections.Generic;
using System;
using Microsoft.UI.Xaml.Media.Animation;

namespace Calculator.Views;

public sealed partial class CalculatorPage : Page
{
    public CalculatorPage()
    {
        ViewModel = App.GetService<CalculatorViewModel>();
        InitializeComponent();
        ClearCalculator();
        this.Focus(FocusState.Programmatic);
    }

    public CalculatorViewModel ViewModel
    {
        get;
    }

    private double _currentNumber;
    private double _previousNumber;
    private double _secondNumber;
    private string _currentOperator = "";
    private bool _isNewNumberInput = true;
    private string _operationString = "";
    private bool _divisionByZeroOccurred = false;
    private List<string> _calculationHistory = new List<string>();

    private void ClearCalculator()
    {
        DisplayTextBlock.Text = "0";
        OperationTextBlock.Text = "";
        _currentNumber = 0;
        _previousNumber = 0;
        _secondNumber = 0;
        _currentOperator = "";
        _isNewNumberInput = true;
        _operationString = "";
        _divisionByZeroOccurred = false;
    }
    private void NumberButton_Click(object sender, RoutedEventArgs e)
    {
        Button clickedButton = (Button)sender;
        string buttonContent = clickedButton.Content.ToString();

        if (_isNewNumberInput || DisplayTextBlock.Text == "0")
        {
            DisplayTextBlock.Text = buttonContent;
            _isNewNumberInput = false;
        }
        else
        {
            if (DisplayTextBlock.Text.Length < 16)
            {
                DisplayTextBlock.Text += buttonContent;
            }
        }
        if (DisplayTextBlock.Text.Length > 0 && DisplayTextBlock.Text != "N/A" && DisplayTextBlock.Text != "Not Defined")
        {
            if (double.TryParse(DisplayTextBlock.Text, out double currentNumber))
            {
                _currentNumber = currentNumber;
                DisplayTextBlock.Text = currentNumber.ToString("");
            }
        }
    }
    private void DecimalButton_Click(object sender, RoutedEventArgs e)
    {
        if (!DisplayTextBlock.Text.Contains("."))
        {
            if (DisplayTextBlock.Text.Length < 16)
            {
                DisplayTextBlock.Text += ".";
            }
        }
        _isNewNumberInput = false;
    }
    private void OperatorButton_Click(object sender, RoutedEventArgs e)
    {
        Button clickedButton = (Button)sender;
        string operatorContent = clickedButton.Content.ToString();
        string standardOperator = "";

        if (operatorContent == "\uE94A") // Division symbol
        {
            standardOperator = "÷";
        }
        else if (operatorContent == "\uE947") // Multiplication symbol
        {
            standardOperator = "×";
        }
        else if (operatorContent == "\uE949") // Subtraction symbol
        {
            standardOperator = "-";
        }
        else if (operatorContent == "\uE948") // Addition symbol
        {
            standardOperator = "+";
        }

        if (!_isNewNumberInput)
        {
            if (_currentOperator != "")
            {
                CalculateIntermediateResult();
            }
            else
            {
                _previousNumber = _currentNumber;
            }
        }
        else if (_operationString.EndsWith(GetOperatorSymbol(_currentOperator) + " "))
        {
            _operationString = _operationString.Substring(0, _operationString.Length - 2) + GetOperatorSymbol(standardOperator) + " ";
            OperationTextBlock.Text = _operationString;
            _currentOperator = standardOperator;
            return;
        }
        else if (_previousNumber == 0)
        {
            _previousNumber = _currentNumber;
        }

        string operatorSymbolForDisplay = GetOperatorSymbol(standardOperator);
        _operationString = $"{_previousNumber} {operatorSymbolForDisplay} ";
        OperationTextBlock.Text = _operationString;

        _currentOperator = standardOperator;
        _isNewNumberInput = true;
        DisplayTextBlock.Text = "0";
    }
    private void EqualsButton_Click(object sender, RoutedEventArgs e)
    {
        if (_currentOperator != "")
        {
            _secondNumber = _currentNumber;
            CalculateFinalResult();
            _currentOperator = "=";
            _previousNumber = _currentNumber;
        }
    }
    private void ClearButton_Click(object sender, RoutedEventArgs e)
    {
        ClearCalculator();
    }
    private void ClearEntryButton_Click(object sender, RoutedEventArgs e)
    {
        DisplayTextBlock.Text = "0";
        _currentNumber = 0;
        _isNewNumberInput = true;
    }
    private void BackspaceButton_Click(object sender, RoutedEventArgs e)
    {
        if (DisplayTextBlock.Text.Length > 1)
        {
            DisplayTextBlock.Text = DisplayTextBlock.Text.Substring(0, DisplayTextBlock.Text.Length - 1);
        }
        else
        {
            DisplayTextBlock.Text = "0";
            _isNewNumberInput = true;
        }
        if (DisplayTextBlock.Text == "-") DisplayTextBlock.Text = "0";
        _currentNumber = double.Parse(DisplayTextBlock.Text == "" || DisplayTextBlock.Text == "-" ? "0" : DisplayTextBlock.Text);
        if (double.TryParse(DisplayTextBlock.Text, out double currentNumber))
        {
            _currentNumber = currentNumber;
            DisplayTextBlock.Text = currentNumber.ToString("N0");
        }
    }
    private void CalculateIntermediateResult()
    {
        if (_currentOperator != "")
        {
            _currentNumber = PerformOperation(_previousNumber, _currentNumber, _currentOperator);
            DisplayTextBlock.Text = _currentNumber.ToString("N0");
            _previousNumber = _currentNumber;
            _isNewNumberInput = true;
        }
    }
    private void CalculateFinalResult()
    {
        if (_currentOperator != "")
        {
            _secondNumber = _currentNumber;
            double result = PerformOperation(_previousNumber, _secondNumber, _currentOperator);
            string operatorSymbol = GetOperatorSymbol(_currentOperator);
            string calculationString = $"{_previousNumber} {operatorSymbol} {_secondNumber} = {result}";

            if (!_divisionByZeroOccurred)
            {
                OperationTextBlock.Text = $"{_previousNumber} {operatorSymbol} {_secondNumber} =";
                if (DisplayTextBlock.Text != "N/A" && DisplayTextBlock.Text != "Not Defined")
                {
                    DisplayTextBlock.Text = result.ToString("N0");
                    _calculationHistory.Add(calculationString);
                }
            }
            else
            {
                OperationTextBlock.Text = "";
            }

            _currentOperator = "=";
            _previousNumber = result;
        }
    }
    private double PerformOperation(double num1, double num2, string operatorSymbol)
    {
        _divisionByZeroOccurred = false;

        switch (operatorSymbol)
        {
            case "+": return num1 + num2;
            case "-": return num1 - num2;
            case "×": return num1 * num2;
            case "÷":
                if (num2 == 0)
                {
                    OperationTextBlock.Text = "";
                    DisplayTextBlock.Text = "Not Defined";
                    _divisionByZeroOccurred = true;
                    return 0;
                }
                return num1 / num2;
            case "^": return Math.Pow(num1, num2);
            default: return num2;
        }
    }
    private string GetOperatorSymbol(string operatorContent)
    {
        switch (operatorContent)
        {
            case "×": return "×";
            case "÷": return "÷";
            case "+": return "+";
            case "-": return "-";
            case "^": return "^";
            default: return operatorContent;
        }
    }
    private void PercentButton_Click(object sender, RoutedEventArgs e)
    {
        if (_currentOperator != "")
        {
            if (_currentOperator == "+" || _currentOperator == "-")
            {
                _currentNumber = _previousNumber * (_currentNumber / 100);
            }
            else if (_currentOperator == "×" || _currentOperator == "÷")
            {
                _currentNumber = _currentNumber / 100;
            }

            DisplayTextBlock.Text = _currentNumber.ToString("N0");
            OperationTextBlock.Text = _operationString + _currentNumber.ToString("N0");
            _isNewNumberInput = true;
        }
        else
        {
            _currentNumber = _currentNumber / 100;
            DisplayTextBlock.Text = _currentNumber.ToString("N0");
            OperationTextBlock.Text = $"{_currentNumber * 100}%";
            _isNewNumberInput = true;
        }
    }
    private void SquareButton_Click(object sender, RoutedEventArgs e)
    {
        OperationTextBlock.Text = $"sqr({_currentNumber})";
        _currentNumber = Math.Pow(_currentNumber, 2);
        DisplayTextBlock.Text = _currentNumber.ToString("N0");
        _isNewNumberInput = true;
    }
    private void PowerButton_Click(object sender, RoutedEventArgs e)
    {
        _isNewNumberInput = true;
        _previousNumber = _currentNumber;
        _currentOperator = "^";
        _operationString = $"{_previousNumber}^";
        OperationTextBlock.Text = _operationString;
        DisplayTextBlock.Text = "0";
    }
    private void SquareRootButton_Click(object sender, RoutedEventArgs e)
    {
        if (_currentNumber < 0)
        {
            DisplayTextBlock.Text = "Invalid input for √";
        }
        else
        {
            OperationTextBlock.Text = $"√({_currentNumber})";
            _currentNumber = Math.Sqrt(_currentNumber);
            DisplayTextBlock.Text = _currentNumber.ToString(); // No format specifier
        }
        _isNewNumberInput = true;
    }
    private void CubeRootButton_Click(object sender, RoutedEventArgs e)
    {
        OperationTextBlock.Text = $"³√({_currentNumber})";
        _currentNumber = Math.Cbrt(_currentNumber);
        DisplayTextBlock.Text = _currentNumber.ToString(); // No format specifier
        _isNewNumberInput = true;
    }
    private void CalculatorPage_KeyDown(object sender, KeyRoutedEventArgs e)
    {
        switch (e.Key)
        {
            case VirtualKey.Number0:
            case VirtualKey.NumberPad0:
                FindButtonAndClick("0");
                break;
            case VirtualKey.Number1:
            case VirtualKey.NumberPad1:
                FindButtonAndClick("1");
                break;
            case VirtualKey.Number2:
            case VirtualKey.NumberPad2:
                FindButtonAndClick("2");
                break;
            case VirtualKey.Number3:
            case VirtualKey.NumberPad3:
                FindButtonAndClick("3");
                break;
            case VirtualKey.Number4:
            case VirtualKey.NumberPad4:
                FindButtonAndClick("4");
                break;
            case VirtualKey.Number5:
            case VirtualKey.NumberPad5:
                FindButtonAndClick("5");
                break;
            case VirtualKey.Number6:
            case VirtualKey.NumberPad6:
                FindButtonAndClick("6");
                break;
            case VirtualKey.Number7:
            case VirtualKey.NumberPad7:
                FindButtonAndClick("7");
                break;
            case VirtualKey.Number8:
            case VirtualKey.NumberPad8:
                FindButtonAndClick("8");
                break;
            case VirtualKey.Number9:
            case VirtualKey.NumberPad9:
                FindButtonAndClick("9");
                break;

            // Decimal point
            case VirtualKey.Decimal:
                FindButtonAndClick(".");
                break;

            // Enter key for equals
            case VirtualKey.Enter:
                FindButtonAndClick("&#xe94e;");
                break;

            // Basic operators
            case VirtualKey.Add:
                FindButtonAndClick("+");
                break;
            case VirtualKey.Subtract:
                FindButtonAndClick("-");
                break;
            case VirtualKey.Multiply:
                FindButtonAndClick("×");
                break;
            case VirtualKey.Divide:
                FindButtonAndClick("÷");
                break;

            // Backspace
            case VirtualKey.Back:
                BackspaceButton_Click(null, null);
                break;

            // Escape for clear
            case VirtualKey.Escape:
                ClearCalculator();
                break;
        }
    }
    private void FindButtonAndClick(string content)
    {
        foreach (var element in FindVisualChildren<Button>(this))
        {
            if (element.Content?.ToString() == content)
            {
                ButtonAutomationPeer peer = ButtonAutomationPeer.CreatePeerForElement(element) as ButtonAutomationPeer;
                peer?.Invoke();
                break;
            }
        }
    }
    private static System.Collections.Generic.IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
    {
        if (depObj != null)
        {
            for (int i = 0; i < Microsoft.UI.Xaml.Media.VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject child = Microsoft.UI.Xaml.Media.VisualTreeHelper.GetChild(depObj, i);
                if (child != null && child is T)
                {
                    yield return (T)child;
                }
                foreach (T childOfChild in FindVisualChildren<T>(child))
                {
                    yield return childOfChild;
                }
            }
        }
    }
    private void CalculatorPage_Loaded(object sender, RoutedEventArgs e)
    {
        this.Focus(FocusState.Programmatic);
        // Start the entrance animation
        var entranceThemeTransition = new EntranceThemeTransition();
        ContentGrid.Transitions = new TransitionCollection { entranceThemeTransition };
    }
}