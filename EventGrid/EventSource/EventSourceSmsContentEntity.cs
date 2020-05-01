using System;
using System.Collections.Generic;
using System.Text;

namespace EventSource
{
    public class EventSourceSmsContentEntity
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Content { get; set; }
    }
}
