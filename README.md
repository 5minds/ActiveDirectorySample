# ActiveDirectorySample
The sample project that goes with the Active Directory Blogpost.

# Installation

To run this project, you need the following prerequisites:

- A windows machine that has the .NET Framework v4.6.1 or higher
- Some kind of accessible ActiveDirectory implementation

# Configuration

To use this project to access your active directory, you may need to adjust the connection strings supplied with this solution, so that they properly reflect your own environment.
You can find them [here](https://github.com/5minds/ActiveDirectorySample/blob/master/Solution/Foundation.ActiveDirectory/Connection/ActiveDirectoryConnector.cs#L28) and [here](https://github.com/5minds/ActiveDirectorySample/blob/master/Solution/User.Repository.ActiveDirectory/UserRepository.cs#L98).

Also make sure that the referenced [sample](https://github.com/5minds/ActiveDirectorySample/blob/master/Solution/User.Repository.ActiveDirectory/UserRepository.cs#L24) [groups](https://github.com/5minds/ActiveDirectorySample/blob/master/Solution/User.Repository.ActiveDirectory/UserRepository.cs#L366) are actually existing groups within your active directory structure!

# Usage

This project comes with a Postman-Environment and a Postman-Collection, which you can use for running queries against the HTTP Endpoints provided with this project.
You can find those [here (environment)](./Docs/ActiveDirectory_Sample_Project.postman_environment.json) and [here (collection)](./Docs/Active_Directory_Sample_Project.postman_collection.json).