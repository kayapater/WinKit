# ⚡ WinKit

**Quick App Installer for Windows** - Install 100+ popular apps with one click using Windows Package Manager (winget).

![.NET 8.0](https://img.shields.io/badge/.NET-8.0-512BD4)
![WPF](https://img.shields.io/badge/WPF-Windows-0078D4)
![License](https://img.shields.io/badge/License-MIT-green)

<p align="center">
  <img src="Resources/winkit.png" alt="WinKit Logo" width="128"/>
</p>

## Features

- 🚀 **100+ Popular Apps** - Browsers, dev tools, gaming platforms, and more
- 🎨 **Modern UI** - Dark and light theme support
- 📦 **Winget Integration** - Automatic winget installation and management
- 🔍 **Category Filtering** - Filter apps by category
- ⚡ **Batch Install** - Install selected apps sequentially
- 🖼️ **Real Logos** - Professional look with brand logos

## Categories

- 🌐 Browsers
- 💬 Communication
- 🎮 Gaming Platforms
- 💻 Development Tools
- 🎨 Multimedia
- 📁 File Tools
- 🔧 System Utilities
- 🎯 GPU & Drivers
- 🔒 Security & VPN
- 📝 Office & Productivity

## Requirements

- Windows 10/11
- .NET 8.0 Runtime
- Winget (auto-installed if missing)

## Installation

### Build from Source

1. Install [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
2. Clone the repository:
   ```bash
   git clone https://github.com/kayapater/WinKit.git
   ```
3. Build and run:
   ```bash
   cd winkit
   dotnet build
   dotnet run
   ```

### Publish

Create a single-file executable:

```bash
dotnet publish -c Release
```

## Usage

1. Launch the application
2. Select a category from the sidebar
3. Click on apps you want to install
4. Click "Seçilenleri Yükle" (Install Selected) button
5. Wait for installation to complete

## Screenshots

### Dark Theme
Modern and eye-friendly dark interface.

### Light Theme
Clean and bright appearance.

## Technologies

- **Framework:** .NET 8.0 WPF
- **Package Manager:** Winget
- **Icon Pack:** MahApps.Metro.IconPacks.SimpleIcons
- **JSON:** Newtonsoft.Json

## License

MIT License - See [LICENSE](LICENSE) for details.

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/new-feature`)
3. Commit your changes (`git commit -am 'Add new feature'`)
4. Push the branch (`git push origin feature/new-feature`)
5. Open a Pull Request

## Contact

For questions or suggestions, please open an issue.
