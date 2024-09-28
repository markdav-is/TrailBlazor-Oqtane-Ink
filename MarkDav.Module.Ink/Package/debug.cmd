XCOPY "..\Client\bin\Debug\net8.0\MarkDav.Module.Ink.Client.Oqtane.dll" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\net8.0\" /Y
XCOPY "..\Client\bin\Debug\net8.0\MarkDav.Module.Ink.Client.Oqtane.pdb" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\net8.0\" /Y
XCOPY "..\Server\wwwroot\*" "..\..\oqtane.framework\Oqtane.Server\wwwroot\" /Y /S /I

XCOPY "..\..\ink\ink-engine-runtime\bin\Debug\netstandard2.0\ink-engine-runtime.dll" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\net8.0\" /Y
XCOPY "..\..\ink\ink-engine-runtime\bin\Debug\netstandard2.0\ink-engine-runtime.pdb" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\net8.0\" /Y
XCOPY "..\..\ink\compiler\bin\Debug\netstandard2.0\ink_compiler.dll" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\net8.0\" /Y
XCOPY "..\..\ink\compiler\bin\Debug\netstandard2.0\ink_compiler.pdb" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\net8.0\" /Y

