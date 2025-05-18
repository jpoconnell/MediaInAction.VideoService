using System;
using System.IO;
using System.Linq;
using System.Text;

var frontends = new[] { "public-web" };
var gateways = new[] {"web", "web-public"};
var services = new[] {"Administration", "Identity", "Emby", "Deluge", "Cmskit" , "File", "Video", "Trakt"};
var grpcServer = new[]  { "video"};
var grpcClient = new[]  { "emby", "deluge" , "file", "trakt"};

string WriteCopyStatements(string type, string name)
{
    var t = type;
    var n = name.ToLower();
    var u = "";
    if ((name.Length > 0) && (name == "services"))
    {
         u = char.ToUpper(n[0]) + n[1..]  + "Service";
    }

    var s = new StringBuilder();
    
    string dir = Directory.GetParent(Environment.CurrentDirectory)
        .Parent // bin
        .Parent
        .Parent
        .Parent
        .FullName;

    string majorDir = Directory.GetParent(Environment.CurrentDirectory)
        .Parent // bin
        .FullName;

    string myDir2 = "";
    if (type == "gateways")
    {
        s.AppendLine($"# {type.ToLower()}" );
        myDir2 = majorDir + "/" + type + "/" ;
        string[] dir2 = Directory.GetDirectories(myDir2);
        foreach (var myDir3 in dir2)
        {
            var myDir4 = Path.GetRelativePath(majorDir, myDir3);
            var myDir5 = myDir3  + "/src/";
            string[] dir5 = Directory.GetDirectories(myDir5);
            string[] dirStubs = dir5[0].Split('/');
            int totCnt = dirStubs.Count();
            var cnt = 0;
            var project = "";
            foreach (var myStub in dirStubs)
            {
                if (cnt == totCnt - 1)
                {
                    project = myStub;
                }

                cnt++;
            }
            s.AppendLine($"COPY \"{myDir4}/src/{project}/{project}.csproj\" \"{myDir4}/src/{project}/{project}.csproj\"");
        }
    }
    else if (type == "libraries")
    {
        s.AppendLine($"# {type.ToLower()}" );
        var myType = "libs";
        myDir2 = majorDir + "/" + myType ;
        string[] dir2 = Directory.GetDirectories(myDir2);
        foreach (var myDir3 in dir2)
        {
            var myDir4 = Path.GetRelativePath(majorDir, myDir3);
            string[] dirStubs = myDir3.Split('/');
            int totCnt = dirStubs.Count();
            var cnt = 0;
            var project = "";
            foreach (var myStub in dirStubs)
            {
                if (cnt == totCnt - 1)
                {
                    project = myStub;
                }

                cnt++;
            }
            s.AppendLine($"COPY \"{myDir4}/{project}.csproj\" \"{myDir4}/{project}.csproj\"");
        }
    }
    else if (type == "shared")
    { 
        s.AppendLine($"# {type.ToLower()}" );
        myDir2 = majorDir + "/" + type;
        string[] dir2 = Directory.GetDirectories(myDir2);
        foreach (var myDir3 in dir2)
        {
            var myDir4 = Path.GetRelativePath(majorDir, myDir3);
            string[] dirStubs = myDir3.Split('/');
            int totCnt = dirStubs.Count();
            var cnt = 0;
            var project = "";
            foreach (var myStub in dirStubs)
            {
                if (cnt == totCnt - 1)
                {
                    project = myStub;
                }

                cnt++;
            }
            s.AppendLine($"COPY \"{myDir4}/{project}.csproj\" \"{myDir4}/{project}.csproj\"");
        }
    }
    else if (type == "frontends")
    {
        var myType = "apps";
        if (name == "angular")
        {
            myDir2 = majorDir + "/" + myType + "/" + name.ToLower() ;
        }
        else if (name == "public-web")
        {
            s.AppendLine($"# {type.ToLower()}" );
            myDir2 = majorDir + "/"+ myType + "/" + name.ToLower() + "/src/";
            var copyDir = dir + "/"+ myType + "/" + name.ToLower() + "/src/";
            string[] dir2 = Directory.GetDirectories(myDir2);
            foreach (var myDir3 in dir2)
            {
                var myDir4 = Path.GetRelativePath(majorDir, myDir3);
                string[] dirStubs = myDir3.Split('/');
                int totCnt = dirStubs.Count();
                var cnt = 0;
                var project = "";
                foreach (var myStub in dirStubs)
                {
                    if (cnt == totCnt - 1)
                    {
                        project = myStub;
                    }

                    cnt++;
                }
                s.AppendLine($"COPY \"{myDir4}/{project}.csproj\" \"{myDir4}/{project}.csproj\"");
            }
        }
    }
    else if (type == "services")
    {
        s.AppendLine($"# {name.ToLower()}" );
        myDir2 = majorDir + "/" + type + "/" + name.ToLower() + "/src/";

        string[] dir2 = Directory.GetDirectories(myDir2);
        foreach (var myDir3 in dir2)
        {
            var myDir4 = Path.GetRelativePath(majorDir, myDir3);
            string[] dirStubs = myDir3.Split('/');
            int totCnt = dirStubs.Count();
            var cnt = 0;
            var project = "";
            foreach (var myStub in dirStubs)
            {
                if (cnt == totCnt - 1)
                {
                    project = myStub;
                }

                cnt++;
            }
            s.AppendLine($"COPY \"{myDir4}/{project}.csproj\" \"{myDir4}/{project}.csproj\"");
        }

        myDir2 = majorDir + "/" + type + "/" + name.ToLower() + "/test/";
        string[] dir3 = Directory.GetDirectories(myDir2);
        foreach (var myDir in dir3)
        {
            var myDir5 = Path.GetRelativePath(majorDir, myDir);
            string[] dirStubs2 = myDir5.Split('/');
            int totCnt2 = dirStubs2.Count();
            var cnt2 = 0;
            var project2 = "";
            foreach (var myStub in dirStubs2)
            {
                if (cnt2 == totCnt2 - 1)
                {
                    project2 = myStub;
                }
                cnt2++;
            }
            s.AppendLine($"COPY \"{myDir5}/{project2}.csproj\" \"{myDir5}/{project2}.csproj\"");
        }
    }
    return s.ToString();
}

