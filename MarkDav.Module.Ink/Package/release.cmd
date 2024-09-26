del "*.nupkg"
"..\..\oqtane.framework\oqtane.package\nuget.exe" pack MarkDav.Module.Ink.nuspec 
XCOPY "*.nupkg" "..\..\oqtane.framework\Oqtane.Server\Packages\" /Y

