function Extract-Version-Number([string] $filecontent)
{
	$pattern='\d+\.\d+\.\d+\.\d+'

	if ($filecontent -match $pattern -eq $true)
	{
		return $matches[0]
	}

	return [string]::Empty
}

function Modify-Build-Number([string] $Version)
{
	$pattern="\d+\.\d+\.\d+\.\d+"

	$BuildNumber=$Env:BUILD_BUILDNUMBER -replace $pattern,$Version

	Write-Host "BuildNumber=$BuildNumber"

	Write-Verbose -Verbose "##vso[build.updatebuildnumber]$BuildNumber"
}

Write-Host "BUILD_SOURCESDIRECTORY: $env:BUILD_SOURCESDIRECTORY"
Write-Host "BUILD_BUILDNUMBER: $env:BUILD_BUILDNUMBER"

$Version=[string]::Empty

# find the SharedAssemblyInfo.cs file
$files=Get-ChildItem $Env:BUILD_SOURCESDIRECTORY -recurse -include SharedAssemblyInfo.cs

if ($files -and $files.count -gt 0)
{
	foreach ($file in $files)
	{
		Write-Host "FileName=$file"

		$FileContent=[IO.File]::ReadAllText($file,[Text.Encoding]::Default)

		$Version=Extract-Version-Number($FileContent)

		break
	}
}

Write-Host "Version=$Version"

Write-Verbose -Verbose "##vso[build.updatebuildnumber]$Version"

