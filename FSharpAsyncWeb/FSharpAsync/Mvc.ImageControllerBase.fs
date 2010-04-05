#light

namespace FSharpAsync.Mvc

open System
open System.Web
open System.Web.Mvc
open System.Net

open FSharpAsync.WebRequestExtensions

type ImageControllerBase() =
    inherit FSharpControllerBase()

    [<NonAction>]
    member c.Load(url: String) =
        async {
            let request = WebRequest.Create(url)
            use! response = request.GetResponseAsync()
            c.HttpContext.Response.ContentType <- response.ContentType
            
            let streamIn = response.GetResponseStream()
            let streamOut = c.HttpContext.Response.OutputStream
            let buffer = Array.zeroCreate 1024

            let hasData = ref true
            while (hasData.Value) do
                let! lengthRead = streamIn.AsyncRead(buffer, 0, buffer.Length)
                if lengthRead > 0 then
                    do! streamOut.AsyncWrite(buffer, 0, lengthRead)
                else
                    hasData := false

            return new EmptyResult()
        }