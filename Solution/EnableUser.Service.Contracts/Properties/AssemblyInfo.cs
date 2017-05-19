using System.Reflection;
using System.Runtime.InteropServices;

using ActiveDirectoryBlogPost.EnableUserMicroservice.Service.Contracts;

// Allgemeine Informationen über eine Assembly werden über die folgenden 
// Attribute gesteuert. Ändern Sie diese Attributwerte, um die Informationen zu ändern,
// die einer Assembly zugeordnet sind.
[assembly: AssemblyTitle("ActiveDirectoryBlogPost.EnableUserMicroservice.Service.Contracts")]
[assembly: AssemblyDescription("The service contracts for the EnableUser Microservice.")]
[assembly: AssemblyCompany("5Minds IT Solutions GmbH & Co KG")]
[assembly: AssemblyProduct("ActiveDirectoryBlogPost.EnableUserMicroservice.Service.Contracts")]
[assembly: AssemblyCopyright("Copyright © 5Minds 2017")]

// Durch Festlegen von ComVisible auf "false" werden die Typen in dieser Assembly unsichtbar 
// für COM-Komponenten.  Wenn Sie auf einen Typ in dieser Assembly von 
// COM aus zugreifen müssen, sollten Sie das ComVisible-Attribut für diesen Typ auf "True" festlegen.
[assembly: ComVisible(false)]

// Die folgende GUID bestimmt die ID der Typbibliothek, wenn dieses Projekt für COM verfügbar gemacht wird
[assembly: Guid("b6effab4-c2e9-49ee-87da-223c5fc6b61f")]

// Versionsinformationen für eine Assembly bestehen aus den folgenden vier Werten:
//
//      Hauptversion
//      Nebenversion 
//      Buildnummer
//      Revision
//
// Sie können alle Werte angeben oder die standardmäßigen Build- und Revisionsnummern 
// übernehmen, indem Sie "*" eingeben:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion(NuGetVersionControl.Version)]
[assembly: AssemblyFileVersion(NuGetVersionControl.Version)]
[assembly: AssemblyInformationalVersion(NuGetVersionControl.PreVersion)]
