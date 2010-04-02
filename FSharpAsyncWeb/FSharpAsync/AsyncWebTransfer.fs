namespace FSharpAsync

open System
open System.IO
open System.Web
open System.Net

open WebRequestExtensions

type AsyncWebTransfer(context: HttpContextBase, url: string) =
    inherit AsyncWorker(Transfer.webTransferAsync context url)