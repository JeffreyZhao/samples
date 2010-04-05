using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSharpAsync
{
    public class CompletedEventArgs : EventArgs
    {
        public CompletedEventArgs(Exception ex)
        {
            this.Error = ex;
        }

        public Exception Error { get; private set; }
    }
}