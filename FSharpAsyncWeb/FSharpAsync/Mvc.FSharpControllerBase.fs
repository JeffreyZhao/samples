#light

namespace FSharpAsync.Mvc

open System
open System.Web
open System.Web.Mvc

type FSharpControllerBase() =
    inherit AsyncController()

    [<NonAction>]
    member c.StartAsync<'a when 'a :> ActionResult>(work: Async<'a>) = 
        c.AsyncManager.OutstandingOperations.Increment() |> ignore

        let onSucceeded r = 
            c.AsyncManager.Parameters.["result"] <- r
            c.AsyncManager.OutstandingOperations.Decrement() |> ignore

        let onFailed ex =
            c.AsyncManager.Parameters.["error"] <- ex
            c.AsyncManager.OutstandingOperations.Decrement() |> ignore

        Async.StartWithContinuations(work, onSucceeded, onFailed, onFailed)