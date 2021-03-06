﻿using System;

namespace WorkerList
{
    [Serializable]
    class DataExeption : Exception
    {
        public DataExeption() { }
        public DataExeption(string message) : base(message) { }
        public DataExeption(string message, Exception inner) : base(message, inner) { }
        protected DataExeption(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
