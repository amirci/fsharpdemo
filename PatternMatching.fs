module FSharpDemo.PatternMatchingTest

open System
open FsUnit
open NUnit.Framework

let rec find elem list =
    match list with
    | e :: rest when e = elem -> true
    | e :: rest -> find elem rest
    | [] -> false
    

let sample = [1;8;22;33;9;19;11]

[<Test>]
let ``When the element exists`` () =    
    sample
    |> find 11
    |> should equal true

[<Test>]
let ``When the element does not exist`` () =
    sample
    |> find 41
    |> should equal false



type Person = {Name:string}

[<Test>]
let ``Only charles says hi`` () =
    let sayHi = function
    | {Name="Charles"} -> "Hi Charles!"
    | _ -> "Who are you?"

    {Name="Charles"}
    |> sayHi
    |> should equal "Hi Charles!"

    {Name="Diego"}
    |> sayHi
    |> should equal "Who are you?"


[<Literal>]
let Three = 3

[<Test>]
let ``Testing literals`` () =
    let filter123 x =
        match x with 
        | 1 | 2 | Three -> true
        | var1 -> false

    let results = [1..5] |> Seq.map filter123

    results
    |> should equal [true; true; true; false; false]




