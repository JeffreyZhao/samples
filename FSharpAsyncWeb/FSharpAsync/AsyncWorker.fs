#light

namespace FSharpAsync

open System

type CompletedEventArgs(ex: Exception) =
    inherit EventArgs()

    member e.Error = ex

[<AbstractClass>]
type AsyncWorker(asyncWork: Async<unit>) = 
    
    let completed = new Event<CompletedEventArgs>()

    [<CLIEvent>]
    member e.Completed = completed.Publish

    member e.StartAsync() = 
        Async.StartWithContinuations
            (asyncWork,
             (fun _ -> completed.Trigger(new CompletedEventArgs(null))),
             (fun ex -> completed.Trigger(new CompletedEventArgs(ex))),
             (fun ex -> ex |> ignore))