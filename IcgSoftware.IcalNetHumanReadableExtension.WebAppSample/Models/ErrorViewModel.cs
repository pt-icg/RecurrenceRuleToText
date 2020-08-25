using System;

namespace IcgSoftware.IcalNetHumanReadableExtension.WebAppSample.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
