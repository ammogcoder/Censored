#addin "Cake.FileHelpers"

var TARGET = Argument ("target", Argument ("t", "Default"));

var version = EnvironmentVariable ("APPVEYOR_BUILD_VERSION") ?? Argument("version", "0.0.9999");

Task ("Default").Does (() =>
{
	NuGetRestore ("./Censored.sln");

	DotNetBuild ("./Censored.sln", c => c.Configuration = "Release");
});

Task ("NuGetPack")
	.IsDependentOn ("Default")
	.Does (() =>
{
	NuGetPack ("./Censored.nuspec", new NuGetPackSettings { 
		Version = version,
		Verbosity = NuGetVerbosity.Detailed,
		OutputDirectory = "./",
		BasePath = "./",
		ToolPath = "./tools/nuget3.exe"
	});	
});


RunTarget (TARGET);