// Generates dockerfiles in a consistent mechanism to take best advantage of the caching
string GenerateDockerfile(string type, string name)
{
    var t = type;
    var n = name;
    var u = char.ToUpper(n[0]) + n[1..];
    var s = new StringBuilder();
    var projectName = "";
    s.AppendLine("FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base");
    s.AppendLine("WORKDIR /app");
    s.AppendLine("EXPOSE 80");
    s.AppendLine("EXPOSE 81");
    
    s.AppendLine("");
    s.AppendLine("# add globalization support");
    s.AppendLine("RUN apk add --no-cache icu-libs");
    s.AppendLine("ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false");
    s.AppendLine("");
    s.AppendLine("# add diagnostic tools");
    s.AppendLine("RUN apk add --no-cache curl");
    
    s.AppendLine("");

    s.AppendLine("FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build");
    s.AppendLine("WORKDIR /work");
    s.AppendLine("");

    s.AppendLine("# Start build cache");
    s.AppendLine("COPY \"MediaInAction.sln\" \"MediaInAction.sln\"");
    s.AppendLine("");

    s.AppendLine("WORKDIR /work/src");
    foreach (var frontend in frontends)
    {
        type = "frontends";
        s.AppendLine(WriteCopyStatements(type,frontend));
    }

    type = "gateways";
    s.AppendLine(WriteCopyStatements(type,type));
    foreach (var service in services)
    {
        type = "services";
        s.AppendLine(WriteCopyStatements(type,service));
    }
    s.AppendLine(WriteCopyStatements("shared","shared"));
    s.AppendLine(WriteCopyStatements("libraries","libraries"));
    
    s.AppendLine("# End build cache");
    s.AppendLine("");
    s.AppendLine("WORKDIR /work");
    s.AppendLine("RUN dotnet restore \"MediaInAction.sln\"");

    var myProject = "";
    var myProject2 = "";
    if (type == "gateways")
    {
        myProject = "MediaInAction." + u;
    }
    else if (type == "services")
    {
        myProject = "MediaInAction." + u + "Service.HttpApi.Host";
        myProject2 = "MediaInAction." + u + "Service.BG.HttpApi.Host";
    }
    
    var thirdLevel = u.ToLower();
    s.AppendLine("COPY . .");

    if (type == "gateways")
    {
        s.AppendLine($"WORKDIR /work/src/{t}/{myProject}");
    }
    else
    {
        s.AppendLine($"WORKDIR /work/src/{t}/{thirdLevel}/{myProject}");
    }
    s.AppendLine("RUN dotnet publish  -c Release -o /app");
    s.AppendLine("");

    if (t == "services")
    {
        projectName = "MediaInAction." + name + "Service.Application.Tests" ;
        s.AppendLine("FROM build as app_tests");
        s.AppendLine($"WORKDIR /work/src/{t}/{thirdLevel}/{projectName}");
        s.AppendLine("");

        projectName = "MediaInAction." + name + "Service.Domain.Tests" ;
        s.AppendLine("FROM build as domain_tests");
        s.AppendLine($"WORKDIR /work/src/{t}/{thirdLevel}/{projectName}");
        s.AppendLine("");  
        
        if ((name == "Identity") || (name == "Video")  )
        {
            projectName = "MediaInAction." + name + "Service.EntityFrameworkCore.Tests" ;
            s.AppendLine("FROM build as db_tests");
            s.AppendLine($"WORKDIR /work/src/{t}/{thirdLevel}/{projectName}");
            s.AppendLine("");  
        }
        else if  ((name == "File") || (name == "Emby") || (name == "Trakt") || (name == "Deluge") )
        {
            projectName = "MediaInAction." + name + "Service.MongoDb.Tests" ;
            s.AppendLine("FROM build as db_tests");
            s.AppendLine($"WORKDIR /work/src/{t}/{thirdLevel}/{projectName}");
            s.AppendLine("");  
        }
    }
    s.AppendLine("FROM build AS publish");
    s.AppendLine("");

    s.AppendLine("FROM base AS final");
    s.AppendLine("WORKDIR /app");
    s.AppendLine("COPY --from=publish /app .");
    s.AppendLine($"ENTRYPOINT [\"dotnet\", \"{myProject}.dll\"]");
    return s.ToString();
}

