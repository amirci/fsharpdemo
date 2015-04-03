module FSharpDemo.OptionTryParseTest

open System
open FsUnit
open NUnit.Framework

let tryParse str =
   try System.Int32.Parse str |> Some
   with _ -> None

let isSome opt =
    match opt with
    | Some _ -> true
    | _ -> false

[<Test>]
let ``Parsing ints`` () =
    let actual = "20" |> tryParse

    actual |> Option.isSome |> should equal true

    actual |> Option.get |> should equal 20


[<Test>]
let ``Parsing a string`` () =
    "Hello"
    |> tryParse
    |> Option.isNone
    |> should equal true