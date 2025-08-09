<p align="center">
  <img src="https://github.com/Chill-Astro/Calculator/blob/main/Calculator/Assets/StoreLogo.scale-400.png" width="100px" height="100px" alt="Calculator Logo">
</p>
<h1 align="center">Calculator</h1>

It's a Simple Calculator Elevated with Powerful Scripted Actions. This simple yet intuitive GUI application combines a clean interface with the ability to perform complex calculations via custom scripts, making it significantly more powerful than standard calculators.

It can also be run from Terminal using App Execution Aliases [ .msix version ]. These are `calc+.exe`, `calculator.exe`.

Installer Version supports `calculator.exe` only.

Currently at **v11.26100.10.0**, and available for **Windows 11 and Windows 10 [ 20H2+ ]**.

**v11.26100.11.0** in Development.

---

## Key Features :

- Simple and Clean GUI. ✅
- Dozens of calculation options. ✅
- Fast and Error-Proof Calculations. ✅
- High Precision for decimals. ✅
- Modern UI with Fluid Animations and Transitions. ✅
- History Support for the Base Calculator UI. ✅
- Theme switching built in. ✅
- Backdrop switching built in. [ B/w Mica Alt and Acrylic ] ✅
- Available in both Msix & Installer Variants. ✅

---

### Install Calculator [ Installer ] from Winget :
      
      winget install Calculator.unp

---

## Previews :

- Image Preview :

![image](https://github.com/user-attachments/assets/1869715f-d336-4efa-a9fc-f8f8c6a87b5b)

- History Preview :

https://github.com/user-attachments/assets/624ade09-6050-4a4e-965d-7ef7938f4c1e

- Scripted Actions Preview :

https://github.com/user-attachments/assets/870b4683-0e88-4f46-b4e2-9220e19e9a86

---

## Installation : 

1.  Download the `.msix` and `.cer` files from the [latest release.](https://github.com/Chill-Astro/Calculator/releases/latest)
2.  Import the `.cer` file to the `Trusted Root Certificates` Store. [ ONLY ON FIRST RUN! ].
3.  Install the `.msix` file.

<p align="center">
  ---------------------[ OR ]---------------------
</p>

-  Download the `Calculator-Setup.msi` to install the app easily.

---

## Building from Source :

- Install Visual Studio 2022 with **WinUI Application Development** and **.NET Desktop Development** workloads.
  - Windows 11 is Recommended. [ Build 26100 + is Best for Development ]
  - [XAML Styler](https://marketplace.visualstudio.com/items?itemName=TeamXavalon.XAMLStyler) is recommended for contributing.
  - .NET 8.0 Runtime LTS is must.
  - Get the latest Windows 11 SDK [26100.xxxx].
  - Commnity Edition is sufficient for contributing and testing. Pro and Enterprise Editions can also be used.
  - Github Copilot and Live Share can be skipped for Storage Saving.
 
![image](https://github.com/user-attachments/assets/0a18b87a-de85-40f9-80bc-ef2575dc221c)

- Get the Code :
  
      git clone https://github.com/Chill-Astro/Calculator.git

- Open [Calculator.sln](/Calculator.sln) in Visual Studio.
- Hit Deploy as shown in Screenshot. [Building is Automatically Done while Deploying.]

![image](https://github.com/user-attachments/assets/d343c12f-03c0-4e52-95d8-925c5262f304)

Calculator is now Deployed and now it shall appear in the Start Menu.

![image](https://github.com/user-attachments/assets/99678fdb-c955-4818-a7bf-5d58fdfa1cfd)

---

## Adding Currency Convertor :

Calculator uses [ExchangeRate-API](https://app.exchangerate-api.com) for Currency conversion. An API key must be mannualy added in the region indicated.

- Open `appsettings.json` : Paste the Code into here.

    {
        "LocalSettingsOptions": {
        "ApplicationDataFolder": "Calculator/ApplicationData",
        "LocalSettingsFile": "LocalSettings.json"
      },
      "CurrencyApiKey": "Enter you API Key here...."
    }

- Buid and Run the Application as shown above.

## Icon Sources and Credits :

- [Icons8](https://icons8.com) : For all the Mensuration and Quadratic Equation Solver Menu Logos, 
- [SVG REPO](https://www.svgrepo.com/) : For Calculator Menu Logo, Unit Convertor, Heron's Formula, and most of the icons.
- [Icomoon](https://icomoon.io/) : For the Base Calculator Icon and Produce the `.ttf` file for the Icons.
- [Microsoft Calculator](https://github.com/microsoft/calculator) : For Square Root and Cube Root Button Icons. Also this inspired me to make this app.
- [Advanced Installer Free](https://www.advancedinstaller.com) : For Creating an Installer for it's Complex Structure.
- [ExchangeRate-API](https://app.exchangerate-api.com) : For Currency Conversion. [ Free Plan, so Currency Conversion is Limited ]

---

## ⚠️ IMPORTANT NOTICE ⚠️

Please be aware: There are fraudulent repositories on GitHub that are cloning this project's name and using AI-generated readmes, but they contain **completely random and unrelated files in each release**. These are NOT official versions of this project.

**ALWAYS ensure you are downloading or cloning this project ONLY from its official and legitimate source:**
`https://github.com/Chill-Astro/Calculator`

I am trying my best to report these people.

---

## Note from Developer :

Appreciate my effort? Why not leave a Star ⭐ ! Also if forked, please credit me for my effort and thanks if you do! :)

---
