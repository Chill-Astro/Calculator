using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace Calculator.Views;

public sealed partial class AgeCalcPage : Page
{
    public AgeCalcPage()
    {
        InitializeComponent();
    }

    private void CalculateButton_Click(object sender, RoutedEventArgs e)
    {
        if (BirthDatePicker.SelectedDate.HasValue)
        {
            DateTime birthDate = BirthDatePicker.SelectedDate.Value.Date;
            DateTime currentDate = DateTime.Now.Date;
            int age = currentDate.Year - birthDate.Year;
            if (currentDate.Month < birthDate.Month || (currentDate.Month == birthDate.Month && currentDate.Day < birthDate.Day))
            {
                age--;
            }
            ResultTextBlock.Text = $"AGE : {age} YEARS OLD".ToUpper();
        }
        else
        {
            ResultTextBlock.Text = "PLEASE SELECT A DATE OF BIRTH".ToUpper();
        }
    }
}