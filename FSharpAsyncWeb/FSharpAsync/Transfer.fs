#light

module FSharpAsync.Transfer

open System
open System.IO
open System.Net
open System.Web

open WebRequestExtensions

let rec transferAsync (streamIn: Stream) (streamOut: Stream) buffer = 
    async {
        let! lengthRead = streamIn.AsyncRead(buffer, 0, buffer.Length)
        if lengthRead > 0 then
            do! streamOut.AsyncWrite(buffer, 0, lengthRead)
            do! transferAsync streamIn streamOut buffer
    }

let transferImperativelyAsync (streamIn: Stream) (streamOut: Stream) buffer = 
    async {
        let hasData = ref true
        while (hasData.Value) do
            let! lengthRead = streamIn.AsyncRead(buffer, 0, buffer.Length)
            if lengthRead > 0 then
                do! streamOut.AsyncWrite(buffer, 0, lengthRead)
            else
                hasData := false
    }

let webTransferAsync (context: HttpContextBase) (url: string) =
    async {
        let request = WebRequest.Create(url)
        use! response = request.GetResponseAsync()
        context.Response.ContentType <- response.ContentType
        
        let streamIn = response.GetResponseStream()
        let streamOut = context.Response.OutputStream
        let buffer = Array.zeroCreate 1024
        do! transferAsync streamIn streamOut buffer
    }