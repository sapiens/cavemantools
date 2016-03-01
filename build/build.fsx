 // include Fake lib
#r "tools/FAKE/tools/FakeLib.dll"

#load "settings.fsx"
#load "utils.fsx"

open Fake
open StringHelper
open EnvironmentHelper
open Settings
open Utils



let buildDir = "./build/"
let pkgFiles= (projName+"*.nupkg") |> combinePaths "release" |> combinePaths outDir



Target "Clean" (fun _ ->  
   CleanDir outDir   
)
 
Target "Build" (fun _ -> 
    restore projDir |> ignore
    let result= compile projDir
    if result = 0 then trace "build ok"
    else 
        failwith "build failed"            
)
 
Target "Pack" ( fun _ ->
    pack projDir |> ignore
    additionalPack 
        |> Seq.map (fun d -> (projDir+d))
        |> Seq.iter (fun d-> 
                            trace ("packing "+d)
                            pack d |> ignore)    
)

Target "Test" (fun _ ->
   runTests clr
   if testOnCore then runTests clrCore 
)

Target "Push"(fun _ -> push pkgFiles |> ignore)

Target "Local"( fun _ ->
   !! pkgFiles |> CopyFiles localNugetRepo
)

// Dependencies
"Clean"
    ==> "Test"
    ==>"Pack"
    ==>"Local"

"Clean"
    ==>"Test"
    ==>"Pack"
    ==>"Push"
   

 
// start build
RunTargetOrDefault "Test"