void SaveDockerFile(string type, string name, string content)
{
    string dir = Directory.GetParent(Environment.CurrentDirectory)
        .Parent // bin
        .Parent
        .Parent
        .Parent
        .FullName;

    string majorDir = Directory.GetParent(Environment.CurrentDirectory)
        .Parent // bin
        .FullName;

    var nameLower = name.ToLower();
    if (type == "services")
    {
        var nameUpper = "MediaInAction." + name + "Service.HttpApi.Host";

        var file = majorDir + $"/{type}/{nameLower}/src/{nameUpper}/Dockerfile";
       var exist = File.Exists(file);
       if (exist == true)
       {
            File.Delete(file);
       }
        File.WriteAllText(file, content);
        Console.WriteLine($"written to {file}");
    }
    else if (type == "apps")
    {
        var myDir2 = majorDir + "/" + type;
        var dockerDir = "";
        if (name == "public-web")
        {
            string[] dir2 = Directory.GetDirectories(myDir2 +  "/public-web/src/");
            dockerDir = dir2[0];
        }
       
        var file = dockerDir + $"/Dockerfile";
        var exists = File.Exists(file);
        if (exists == true)
        {
            File.Delete(file);
        }
        File.WriteAllText(file, content);
        Console.WriteLine($"written to {file}");   
    }
    else if (type == "gateways")
    {
        var myDir2 = majorDir + "/" + type;
        var dockerDir = "";
        if (name == "web")
        {
            string[] dir2 = Directory.GetDirectories(myDir2 +  "/web/src/");
            dockerDir = dir2[0];
        }
        else if (name == "web-public")
        {
            string[] dir2 = Directory.GetDirectories(myDir2 +  "/web-public/src/");
            dockerDir = dir2[0];
        }
        var file = dockerDir + $"/Dockerfile";
        var exists = File.Exists(file);
        if (exists == true)
        {
            File.Delete(file);
        }
        File.WriteAllText(file, content);
        Console.WriteLine($"written to {file}");   
    }
}

void Generate(string[] names, string type)
{
    foreach (var name in names)
    {
        var dockerfile = GenerateDockerfile(type, name);
        SaveDockerFile(type, name, dockerfile);
    }
}
Generate(frontends, "apps");
Generate(gateways, "gateways");
Generate(services, "services");