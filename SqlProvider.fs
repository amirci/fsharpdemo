﻿module FSharpDemo.SqlProviderTest

open System
open FsUnit
open NUnit.Framework
open NUnit.Framework.Constraints
open Microsoft.FSharp.Data.TypeProviders

module Database =
    [<Literal>]
    let connStr = "Data Source=devlocal;Initial Catalog=MediaLibrary;Integrated Security=True"

    // type Schema = SqlDataConnection<connStr>
    type Schema = DbmlFile<"schema.dbml", ResolutionFolder="db", ContextTypeName="DataContext">

    let instance() = new Schema.DataContext(connStr)

    type Movie = Schema.Movies
    type Director = Schema.Directors

[<SetUp>]
let resetDb () =
    let db = Database.instance()
    db.ExecuteCommand(sprintf "TRUNCATE TABLE [%s].[%s]" "dbo" "movies")
    |> ignore

[<Test>]
let ``By default there are no movies`` () =
    Database.instance().Movies
    |> Seq.length
    |> should equal 0


[<Test>]
let ``Can write and read some movies`` () =
    use db = Database.instance()

    let movies = [1..10]
                 |> Seq.map (fun i -> Database.Movie(Title=(sprintf "Great movie %d" i), ReleaseYear=1990+i))
    
    movies |> db.Movies.InsertAllOnSubmit

    db.SubmitChanges()

    let actual = query {
        for m in db.Movies do
        select m.Title
    }

    let expected = movies |> Seq.map (fun m -> m.Title)

    actual
    |> should equal expected


[<Test>]
let ``Find the directors`` () =
    use db = Database.instance()

    let mel = Database.Director(Name="Mel Brooks")

    db.Directors.InsertOnSubmit mel

    let movies = [1..10]
                 |> Seq.map (fun i -> 
                    Database.Movie(Title=(sprintf "Great movie %d" i), 
                                   ReleaseYear=1990+i,
                                   Directors=mel)
                  )
    
    movies |> db.Movies.InsertAllOnSubmit

    db.SubmitChanges()

    db.Movies
    |> Seq.map (fun m -> m.Directors)
    |> Seq.distinct
    |> should equal [mel]



