module FSharpDemo.ShapeTest

open System
open FsUnit
open NUnit.Framework

type Radius = | Radius of float

type Shape = 
| Circle of Radius
| Rectangle of Height:float * Width:float

let areaCalc shape =
    match shape with
    | Circle(Radius radius) -> Math.PI * radius ** 2.0
    | Rectangle (Height=h; Width=w) -> h * w


[<Test>]
let ``Calculating area for a circle`` () =
    Circle(Radius 5.0)
    |> areaCalc
    |> should equal (Math.PI * 5.0 ** 2.0)


[<Test>]
let ``Calculating area for a rectangle`` () =
    Rectangle (Height=10.0, Width=10.0)
    |> areaCalc
    |> should equal 100.0