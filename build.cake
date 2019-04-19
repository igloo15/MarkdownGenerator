#l "nuget:?package=Cake.igloo15.Scripts.Bundle.CSharp&version=2.0.3"

var target = Argument<string>("target", "Default");

AddSetup((d) => {
    d["Markdown-Generator-Filter"] = "./dist/**/publish/igloo15*.dll";
});

AddTeardown((d) => {
    Information("Finished All Tasks");
});

Task("Pack")
    .IsDependentOn("Standard-All")
    .IsDependentOn("CSharp-NetCore-Pack-All")
    .IsDependentOn("Changelog-Generate")
    .IsDependentOn("Markdown-Generate-Api")
    .CompleteTask();

Task("Push")
    .IsDependentOn("Pack")
    .IsDependentOn("NuGet-Push")
    .CompleteTask();

    

Task("Default")
    .IsDependentOn("Pack")
    .CompleteTask();

Task("Deploy")
	.IsDependentOn("Push")
    .CompleteTask();

RunTarget(target);

