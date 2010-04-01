using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace CSharpAsync
{
    public class AsyncTransfer
    {
        private Stream m_streamIn;
        private Stream m_streamOut;

        public AsyncTransfer(Stream streamIn, Stream streamOut)
        {
            this.m_streamIn = streamIn;
            this.m_streamOut = streamOut;
        }

        public void StartAsync()
        {
            byte[] buffer = new byte[1024];

            this.m_streamIn.BeginRead(
                buffer, 0, buffer.Length,
                this.EndReadInputStreamCallback, buffer);
        }

        private void EndReadInputStreamCallback(IAsyncResult ar)
        {
            var buffer = (byte[])ar.AsyncState;
            int lengthRead;

            try
            {
                lengthRead = this.m_streamIn.EndRead(ar);
            }
            catch (Exception ex)
            {
                this.OnCompleted(ex);
                return;
            }

            if (lengthRead <= 0)
            {
                this.OnCompleted(null);
            }
            else
            {
                try
                {
                    this.m_streamOut.BeginWrite(
                        buffer, 0, lengthRead,
                        this.EndWriteOutputStreamCallback, buffer);
                }
                catch (Exception ex)
                {
                    this.OnCompleted(ex);
                }
            }
        }

        private void EndWriteOutputStreamCallback(IAsyncResult ar)
        {
            try
            {
                this.m_streamOut.EndWrite(ar);

                var buffer = (byte[])ar.AsyncState;
                this.m_streamIn.BeginRead(
                    buffer, 0, buffer.Length,
                    this.EndReadInputStreamCallback, buffer);
            }
            catch (Exception ex)
            {
                this.OnCompleted(ex);
            }
        }

        private void OnCompleted(Exception ex)
        {
            var handler = this.Completed;
            if (handler != null)
            {
                handler(this, new CompletedEventArgs(ex));
            }
        }

        public event EventHandler<CompletedEventArgs> Completed;
    }
}