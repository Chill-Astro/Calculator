using Calculator.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace Calculator.Views;

public sealed partial class DiagonalPage : Page
{
    public DiagonalViewModel ViewModel
    {
        get;
    }

    public DiagonalPage()
    {
        ViewModel = App.GetService<DiagonalViewModel>();
        InitializeComponent();
    }
}
