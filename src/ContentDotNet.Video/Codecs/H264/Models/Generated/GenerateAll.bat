@ECHO OFF
ECHO Generating...

..\..\..\..\..\..\generators\Misc\TypeGenerator\bin\Debug\net8.0\TypeGenerator.exe ..\Types\NalUnits.types.json

echo [GENERATED] NalUnits.types.json

..\..\..\..\..\..\generators\Misc\TypeGenerator\bin\Debug\net8.0\TypeGenerator.exe ..\Types\SpsPpsVuiHrd.types.json

echo [GENERATED] SpsPpsVuiHrd.types.json

..\..\..\..\..\..\generators\Misc\TypeGenerator\bin\Debug\net8.0\TypeGenerator.exe ..\Types\SliceHeader.types.json

echo [GENERATED] SpsPpsVuiHrd.types.json

echo Done. :)
