using System;
namespace RestAPITest
{
    public class Fact
    {
        public string _id { get; set; }
        public int _v { get; set; }
        public string user { get; set; }
        public string text { get; set; }
        public string updatedAt { get; set; }
        public string createdAt { get; set; }
        public bool used { get; set; }
        public bool deleted { get; set; }
        public string source { get; set; }
        public string type { get; set; }
    }
}
