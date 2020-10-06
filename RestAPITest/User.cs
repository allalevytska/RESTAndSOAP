using System;
namespace RestAPITest
{
    public class User
    {
        private string Name { get; set; }
        private string Job { get; set; }

        public User(string name, string job)
        {
            Name = name;
            Job = job;
        }
    }
}
