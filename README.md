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
`>AppCenterBuildAllAppBranches.exe --ownername v-argolu-microsoft.com --appname Foobar --apikey <<hidden>>`

Output:
```
feature/fix-develop build completed in 198 seconds. Link to build logs: https://appcenter.ms/users/v-argolu-microsoft.com/apps/Foobar/build/branches/feature%2Ffix-develop/builds/37
develop build failed in 126 seconds. Link to build logs: https://appcenter.ms/users/v-argolu-microsoft.com/apps/Foobar/build/branches/develop/builds/36
master build completed in 126 seconds. Link to build logs: https://appcenter.ms/users/v-argolu-microsoft.com/apps/Foobar/build/branches/master/builds/38
```
