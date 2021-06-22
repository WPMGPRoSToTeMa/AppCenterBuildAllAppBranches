# AppCenterBuildAllAppBranches
Console tool that allows you to build all the configured branches of your AppCenter application and then see the result of the builds.

## Usage
In order to use this tool you need an application on [AppCenter](https://appcenter.ms/) and either an App API token (recommended) or User API token with `Full Access`.

### Parameters
|Short|Long|Required|Default Value|Description|
|-|-|:-:|:-:|-|
|-o|--ownername|✅||The name of the owner|
|-a|--appname|✅||The name of the application|
|-k|--apikey|✅||Application API token or user API token|
||--rate||10|A delay between build status queries (in seconds)|

### Example
`>AppCenterBuildAllAppBranches.exe --ownername artem --appname foobar --apikey <<hidden>>`

Output:
```
develop build failed in 95 seconds. Link to build logs: https://build.appcenter.ms/v0.1/public/apps/<<hidden>>/downloads?token=<<hidden>>
feature/fix-develop build completed in 150 seconds. Link to build logs: https://build.appcenter.ms/v0.1/public/apps/<<hidden>>/downloads?token=<<hidden>>
master build completed in 148 seconds. Link to build logs: https://build.appcenter.ms/v0.1/public/apps/<<hidden>>/downloads?token=<<hidden>>
```
