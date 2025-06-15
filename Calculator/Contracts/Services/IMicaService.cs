﻿// File: Contracts/Services/IMicaService.cs
using Microsoft.UI.Xaml;

namespace Calculator.Contracts.Services;

public interface IMicaService
{
    bool IsMicaAltEnabled
    {
        get; set;
    }

    void ApplyMica(ElementTheme currentTheme);

    Task SaveMicaSettingAsync(bool isMicaAltEnabled);

    Task LoadMicaSettingAsync();
}