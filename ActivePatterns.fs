module FSharpDemo.ActivePatternsTest

open System
open FsUnit
open NUnit.Framework

let trueSome =
    function
    | true, i -> Some i
    | _ -> None

let (|Integer|_|) (str: string) = str |> Int32.TryParse  |> trueSome

let (|Float|_|) (str: string)   = str |> Double.TryParse |> trueSome

[<Test>]
let ``When is an int matches int`` () =
    match "20" with
    | Integer i -> i
    | _ -> 0
    |> should equal 20
    
[<Test>]
let ``When is a float matches float`` () =
    match "2.0" with
    | Float f -> f
    | _ -> 0.0
    |> should equal 2.0
