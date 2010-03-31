namespace FSharpAsync

open System
open System.IO
open System.Web
open System.Net

open WebRequestExtensions

type AsyncWebTransfer(context: HttpContextBase, url: string) =
    inherit AsyncWorker()

    override t.WorkAsync() = 
        async {
            let request = WebRequest.Create(url)
            use! response = request.GetResponseAsync()
            context.Response.ContentType <- response.ContentType
            
            let streamIn = response.GetResponseStream()
            let streamOut = context.Response.OutputStream
            let transfer = new AsyncTransfer(streamIn, streamOut)
            do! transfer.WorkAsync()
        }