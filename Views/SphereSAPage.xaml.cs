﻿using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace Calculator.Views;

public sealed partial class SphereSAPage : Page
{
    public SphereSAPage()
    {
        InitializeComponent();
    }

    private void CalculateButton_Click(object sender, RoutedEventArgs e)
    {
        if (double.TryParse(RadiusTextBox.Text, out double radius))
        {
            double surfaceArea = 4 * Math.PI * Math.Pow(radius, 2);
            ResultTextBlock.Text = (surfaceArea.ToString("F2") + " sq. units").ToUpper();
        }
        else
        {
            ResultTextBlock.Text = "INVALID INPUT".ToUpper();
        }
    }
}