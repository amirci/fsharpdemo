module FSharpDemo.TryParseTest

open System
open FsUnit
open NUnit.Framework

let tryParse str =
   try 
      true, System.Int32.Parse str
   with _ -> (false, 0)  // any exception


[<Test>]
let ``Parsing ints`` () =
    "20"
    |> tryParse
    |> should equal (true, 20)


[<Test>]
let ``Parsing a string`` () =
    "Hello"
    |> tryParse
    |> fst
    |> should equal false