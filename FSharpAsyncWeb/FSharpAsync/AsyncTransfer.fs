﻿#light

namespace FSharpAsync

open System
open System.IO

type AsyncTransfer(streamIn: Stream, streamOut: Stream) = 
    inherit AsyncWorker()

    let rec transferAsync buffer = 
        async {
            let! lengthRead = streamIn.AsyncRead(buffer, 0, buffer.Length)
            if lengthRead > 0 then
                do! streamOut.AsyncWrite(buffer, 0, lengthRead)
                do! transferAsync buffer
        }

    let transferImperativelyAsync buffer = 
        async {
            let hasData = ref true
            while (hasData.Value) do
                let! lengthRead = streamIn.AsyncRead(buffer, 0, buffer.Length)
                if lengthRead > 0 then
                    do! streamOut.AsyncWrite(buffer, 0, lengthRead)
                else
                    hasData := false
        }

    override t.DoWorkAsync() = transferAsync (Array.zeroCreate 1024)