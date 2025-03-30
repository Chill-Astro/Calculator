using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using Windows.ApplicationModel.DataTransfer;

namespace Calculator.Views;

public sealed partial class BaseConverterPage : Page
{
    public BaseConverterPage()
    {
        InitializeComponent();
    }

    private void ConvertButton_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(InputTextBox.Text) || FromBaseComboBox.SelectedItem == null || ToBaseComboBox.SelectedItem == null)
        {
            ResultTextBlock.Text = "Please enter a number and select both bases.";
            return;
        }

        if (!(FromBaseComboBox.SelectedItem is ComboBoxItem fromBaseItem) || !(ToBaseComboBox.SelectedItem is ComboBoxItem toBaseItem))
        {
            ResultTextBlock.Text = "Error in base selection.";
            return;
        }

        if (!int.TryParse(fromBaseItem.Content.ToString().Split('(')[1].TrimEnd(')'), out int fromBase) ||
            !int.TryParse(toBaseItem.Content.ToString().Split('(')[1].TrimEnd(')'), out int toBase))
        {
            ResultTextBlock.Text = "Invalid base format.";
            return;
        }

        string inputNumber = InputTextBox.Text.ToUpper(); // Handle hexadecimal A-F

        try
        {
            long decimalValue = Convert.ToInt64(inputNumber, fromBase);
            string result = Convert.ToString(decimalValue, toBase).ToUpper();
            ResultTextBlock.Text = result;
        }
        catch (FormatException)
        {
            ResultTextBlock.Text = $"Invalid input '{inputNumber}' for base {fromBase}.";
        }
        catch (Exception ex)
        {
            ResultTextBlock.Text = $"An error occurred: {ex.Message}";
        }
    }
}