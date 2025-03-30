using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;

namespace Calculator.Views;

public sealed partial class UnitConverterPage : Page
{
    private Dictionary<string, List<string>> unitsByCategory = new Dictionary<string, List<string>>()
    {
        {"Length", new List<string>{"Meters", "Feet", "Inches", "Kilometers", "Miles", "Centimeters", "Millimeters", "Yards", "Nautical Miles"}},
        {"Weight (Mass)", new List<string>{"Kilograms", "Pounds", "Grams", "Ounces", "Milligrams", "Stones", "Tons (metric)", "Tons (US)"}},
        {"Volume", new List<string>{"Liters", "Gallons (US)", "Gallons (UK)", "Cubic Meters", "Cubic Feet", "Cubic Inches", "Milliliters", "Cups (US)", "Pints (US)", "Quarts (US)"}},
        {"Temperature", new List<string>{"Celsius", "Fahrenheit", "Kelvin"}},
        {"Area", new List<string>{"Square Meters", "Square Feet", "Square Inches", "Square Kilometers", "Square Miles", "Hectares", "Acres"}},
        {"Time", new List<string>{"Seconds", "Minutes", "Hours", "Days", "Weeks", "Years"}},
        {"Speed", new List<string>{"Meters per second", "Kilometers per hour", "Miles per hour", "Knots"}},
        {"Energy", new List<string>{"Joules", "Kilojoules", "Calories", "Kilocalories", "British Thermal Units"}},
        {"Pressure", new List<string>{"Pascals", "Kilopascals", "Bar", "Pounds per square inch", "Atmospheres"}},
        {"Power", new List<string>{"Watts", "Kilowatts", "Horsepower"}},
        {"Data (Digital Storage)", new List<string>{"Bits", "Bytes", "Kilobytes", "Megabytes", "Gigabytes", "Terabytes"}},
        {"Angle", new List<string>{"Degrees", "Radians"}}
    };

    public UnitConverterPage()
    {
        InitializeComponent();
        CategoryComboBox.SelectedIndex = 0; // Default to Length
    }

