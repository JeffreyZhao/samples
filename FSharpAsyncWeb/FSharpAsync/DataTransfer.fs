#light

namespace FSharpAsync

open System
open System.IO

type DataTransfer(streamIn: Stream, streamOut: Stream) = 

    let mutable readAsync = true
    let mutable writeAsync = true

    let rec transferAsync buffer = 
        async { 
            let lengthRead = ref 0

            if readAsync then
                let! l = streamIn.AsyncRead(buffer, 0, buffer.Length)
                lengthRead := l
            else
                lengthRead := streamIn.Read(buffer, 0, buffer.Length)

            if (lengthRead.Value) > 0 then
                if writeAsync then
                    do! streamOut.AsyncWrite(buffer, 0, lengthRead.Value)
                else 
                    streamOut.Write(buffer, 0, lengthRead.Value)

                do! transferAsync buffer
        }

    member t.WorkAsync() = 0