// File: ViewModels/SettingsViewModel.cs
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Calculator.Contracts.Services;
using Calculator.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.ApplicationModel;
using WinUIEx;

namespace Calculator.ViewModels;

public enum MicaEffectType
{
    None, Mica, MicaAlt
}

public partial class SettingsViewModel : ObservableRecipient
{
    private readonly IThemeSelectorService _themeSelectorService;
    private readonly IMicaService _micaService; // Inject IMicaService

    [ObservableProperty]
    private ElementTheme _elementTheme;

    [ObservableProperty]
    private string _versionDescription;

    [ObservableProperty]
    private bool _isMicaAltEnabled;

    public ICommand SwitchThemeCommand
    {
        get;
    }
    public ICommand SetMicaEffectCommand
    {
        get;
    }

    public SettingsViewModel(IThemeSelectorService themeSelectorService, IMicaService micaService) // Update constructor
    {
        _themeSelectorService = themeSelectorService;
        _micaService = micaService;
        _elementTheme = _themeSelectorService.Theme;
        _versionDescription = GetVersionDescription();
        IsMicaAltEnabled = _micaService.IsMicaAltEnabled; // Load initial state

        SwitchThemeCommand = new RelayCommand<ElementTheme>(
            async (param) =>
            {
                if (ElementTheme != param)
                {
                    ElementTheme = param;
                    await _themeSelectorService.SetThemeAsync(param);                    
                }
            });

        SetMicaEffectCommand = new RelayCommand(SaveMicaSetting);

        this.PropertyChanged += SettingsViewModel_PropertyChanged;
    }

    private void SettingsViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IsMicaAltEnabled))
        {
            SaveMicaSetting();
        }
    }

    private async void SaveMicaSetting()
    {
        await _micaService.SaveMicaSettingAsync(IsMicaAltEnabled);
    }

    private static string GetVersionDescription()
    {
        Version version;

        if (RuntimeHelper.IsMSIX)
        {
            var packageVersion = Package.Current.Id.Version;
            version = new(packageVersion.Major, packageVersion.Minor, packageVersion.Build, packageVersion.Revision);
        }
        else
        {
            version = Assembly.GetExecutingAssembly().GetName().Version!;
        }

        return $" {"AppDisplayName".GetLocalized()} - v{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
    }
}