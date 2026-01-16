using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using MahApps.Metro.IconPacks;

namespace WinKit
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<ProgramItem> _allPrograms = new();
        private ObservableCollection<ProgramItem> _displayedPrograms = new();
        private List<Category> _categories = new();
        private bool _isWingetAvailable = false;
        private bool _isInstalling = false;
        private CancellationTokenSource? _cancellationTokenSource;
        
        // Simple Icons mapping - WingetId -> PackIconSimpleIconsKind
        private static readonly Dictionary<string, PackIconSimpleIconsKind> IconMapping = new()
        {
            // Tarayıcılar
            {"Google.Chrome", PackIconSimpleIconsKind.GoogleChrome},
            {"Mozilla.Firefox", PackIconSimpleIconsKind.Firefox},
            {"Brave.Brave", PackIconSimpleIconsKind.Brave},
            {"Microsoft.Edge", PackIconSimpleIconsKind.MicrosoftEdge},
            {"Opera.OperaGX", PackIconSimpleIconsKind.Opera},
            {"Zen-Team.Zen-Browser", PackIconSimpleIconsKind.Windows11},
            {"VivaldiTechnologies.Vivaldi", PackIconSimpleIconsKind.Vivaldi},
            {"TheBrowserCompany.Arc", PackIconSimpleIconsKind.Arc},
            
            // İletişim
            {"Discord.Discord", PackIconSimpleIconsKind.Discord},
            {"Telegram.TelegramDesktop", PackIconSimpleIconsKind.Telegram},
            {"WhatsApp.WhatsApp", PackIconSimpleIconsKind.WhatsApp},
            {"SlackTechnologies.Slack", PackIconSimpleIconsKind.Slack},
            {"Zoom.Zoom", PackIconSimpleIconsKind.Zoom},
            {"Microsoft.Teams", PackIconSimpleIconsKind.MicrosoftTeams},
            {"TeamViewer.TeamViewer", PackIconSimpleIconsKind.TeamViewer},
            {"OpenWhisperSystems.Signal", PackIconSimpleIconsKind.Signal},
            
            // Oyun Platformları
            {"Valve.Steam", PackIconSimpleIconsKind.Steam},
            {"EpicGames.EpicGamesLauncher", PackIconSimpleIconsKind.EpicGames},
            {"ElectronicArts.EADesktop", PackIconSimpleIconsKind.Ea},
            {"Ubisoft.Connect", PackIconSimpleIconsKind.Ubisoft},
            {"GOG.Galaxy", PackIconSimpleIconsKind.Windows11},
            {"Blizzard.BattleNet", PackIconSimpleIconsKind.Windows11},
            {"Rockstar.Launcher", PackIconSimpleIconsKind.RockstarGames},
            {"Microsoft.GamingApp", PackIconSimpleIconsKind.Xbox},
            {"Amazon.Games", PackIconSimpleIconsKind.Amazon},
            {"itch.itch", PackIconSimpleIconsKind.Itchdotio},
            {"Tencent.Gameloop", PackIconSimpleIconsKind.TencentQq},
            {"ParadoxInteractive.ParadoxLauncher", PackIconSimpleIconsKind.Windows11},
            
            // GPU & Sürücüler
            {"Nvidia.GeForceExperience", PackIconSimpleIconsKind.Nvidia},
            {"Nvidia.Broadcast", PackIconSimpleIconsKind.Nvidia},
            {"AMD.RyzenMaster", PackIconSimpleIconsKind.Amd},
            {"Intel.IntelArcControl", PackIconSimpleIconsKind.Intel},
            {"Guru3D.Afterburner", PackIconSimpleIconsKind.Msi},
            {"TechPowerUp.GPU-Z", PackIconSimpleIconsKind.Windows11},
            {"REALiX.HWiNFO", PackIconSimpleIconsKind.Windows11},
            {"Wagnardsoft.DisplayDriverUninstaller", PackIconSimpleIconsKind.Windows11},
            {"TechPowerUp.NVCleanstall", PackIconSimpleIconsKind.Nvidia},
            {"Geeks3D.FurMark", PackIconSimpleIconsKind.Windows11},
            {"Guru3D.RTSS", PackIconSimpleIconsKind.Speedtest},
            
            // Geliştirme
            {"Microsoft.VisualStudioCode", PackIconSimpleIconsKind.VisualStudioCode},
            {"Microsoft.VisualStudio.2022.Community", PackIconSimpleIconsKind.VisualStudio},
            {"Anysphere.Cursor", PackIconSimpleIconsKind.Windows11},
            {"Git.Git", PackIconSimpleIconsKind.Git},
            {"GitHub.GitHubDesktop", PackIconSimpleIconsKind.GitHub},
            {"OpenJS.NodeJS.LTS", PackIconSimpleIconsKind.Nodedotjs},
            {"Python.Python.3.12", PackIconSimpleIconsKind.Python},
            {"Notepad++.Notepad++", PackIconSimpleIconsKind.NotepadPlusPlus},
            {"Microsoft.WindowsTerminal", PackIconSimpleIconsKind.WindowsTerminal},
            {"Microsoft.PowerShell", PackIconSimpleIconsKind.PowerShell},
            {"Docker.DockerDesktop", PackIconSimpleIconsKind.Docker},
            {"Postman.Postman", PackIconSimpleIconsKind.Postman},
            {"Google.AndroidStudio", PackIconSimpleIconsKind.AndroidStudio},
            {"JetBrains.Toolbox", PackIconSimpleIconsKind.JetBrains},
            {"SublimeHQ.SublimeText.4", PackIconSimpleIconsKind.SublimeText},
            {"CMake.CMake", PackIconSimpleIconsKind.CMake},
            {"ApacheFriends.Xampp", PackIconSimpleIconsKind.Xampp},
            {"jrsoftware.InnoSetup", PackIconSimpleIconsKind.Windows11},
            {"Embarcadero.Dev-Cpp", PackIconSimpleIconsKind.CPlusPlus},
            {"EclipseAdoptium.Temurin.21.JDK", PackIconSimpleIconsKind.EclipseIde},
            {"WixToolset.WiX", PackIconSimpleIconsKind.Windows11},
            
            // Multimedya
            {"VideoLAN.VLC", PackIconSimpleIconsKind.VlcMediaPlayer},
            {"Spotify.Spotify", PackIconSimpleIconsKind.Spotify},
            {"OBSProject.OBSStudio", PackIconSimpleIconsKind.ObsStudio},
            {"Blackmagic.DaVinciResolve", PackIconSimpleIconsKind.DaVinciResolve},
            {"Audacity.Audacity", PackIconSimpleIconsKind.Audacity},
            {"GIMP.GIMP", PackIconSimpleIconsKind.Gimp},
            {"HandBrake.HandBrake", PackIconSimpleIconsKind.Windows11},
            {"KDE.Kdenlive", PackIconSimpleIconsKind.Kdenlive},
            {"PeterPawlowski.foobar2000", PackIconSimpleIconsKind.Foobar2000},
            {"ShareX.ShareX", PackIconSimpleIconsKind.ShareX},
            {"NickeManarin.ScreenToGif", PackIconSimpleIconsKind.Windows11},
            {"Adobe.CreativeCloud", PackIconSimpleIconsKind.Adobe},
            
            // Dosya Araçları
            {"7zip.7zip", PackIconSimpleIconsKind._7Zip},
            {"RARLab.WinRAR", PackIconSimpleIconsKind.Windows11},
            {"voidtools.Everything", PackIconSimpleIconsKind.Windows11},
            {"qBittorrent.qBittorrent", PackIconSimpleIconsKind.Qbittorrent},
            {"SoftDeluxe.FreeDownloadManager", PackIconSimpleIconsKind.Windows11},
            {"AntibodySoftware.WizTree", PackIconSimpleIconsKind.Windows11},
            {"Google.QuickShare", PackIconSimpleIconsKind.Google},
            {"Syncthing.Syncthing", PackIconSimpleIconsKind.Syncthing},
            {"DigitalVolcano.DuplicateCleaner", PackIconSimpleIconsKind.Windows11},
            
            // Sistem Araçları
            {"Microsoft.PowerToys", PackIconSimpleIconsKind.Windows11},
            {"CPUID.CPU-Z", PackIconSimpleIconsKind.Windows11},
            {"CPUID.HWMonitor", PackIconSimpleIconsKind.Windows11},
            {"Microsoft.Sysinternals.ProcessExplorer", PackIconSimpleIconsKind.Windows11},
            {"Microsoft.Sysinternals.Autoruns", PackIconSimpleIconsKind.Windows11},
            {"JAMSoftware.TreeSize.Free", PackIconSimpleIconsKind.Windows11},
            {"RevoUninstaller.RevoUninstaller", PackIconSimpleIconsKind.Windows11},
            {"Winaero.Tweaker", PackIconSimpleIconsKind.Windows11},
            {"CrystalDewWorld.CrystalDiskInfo", PackIconSimpleIconsKind.Windows11},
            {"Piriform.Speccy", PackIconSimpleIconsKind.CCleaner},
            {"Razer.Synapse3", PackIconSimpleIconsKind.Razer},
            {"CheatEngine.CheatEngine", PackIconSimpleIconsKind.Windows11},
            
            // Güvenlik & VPN
            {"Bitwarden.Bitwarden", PackIconSimpleIconsKind.Bitwarden},
            {"ProtonTechnologies.ProtonVPN", PackIconSimpleIconsKind.Proton},
            {"NordVPN.NordVPN", PackIconSimpleIconsKind.NordVpn},
            {"Malwarebytes.Malwarebytes", PackIconSimpleIconsKind.Malwarebytes},
            {"KeePassXCTeam.KeePassXC", PackIconSimpleIconsKind.KeePassXc},
            {"Cloudflare.Warp", PackIconSimpleIconsKind.Cloudflare},
            {"WireGuard.WireGuard", PackIconSimpleIconsKind.WireGuard},
            
            // Ofis & Üretkenlik
            {"Notion.Notion", PackIconSimpleIconsKind.Notion},
            {"Obsidian.Obsidian", PackIconSimpleIconsKind.Obsidian},
            {"Adobe.Acrobat.Reader.64-bit", PackIconSimpleIconsKind.AdobeAcrobatReader},
            {"TheDocumentFoundation.LibreOffice", PackIconSimpleIconsKind.LibreOffice},
            {"SumatraPDF.SumatraPDF", PackIconSimpleIconsKind.Windows11},
            {"Doist.Todoist", PackIconSimpleIconsKind.Todoist},
            {"Logseq.Logseq", PackIconSimpleIconsKind.Logseq},
            {"Joplin.Joplin", PackIconSimpleIconsKind.Joplin},
        };
        
        public static PackIconSimpleIconsKind GetIconKind(string wingetId)
        {
            if (IconMapping.TryGetValue(wingetId, out var kind))
                return kind;
            return PackIconSimpleIconsKind.Windows11;
        }

        public MainWindow()
        {
            InitializeComponent();
            InitializeData();
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await CheckWingetStatus();
        }

        private void ThemeToggle_Click(object sender, RoutedEventArgs e)
        {
            App.ToggleTheme();
            ThemeIcon.Text = App.IsDarkTheme ? "🌙" : "☀️";
        }

        private void InitializeData()
        {
            _categories = new List<Category>
            {
                new Category { Name = "Tümü", Id = "all", Icon = "📋" },
                new Category { Name = "Tarayıcılar", Id = "browsers", Icon = "🌐" },
                new Category { Name = "İletişim", Id = "communication", Icon = "💬" },
                new Category { Name = "Oyun Platformları", Id = "gaming", Icon = "🎮" },
                new Category { Name = "Geliştirme", Id = "development", Icon = "💻" },
                new Category { Name = "Multimedya", Id = "multimedia", Icon = "🎨" },
                new Category { Name = "Dosya Araçları", Id = "files", Icon = "📁" },
                new Category { Name = "Sistem Araçları", Id = "system", Icon = "🔧" },
                new Category { Name = "GPU & Sürücüler", Id = "gpu", Icon = "🎯" },
                new Category { Name = "Güvenlik & VPN", Id = "security", Icon = "🔒" },
                new Category { Name = "Ofis & Üretkenlik", Id = "office", Icon = "📝" },
            };

            _allPrograms = new ObservableCollection<ProgramItem>
            {
                // Tarayıcılar
                new ProgramItem { Name = "Google Chrome", WingetId = "Google.Chrome", Category = "browsers", BrandColor = "#4285F4", Description = "Popüler web tarayıcısı" },
                new ProgramItem { Name = "Mozilla Firefox", WingetId = "Mozilla.Firefox", Category = "browsers", BrandColor = "#FF7139", Description = "Açık kaynak tarayıcı" },
                new ProgramItem { Name = "Brave Browser", WingetId = "Brave.Brave", Category = "browsers", BrandColor = "#FB542B", Description = "Gizlilik odaklı tarayıcı" },
                new ProgramItem { Name = "Microsoft Edge", WingetId = "Microsoft.Edge", Category = "browsers", BrandColor = "#0078D4", Description = "Microsoft tarayıcısı" },
                new ProgramItem { Name = "Opera GX", WingetId = "Opera.OperaGX", Category = "browsers", BrandColor = "#FA1E4E", Description = "Oyuncu tarayıcısı" },
                new ProgramItem { Name = "Zen Browser", WingetId = "Zen-Team.Zen-Browser", Category = "browsers", BrandColor = "#7C3AED", Description = "Minimalist tarayıcı" },
                new ProgramItem { Name = "Vivaldi", WingetId = "VivaldiTechnologies.Vivaldi", Category = "browsers", BrandColor = "#EF3939", Description = "Özelleştirilebilir tarayıcı" },
                new ProgramItem { Name = "Arc Browser", WingetId = "TheBrowserCompany.Arc", Category = "browsers", BrandColor = "#414CE8", Description = "Modern tarayıcı deneyimi" },

                // İletişim
                new ProgramItem { Name = "Discord", WingetId = "Discord.Discord", Category = "communication", BrandColor = "#5865F2", Description = "Oyuncu sohbet uygulaması" },
                new ProgramItem { Name = "Telegram", WingetId = "Telegram.TelegramDesktop", Category = "communication", BrandColor = "#26A5E4", Description = "Güvenli mesajlaşma" },
                new ProgramItem { Name = "WhatsApp", WingetId = "WhatsApp.WhatsApp", Category = "communication", BrandColor = "#25D366", Description = "Mesajlaşma uygulaması" },
                new ProgramItem { Name = "Slack", WingetId = "SlackTechnologies.Slack", Category = "communication", BrandColor = "#4A154B", Description = "İş iletişim platformu" },
                new ProgramItem { Name = "Zoom", WingetId = "Zoom.Zoom", Category = "communication", BrandColor = "#2D8CFF", Description = "Video konferans" },
                new ProgramItem { Name = "Microsoft Teams", WingetId = "Microsoft.Teams", Category = "communication", BrandColor = "#6264A7", Description = "Ekip işbirliği" },
                new ProgramItem { Name = "TeamViewer", WingetId = "TeamViewer.TeamViewer", Category = "communication", BrandColor = "#004680", Description = "Uzaktan erişim" },
                new ProgramItem { Name = "Signal", WingetId = "OpenWhisperSystems.Signal", Category = "communication", BrandColor = "#3A76F0", Description = "Şifreli mesajlaşma" },

                // Oyun Platformları
                new ProgramItem { Name = "Steam", WingetId = "Valve.Steam", Category = "gaming", BrandColor = "#1B2838", Description = "En büyük oyun platformu" },
                new ProgramItem { Name = "Epic Games", WingetId = "EpicGames.EpicGamesLauncher", Category = "gaming", BrandColor = "#313131", Description = "Epic oyun mağazası" },
                new ProgramItem { Name = "EA App", WingetId = "ElectronicArts.EADesktop", Category = "gaming", BrandColor = "#FF4747", Description = "EA oyun platformu" },
                new ProgramItem { Name = "Ubisoft Connect", WingetId = "Ubisoft.Connect", Category = "gaming", BrandColor = "#0070FF", Description = "Ubisoft platformu" },
                new ProgramItem { Name = "GOG Galaxy", WingetId = "GOG.Galaxy", Category = "gaming", BrandColor = "#86328A", Description = "DRM-free oyunlar" },
                new ProgramItem { Name = "Battle.net", WingetId = "Blizzard.BattleNet", Category = "gaming", BrandColor = "#00AEFF", Description = "Blizzard platformu" },
                new ProgramItem { Name = "Rockstar Launcher", WingetId = "Rockstar.Launcher", Category = "gaming", BrandColor = "#FCAF17", Description = "Rockstar platformu" },
                new ProgramItem { Name = "Xbox App", WingetId = "Microsoft.GamingApp", Category = "gaming", BrandColor = "#107C10", Description = "Xbox PC uygulaması" },
                new ProgramItem { Name = "Amazon Games", WingetId = "Amazon.Games", Category = "gaming", BrandColor = "#FF9900", Description = "Amazon oyun platformu" },
                new ProgramItem { Name = "Itch.io", WingetId = "itch.itch", Category = "gaming", BrandColor = "#FA5C5C", Description = "Indie oyun platformu" },
                new ProgramItem { Name = "GameLoop", WingetId = "Tencent.Gameloop", Category = "gaming", BrandColor = "#00A4FF", Description = "Android emülatör" },
                new ProgramItem { Name = "Paradox Launcher", WingetId = "ParadoxInteractive.ParadoxLauncher", Category = "gaming", BrandColor = "#DC143C", Description = "Paradox oyun platformu" },

                // GPU & Sürücüler
                new ProgramItem { Name = "NVIDIA App", WingetId = "Nvidia.GeForceExperience", Category = "gpu", BrandColor = "#76B900", Description = "NVIDIA GPU yönetimi" },
                new ProgramItem { Name = "NVIDIA Broadcast", WingetId = "Nvidia.Broadcast", Category = "gpu", BrandColor = "#76B900", Description = "AI ses/video geliştirme" },
                new ProgramItem { Name = "AMD Adrenalin", WingetId = "AMD.RyzenMaster", Category = "gpu", BrandColor = "#ED1C24", Description = "AMD GPU yazılımı" },
                new ProgramItem { Name = "Intel Arc Control", WingetId = "Intel.IntelArcControl", Category = "gpu", BrandColor = "#0071C5", Description = "Intel Arc GPU yönetimi" },
                new ProgramItem { Name = "MSI Afterburner", WingetId = "Guru3D.Afterburner", Category = "gpu", BrandColor = "#FF0000", Description = "GPU overclock aracı" },
                new ProgramItem { Name = "GPU-Z", WingetId = "TechPowerUp.GPU-Z", Category = "gpu", BrandColor = "#2D9CDB", Description = "GPU bilgi aracı" },
                new ProgramItem { Name = "HWiNFO", WingetId = "REALiX.HWiNFO", Category = "gpu", BrandColor = "#1E88E5", Description = "Donanım bilgisi" },
                new ProgramItem { Name = "DDU", WingetId = "Wagnardsoft.DisplayDriverUninstaller", Category = "gpu", BrandColor = "#E91E63", Description = "Sürücü temizleyici" },
                new ProgramItem { Name = "NVCleanstall", WingetId = "TechPowerUp.NVCleanstall", Category = "gpu", BrandColor = "#76B900", Description = "Temiz NVIDIA kurulumu" },
                new ProgramItem { Name = "FurMark", WingetId = "Geeks3D.FurMark", Category = "gpu", BrandColor = "#FF5722", Description = "GPU stres testi" },
                new ProgramItem { Name = "RivaTuner Statistics", WingetId = "Guru3D.RTSS", Category = "gpu", BrandColor = "#007ACC", Description = "FPS overlay & limiter" },

                // Geliştirme
                new ProgramItem { Name = "Visual Studio Code", WingetId = "Microsoft.VisualStudioCode", Category = "development", BrandColor = "#007ACC", Description = "Popüler kod editörü" },
                new ProgramItem { Name = "Visual Studio 2022", WingetId = "Microsoft.VisualStudio.2022.Community", Category = "development", BrandColor = "#5C2D91", Description = "Tam özellikli IDE" },
                new ProgramItem { Name = "Cursor", WingetId = "Anysphere.Cursor", Category = "development", BrandColor = "#000000", Description = "AI kod editörü" },
                new ProgramItem { Name = "Git", WingetId = "Git.Git", Category = "development", BrandColor = "#F05032", Description = "Versiyon kontrol" },
                new ProgramItem { Name = "GitHub Desktop", WingetId = "GitHub.GitHubDesktop", Category = "development", BrandColor = "#6E40C9", Description = "GitHub uygulaması" },
                new ProgramItem { Name = "Node.js LTS", WingetId = "OpenJS.NodeJS.LTS", Category = "development", BrandColor = "#339933", Description = "JavaScript runtime" },
                new ProgramItem { Name = "Python 3.12", WingetId = "Python.Python.3.12", Category = "development", BrandColor = "#3776AB", Description = "Python dili" },
                new ProgramItem { Name = "Notepad++", WingetId = "Notepad++.Notepad++", Category = "development", BrandColor = "#90E59A", Description = "Gelişmiş metin editörü" },
                new ProgramItem { Name = "Windows Terminal", WingetId = "Microsoft.WindowsTerminal", Category = "development", BrandColor = "#4D4D4D", Description = "Modern terminal" },
                new ProgramItem { Name = "PowerShell 7", WingetId = "Microsoft.PowerShell", Category = "development", BrandColor = "#5391FE", Description = "Modern PowerShell" },
                new ProgramItem { Name = "Docker Desktop", WingetId = "Docker.DockerDesktop", Category = "development", BrandColor = "#2496ED", Description = "Konteyner platformu" },
                new ProgramItem { Name = "Postman", WingetId = "Postman.Postman", Category = "development", BrandColor = "#FF6C37", Description = "API geliştirme aracı" },
                new ProgramItem { Name = "Android Studio", WingetId = "Google.AndroidStudio", Category = "development", BrandColor = "#3DDC84", Description = "Android IDE" },
                new ProgramItem { Name = "JetBrains Toolbox", WingetId = "JetBrains.Toolbox", Category = "development", BrandColor = "#FF318C", Description = "JetBrains IDE yönetimi" },
                new ProgramItem { Name = "Sublime Text", WingetId = "SublimeHQ.SublimeText.4", Category = "development", BrandColor = "#FF9800", Description = "Hızlı kod editörü" },
                new ProgramItem { Name = "CMake", WingetId = "CMake.CMake", Category = "development", BrandColor = "#064F8C", Description = "Cross-platform build" },
                new ProgramItem { Name = "XAMPP", WingetId = "ApacheFriends.Xampp", Category = "development", BrandColor = "#FB7A24", Description = "Web sunucu paketi" },
                new ProgramItem { Name = "Inno Setup", WingetId = "jrsoftware.InnoSetup", Category = "development", BrandColor = "#264BCC", Description = "Installer oluşturucu" },
                new ProgramItem { Name = "Dev-C++", WingetId = "Embarcadero.Dev-Cpp", Category = "development", BrandColor = "#00599C", Description = "C/C++ IDE" },
                new ProgramItem { Name = "Eclipse Temurin JDK", WingetId = "EclipseAdoptium.Temurin.21.JDK", Category = "development", BrandColor = "#FF7800", Description = "Java Development Kit" },
                new ProgramItem { Name = "WiX Toolset", WingetId = "WixToolset.WiX", Category = "development", BrandColor = "#FFC72C", Description = "MSI installer aracı" },

                // Multimedya
                new ProgramItem { Name = "VLC Player", WingetId = "VideoLAN.VLC", Category = "multimedia", BrandColor = "#FF8800", Description = "Evrensel oynatıcı" },
                new ProgramItem { Name = "Spotify", WingetId = "Spotify.Spotify", Category = "multimedia", BrandColor = "#1DB954", Description = "Müzik streaming" },
                new ProgramItem { Name = "OBS Studio", WingetId = "OBSProject.OBSStudio", Category = "multimedia", BrandColor = "#302E31", Description = "Yayın ve kayıt" },
                new ProgramItem { Name = "DaVinci Resolve", WingetId = "Blackmagic.DaVinciResolve", Category = "multimedia", BrandColor = "#E52222", Description = "Video düzenleme" },
                new ProgramItem { Name = "Audacity", WingetId = "Audacity.Audacity", Category = "multimedia", BrandColor = "#0000CC", Description = "Ses düzenleme" },
                new ProgramItem { Name = "GIMP", WingetId = "GIMP.GIMP", Category = "multimedia", BrandColor = "#5C5543", Description = "Resim düzenleme" },
                new ProgramItem { Name = "HandBrake", WingetId = "HandBrake.HandBrake", Category = "multimedia", BrandColor = "#00A0C1", Description = "Video dönüştürücü" },
                new ProgramItem { Name = "Kdenlive", WingetId = "KDE.Kdenlive", Category = "multimedia", BrandColor = "#527EB2", Description = "Açık kaynak video düzenleme" },
                new ProgramItem { Name = "foobar2000", WingetId = "PeterPawlowski.foobar2000", Category = "multimedia", BrandColor = "#FF6600", Description = "Gelişmiş müzik oynatıcı" },
                new ProgramItem { Name = "ShareX", WingetId = "ShareX.ShareX", Category = "multimedia", BrandColor = "#1E88E5", Description = "Ekran yakalama" },
                new ProgramItem { Name = "ScreenToGif", WingetId = "NickeManarin.ScreenToGif", Category = "multimedia", BrandColor = "#2ECC71", Description = "GIF kayıt aracı" },
                new ProgramItem { Name = "Adobe Creative Cloud", WingetId = "Adobe.CreativeCloud", Category = "multimedia", BrandColor = "#FF0000", Description = "Adobe uygulamaları hub" },

                // Dosya Araçları
                new ProgramItem { Name = "7-Zip", WingetId = "7zip.7zip", Category = "files", BrandColor = "#000000", Description = "Arşiv yöneticisi" },
                new ProgramItem { Name = "WinRAR", WingetId = "RARLab.WinRAR", Category = "files", BrandColor = "#6B2D75", Description = "Arşiv yöneticisi" },
                new ProgramItem { Name = "Everything", WingetId = "voidtools.Everything", Category = "files", BrandColor = "#FF8C00", Description = "Anında dosya arama" },
                new ProgramItem { Name = "qBittorrent", WingetId = "qBittorrent.qBittorrent", Category = "files", BrandColor = "#2F67BA", Description = "Torrent istemcisi" },
                new ProgramItem { Name = "Free Download Manager", WingetId = "SoftDeluxe.FreeDownloadManager", Category = "files", BrandColor = "#3498DB", Description = "İndirme yöneticisi" },
                new ProgramItem { Name = "WizTree", WingetId = "AntibodySoftware.WizTree", Category = "files", BrandColor = "#FF5722", Description = "Disk analizi" },
                new ProgramItem { Name = "Quick Share", WingetId = "Google.QuickShare", Category = "files", BrandColor = "#4285F4", Description = "Android dosya paylaşımı" },
                new ProgramItem { Name = "Syncthing", WingetId = "Syncthing.Syncthing", Category = "files", BrandColor = "#0891D1", Description = "Dosya senkronizasyonu" },
                new ProgramItem { Name = "Duplicate Cleaner", WingetId = "DigitalVolcano.DuplicateCleaner", Category = "files", BrandColor = "#E74C3C", Description = "Yinelenen dosya bulucu" },

                // Sistem Araçları
                new ProgramItem { Name = "PowerToys", WingetId = "Microsoft.PowerToys", Category = "system", BrandColor = "#0078D4", Description = "Windows güç araçları" },
                new ProgramItem { Name = "CPU-Z", WingetId = "CPUID.CPU-Z", Category = "system", BrandColor = "#0066CC", Description = "İşlemci bilgisi" },
                new ProgramItem { Name = "HWMonitor", WingetId = "CPUID.HWMonitor", Category = "system", BrandColor = "#2ECC71", Description = "Sıcaklık izleme" },
                new ProgramItem { Name = "Process Explorer", WingetId = "Microsoft.Sysinternals.ProcessExplorer", Category = "system", BrandColor = "#00BCD4", Description = "Gelişmiş görev yöneticisi" },
                new ProgramItem { Name = "Autoruns", WingetId = "Microsoft.Sysinternals.Autoruns", Category = "system", BrandColor = "#9C27B0", Description = "Başlangıç yönetimi" },
                new ProgramItem { Name = "TreeSize", WingetId = "JAMSoftware.TreeSize.Free", Category = "system", BrandColor = "#4CAF50", Description = "Disk kullanım analizi" },
                new ProgramItem { Name = "Revo Uninstaller", WingetId = "RevoUninstaller.RevoUninstaller", Category = "system", BrandColor = "#FF5722", Description = "Temiz kaldırma" },
                new ProgramItem { Name = "Winaero Tweaker", WingetId = "Winaero.Tweaker", Category = "system", BrandColor = "#607D8B", Description = "Windows özelleştirme" },
                new ProgramItem { Name = "CrystalDiskInfo", WingetId = "CrystalDewWorld.CrystalDiskInfo", Category = "system", BrandColor = "#00BCD4", Description = "Disk sağlığı" },
                new ProgramItem { Name = "Speccy", WingetId = "Piriform.Speccy", Category = "system", BrandColor = "#1976D2", Description = "Sistem bilgisi" },
                new ProgramItem { Name = "Razer Synapse 3", WingetId = "Razer.Synapse3", Category = "system", BrandColor = "#00FF00", Description = "Razer cihaz yönetimi" },
                new ProgramItem { Name = "Cheat Engine", WingetId = "CheatEngine.CheatEngine", Category = "system", BrandColor = "#FF0000", Description = "Memory scanner & debugger" },

                // Güvenlik & VPN
                new ProgramItem { Name = "Bitwarden", WingetId = "Bitwarden.Bitwarden", Category = "security", BrandColor = "#175DDC", Description = "Şifre yöneticisi" },
                new ProgramItem { Name = "Proton VPN", WingetId = "ProtonTechnologies.ProtonVPN", Category = "security", BrandColor = "#6D4AFF", Description = "Güvenli VPN" },
                new ProgramItem { Name = "NordVPN", WingetId = "NordVPN.NordVPN", Category = "security", BrandColor = "#4687FF", Description = "Popüler VPN" },
                new ProgramItem { Name = "Malwarebytes", WingetId = "Malwarebytes.Malwarebytes", Category = "security", BrandColor = "#0061EE", Description = "Kötü yazılım koruması" },
                new ProgramItem { Name = "KeePassXC", WingetId = "KeePassXCTeam.KeePassXC", Category = "security", BrandColor = "#6CAC4D", Description = "Açık kaynak şifre yönetimi" },
                new ProgramItem { Name = "Cloudflare WARP", WingetId = "Cloudflare.Warp", Category = "security", BrandColor = "#F48120", Description = "Hızlı DNS & VPN" },
                new ProgramItem { Name = "WireGuard", WingetId = "WireGuard.WireGuard", Category = "security", BrandColor = "#88171A", Description = "Modern VPN protokolü" },

                // Ofis & Üretkenlik
                new ProgramItem { Name = "Notion", WingetId = "Notion.Notion", Category = "office", BrandColor = "#000000", Description = "Not ve proje yönetimi" },
                new ProgramItem { Name = "Obsidian", WingetId = "Obsidian.Obsidian", Category = "office", BrandColor = "#7C3AED", Description = "Markdown notlar" },
                new ProgramItem { Name = "Adobe Reader", WingetId = "Adobe.Acrobat.Reader.64-bit", Category = "office", BrandColor = "#EC1C24", Description = "PDF okuyucu" },
                new ProgramItem { Name = "LibreOffice", WingetId = "TheDocumentFoundation.LibreOffice", Category = "office", BrandColor = "#18A303", Description = "Açık kaynak ofis" },
                new ProgramItem { Name = "SumatraPDF", WingetId = "SumatraPDF.SumatraPDF", Category = "office", BrandColor = "#FFCC00", Description = "Hafif PDF okuyucu" },
                new ProgramItem { Name = "Todoist", WingetId = "Doist.Todoist", Category = "office", BrandColor = "#E44332", Description = "Görev yönetimi" },
                new ProgramItem { Name = "Logseq", WingetId = "Logseq.Logseq", Category = "office", BrandColor = "#5FB236", Description = "Bilgi yönetimi" },
                new ProgramItem { Name = "Joplin", WingetId = "Joplin.Joplin", Category = "office", BrandColor = "#1071D3", Description = "Açık kaynak not alma" },
            };

            foreach (var category in _categories)
            {
                var textBlock = new TextBlock
                {
                    Text = $"{category.Icon}  {category.Name}",
                    FontSize = 13,
                    Tag = category.Id
                };
                textBlock.SetResourceReference(TextBlock.ForegroundProperty, "TextPrimaryBrush");
                CategoryList.Items.Add(textBlock);
            }

            CategoryList.SelectedIndex = 0;
            RefreshProgramList("all");
        }

        private void CategoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoryList.SelectedItem is TextBlock textBlock && textBlock.Tag is string categoryId)
            {
                RefreshProgramList(categoryId);
            }
        }

        private void RefreshProgramList(string categoryId)
        {
            _displayedPrograms.Clear();

            var programs = categoryId == "all"
                ? _allPrograms.ToList()
                : _allPrograms.Where(p => p.Category == categoryId).ToList();

            foreach (var program in programs.OrderBy(p => p.Name))
            {
                _displayedPrograms.Add(program);
            }

            ProgramsControl.ItemsSource = _displayedPrograms;

            var category = _categories.FirstOrDefault(c => c.Id == categoryId);
            CategoryTitle.Text = category?.Name ?? "Tüm Programlar";
            ProgramCount.Text = $"{programs.Count} program";

            UpdateSelectedCount();
        }

        private void ProgramCard_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is ProgramItem program)
            {
                program.IsSelected = !program.IsSelected;
                UpdateSelectedCount();
            }
        }

        private void SelectAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (var program in _displayedPrograms)
            {
                program.IsSelected = true;
            }
            UpdateSelectedCount();
        }

        private void DeselectAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (var program in _displayedPrograms)
            {
                program.IsSelected = false;
            }
            UpdateSelectedCount();
        }

        private async Task CheckWingetStatus()
        {
            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "winget",
                        Arguments = "--version",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true
                    }
                };

                process.Start();
                await process.WaitForExitAsync();

                if (process.ExitCode == 0)
                {
                    _isWingetAvailable = true;
                    WingetStatusDot.Fill = (SolidColorBrush)FindResource("SuccessBrush");
                    WingetStatusText.Text = "Winget hazır";
                }
                else
                {
                    SetWingetNotAvailable();
                }
            }
            catch
            {
                SetWingetNotAvailable();
            }
        }

        private void SetWingetNotAvailable()
        {
            _isWingetAvailable = false;
            WingetStatusDot.Fill = (SolidColorBrush)FindResource("ErrorBrush");
            WingetStatusText.Text = "Winget bulunamadı";
            InstallWingetAutomatically();
        }

        private void UpdateSelectedCount()
        {
            var count = _allPrograms.Count(p => p.IsSelected);
            SelectedCountText.Text = $"{count} program seçildi";
            InstallBtn.IsEnabled = count > 0 && _isWingetAvailable && !_isInstalling;
        }

        private void UpdateProgress(double percent, string status)
        {
            Dispatcher.Invoke(() =>
            {
                StatusText.Text = status;
                ProgressBar.Width = 400 * (percent / 100);
            });
        }

        private void UpdateOverlayProgress(string appName, int current, int total, int success, int fail)
        {
            Dispatcher.Invoke(() =>
            {
                double percent = total > 0 ? (double)current / total * 100 : 0;
                
                OverlayAppName.Text = appName;
                OverlayAppStatus.Text = "Kuruluyor...";
                OverlayProgressBar.Width = 356 * (percent / 100); // 420 - 64 padding
                OverlayProgressText.Text = $"{current} / {total}";
                OverlayPercentText.Text = $"{(int)percent}%";
                OverlaySuccessCount.Text = $"{success} başarılı";
                OverlayFailCount.Text = $"{fail} başarısız";
            });
        }

        private void ShowInstallOverlay()
        {
            Dispatcher.Invoke(() =>
            {
                InstallOverlay.Visibility = Visibility.Visible;
            });
        }

        private void HideInstallOverlay()
        {
            Dispatcher.Invoke(() =>
            {
                InstallOverlay.Visibility = Visibility.Collapsed;
            });
        }

        private void CancelInstall_Click(object sender, RoutedEventArgs e)
        {
            _cancellationTokenSource?.Cancel();
            CancelInstallBtn.IsEnabled = false;
            OverlayAppStatus.Text = "İptal ediliyor...";
        }

        private async void InstallWingetAutomatically()
        {
            StatusText.Text = "Winget kuruluyor...";

            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "powershell",
                        Arguments = "-ExecutionPolicy Bypass -Command \"Add-AppxPackage -RegisterByFamilyName -MainPackage Microsoft.DesktopAppInstaller_8wekyb3d8bbwe\"",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true,
                        Verb = "runas"
                    }
                };

                process.Start();
                await process.WaitForExitAsync();

                if (!_isWingetAvailable)
                {
                    StatusText.Text = "Winget indiriliyor...";
                    await DownloadAndInstallWinget();
                }

                await CheckWingetStatus();
            }
            catch (Exception ex)
            {
                StatusText.Text = "Winget kurulumu başarısız";
                MessageBox.Show($"Winget kurulumu başarısız: {ex.Message}\n\nManuel olarak Microsoft Store'dan 'App Installer' uygulamasını kurabilirsiniz.",
                    "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task DownloadAndInstallWinget()
        {
            using var client = new HttpClient();
            var releasesUrl = "https://api.github.com/repos/microsoft/winget-cli/releases/latest";
            client.DefaultRequestHeaders.Add("User-Agent", "WinKit");

            try
            {
                var response = await client.GetStringAsync(releasesUrl);
                var startIndex = response.IndexOf("https://github.com/microsoft/winget-cli/releases/download");
                if (startIndex > 0)
                {
                    var endIndex = response.IndexOf(".msixbundle", startIndex) + ".msixbundle".Length;
                    var downloadUrl = response.Substring(startIndex, endIndex - startIndex);

                    var tempPath = Path.Combine(Path.GetTempPath(), "winget.msixbundle");

                    StatusText.Text = "Winget paketi indiriliyor...";
                    var bytes = await client.GetByteArrayAsync(downloadUrl);
                    await File.WriteAllBytesAsync(tempPath, bytes);

                    StatusText.Text = "Winget kuruluyor...";
                    var process = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = "powershell",
                            Arguments = $"-ExecutionPolicy Bypass -Command \"Add-AppxPackage -Path '{tempPath}'\"",
                            UseShellExecute = true,
                            Verb = "runas"
                        }
                    };

                    process.Start();
                    await process.WaitForExitAsync();

                    File.Delete(tempPath);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Winget indirme hatası: {ex.Message}");
            }
        }

        private async void Install_Click(object sender, RoutedEventArgs e)
        {
            if (!_isWingetAvailable)
            {
                MessageBox.Show("Önce Winget kurulmalı!", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var selectedPrograms = _allPrograms.Where(p => p.IsSelected).ToList();
            if (selectedPrograms.Count == 0) return;

            _isInstalling = true;
            _cancellationTokenSource = new CancellationTokenSource();

            InstallBtn.IsEnabled = false;
            CancelInstallBtn.IsEnabled = true;
            ShowInstallOverlay();

            int successCount = 0;
            int failCount = 0;
            int currentIndex = 0;
            int total = selectedPrograms.Count;

            UpdateOverlayProgress(selectedPrograms[0].Name, 0, total, 0, 0);

            foreach (var program in selectedPrograms)
            {
                if (_cancellationTokenSource.Token.IsCancellationRequested)
                    break;

                currentIndex++;
                UpdateOverlayProgress(program.Name, currentIndex, total, successCount, failCount);
                UpdateProgress((double)(currentIndex - 1) / total * 100, $"{program.Name} kuruluyor...");

                try
                {
                    var process = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = "winget",
                            Arguments = $"install --id {program.WingetId} --silent --accept-package-agreements --accept-source-agreements",
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            CreateNoWindow = true,
                            StandardOutputEncoding = Encoding.UTF8,
                            StandardErrorEncoding = Encoding.UTF8
                        }
                    };

                    process.Start();
                    await process.WaitForExitAsync(_cancellationTokenSource.Token);

                    if (process.ExitCode == 0 || process.ExitCode == -1978335189)
                    {
                        successCount++;
                        program.IsSelected = false;
                    }
                    else
                    {
                        failCount++;
                    }
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch
                {
                    failCount++;
                }

                UpdateOverlayProgress(program.Name, currentIndex, total, successCount, failCount);
                await Task.Delay(100);
            }

            HideInstallOverlay();
            _isInstalling = false;
            UpdateSelectedCount();
            UpdateProgress(100, $"Tamamlandı: {successCount} başarılı, {failCount} başarısız");

            MessageBox.Show(
                $"Kurulum tamamlandı!\n\n✓ Başarılı: {successCount}\n✕ Başarısız: {failCount}",
                "Kurulum Sonucu",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            UpdateProgress(0, "");
        }
    }

    public class ProgramItem : INotifyPropertyChanged
    {
        private bool _isSelected;
        private static readonly Dictionary<string, string> PngIconMapping = new()
        {
            {"Google.Chrome", "chrome.png"},
            {"Microsoft.VisualStudioCode", "code.png"},
            {"Discord.Discord", "discord.png"},
            {"Microsoft.Edge", "edge.png"},
            {"Mozilla.Firefox", "firefox.png"},
            {"OpenJS.NodeJS.LTS", "node.png"},
            {"Notion.Notion", "notion.png"},
            {"Notepad++.Notepad++", "npp.png"},
            {"OBSProject.OBSStudio", "obs.png"},
            {"Python.Python.3.12", "python.png"},
            {"ShareX.ShareX", "sharex.png"},
            {"Spotify.Spotify", "spotify.png"},
            {"Microsoft.Teams", "teams.png"},
            {"Microsoft.WindowsTerminal", "terminal.png"},
            {"WhatsApp.WhatsApp", "whatsapp.png"},
            {"Zoom.Zoom", "zoom.png"},
        };

        public string Name { get; set; } = "";
        public string WingetId { get; set; } = "";
        public string Category { get; set; } = "";
        public string BrandColor { get; set; } = "#0078D4";
        public string Description { get; set; } = "";

        public PackIconSimpleIconsKind IconKind => MainWindow.GetIconKind(WingetId);
        
        public bool HasPngIcon => PngIconMapping.ContainsKey(WingetId);
        
        public string IconPath
        {
            get
            {
                if (PngIconMapping.TryGetValue(WingetId, out var iconFile))
                {
                    return $"pack://application:,,,/Resources/Icons/{iconFile}";
                }
                return "";
            }
        }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class Category
    {
        public string Name { get; set; } = "";
        public string Id { get; set; } = "";
        public string Icon { get; set; } = "";
    }

    public class StringToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string colorStr)
            {
                try
                {
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorStr));
                }
                catch
                {
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0078D4"));
                }
            }
            return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0078D4"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IconKindConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PackIconSimpleIconsKind kind)
                return kind;
            return PackIconSimpleIconsKind.Windows11;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class InverseBoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
                return b ? Visibility.Collapsed : Visibility.Visible;
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
