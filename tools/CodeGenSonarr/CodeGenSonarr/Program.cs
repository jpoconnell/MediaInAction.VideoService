// See https://aka.ms/new-console-template for more information

using NSwag;
using NSwag.CodeGeneration.CSharp;

System.Net.WebClient wclient = new System.Net.WebClient();         

var document = await OpenApiDocument.FromJsonAsync(wclient.DownloadString("https://raw.githubusercontent.com/Sonarr/Sonarr/develop/src/Sonarr.Api.V3/openapi.json"));

wclient.Dispose();

var settings = new CSharpClientGeneratorSettings
{
    ClassName = "SonarrProxy", 
    CSharpGeneratorSettings = 
    {
        Namespace = "MediaInAction.Sonarr.Api.V3"
    }
};

var generator = new CSharpClientGenerator(document, settings);	
var code = generator.GenerateFile();

System.IO.File.WriteAllText("SonarrProxy.cs", code);