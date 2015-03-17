<?xml version="1.0" encoding="utf-8"?>
<package xmlns="http://schemas.microsoft.com/packaging/2012/06/nuspec.xsd">
    <metadata>
        <id>JF.Azure.TableStorage</id>
        <version>1.0.0</version>
        <title>JF.Azure.TableStorage</title>
        <authors>Josef Fazekas</authors>
        <owners>Josef Fazekas</owners>
        <requireLicenseAcceptance>false</requireLicenseAcceptance>
        <description>Microsoft Azure Table Storage helper</description>
        <copyright>Copyright © Josef Fazekas 2015</copyright>
        <language>en-GB</language>
        <tags>Azure, Table Storage</tags>
        <dependencies>
            <group targetFramework=".NETFramework4.5.1">
                <dependency id="Microsoft.Data.Edm" version="5.6.2" />
                <dependency id="Microsoft.Data.OData" version="5.6.2" />
                <dependency id="Microsoft.Data.Services.Client" version="5.6.2" />
                <dependency id="Newtonsoft.Json" version="5.0.8" />
                <dependency id="System.Spatial" version="5.6.2" />
                <dependency id="WindowsAzure.Storage" version="4.3.0" />
            </group>
        </dependencies>
        <frameworkAssemblies>
            <frameworkAssembly assemblyName="System" targetFramework=".NETFramework4.5.1" />
            <frameworkAssembly assemblyName="System.Core" targetFramework=".NETFramework4.5.1" />
            <frameworkAssembly assemblyName="System.Data" targetFramework=".NETFramework4.5.1" />
        </frameworkAssemblies>
    </metadata>
    <files>
        <file src="JF.Azure.TableStorage\bin\Release\JF.Azure.TableStorage.XML" target="lib\JF.Azure.TableStorage.XML" />
        <file src="JF.Azure.TableStorage\bin\Release\JF.Azure.TableStorage.pdb" target="lib\JF.Azure.TableStorage.pdb" />
        <file src="JF.Azure.TableStorage\bin\Release\JF.Azure.TableStorage.dll" target="lib\JF.Azure.TableStorage.dll" />
    </files>
</package>