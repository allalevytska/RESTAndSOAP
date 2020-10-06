using NUnit.Framework;
using RestSharp;
using Newtonsoft.Json;

namespace RestAPITest
{
    public class Tests
    {
        private RestClient client = new RestClient("https://reqres.in/api");
        private RestClient clientCat = new RestClient("https://cat-fact.herokuapp.com");


        [OneTimeSetUp]
        public void Setup()
        {

        }

        [Test]
        public void VerifyResultSuccessful()
        {
            var request = new RestRequest("users/2");
            var response = client.Get(request);

            Assert.True(response.IsSuccessful);
            Assert.AreEqual(response.ResponseStatus, ResponseStatus.Completed);
        }

        [TestCase("John", "QA")]
        [TestCase("Alla", "rockie")]
        public void VerifyCanCreateUser(string name, string job)
        {
            var request = new RestRequest("/api/users");
            request.AddJsonBody(new User(name, job));

            var response = client.Post(request);

            Assert.True(response.IsSuccessful);
            Assert.AreEqual(response.ResponseStatus, ResponseStatus.Completed);
        }

        [Test]
        public void VerifyRandomFactContainsText()
        {
            var request = new RestRequest("facts/random");
            var response = clientCat.Get(request);

            var fact = JsonConvert.DeserializeObject<Fact>(response.Content);

            Assert.True(response.IsSuccessful);
            Assert.AreEqual(response.ResponseStatus, ResponseStatus.Completed);
            Assert.AreEqual(fact.type, "cat");
            Assert.IsNotEmpty(fact.text);
        }

        [TestCase("Sign in first")]
        public void VerifyDefaultMessagePresenseForUnauthorizedUser(string text)
        {
            var request = new RestRequest("users/me");
            var response = clientCat.Get(request);

            var message = JsonConvert.DeserializeObject<Message>(response.Content);

            Assert.AreEqual(response.ResponseStatus, ResponseStatus.Completed);
            Assert.AreEqual(message.message, text);
        }
    }
}