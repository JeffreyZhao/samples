#light

namespace FSharpAsync

open System

type CompletedEventArgs(ex: Exception) =
    inherit EventArgs()

    member e.Exception = ex

[<AbstractClass>]
type AsyncWorker() = 
    
    let completed = new Event<CompletedEventArgs>()

    [<CLIEvent>]
    member e.Completed = completed.Publish

    abstract WorkAsync: unit -> Async<unit>

    member e.Start() = 
        Async.StartWithContinuations
            (e.WorkAsync(),
             (fun _ -> completed.Trigger(new CompletedEventArgs(null))),
             (fun ex -> completed.Trigger(new CompletedEventArgs(ex))),
             (fun ex -> ex |> ignore))