$files = Get-ChildItem $PSScriptRoot *.Test.dll -Recurse

foreach ($file in $files) {
    Write-Host $file.FullName
    & "C:\Tools\xUnit20\xunit.console.x86.exe" $file.FullName -appveyor | Out-Host
}
