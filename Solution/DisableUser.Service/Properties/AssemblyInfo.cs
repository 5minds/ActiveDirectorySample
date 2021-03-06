﻿using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using ActiveDirectoryBlogPost.DisableUserMicroservice.Service;

// Allgemeine Informationen über eine Assembly werden über die folgenden 
// Attribute gesteuert. Ändern Sie diese Attributwerte, um die Informationen zu ändern,
// die einer Assembly zugeordnet sind.
[assembly: AssemblyTitle("ActiveDirectoryBlogPost.DisableUserMicroservice.Service")]
[assembly: AssemblyDescription("The service implementation for the DisableUser Microservice.")]
[assembly: AssemblyCompany("5Minds IT Solutions GmbH & Co KG")]
[assembly: AssemblyProduct("ActiveDirectoryBlogPost.DisableUserMicroservice.Service")]
[assembly: AssemblyCopyright("Copyright © 5Minds 2017")]

// Durch Festlegen von ComVisible auf "false" werden die Typen in dieser Assembly unsichtbar 
// für COM-Komponenten.  Wenn Sie auf einen Typ in dieser Assembly von 
// COM aus zugreifen müssen, sollten Sie das ComVisible-Attribut für diesen Typ auf "True" festlegen.
[assembly: ComVisible(false)]

// Die folgende GUID bestimmt die ID der Typbibliothek, wenn dieses Projekt für COM verfügbar gemacht wird
[assembly: Guid("3a03bd20-ee17-48f3-8999-7971a99f2f13")]

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
