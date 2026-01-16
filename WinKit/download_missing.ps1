$destPath = "e:\kurulumsonrasi\KurulumSonrasi\Resources\Icons"

# Eksik ikonlar için alternatif kaynaklar
$icons = @{
    # İletişim eksik
    "zoom" = "https://st1.zoom.us/zoom.ico"
    "teams" = "https://statics.teams.cdn.office.net/evergreen-assets/icons/microsoft_teams_logo.png"
    "teamviewer" = "https://www.teamviewer.com/favicon.ico"
    
    # Oyun eksik
    "ea" = "https://www.ea.com/assets/images/ea-logo-white.png"
    "itch" = "https://static.itch.io/images/itchio-textless-black.svg"
    
    # GPU eksik
    "amd" = "https://www.amd.com/content/dam/amd/en/images/logos/amd-logo-master-rgb-for-online.svg"
    "intel" = "https://www.intel.com/content/dam/www/central-libraries/us/en/images/2022-08/intel-logomark-blue-mono-rgb.svg"
    "msi" = "https://storage-asset.msi.com/global/picture/favicon.ico"
    
    # Geliştirme eksik
    "cursor" = "https://www.cursor.so/brand/icon.png"
    "terminal" = "https://raw.githubusercontent.com/microsoft/terminal/main/res/terminal/Terminal.ico"
    "android" = "https://developer.android.com/static/images/brand/Android_Robot.png"
    
    # Multimedya eksik
    "obs" = "https://obsproject.com/assets/images/new_icon_small-r.png"
    "davinci" = "https://www.blackmagicdesign.com/favicon.ico"
    "gimp" = "https://www.gimp.org/images/frontpage/wilber-big.png"
    "handbrake" = "https://handbrake.fr/img/logo.png"
    "kdenlive" = "https://kdenlive.org/favicon.ico"
    "sharex" = "https://getsharex.com/img/ShareX_Logo.png"
    
    # Dosya eksik
    "winrar" = "https://www.win-rar.com/fileadmin/images/winrar-logo.png"
    "qbittorrent" = "https://www.qbittorrent.org/favicon.ico"
    
    # Sistem eksik
    "powertoys" = "https://raw.githubusercontent.com/microsoft/PowerToys/main/.github/images/logo/PowerToys.png"
    "razer" = "https://www.razer.com/favicon.ico"
    
    # Güvenlik eksik
    "protonvpn" = "https://protonvpn.com/favicon.ico"
    "nordvpn" = "https://nordvpn.com/favicon.ico"
    "malwarebytes" = "https://www.malwarebytes.com/favicon.ico"
    "keepass" = "https://keepass.info/favicon.ico"
    "cloudflare" = "https://www.cloudflare.com/favicon.ico"
    "wireguard" = "https://www.wireguard.com/img/icons/favicon-196x196.png"
    
    # Ofis eksik
    "notion" = "https://www.notion.so/images/favicon.ico"
    "libreoffice" = "https://www.libreoffice.org/themes/flavour-libre/img/logo.svg"
    "todoist" = "https://todoist.com/favicon.ico"
    "logseq" = "https://logseq.com/icon.png"
    "joplin" = "https://joplinapp.org/images/Icon512.png"
}

$headers = @{
    "User-Agent" = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36"
}

foreach ($icon in $icons.GetEnumerator()) {
    $dest = Join-Path $destPath "$($icon.Key).png"
    if (Test-Path $dest) {
        Write-Host "SKIP: $($icon.Key) (zaten var)" -ForegroundColor Gray
        continue
    }
    try {
        Invoke-WebRequest -Uri $icon.Value -OutFile $dest -Headers $headers -ErrorAction Stop -TimeoutSec 10
        Write-Host "OK: $($icon.Key)" -ForegroundColor Green
    } catch {
        Write-Host "FAIL: $($icon.Key)" -ForegroundColor Red
    }
    Start-Sleep -Milliseconds 500
}

Write-Host "`nTamamlandi!" -ForegroundColor Cyan
