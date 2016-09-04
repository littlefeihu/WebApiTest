using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebAPITest.Models
{
    [DataContract]
    public class UriResolutionResult
    {
        [DataMember]
        public string VirtualPathRoot { get; set; }
        [DataMember]
        public string Method { get; set; }
        [DataMember]
        public bool Matched { get; set; }

        public UriResolutionResult(string virtualPathRoot,string method,bool matched)
        {
            this.VirtualPathRoot = virtualPathRoot;
            this.Method = method;
            this.Matched = matched;
        }
    }
}