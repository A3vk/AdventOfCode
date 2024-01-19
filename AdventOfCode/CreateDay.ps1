param (
    [Parameter(Mandatory=$true)]
    [int]$year,
    [Parameter(Mandatory=$true)]
    [int]$day,
    [string]$name
)

$dayName = 'Day{0:D2}' -f $day

Copy-Item ./EmptyDay ./$year -Recurse
Rename-Item ./$year/EmptyDay $dayName
((Get-Content -path ./$year/$dayName/Solution.cs -Raw) -replace "EmptyDay", "_$year.$dayName") | Set-Content -Path ./$year/$dayName/Solution.cs
((Get-Content -path ./$year/$dayName/Solution.cs -Raw) -replace "// Problem name", "[ProblemName(`"$name`")]") | Set-Content -Path ./$year/$dayName/Solution.cs