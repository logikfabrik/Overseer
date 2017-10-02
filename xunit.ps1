$files = Get-ChildItem $PSScriptRoot *.Test.dll -Recurse

foreach ($file in $files) {
    Write-Host $file.FullName
    & "xunit.console.x86.exe" $file.FullName -appveyor | Out-Host
}
