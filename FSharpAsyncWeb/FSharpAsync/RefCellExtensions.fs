#light

module FSharpAsync.RefCellExtensions

let inline valueOf r = !r // get the ref value of reference cell