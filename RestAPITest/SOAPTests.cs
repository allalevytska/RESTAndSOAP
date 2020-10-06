using NUnit.Framework;
using RestSharp;
using System;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;

namespace RestAPITest
{
    public class SOAPTests
    {
        RestClient client = new RestClient("http://www.dneonline.com/calculator.asmx");
        RestRequest request = new RestRequest(Method.POST);
        XNamespace ns = "http://tempuri.org/";

        [OneTimeSetUp]
        public void Setup()
        {
            client.Timeout = -1;
            request.AddHeader("Content-Type", "text/xml; charset=utf-8");
        }

        [TestCase(10, 10, 0)]
        [TestCase(10, 5, 5)]
        [TestCase(10, 0, 10)]
        [TestCase(5, 10, -5)]
        public void VerifySubstract(int a, int b, int expected_result)
        {

            request.AddHeader("SOAPAction", "http://tempuri.org/Subtract");
            request.AddParameter("text/xml; charset=utf-8", $"<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">\n  <soap:Body>\n    <Subtract xmlns=\"http://tempuri.org/\">\n      <intA>{a}</intA>\n      <intB>{b}</intB>\n    </Subtract>\n  </soap:Body>\n</soap:Envelope>", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            XDocument document = XDocument.Parse(response.Content);

            IEnumerable<XElement> responses = document.Descendants(ns + "SubtractResponse");
            var SubtractResult = responses.Select(x => x.Element(ns + "SubtractResult").Value).First();

            Assert.AreEqual(expected_result, Int32.Parse(SubtractResult));
        }

        [TestCase(2, 2, 4)]
        [TestCase(1, 2, 2)]
        [TestCase(1, 1, 1)]
        public void VerifyMultiplication(int a, int b, int expected_result)
        {
            request.AddHeader("SOAPAction", "http://tempuri.org/Multiply");
            request.AddParameter("text/xml; charset=utf-8", $"<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">\n  <soap:Body>\n    <Multiply xmlns=\"http://tempuri.org/\">\n      <intA>{a}</intA>\n      <intB>{b}</intB>\n    </Multiply>\n  </soap:Body>\n</soap:Envelope>", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            XDocument document = XDocument.Parse(response.Content);

            IEnumerable<XElement> responses = document.Descendants(ns + "MultiplyResponse");
            var MultiplyResult = responses.Select(x => x.Element(ns + "MultiplyResult").Value).First();

            Assert.AreEqual(expected_result, Int32.Parse(MultiplyResult));
        }

        [TestCase(8, 2, 4)]
        [TestCase(64, 2, 32)]
        [TestCase(1, 1, 1)]
        public void VerifyDivision(int a, int b, int expected_result)
        {
            request.AddHeader("SOAPAction", "http://tempuri.org/Divide");
            request.AddParameter("text/xml; charset=utf-8", $"<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">\n  <soap:Body>\n    <Divide xmlns=\"http://tempuri.org/\">\n      <intA>{a}</intA>\n      <intB>{b}</intB>\n    </Divide>\n  </soap:Body>\n</soap:Envelope>", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            XDocument document = XDocument.Parse(response.Content);

            IEnumerable<XElement> responses = document.Descendants(ns + "DivideResponse");
            var DivideResult = responses.Select(x => x.Element(ns + "DivideResult").Value).First();

            Assert.AreEqual(expected_result, Int32.Parse(DivideResult));
        }
    }
}
