using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Http.ModelBinding;

namespace WebAPITest.Models
{
    [DataContract]
    public class Contact
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string PhoneNo { get; set; }
        [DataMember]
        public string EmailAddress { get; set; }
        [DataMember]
        public Address Address { get; set; }
    }
    [DataContract]
    public class Address
    {
        [DataMember]
        public string Province { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string District { get; set; }
        [DataMember]
        public string Street { get; set; }
    }
}