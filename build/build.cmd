@echo off
cls
rem "tools\nuget.exe" "install" "FAKE" "-OutputDirectory" "tools" "-ExcludeVersion"
"tools\FAKE\tools\Fake.exe" build.fsx %*
pause