#light

namespace FSharpAsync

open System
open System.IO

type AsyncTransfer(streamIn: Stream, streamOut: Stream) = 
    inherit AsyncWorker(
        Transfer.transferAsync streamIn streamOut (Array.zeroCreate 1024))