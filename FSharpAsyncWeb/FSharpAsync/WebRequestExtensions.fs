module FSharpAsync.WebRequestExtensions

open System.Net

type WebRequest with
    member r.GetResponseAsync() =
        Async.FromBeginEnd (r.BeginGetResponse, r.EndGetResponse)