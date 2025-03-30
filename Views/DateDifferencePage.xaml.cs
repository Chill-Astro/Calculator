using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace Calculator.Views;

public sealed partial class DateDifferencePage : Page
{
    public DateDifferencePage()
    {
        InitializeComponent();
    }

    private void CalculateButton_Click(object sender, RoutedEventArgs e)
    {
        if (StartDatePicker.SelectedDate.HasValue && EndDatePicker.SelectedDate.HasValue)
        {
            DateTime startDate = StartDatePicker.SelectedDate.Value.Date;
            DateTime endDate = EndDatePicker.SelectedDate.Value.Date;

            TimeSpan difference = endDate - startDate;
            ResultTextBlock.Text = $"Difference: {difference.TotalDays} days".ToUpper();
        }
        else
        {
            ResultTextBlock.Text = "PLEASE SELECT BOTH DATES".ToUpper();
        }
    }
}