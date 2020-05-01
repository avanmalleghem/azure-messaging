// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}

namespace EventHandler
{
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
