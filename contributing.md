### Publishing
To publish a new version, tag a commit with the package prefix and package version for which you wish to deploy. The prefix for a given package can be found in the associated `.csproj` under one of the property group sections.

For example, to publish version `6.0.1` of `TurTools.Utilities` for the commit `019a420e197b41b668d302a2cdc2d795ca1f90c7`, run the following:
```
git tag -a "utilities-6.0.1" 019a420e197b41b668d302a2cdc2d795ca1f90c7 -m "Release version 6.0.1 of TurTools.Utilities"
```

Followed by:

```
git push --tags
```

Then create a release in github for the newly created tag, modify the release notes as needed, and the github action will take it from there.