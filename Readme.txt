First, disable MSIX Packaging in Calculator Properties in Visual Studio.

Export using this code :

dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true