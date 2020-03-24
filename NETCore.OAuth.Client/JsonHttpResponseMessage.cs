﻿using System.Net.Http;

namespace NETCore.OAuth.Client
{
    public class JsonHttpResponseMessage<T>
    {
        private readonly HttpResponseMessage _message;
        private readonly T _data;

        public JsonHttpResponseMessage(HttpResponseMessage message, T data = default)
        {
            _message = message;
            _data = data;
        }

        public HttpResponseMessage Message => _message;

        public T Data => _data;
    }
}