param(
    [string]$PpamPath = (Join-Path $PSScriptRoot "FII_PageTotals.ppam")
)

$destDir = Join-Path $env:APPDATA "Microsoft\AddIns"
New-Item -ItemType Directory -Force -Path $destDir | Out-Null
$destFile = Join-Path $destDir (Split-Path -Leaf $PpamPath)
Copy-Item -LiteralPath $PpamPath -Destination $destFile -Force

$key = "HKCU:\Software\Microsoft\Office\16.0\PowerPoint\AddIns\FII_PageTotals"
New-Item -Path $key -Force | Out-Null
New-ItemProperty -Path $key -Name "Path" -Value $destFile -PropertyType String -Force | Out-Null
New-ItemProperty -Path $key -Name "AutoLoad" -Value 1 -PropertyType DWord -Force | Out-Null
New-ItemProperty -Path $key -Name "LoadBehavior" -Value 3 -PropertyType DWord -Force | Out-Null

Write-Output "Installed: $destFile"
Write-Output "Registry: $key"
Write-Output "Restart PowerPoint, then check File > Options > Add-ins > PowerPoint Add-ins."
