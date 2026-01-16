$destPath = "e:\kurulumsonrasi\KurulumSonrasi\Resources\Icons"

$icons = @{
    # Tarayıcılar
    "chrome" = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e1/Google_Chrome_icon_%28February_2022%29.svg/120px-Google_Chrome_icon_%28February_2022%29.svg.png"
    "firefox" = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a0/Firefox_logo%2C_2019.svg/120px-Firefox_logo%2C_2019.svg.png"
    "brave" = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c4/Brave_lion.png/120px-Brave_lion.png"
    "edge" = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/98/Microsoft_Edge_logo_%282019%29.svg/120px-Microsoft_Edge_logo_%282019%29.svg.png"
    "opera" = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/49/Opera_2015_icon.svg/120px-Opera_2015_icon.svg.png"
    "vivaldi" = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e4/Vivaldi_web_browser_logo.svg/120px-Vivaldi_web_browser_logo.svg.png"
    "arc" = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/01/Arc_%28browser%29_logo.svg/120px-Arc_%28browser%29_logo.svg.png"
    "zen" = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5b/Zen_Browser_Logo.png/120px-Zen_Browser_Logo.png"
    "operagx" = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/35/Opera_GX_browser_logo.svg/120px-Opera_GX_browser_logo.svg.png"
    
    # İletişim
    "discord" = "https://upload.wikimedia.org/wikipedia/tr/thumb/f/f4/Discord_logo.svg/120px-Discord_logo.svg.png"
    "telegram" = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/82/Telegram_logo.svg/120px-Telegram_logo.svg.png"
    "whatsapp" = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/6b/WhatsApp.svg/120px-WhatsApp.svg.png"
    "slack" = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d5/Slack_icon_2019.svg/120px-Slack_icon_2019.svg.png"
    "zoom" = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7b/Zoom_Communications_Logo.svg/120px-Zoom_Communications_Logo.svg.png"
    "teams" = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c9/Microsoft_Office_Teams_%282018%E2%80%93present%29.svg/120px-Microsoft_Office_Teams_%282018%E2%80%93present%29.svg.png"
    "teamviewer" = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/90/TeamViewer_logo.svg/120px-TeamViewer_logo.svg.png"
    "signal" = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/8d/Signal-Logo.svg/120px-Signal-Logo.svg.png"
    
    # Oyun Platformları
    "steam" = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/Steam_icon_logo.svg/120px-Steam_icon_logo.svg.png"
    "epic" = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/31/Epic_Games_logo.svg/120px-Epic_Games_logo.svg.png"
    "ea" = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/0d/EA_logo_2022.svg/120px-EA_logo_2022.svg.png"
    "ubisoft" = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/78/Ubisoft_logo.svg/120px-Ubisoft_logo.svg.png"
    "gog" = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/2e/GOG.com_logo.svg/120px-GOG.com_logo.svg.png"
    "battlenet" = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/23/Blizzard_Entertainment_Logo_2015.svg/120px-Blizzard_Entertainment_Logo_2015.svg.png"
    "rockstar" = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/53/Rockstar_Games_Logo.svg/120px-Rockstar_Games_Logo.svg.png"
    "xbox" = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f9/Xbox_one_logo.svg/120px-Xbox_one_logo.svg.png"
    "amazon" = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a9/Amazon_logo.svg/120px-Amazon_logo.svg.png"
    "itch" = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/18/Itchio_textless_logo.svg/120px-Itchio_textless_logo.svg.png"
    
    # GPU & Sürücüler
    "nvidia" = "https://upload.wikimedia.org/wikipedia/sco/thumb/2/21/Nvidia_logo.svg/120px-Nvidia_logo.svg.png"
    "amd" = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7c/AMD_Logo.svg/120px-AMD_Logo.svg.png"
    "intel" = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c9/Intel-logo.svg/120px-Intel-logo.svg.png"
    "msi" = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/13/MSI_Logo.svg/120px-MSI_Logo.svg.png"
    
    # Geliştirme
    "vscode" = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9a/Visual_Studio_Code_1.35_icon.svg/120px-Visual_Studio_Code_1.35_icon.svg.png"
    "vs" = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/2c/Visual_Studio_Icon_2022.svg/120px-Visual_Studio_Icon_2022.svg.png"
    "git" = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/3f/Git_icon.svg/120px-Git_icon.svg.png"
    "github" = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/91/Octicons-mark-github.svg/120px-Octicons-mark-github.svg.png"
    "nodejs" = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d9/Node.js_logo.svg/120px-Node.js_logo.svg.png"
    "python" = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c3/Python-logo-notext.svg/120px-Python-logo-notext.svg.png"
    "notepadpp" = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/69/Notepad%2B%2B_Logo.svg/120px-Notepad%2B%2B_Logo.svg.png"
    "terminal" = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/48/Windows_Terminal_Logo.svg/120px-Windows_Terminal_Logo.svg.png"
    "powershell" = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/af/PowerShell_Core_6.0_icon.png/120px-PowerShell_Core_6.0_icon.png"
    "docker" = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/4e/Docker_%28container_engine%29_logo.svg/120px-Docker_%28container_engine%29_logo.svg.png"
    "postman" = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c2/Postman_%28software%29.png/120px-Postman_%28software%29.png"
    "android" = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c1/Android_Studio_icon_%282023%29.svg/120px-Android_Studio_icon_%282023%29.svg.png"
    "jetbrains" = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9c/IntelliJ_IDEA_Icon.svg/120px-IntelliJ_IDEA_Icon.svg.png"
    "sublime" = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/79/Breezeicons-apps-48-sublime-text.svg/120px-Breezeicons-apps-48-sublime-text.svg.png"
    "cmake" = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/13/Cmake.svg/120px-Cmake.svg.png"
    "xampp" = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/03/Xampp_logo.svg/120px-Xampp_logo.svg.png"
    
    # Multimedya
    "vlc" = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e6/VLC_Icon.svg/120px-VLC_Icon.svg.png"
    "spotify" = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/84/Spotify_icon.svg/120px-Spotify_icon.svg.png"
    "obs" = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/14/Open_Broadcaster_Software_Logo.png/120px-Open_Broadcaster_Software_Logo.png"
    "davinci" = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/90/DaVinci_Resolve_17_logo.svg/120px-DaVinci_Resolve_17_logo.svg.png"
    "audacity" = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f6/Audacity_Logo.svg/120px-Audacity_Logo.svg.png"
    "gimp" = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/45/The_GIMP_icon_-_gnome.svg/120px-The_GIMP_icon_-_gnome.svg.png"
    "handbrake" = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d9/Handbrake_Icon.png/120px-Handbrake_Icon.png"
    "kdenlive" = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/60/Kdenlive-logo.svg/120px-Kdenlive-logo.svg.png"
    "sharex" = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/2f/ShareX_Logo.png/120px-ShareX_Logo.png"
    "adobe" = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/4c/Adobe_Creative_Cloud_rainbow_icon.svg/120px-Adobe_Creative_Cloud_rainbow_icon.svg.png"
    
    # Dosya Araçları
    "7zip" = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f2/7ziplogo.svg/120px-7ziplogo.svg.png"
    "winrar" = "https://upload.wikimedia.org/wikipedia/en/thumb/9/95/WinRAR_Logo.png/120px-WinRAR_Logo.png"
    "qbittorrent" = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/66/New_qBittorrent_Logo.svg/120px-New_qBittorrent_Logo.svg.png"
    "syncthing" = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/SyncthingAugworpress.png/120px-SyncthingAugworpress.png"
    
    # Sistem Araçları
    "powertoys" = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/Microsoft_PowerToys_icon.svg/120px-Microsoft_PowerToys_icon.svg.png"
    "razer" = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/96/Razer_snake_wordmark.svg/120px-Razer_snake_wordmark.svg.png"
    
    # Güvenlik & VPN
    "bitwarden" = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/cc/Bitwarden_logo.svg/120px-Bitwarden_logo.svg.png"
    "keepass" = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/40/Keepassxc_icon.svg/120px-Keepassxc_icon.svg.png"
    "wireguard" = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a7/WireGuard_logo.svg/120px-WireGuard_logo.svg.png"
    "cloudflare" = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/94/Cloudflare_Logo.png/120px-Cloudflare_Logo.png"
    "protonvpn" = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ab/Proton_VPN_logo.svg/120px-Proton_VPN_logo.svg.png"
    "nordvpn" = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/6b/NordVPN_logo.svg/120px-NordVPN_logo.svg.png"
    "malwarebytes" = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/99/Malwarebytes_logo_and_wordmark.svg/120px-Malwarebytes_logo_and_wordmark.svg.png"
    
    # Ofis & Üretkenlik
    "notion" = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e9/Notion-logo.svg/120px-Notion-logo.svg.png"
    "obsidian" = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/10/2023_Obsidian_logo.svg/120px-2023_Obsidian_logo.svg.png"
    "acrobat" = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/42/Adobe_Acrobat_DC_logo_2020.svg/120px-Adobe_Acrobat_DC_logo_2020.svg.png"
    "libreoffice" = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/80/LibreOffice_6.1_Start_Center_Icon.svg/120px-LibreOffice_6.1_Start_Center_Icon.svg.png"
    "todoist" = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/Todoist_logo.svg/120px-Todoist_logo.svg.png"
    "logseq" = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ab/Logseq_Logo.svg/120px-Logseq_Logo.svg.png"
    "joplin" = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a4/Joplin-icon.svg/120px-Joplin-icon.svg.png"
}

foreach ($icon in $icons.GetEnumerator()) {
    $dest = Join-Path $destPath "$($icon.Key).png"
    try {
        Invoke-WebRequest -Uri $icon.Value -OutFile $dest -UserAgent "Mozilla/5.0" -ErrorAction Stop
        Write-Host "OK: $($icon.Key)" -ForegroundColor Green
    } catch {
        Write-Host "FAIL: $($icon.Key) - $($_.Exception.Message)" -ForegroundColor Red
    }
}
Write-Host "`nDownload complete!" -ForegroundColor Cyan
