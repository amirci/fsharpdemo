module FSharpDemo.CurryingTest

open System
open FsUnit
open NUnit.Framework

let add x y = x + y


[<Test>]
let ``Increment is the same as adding one`` () =
    let inc = add 1
    
    inc 1
    |> should equal 2



let swapargs f x y = f y x
let lessThanFive = swapargs (<) 5

[<Test>]
let ``Searching with less than five predicate`` () =

    let collection = [1;8;2;19;21;4;]

    collection
    |> Seq.filter lessThanFive
    |> should equal [1;2;4]
