#r "tools/FAKE/tools/FakeLib.dll"
open Fake

let projName="CavemanTools"
let projDir= "..\src" @@ projName
let testDir="..\src" @@ "Tests"

let testOnCore=true
let additionalPack=[]

let localNugetRepo="E:/Libs/nuget"
let nugetExeDir="tools"