    private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (CategoryComboBox.SelectedItem is ComboBoxItem selectedItem)
        {
            string category = selectedItem.Content.ToString();
            if (unitsByCategory.ContainsKey(category))
            {
                FromUnitComboBox.ItemsSource = unitsByCategory[category];
                ToUnitComboBox.ItemsSource = unitsByCategory[category];
                FromUnitComboBox.SelectedIndex = 0;
                ToUnitComboBox.SelectedIndex = 1; // Default to a different unit
            }
        }
    }

    private void ConvertButton_Click(object sender, RoutedEventArgs e)
    {
        if (double.TryParse(ValueToConvertTextBox.Text, out double valueToConvert) &&
            FromUnitComboBox.SelectedItem is string fromUnit &&
            ToUnitComboBox.SelectedItem is string toUnit &&
            CategoryComboBox.SelectedItem is ComboBoxItem selectedCategory)
        {
            string category = selectedCategory.Content.ToString();
            double result = 0;

            // Perform conversion logic here based on category and units
            switch (category)
            {
                case "Length":
                    result = ConvertLength(valueToConvert, fromUnit, toUnit);
                    break;
                case "Weight (Mass)":
                    result = ConvertWeight(valueToConvert, fromUnit, toUnit);
                    break;
                case "Volume":
                    result = ConvertVolume(valueToConvert, fromUnit, toUnit);
                    break;
                case "Temperature":
                    result = ConvertTemperature(valueToConvert, fromUnit, toUnit);
                    break;
                case "Area":
                    result = ConvertArea(valueToConvert, fromUnit, toUnit);
                    break;
                case "Time":
                    result = ConvertTime(valueToConvert, fromUnit, toUnit);
                    break;
                case "Speed":
                    result = ConvertSpeed(valueToConvert, fromUnit, toUnit);
                    break;
                case "Energy":
                    result = ConvertEnergy(valueToConvert, fromUnit, toUnit);
                    break;
                case "Pressure":
                    result = ConvertPressure(valueToConvert, fromUnit, toUnit);
                    break;
                case "Power":
                    result = ConvertPower(valueToConvert, fromUnit, toUnit);
                    break;
                case "Data (Digital Storage)":
                    result = ConvertData(valueToConvert, fromUnit, toUnit);
                    break;
                case "Angle":
                    result = ConvertAngle(valueToConvert, fromUnit, toUnit);
                    break;
            }

            ResultTextBlock.Text = $"{valueToConvert} {fromUnit} = {result:F2} {toUnit}".ToUpper();
        }
        else
        {
            ResultTextBlock.Text = "INVALID INPUT OR UNIT SELECTION".ToUpper();
        }
    }

    // Conversion logic functions (to be implemented)
    private double ConvertLength(double value, string from, string to)
    {
        // Implement length conversion logic
        if (from == to) return value;
        // Example conversions (you need to add all)
        if (from == "Meters" && to == "Feet") return value * 3.28084;
        if (from == "Feet" && to == "Meters") return value / 3.28084;
        if (from == "Meters" && to == "Kilometers") return value / 1000;
        if (from == "Kilometers" && to == "Meters") return value * 1000;
        // ... add many more length conversions
        return 0; // Placeholder
    }

    private double ConvertWeight(double value, string from, string to)
    {
        // Implement weight conversion logic
        if (from == to) return value;
        // Example conversions
        if (from == "Kilograms" && to == "Pounds") return value * 2.20462;
        if (from == "Pounds" && to == "Kilograms") return value / 2.20462;
        // ... add many more weight conversions
        return 0; // Placeholder
    }

    private double ConvertVolume(double value, string from, string to)
    {
        // Implement volume conversion logic
        if (from == to) return value;
        // Example conversions
        if (from == "Liters" && to == "Gallons (US)") return value * 0.264172;
        if (from == "Gallons (US)" && to == "Liters") return value / 0.264172;
        // ... add many more volume conversions
        return 0; // Placeholder
    }

    private double ConvertTemperature(double value, string from, string to)
    {
        if (from == to) return value;
        if (from == "Celsius" && to == "Fahrenheit") return (value * 9 / 5) + 32;
        if (from == "Fahrenheit" && to == "Celsius") return (value - 32) * 5 / 9;
        if (from == "Celsius" && to == "Kelvin") return value + 273.15;
        if (from == "Kelvin" && to == "Celsius") return value - 273.15;
        if (from == "Fahrenheit" && to == "Kelvin") return (value - 32) * 5 / 9 + 273.15;
        if (from == "Kelvin" && to == "Fahrenheit") return (value - 273.15) * 9 / 5 + 32;
        return 0; // Placeholder
    }

    private double ConvertArea(double value, string from, string to)
    {
        // Implement area conversion logic
        if (from == to) return value;
        // Add area conversions
        return 0; // Placeholder
    }

    private double ConvertTime(double value, string from, string to)
    {
        // Implement time conversion logic
        if (from == to) return value;
        // Add time conversions
        return 0; // Placeholder
    }

    private double ConvertSpeed(double value, string from, string to)
    {
        // Implement speed conversion logic
        if (from == to) return value;
        // Add speed conversions
        return 0; // Placeholder
    }

    private double ConvertEnergy(double value, string from, string to)
    {
        // Implement energy conversion logic
        if (from == to) return value;
        // Add energy conversions
        return 0; // Placeholder
    }

    private double ConvertPressure(double value, string from, string to)
    {
        // Implement pressure conversion logic
        if (from == to) return value;
        // Add pressure conversions
        return 0; // Placeholder
    }

    private double ConvertPower(double value, string from, string to)
    {
        // Implement power conversion logic
        if (from == to) return value;
        // Add power conversions
        return 0; // Placeholder
    }

    private double ConvertData(double value, string from, string to)
    {
        // Implement data conversion logic (consider powers of 1024)
        if (from == to) return value;
        // Add data conversions
        return 0; // Placeholder
    }

    private double ConvertAngle(double value, string from, string to)
    {
        if (from == to) return value;
        if (from == "Degrees" && to == "Radians") return value * Math.PI / 180;
        if (from == "Radians" && to == "Degrees") return value * 180 / Math.PI;
        return 0; // Placeholder
    }
}