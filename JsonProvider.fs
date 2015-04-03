module FSharpDemo.JsonProviderTest

open System
open FsUnit
open NUnit.Framework
open NUnit.Framework.Constraints
open FSharp.Data

type Library = JsonProvider<""" {
    "movies": [{ "title": "A Movie", "director": "A Director"}],
    "books": [{"title": "A title", "author": "An author"}]
    } """>

[<Test>]
let ``Create a json with movies and books`` () =
    let haveSubstring (s:string) = SubstringConstraint s
    let movies  = [|Library.Movy(title="The Matrix", director="The Wachowski brothers")|]
    let books   = [|Library.Book(title="It", author="Stephen King")|]
    let library = Library.Root(movies=movies, books=books)

    printf "Json result: %s" (library.JsonValue.ToString())

    library.JsonValue.ToString()
    |> should haveSubstring "The Matrix"

    // do a post request
    // library.JsonValue.Request("my.library.com")

type People = JsonProvider<""" [{ "name":"John", "age":94 }] """>
// type People = JsonProvider<""" [{ "name":"John", "age":94 }, { "name":"Tomas" }] """>


[<Test>]
let ``Parsing a bunch of people`` () =
    let people = People.Parse """[{"name": "Andrew", "age": 27}]"""

    people.Length
    |> should equal 1

    people.[0].Name
    |> should equal "Andrew"



type GitHub = JsonProvider<"https://api.github.com/repos/fsharp/FSharp.Data/issues">

let topRecentlyUpdatedIssues = 
    GitHub.GetSamples()
    |> Seq.filter (fun issue -> issue.State = "open")
    |> Seq.sortBy (fun issue -> System.DateTime.Now - issue.UpdatedAt)
    |> Seq.truncate 5

let logIssues (issues:GitHub.Root seq) =
    for issue in issues do
        printfn "#%d %s" issue.Number issue.Title
    issues

[<Test>]
let ``The issues contain`` () =
    topRecentlyUpdatedIssues
    |> logIssues
    |> Seq.forall (fun issue -> issue.State = "open")
    |> should equal true