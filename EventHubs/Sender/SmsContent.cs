using System;

namespace Sender
{
    [Serializable]
    public class SmsContent
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Content { get; set; }

        public override string ToString()
        {
            return $"From {From} To {To} : {Content}";
        }
    }
}