﻿using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace Calculator.Views;

public sealed partial class RectDiagPage : Page
{
    public RectDiagPage()
    {
        InitializeComponent();
    }

    private void CalculateButton_Click(object sender, RoutedEventArgs e)
    {
        if (double.TryParse(LengthTextBox.Text, out double length) &&
            double.TryParse(BreadthTextBox.Text, out double breadth))
        {
            double diagonal = Math.Sqrt(Math.Pow(length, 2) + Math.Pow(breadth, 2));
            ResultTextBlock.Text = (diagonal.ToString("F2") + " units").ToUpper();
        }
        else
        {
            ResultTextBlock.Text = "INVALID INPUT".ToUpper();
        }
    }
}