using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;

namespace CSharpAsync
{
    public class AsyncWebTransfer
    {
        private WebRequest m_request;
        private WebResponse m_response;

        private HttpContextBase m_context;
        private string m_url;

        public AsyncWebTransfer(HttpContextBase context, string url)
        {
            this.m_context = context;
            this.m_url = url;
        }

        public void Start()
        {
            this.m_request = WebRequest.Create(this.m_url);
            this.m_request.BeginGetResponse(this.EndGetResponseCallback, null);
        }

        private void EndGetResponseCallback(IAsyncResult ar)
        {
            try
            {
                this.m_response = this.m_request.EndGetResponse(ar);
                this.m_context.Response.ContentType = this.m_response.ContentType;

                var streamIn = this.m_response.GetResponseStream();
                var streamOut = this.m_context.Response.OutputStream;

                var transfer = new AsyncTransfer(streamIn, streamOut);
                transfer.Completed += OnTransferCompleted;
                transfer.Start();
            }
            catch(Exception ex)
            {
                this.OnCompleted(ex);
            }
        }

        private void OnTransferCompleted(object sender, CompletedEventArgs args)
        {
            this.OnCompleted(args.Exception);
        }

        private void OnCompleted(Exception ex)
        {
            if (this.m_response != null)
            {
                this.m_response.Close();
                this.m_response = null;
            }

            var handler = this.Completed;
            if (handler != null)
            {
                handler(this, new CompletedEventArgs(ex));
            }
        }

        public event EventHandler<CompletedEventArgs> Completed;
    }
}