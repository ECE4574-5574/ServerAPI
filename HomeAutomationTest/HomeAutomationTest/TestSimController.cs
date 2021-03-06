using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using HomeAutomationServer.Services;
using HomeAutomationServer.Models;

using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.SimpleDB;
using Amazon.SimpleDB.Model;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;

namespace HomeAutomationTest
{
    [TestClass]
    public class TestSimController
    {
        DeviceRepository deviceRepo = new DeviceRepository();
        HouseRepository houseRepo = new HouseRepository();

        private string URI = "http://serverapi1.azurewebsites.net";
        //private string URI = "http://localhost:8080/";
        
        [TestMethod]
        public void TestPostTimeFrame()
        {
            Console.WriteLine("Starting TestPostTimeFrame.");

            WebRequest request = WebRequest.Create(URI + "/api/sim/timeframe");
            request.ContentType = "application/json";
            request.Method = "POST";

            JObject jobject = new JObject();
            jobject["time"] = "2015-04-06T18:05:05Z";
            jobject["localTime"] = "2015-04-06T18:05:05Z";
           // jobject["rate"] = "double";

            string json = jobject.ToString();

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            }

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {

                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.AreEqual(str, "true");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestPostOnSimHarness failed.");
                Assert.Fail("Web exception occurred. HTTP Request failed");
            }

            request = WebRequest.Create(URI + "/api/sim/timeframe");
            request.ContentType = "application/json";
            request.Method = "POST";

            jobject = new JObject();
            jobject["hello"] = "1997-07-16T19:20:30+01:00";
            jobject["kitty"] = "ISO 8601";
            jobject["fail"] = "double";

            json = jobject.ToString();

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            }

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {

                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.AreEqual(str, "false");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestPostOnSimHarness failed.");
                Assert.Fail();
            }

        }

        // Testing update location on 
        [TestMethod]
        public void TestPostUpdateLocation()
        {
            Debug.WriteLine("Starting TestPostUpdateLocation...");

            string username = "test_post_update_username";
            string password = "password";

            // First create a user ...
            // POST api/storage/user	
            // Posts the users information provided by JSON object data.
            /*
            WebRequest request = WebRequest.Create(URI + "/api/storage/user/" + username);
            request.ContentType = "application/json";
            request.Method = "POST";

            JObject jobject = new JObject();
            jobject["UserID"] = username;
            jobject["Password"] = password;

            string json = jobject.ToString();

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            }

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {

                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.AreEqual(str, "true");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestPostUpdateLocation failed. Couldn't post a user");
                Assert.Fail("TestPostUpdateLocation failed. Couldn't post a user");
            }
            */
            // Now post update location
            // POST api/app/user/updateposition/{username}

            WebRequest request = WebRequest.Create(URI + "/api/app/user/updateposition/" + username);
            request.ContentType = "application/json";
            request.Method = "POST";

            JObject jobject = new JObject();
            jobject["time"] = "2015-04-06T18:05:05Z";
            jobject["userID"] = username;
            jobject["lat"] = "37.874342";
            jobject["lon"] = "-86.342234";
            jobject["alt"] = "21.5452";

            string json = jobject.ToString();

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            }

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {

                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.AreEqual(str, "true");
                }
            }

            catch (WebException we)
            {
                Debug.WriteLine("TestPostUpdateLocation failed.");
                Assert.Fail("Web exception occurred");
            }

            /*
            // of the array is one line of the file. 
            string[] lines = System.IO.File.ReadAllLines(@"C:\keys.txt");
            string accK = lines[0];
            string secK = lines[1];

            NotificationManager nm = new NotificationManager(accK, secK, Amazon.RegionEndpoint.USEast1);
            AmazonSimpleNotificationServiceClient snsClient = new AmazonSimpleNotificationServiceClient(accK, secK, Amazon.RegionEndpoint.USEast1);
            
            var topic = snsClient.FindTopic(nm.getTopicName(username));
            string topicArn1 = "arn:aws:sns:us-east-1:336632281456:" + nm.getTopicName(username);
            Assert.AreEqual(topic.TopicArn, topicArn1);
             * */
        }

        // Testing delete user on 
        [TestMethod]
        public void TestDeleteUser()
        {
            // DELETE api/storage/api/stroage/user/{username}	
            // Deletes the user specified by the username.

            string userid = "100000000";

            WebRequest request = WebRequest.Create(URI + "/api/app/user/delete/" + userid);
            request.ContentType = "application/json";
            request.Method = "DELETE";


            JObject jobjects = new JObject();
            string json = jobjects.ToString();

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            }

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.AreEqual(str, "false");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestDeleteUser failed.");
                Console.Write(we.Message);
                Assert.Fail("Web Exception occurred");
            }

            // ADDING A VALID USER TO THE SERVER 

            string username = "test_delete_user_username";
            string password = "password";

            // First create a user ...
            // POST api/storage/user	
            // Posts the users information provided by JSON object data.

            request = WebRequest.Create(URI + "/api/storage/user");
            request.ContentType = "application/json";
            request.Method = "POST";

            JObject jobject = new JObject();
            jobject["username"] = username;
            jobject["password"] = password;

            json = jobject.ToString();
            userid = "";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            }

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    userid = reader.ReadToEnd();
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestDeleteUser failed. Couldn't post a user");
                Assert.Fail();
            }

            request = WebRequest.Create(URI + "/api/app/user/delete/" + userid);
            request.ContentType = "application/json";
            request.Method = "DELETE";
            
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write("");
                streamWriter.Close();
            }
            
            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK); 
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.AreEqual(str, "true");
                }
            }

            catch (WebException we)
            {
                Assert.Fail();
            }

            //Now Trying to get that user

            request = WebRequest.Create(URI + "/api/app/user/userid/" + username + "/" + password);
            //request.ContentType = "application/json";
            request.Method = "GET";

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreNotEqual(response.StatusCode, HttpStatusCode.OK);
                }
            }

            catch (WebException we)
            {

            }

            string[] lines = System.IO.File.ReadAllLines(@"C:\keys.txt");
            string accK = lines[0];
            string secK = lines[1];

            AmazonSimpleNotificationServiceClient snsClient = new AmazonSimpleNotificationServiceClient(accK, secK, Amazon.RegionEndpoint.USEast1);

            var appsResponse = snsClient.ListPlatformApplications();
            int i = 0;

            foreach (var app in appsResponse.PlatformApplications)
            {
                var appAttrsRequest = new GetPlatformApplicationAttributesRequest
                {
                    PlatformApplicationArn = app.PlatformApplicationArn
                };

                i++;
                var appAttrsResponse = snsClient.GetPlatformApplicationAttributes(appAttrsRequest);
                System.Diagnostics.Trace.WriteLine(app.PlatformApplicationArn);

            }

            //Assert.AreEqual(0, i);
        }
        
        [TestMethod]
        public void TestSendNotification()
        {
             // First Posting the User
            string username = "test_post_device_user_username";
            string password = "password";

            // First create a user ...
            // POST api/storage/user	
            // Posts the users information provided by JSON object data.

            WebRequest request = WebRequest.Create(URI + "/api/storage/user");
            request.ContentType = "application/json";
            request.Method = "POST";

            JObject jobject = new JObject();
            jobject["username"] = username;
            jobject["password"] = password;

            string json = jobject.ToString();
            string userid = "";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            }

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {

                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    userid = reader.ReadToEnd();
                    Assert.AreNotEqual(userid, "false");
                    //Assert.Inconclusive(userid);
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestPostUser failed. Couldn't post a user");
                Assert.Fail("Webexception Occurred. TestPostUser failed. Couldn't post a user");
            }
            
            // Now post the device token user/devicetoken/{username}/{pass}
            
            request = WebRequest.Create(URI + "/api/app/user/devicetoken/" + username +"/" + password);
            request.ContentType = "application/json";
            request.Method = "POST";

            jobject = new JObject();
            jobject["deviceToken"] = username + "_token";
            json = jobject.ToString();
            
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            }

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {

                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string resp = reader.ReadToEnd();
                    Assert.AreEqual(resp, "true");
                    //Assert.Inconclusive(userid);
                }
            }
            
            catch (WebException we)
            {
                Console.WriteLine("TestPostUser failed. Couldn't post a user");
                Assert.Fail("Webexception Occurred. TestPostUser failed. Couldn't post a user");
            }
            
            //Now Sending a notification
            request = WebRequest.Create(URI + "/api/app/user/notify/" + username + "/" + password);
            request.ContentType = "application/json";
            request.Method = "POST";
            
            jobject = new JObject();
            jobject["message"] = username;
            json = jobject.ToString();
            
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            }
            
            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {

                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string resp = reader.ReadToEnd();
                    Assert.AreEqual(resp, "true");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestPostUser failed. Couldn't post a user");
                Assert.Fail("Webexception Occurred. TestPostUser failed. Couldn't post a user");
            }
            
            request = WebRequest.Create(URI + "/api/app/user/delete/" + userid);
            request.ContentType = "application/json";
            request.Method = "DELETE";
            
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write("");
                streamWriter.Close();
            }
            
            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                }
            }

            catch (WebException we)
            {
            }
        }
        
        [TestMethod]
        public void TestPostCommand()
        {
            string username = "example_username";
            string password = "password";

            // First create a user ...
            // POST api/storage/user	
            // Posts the users information provided by JSON object data.

            WebRequest request = WebRequest.Create(URI + "/api/storage/user");
            request.ContentType = "application/json";
            request.Method = "POST";

            JObject jobject = new JObject();
            jobject["username"] = username;
            jobject["password"] = password;

            string json = jobject.ToString();
            string userid = "";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            }

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {

                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    userid = reader.ReadToEnd();
                    Assert.AreNotEqual(userid, "false");
                    //Assert.Inconclusive(userid);
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestPostUser failed. Couldn't post a user");
                Assert.Fail("Webexception Occurred. TestPostUser failed. Couldn't post a user");
            }
            
            request = WebRequest.Create(URI + "/api/app/user/command");
            request.ContentType = "application/json";
            request.Method = "POST";
            
            jobject = new JObject();
            jobject["userID"] = userid;
            jobject["command-string"] = password;
            jobject["time"] = "ISO8601";
            jobject["device-blob"] = "this blob doesn't matter";

            json = jobject.ToString();

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            }
            
            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {

                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.AreEqual(str, "true");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestPostUser failed. Couldn't post a user");
                Assert.Fail("Webexception Occurred. TestPostUser failed. Couldn't post a user");
            }
            
            request = WebRequest.Create(URI + "/api/app/user/delete/" + userid);
            request.ContentType = "application/json";
            request.Method = "DELETE";
            
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write("");
                streamWriter.Close();
            }
            
            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                }
            }

            catch (WebException we)
            {
            }
        }
        
        [TestMethod]
        public void TestGetUserID()
        {
            string username = "test_getuserid";
            string password = "password";

            // First create a user ...
            // POST api/storage/user	
            // Posts the users information provided by JSON object data.

            WebRequest request = WebRequest.Create(URI + "/api/storage/user");
            request.ContentType = "application/json";
            request.Method = "POST";

            JObject jobject = new JObject();
            jobject["username"] = username;
            jobject["password"] = password;

            string json = jobject.ToString();
            string userid = "";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            }

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {

                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    userid = reader.ReadToEnd();
                    Assert.AreNotEqual(userid, "false");
                    //Assert.Inconclusive(userid);
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestPostUser failed. Couldn't post a user");
                Assert.Fail("Webexception Occurred. TestPostUser failed. Couldn't post a user");
            }

            //Now Trying to get that user

            request = WebRequest.Create(URI + "/api/app/user/userid/" + username + "/" + password);
            //request.ContentType = "application/json";
            request.Method = "GET";

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string id = reader.ReadToEnd();
                    Assert.AreNotEqual(id, "false");
                    //Assert.Inconclusive(userid);
                    Assert.AreEqual(userid, id);
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestPostUser failed. Couldn't get the user");
                Assert.Fail("WebException Occurred");
            }
            
            request = WebRequest.Create(URI + "/api/app/user/delete/" + userid);
            request.ContentType = "application/json";
            request.Method = "DELETE";
            
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write("");
                streamWriter.Close();
            }
            
            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                }
            }

            catch (WebException we)
            {
            }
        }
        
        [TestMethod]
        public void TestPostDeviceToken()
        {
            // First Posting the User
            string username = "test_post_device_user_username";
            string password = "password";

            // First create a user ...
            // POST api/storage/user	
            // Posts the users information provided by JSON object data.

            WebRequest request = WebRequest.Create(URI + "/api/storage/user");
            request.ContentType = "application/json";
            request.Method = "POST";

            JObject jobject = new JObject();
            jobject["username"] = username;
            jobject["password"] = password;

            string json = jobject.ToString();
            string userid = "";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            }

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {

                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    userid = reader.ReadToEnd();
                    Assert.AreNotEqual(userid, "false");
                    //Assert.Inconclusive(userid);
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestPostUser failed. Couldn't post a user");
                Assert.Fail("Webexception Occurred. TestPostUser failed. Couldn't post a user");
            }
            
            // Now post the device token user/devicetoken/{username}/{pass}
            
            request = WebRequest.Create(URI + "/api/app/user/devicetoken/" + username +"/" + password);
            request.ContentType = "application/json";
            request.Method = "POST";

            jobject = new JObject();
            jobject["deviceToken"] = username + "_token";
            json = jobject.ToString();
            
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            }

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {

                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string resp = reader.ReadToEnd();
                    Assert.AreEqual(resp, "true");
                    //Assert.Inconclusive(userid);
                }
            }
            
            catch (WebException we)
            {
                Console.WriteLine("TestPostUser failed. Couldn't post a user");
                Assert.Fail("Webexception Occurred. TestPostUser failed. Couldn't post a user");
            }
            
            request = WebRequest.Create(URI + "/api/app/user/delete/" + userid);
            request.ContentType = "application/json";
            request.Method = "DELETE";
            
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write("");
                streamWriter.Close();
            }
            
            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                }
            }

            catch (WebException we)
            {
            }
        }

        // Testing POST USER on 
        [TestMethod]
        public void TestPostUser()
        {
            string username = "test_post_user_username";
            string password = "password";

            // First create a user ...
            // POST api/storage/user	
            // Posts the users information provided by JSON object data.

            WebRequest request = WebRequest.Create(URI + "/api/storage/user");
            request.ContentType = "application/json";
            request.Method = "POST";

            JObject jobject = new JObject();
            jobject["username"] = username;
            jobject["password"] = password;

            string json = jobject.ToString();
            string userid = "";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            }

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {

                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    userid = reader.ReadToEnd();
                    Assert.AreNotEqual(userid, "false");
                    //Assert.Inconclusive(userid);
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestPostUser failed. Couldn't post a user");
                Assert.Fail("Webexception Occurred. TestPostUser failed. Couldn't post a user");
            }

            //Now Trying to get that user

            request = WebRequest.Create(URI + "/api/app/user/userid/" + username + "/" + password);
            //request.ContentType = "application/json";
            request.Method = "GET";

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string id = reader.ReadToEnd();
                    Assert.AreNotEqual(id, "false");
                    //Assert.Inconclusive(userid);
                    Assert.AreEqual(userid, id);
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestPostUser failed. Couldn't get the user");
                Assert.Fail("WebException Occurred");
            }
            
            //DELETE dummy user
            
            request = WebRequest.Create(URI + "/api/app/user/delete/" + userid);
            request.ContentType = "application/json";
            request.Method = "DELETE";
            
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write("");
                streamWriter.Close();
            }
            
            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                }
            }

            catch (WebException we)
            {
            }
        }

        // Test GET user
        [TestMethod]
        public void TestGetUser()
        {

            string username = "test_get_user_username";
            string password = "password";

            WebRequest request = WebRequest.Create(URI + "/api/app/user/userid/" + username + "/" + password);
            request.ContentType = "application/json";
            request.Method = "GET";

            JObject jobject = new JObject();
            string json = jobject.ToString();


            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreNotEqual(response.StatusCode, HttpStatusCode.OK);
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestGetUser failed. Couldn't post a user");
            }


            request = WebRequest.Create(URI + "/api/storage/user");
            request.ContentType = "application/json";
            request.Method = "POST";

            jobject = new JObject();
            jobject["username"] = username;
            jobject["password"] = password;

            json = jobject.ToString();
            string userid = "";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            }

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {

                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    userid = reader.ReadToEnd();
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestPostUser failed. Couldn't post a user");
                Assert.Fail("Webexception Occurred. TestPostUser failed. Couldn't post a user");
            }

            //Now Trying to get that user

            request = WebRequest.Create(URI + "/api/app/user/userid/" + username + "/" + password);
            //request.ContentType = "application/json";
            request.Method = "GET";

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    Assert.AreEqual(userid, reader.ReadToEnd());
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestPostUser failed. Couldn't get the user");
                Assert.Fail("WebException Occurred");
            }
            
            //DELETE dummy user
            
            request = WebRequest.Create(URI + "/api/app/user/delete/" + userid);
            request.ContentType = "application/json";
            request.Method = "DELETE";
            
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write("");
                streamWriter.Close();
            }
            
            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                }
            }

            catch (WebException we)
            {
            }

        }

         // Test delete house
        [TestMethod]
        public void TestDeleteHouse()
        {
            // Posting a house
            // POST api/storage/house	
            // Posts the house with the JSON object information provided.

            int houseid = 101;

            WebRequest request = WebRequest.Create(URI + "/api/storage/house");
            request.ContentType = "application/json";
            request.Method = "POST";

            JObject jobject = new JObject();
           // jobject["houseId"] = houseid;
            jobject["name"] = "myhouse";
            string json = jobject.ToString();

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            }

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    houseid = Convert.ToInt32(str);
                    //Assert.AreEqual(str, "true");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestPostHouse failed. Couldn't post a house");
                Assert.Fail("WebException occurred. TestPostHouse failed. Couldn't post a house");
            }
            
            // DELETE api/storage/house/{houseid}	
            // Deletes the house with the specified houseid.

            request = WebRequest.Create(URI + "/api/storage/house/" + houseid);
            request.Method = "DELETE";
            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.AreEqual(str, "true");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestDeleteHouse failed. Couldn't delete a house");
                Assert.Fail("Got a web exception when deleting a house. Possibly can't communicate with Persistent Storage.");
            }
        }


         // Test delete house
        [TestMethod]
        public void TestPostHouse()
        {
            // Posting a house
            // POST api/storage/house	
            // Posts the house with the JSON object information provided.

            int houseid = 101;

            WebRequest request = WebRequest.Create(URI + "/api/storage/house");
            request.ContentType = "application/json";
            request.Method = "POST";

            JObject jobject = new JObject();
            jobject["houseId"] = houseid;
            jobject["name"] = "myhouse";
            string json = jobject.ToString();

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            }

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    houseid = Convert.ToInt32(str);
                    //Assert.AreEqual(str, "true");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestPostHouse failed. Couldn't post a house");
                Assert.Fail("WebException occurred. TestPostHouse failed. Couldn't post a house");
            }

            // GET THAT HOUSE
            // GET api/storage/house/{houseid}

            request = WebRequest.Create(URI + "/api/storage/house/" + houseid);
            //request.ContentType = "application/json";
            request.Method = "GET";
            /*
            //jobject = new JObject();
            //jobject["houseId"] = houseid;
            //jobject["name"] = "myhouse";
            //json = jobject.ToString();

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            } */

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    JObject j = JObject.Parse(str);
                    Assert.AreEqual(json, j.ToString());
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestPostHouse failed. Couldn't post a house");
                Assert.Fail("WebException occurreced. Can't get house.");
            }
        }

         // Test delete house
        [TestMethod]
        public void TestGetHouse()
        {
            int houseid = -1;

            WebRequest request = WebRequest.Create(URI + "/api/storage/house/" + houseid);
            //request.ContentType = "application/json";
            request.Method = "GET";
            /*
            JObject jobject = new JObject();
            jobject["houseId"] = houseid;
            jobject["name"] = "myhouse";
            string json = jobject.ToString();

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            } */

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.AreEqual(str, null);
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestGetHouse failed. Couldn't get a house");
               // Assert.Fail("WebException occurred. Can't get house.");
            }

            // Posting a house
            // POST api/storage/house   
            // Posts the house with the JSON object information provided.


            request = WebRequest.Create(URI + "/api/storage/house/?{houseid=" + houseid + "}");
            request.ContentType = "application/json";
            request.Method = "POST";

            JObject jobject = new JObject();
            jobject["houseId"] = houseid;
            jobject["name"] = "myhouse";
            string json = jobject.ToString();

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            }

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    houseid = Convert.ToInt32(str);
                   // Assert.AreEqual(str, "true");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestGetHouse failed. Couldn't get a house");
                Assert.Fail();
            }

            // GET THAT HOUSE
            // GET api/storage/house/{houseid}

            request = WebRequest.Create(URI + "/api/storage/house/" + houseid);
            //request.ContentType = "application/json";
            request.Method = "GET";

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    JObject j = JObject.Parse(str);
                    Assert.AreEqual(j.ToString(), json);
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestGetHouse failed. Couldn't get a house");
                Assert.Fail();
            }
        }


        [TestMethod]
        public void TestEnumerateDevices()
        {
            //initially in the "house" there are 3 devices
            /* run the house with this command:
             * .\House.exe -i house1 -t '{ \"storageLocation\": \"54.152.190.217\", \"serverLocation\": \"http://5574serverapi.azurewebsites.net/\", \"users\": [ { \"Username\":\"User1\", \"UserID\": \"12345\", \"Password\": \"thePassword\", \"Coordinates\": { \"x\":\"1234\", \"y\":\"4321\", \"z\":\"6789\" } }, { \"Username\": \"User2\", \"UserID\": \"67890\", \"Password\": \"secondPassword\", \"Coordinates\": { \"x\":\"1234\", \"y\":\"4321\", \"z\":\"6789\" } } ], \"houses\": [ { \"name\": \"house1\", \"port\": 8081, \"id\": 0, \"devices\": [ { \"name\": \"light1\", \"class\": \"LightSwitch\", \"type\": \"Simulated\", \"startState\": false }, { \"name\": \"Kitchen Ceiling Fan\", \"class\": \"CeilingFan\", \"type\": \"Simulated\", \"Enabled\": false, \"State\": 0 }, { \"name\": \"HVAC\", \"class\": \"Thermostat\", \"Enabled\": false, \"SetPoint\": 26.6 } ], \"rooms\": [ { \"name\": \"Kitchen\", \"dimensions\": { \"x\": 100, \"y\": 200 }, \"roomLevel\": 1, \"doors\": [ { \"x\": 20, \"y\": 200, \"connectingRoom\": 1 } ], \"devices\": [ 1 ] }, { \"name\": \"Family Room\", \"dimensions\": { \"x\": 300, \"y\": 200 }, \"roomLevel\": 1, \"doors\": [ { \"x\": 20, \"y\": 0, \"connectingRoom\": 0 } ], \"devices\": [ 0 ] } ], \"weather\": [ { \"Time\": \"2015-04-08T13:25:21.803833-04:00\", \"Temp\": 50 }, { \"Time\": \"2015-04-08T13:28:24.803833-04:00\", \"Temp\": 30 } ] } ]}' -f '{\"SimEpoch\": \"2015-04-08T13:25:20.0-04:00\"}'*/
            
            JArray unregDevices;
            JArray retVal = new JArray();
            UInt64 houseID;
            UInt64 deviceID;
            JObject house = new JObject();
            JObject device = new JObject();
            //provide the house with its url
            house["house_url"] = "http://localhost:8081";
            house["name"] = "myhouse";
            houseID = houseRepo.SaveHouse(house);

            //save one device to persistent storage with ID -0
            device["houseID"] = houseID;
            device["roomID"] = 0;
            device["Type"] = 1;
            device["Enabled"] = false;
            device["Value"] = 0;
            device["ID"] = 0;
            device["Class"] = "LightSwitch";
            device["Name"] = "light1";
            deviceID = deviceRepo.SaveDevice(device);
            String houseid = houseID.ToString();

            //get unregistered devices from the house. it should return 2 devices with ID-1 and ID-2
            unregDevices = deviceRepo.SendUnregisteredDevice(houseid);

            Assert.ReferenceEquals(unregDevices[0]["ID"], 1);
            Assert.ReferenceEquals(unregDevices[1]["ID"], 2);

        }

        [TestMethod]
        public void TestSimulatedPost()
        {
            JArray unregDevices;
            JArray retVal = new JArray();
            UInt64 houseID;
            UInt64 deviceID;
            JObject house = new JObject();
            JObject device = new JObject();
            //provide the house with its url
            house["house_url"] = "http://localhost:8081";
            house["name"] = "myhouse";
            houseID = houseRepo.SaveHouse(house);

            WebRequest request = WebRequest.Create("http://localhost:8081/api/device"); // this is local house uri
            request.Method = "GET";
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format(
                    "Server error (HTTP {0}: {1}).",
                    response.StatusCode,
                    response.StatusDescription));
            }


            //save one device to persistent storage with ID -0
            device["houseID"] = houseID;
            device["roomID"] = 0;
            device["Type"] = 1;
            device["Enabled"] = false;
            device["Value"] = 0;
            device["ID"] = 0;
            device["Class"] = "LightSwitch";
            device["Name"] = "light1";
            deviceID = deviceRepo.SaveDevice(device);
            String houseid = houseID.ToString();
            device["Enabled"] = true;
            bool ret;
            //get unregistered devices from the house. it should return 2 devices with ID-1 and ID-2
            ret = deviceRepo.updateSimulation(houseID, 0, deviceID, device);
            Assert.IsTrue(ret);

          

        }


        //////////////////////////BEGINS STORAGE TESTS/////////////////////////
        //Tests Posts a device with the JSON object data given.
        //Also Tests GET devices in specific houseID
        [TestMethod]
        public void TestPostDevice()
        {

            int houseid = 0;

            WebRequest request = WebRequest.Create(URI + "/api/storage/house");
            request.ContentType = "application/json";
            request.Method = "POST";

            JObject jobject = new JObject();
            jobject["houseId"] = houseid;
            jobject["name"] = "myhouse";
            string json = jobject.ToString();

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            }

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    houseid = Convert.ToInt32(str);
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestPostHouse failed. Couldn't post a house");
                Assert.Fail("WebException occurred. TestPostHouse failed. Couldn't post a house");
            }

            // Posting a ROOM

            request = WebRequest.Create(URI + "/api/storage/space/");
            request.ContentType = "application/json";
            request.Method = "POST";

            JObject jobjects = new JObject();
            jobjects["houseID"] = houseid;
            jobjects["roomid"] = "1234";
            jobjects["type"] = 1;
            jobjects["name"] = "BedroomLight";
            jobjects["x"] = "100";
            jobjects["y"] = "300";

            json = jobjects.ToString();

            int roomId = 0;

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            }

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {

                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    roomId = Convert.ToInt32(str);
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestStoragePostSpace failed, Didnt Post.");
                Assert.Fail();
            }

            // Posting a device

            request = WebRequest.Create(URI + "/api/storage/device");
            request.ContentType = "application/json";
            request.Method = "POST";

            jobjects = new JObject();
            //jobjects["ID"] = "1234";
            jobjects["houseID"] = houseid;
            jobjects["roomID"] = roomId;
            //jobjects["deviceID"] = "5";
            jobjects["Type"] = 0;
            //jobjects["Name"] = "BedroomLight";
            json = jobjects.ToString();

            int deviceId = 0;

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            }

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    deviceId = Convert.ToInt32(str);
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestStorageDevicePost failed. Couldn't post the device");
                Assert.Fail("WebException occurred.\n");
            }

            //Now Trying to get a Device inside a house
            request = WebRequest.Create(URI + "/api/storage/device/" + houseid);
            //request.ContentType = "application/json";
            request.Method = "GET";

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    JArray j;
                    j = JArray.Parse(str);
                    Assert.AreEqual((int)j[0]["device-id"], deviceId);
                   // Assert.Fail(str + Convert.ToString(deviceId));
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestStorageDevicePost failed. Couldn't get the device");
                Assert.Fail();
            }

        }

        //Storage Device GET api/storage/device/{houseid}/{spaceid}/{deviceid}
        [TestMethod]
        public void TestPostDeviceId()
        {
            WebRequest request = WebRequest.Create(URI + "/api/storage/device");
            request.ContentType = "application/json";
            request.Method = "POST";

            int houseid = 67;
            int roomid = 20;
            int deviceid = 0;

            JObject jobjects = new JObject();
            jobjects["ID"] = "1234";
            jobjects["houseID"] = houseid;
            jobjects["roomID"] = roomid;
            jobjects["deviceID"] = "5";
            jobjects["type"] = "Light";
            jobjects["Name"] = "BedroomLight";
            string json = jobjects.ToString();

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            }

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    deviceid = Convert.ToInt32(str);
                    //Assert.AreEqual(str, "true");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestStorageDevicePost failed.");
                Assert.Fail();
            }

            //Now try to GET DeviceId w/ wrong Data

            request = WebRequest.Create(URI + "/api/storage/device/" + houseid + "/" + "-1" + "/" + deviceid);
            request.ContentType = "application/json";
            request.Method = "GET";

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();

                    Assert.AreEqual(str, "[]");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("StorageGetDeviceID failed.");
                Assert.Fail();
            }

            // NOW Try to get the CORRECT Device ID Data

            request = WebRequest.Create(URI + "/api/storage/device/" + houseid + "/" + roomid + "/" + deviceid );
            request.ContentType = "application/json";
            request.Method = "GET";

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.AreNotEqual(str, "null");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("StorageGetDeviceID failed.");
                Assert.Fail();
            }
        }

        //GET api/storage/device/{houseid}/{spaceid}	
        //Gets all of the devices information with the specified house ID and space ID.
        /// GET FOR ROOM ID (ERROR & OK)
        /// </summary>
        [TestMethod]
        public void TestGetRoomID()
        {
            int houseid = 0;
            WebRequest request = WebRequest.Create(URI + "/api/storage/house");
            request.ContentType = "application/json";
            request.Method = "POST";

            JObject jobject = new JObject();
            jobject["blob"] = houseid;
            jobject["name"] = "myhouse";
            string json = jobject.ToString();

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            }

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    houseid = Convert.ToInt32(str);
                    //Assert.AreEqual(str, "true");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestPostHouse failed. Couldn't post a house");
                Assert.Fail("WebException occurred. TestPostHouse failed. Couldn't post a house");
            }

            //FIRST POST Data

            request = WebRequest.Create(URI + "/api/storage/space");
            request.ContentType = "application/json";
            request.Method = "POST";

            JObject jobjects = new JObject();
            jobjects["ID"] = "1234";
            jobjects["houseID"] = houseid;
            jobjects["deviceid"] = "5";
            jobjects["type"] = "Light";
            jobjects["Name"] = "BedroomLight";
            json = jobjects.ToString();

            int roomid = 0;

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            }

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    roomid = Convert.ToInt32(str);
                    //Assert.AreEqual(str, "true");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestStorageDevicePost failed.");
                Assert.Fail("WebException Occurred when ");
            }

            //Testing with giving FALSE DATA 
            request = WebRequest.Create(URI + "/api/storage/space/" + 0 + "/" + 1);
            request.ContentType = "application/json";
            request.Method = "GET";

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.AreEqual(str, "null");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestStorageGetRoomId failed.");
                Assert.Fail();
            }

            //NOW TRY TO GET THE CORRECT ROOM ID DATA (URI + "/api/storage/space/" + houseid + "/" + spaceID
            request = WebRequest.Create(URI + "/api/storage/space/" + houseid + "/" + roomid);
            request.ContentType = "application/json";
            request.Method = "GET";

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    JObject j = JObject.Parse(str);
                    Assert.AreEqual(json, j.ToString());
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestStorageGetRoomId failed.");
                Assert.Fail();
            }
        }

        //GET api/storage/device/{houseid}/{spaceid}/{type}	
        //Gets all of the devices in the space of the type specified, with the provided house ID and space ID.
        [TestMethod]
        public void TestGetDevicedRommType()
        {
            //FIRST POST Data			
            WebRequest request = WebRequest.Create(URI + "/api/storage/device");
            request.ContentType = "application/json";
            request.Method = "POST";

            int deviceid = 0;

            JObject jobjects = new JObject();
            jobjects["ID"] = "1234";
            jobjects["houseID"] = "60";
            jobjects["roomID"] = "20";
            jobjects["Type"] = "Light";
            jobjects["Name"] = "BedroomLight";
            string json = jobjects.ToString();

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            }

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    deviceid = Convert.ToInt32(str);
                    //Assert.AreEqual(str, "true");
                }
            }

            catch (WebException we)
            {
                Assert.Fail("StorageGetDevicesInRoomofType failed. Couldn't post a user");
            }

            //TEST GETTING FALSE DEVICE TYPES
            request = WebRequest.Create(URI + "/api/storage/device/" + 60 + "/" + 20 + "/unkown");
            request.ContentType = "application/json";
            request.Method = "GET";

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.AreEqual(str, "null");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("StorageGetDevicesInRoomofType failed.");
                Assert.Fail();
            }
            //TEST GETTING CORRECT DEVICE TYPES
            request = WebRequest.Create(URI + "/api/storage/device/" + 60 + "/" + 20 + "/Light");
            request.ContentType = "application/json";
            request.Method = "GET";

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.AreEqual(str, "true");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("StorageGetDevicesInRoomofType failed.");
                Assert.Fail();
            }
        }

        //GET api/storage/device/{houseid}/{type}	
        //Gets all of the devices in the house of the specified type, with the provided house ID.
        [TestMethod]
        public void TestGetDevicesHouseType()
        {
            //FIRST POST Data			
            WebRequest request = WebRequest.Create(URI + "/api/storage/device");
            request.ContentType = "application/json";
            request.Method = "POST";

            JObject jobjects = new JObject();
            jobjects["ID"] = "1234";
            jobjects["houseid"] = "567";
            jobjects["roomid"] = "890";
            jobjects["deviceid"] = "5";
            jobjects["type"] = "Light";
            jobjects["Name"] = "BedroomLight";
            string json = jobjects.ToString();

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            }

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.AreEqual(str, "true");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("StorageGetDevicesInHouseType failed.");
                Assert.Fail();
            }

            //TEST Getting CORRECT DATA 
            request = WebRequest.Create(URI + "/api/storage/device/?{houseid=" + 567 + "/&{type=Light}");
            request.ContentType = "application/json";
            request.Method = "GET";

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.AreEqual(str, "true");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("StorageGetDevicesInHouseType failed.");
                Assert.Fail();
            }
            //Test Getting WRONG DATA
            request = WebRequest.Create(URI + "api/storage/device/?{houseid=" + 991 + "/&{type=IDontKnow}");
            request.ContentType = "application/json";
            request.Method = "GET";

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.AreEqual(str, "false");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("StorageGetDevicesInHouseType failed. ");
                Assert.Fail();
            }
        }

        //DELETE api/storage/device/{houseid}/{spaceid}/{deviceid}	
        //Deletes a device with the specified house ID, space ID, and device ID provided.
        [TestMethod]
        public void TestDeleteDevice()
        {
            bool retVal;
            UInt64 houseId, deviceId;
            JToken compare;
            JArray getDev;
            JObject house = new JObject();
            JObject device = new JObject();
            house["house_url"] = "http://localhost:8081";
            house["name"] = "myhouse";
            houseId = houseRepo.SaveHouse(house);


            //JToken blobVal;
            //String blob = (string)devicesInStorage[j]["blob"];
            ////if (blob.Contains("I"))
            ////{
            ////    int h;
            ////    h = 1;
            ////}

            //blobVal = JToken.Parse(blob);
            //UInt64 id2 = (UInt64)(blobVal[j]["ID"]);

            device["houseID"] = houseId;
            device["roomID"] = 0;
            device["Type"] = 1;
            device["Enabled"] = false;
            device["Value"] = 0;
            device["ID"] = 0;
            device["Class"] = "LightSwitch";
            device["Name"] = "light1";
            deviceId = deviceRepo.SaveDevice(device);

            compare = (JToken)device;
            //Test Deleting invalid Data	
            deviceRepo.DeleteDevice("0", "0", "0");
            JToken test;
            getDev = deviceRepo.GetDevice(houseId.ToString());
            String blob = (string)getDev[0]["blob"];
            test = JToken.Parse(blob);
            Assert.AreEqual(compare["ID"], test["ID"]);
            //Test Deleting actual Device
            deviceRepo.DeleteDevice(houseId.ToString(), "0", deviceId.ToString());
            getDev = deviceRepo.GetDevice(houseId.ToString());
            Assert.AreEqual(getDev.Count, 0);
           }

        /////////////ENDS TEST FROM STORAGE API///////////////////////////////////////

        /////////////////////BEGINS TEST FOR SPACE//////////////////////////////
        //POST api/storage/space	
        //Posts the space with the JSON object data information provided.
        //Also Test GETS Room
        [TestMethod]
        public void TestPostSpace()
        {
            int houseid = 0;
            WebRequest request = WebRequest.Create(URI + "/api/storage/house");
            request.ContentType = "application/json";
            request.Method = "POST";

            JObject jobject = new JObject();
            jobject["blob"] = houseid;
            jobject["name"] = "myhouse";
            string json = jobject.ToString();

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            }

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    houseid = Convert.ToInt32(str);
                    //Assert.AreEqual(str, "true");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestPostHouse failed. Couldn't post a house");
                Assert.Fail("WebException occurred. TestPostHouse failed. Couldn't post a house");
            }

            request = WebRequest.Create(URI + "/api/storage/space");
            request.ContentType = "application/json";
            request.Method = "POST";

            JObject jobjects = new JObject();
            jobjects["houseID"] = houseid;
            jobjects["roomid"] = "1234";
            jobjects["type"] = "Light";
            jobjects["name"] = "BedroomLight";
            jobjects["x"] = "100";
            jobjects["y"] = "300";

            int spaceID = 0;

            json = jobjects.ToString();

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            }

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {

                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    spaceID = Convert.ToInt32(str);
                    //Assert.AreEqual(str, "true");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestStoragePostSpace failed, Didnt Post.");
                Assert.Fail();
            }

            //Now Try to Get That  (Getting House and Room/Space ID)
            request = WebRequest.Create(URI + "/api/storage/space/" + houseid + "/" + spaceID);
            request.ContentType = "application/json";
            request.Method = "GET";

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {

                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    JObject j = JObject.Parse(str);
                    Assert.AreEqual(json, j.ToString());
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestStoragePostSpace failed, Didnt Get the Data.");
                Assert.Fail();
            }

            //NOW Try to Get Incorrect Data From Post

            request = WebRequest.Create(URI + "/api/storage/space/" + 55 + "/" + 65);
            request.ContentType = "application/json";
            request.Method = "GET";

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();

                    Assert.AreEqual(str, "null");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestStoragePostSpace, incorrect data GET failed.");
                Assert.Fail();
            }
        }

        //DELETE api/storage/space/{houseid}/{spaceid}	
        //Deletes the space specified by the houseid and spaceid
        [TestMethod]
        public void TestDeleteRoom()
        {
            //FIRST TRY DELETING A ROOM BEFORE POSTING ONE
         
			WebRequest request = WebRequest.Create(URI + "/api/storage/house");
			request.ContentType = "application/json";
			request.Method = "POST";

            // postable IDs
            int houseID = 1010;
            int roomID = 1011;

			JObject jobject = new JObject();
			jobject["blob"] = houseID;
			jobject["name"] = "myhouse";
			string json = jobject.ToString();

			using (var streamWriter = new StreamWriter(request.GetRequestStream()))
			{
				streamWriter.Write(json);
				streamWriter.Close();
			}

			try
			{
				using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
				{
					Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
					var stream = response.GetResponseStream();
					var reader = new StreamReader(stream);
					string str = reader.ReadToEnd();
					houseID = Convert.ToInt32(str);
					//Assert.AreEqual(str, "true");
				}
			}

			catch (WebException we)
			{
				Console.WriteLine("TestPostHouse failed. Couldn't post a house");
				Assert.Fail("WebException occurred. TestPostHouse failed. Couldn't post a house");
			}

			request = WebRequest.Create(URI + "/api/storage/space");
			request.ContentType = "application/json";
			request.Method = "POST";

			JObject jobjects = new JObject();
			jobjects["houseID"] = houseID;
			jobjects["roomid"] = "1234";
			jobjects["type"] = "Light";
			jobjects["name"] = "BedroomLight";
			jobjects["x"] = "100";
			jobjects["y"] = "300";

			int spaceID = 0;

			json = jobjects.ToString();

			using (var streamWriter = new StreamWriter(request.GetRequestStream()))
			{
				streamWriter.Write(json);
				streamWriter.Close();
			}

			try
			{
				using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
				{

					Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
					var stream = response.GetResponseStream();
					var reader = new StreamReader(stream);
					string str = reader.ReadToEnd();
					spaceID = Convert.ToInt32(str);
					//Assert.AreEqual(str, "true");
				}
			}

			catch (WebException we)
			{
				Console.WriteLine("TestStoragePostSpace failed, Didnt Post.");
				Assert.Fail();
			}
	        //DELETE THE Room

			// is this correct format?
			request = WebRequest.Create(URI + "/api/storage/space/" + houseID + "/" + roomID );
            request.ContentType = "application/json";
            request.Method = "DELETE";

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.AreEqual(str, "true");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestDeleteRoom failed.");
                Assert.Fail();
            }

        }

        //Testing Logs
        [TestMethod]
        public void TestLogFile()
        {
            WebRequest request = WebRequest.Create(URI + "/api/logfile");
            request.ContentType = "application/json";
            request.Method = "DELETE";

            int count = 0;

            JObject jobject = new JObject();
            string json = jobject.ToString();

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            }

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreNotEqual(response.StatusCode, HttpStatusCode.BadRequest);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestLogFile failed.");
                Assert.Fail("WebException occurred. TestLogFile failed.");
            }

            request = WebRequest.Create(URI + "/api/logfile/count");
            request.ContentType = "application/json";
            request.Method = "GET";

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreNotEqual(response.StatusCode, HttpStatusCode.BadRequest);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    count = Convert.ToInt32(str);
                    Assert.AreEqual(count, 0);
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestLogFile failed.");
                Assert.Fail("WebException occurred. TestLogFile failed.");
            }

            //Doing Bad Request
            request = WebRequest.Create(URI + "/api/sim/timeframe");
            request.ContentType = "application/json";
            request.Method = "POST";

            JObject jobjects = new JObject();
            jobjects["sds"] = "-1";
            jobjects["rosdsomID"] = "-1";
            json = jobjects.ToString();

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            }

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestLogFile failed.");
                //Assert.Fail("WebException occurred. TestLogFile failed.");
            }

            request = WebRequest.Create(URI + "/api/logfile/count");
            request.ContentType = "application/json";
            request.Method = "GET";

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreNotEqual(response.StatusCode, HttpStatusCode.BadRequest);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    count = Convert.ToInt32(str);
                    Assert.AreEqual(count, 1);
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestLogFile failed.");
                Assert.Fail("WebException occurred. TestLogFile failed.");
            }
        }

        //Test POST STATE to Persistent Storage
        [TestMethod]
        public void TestPostState()
        {
            int houseid = 0;

            WebRequest request = WebRequest.Create(URI + "/api/storage/house");
            request.ContentType = "application/json";
            request.Method = "POST";

            JObject jobject = new JObject();
            jobject["houseId"] = houseid;
            jobject["name"] = "myhouse";
            string json = jobject.ToString();

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            }

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    houseid = Convert.ToInt32(str);
                    //Assert.AreEqual(str, "true");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestPostHouse failed. Couldn't post a house");
                Assert.Fail("WebException occurred. TestPostHouse failed. Couldn't post a house");
            }

            // Posting a ROOM

            request = WebRequest.Create(URI + "/api/storage/space/");
            request.ContentType = "application/json";
            request.Method = "POST";

            JObject jobjects = new JObject();
            jobjects["houseID"] = houseid;
            jobjects["roomid"] = "1234";
            jobjects["type"] = "Light";
            jobjects["name"] = "BedroomLight";
            jobjects["x"] = "100";
            jobjects["y"] = "300";

            json = jobjects.ToString();

            int roomId = 0;

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            }

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {

                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    roomId = Convert.ToInt32(str);
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestStoragePostSpace failed, Didnt Post.");
                Assert.Fail();
            }

            // Posting a device

            request = WebRequest.Create(URI + "/api/storage/device");
            request.ContentType = "application/json";
            request.Method = "POST";

            jobjects = new JObject();
            //jobjects["ID"] = "1234";
            jobjects["houseID"] = houseid;
            jobjects["roomID"] = roomId;
            //jobjects["deviceID"] = "5";
            jobjects["Type"] = "Light";
            //jobjects["Name"] = "BedroomLight";
            json = jobjects.ToString();

            int deviceId = 0;

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            }

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    deviceId = Convert.ToInt32(str);
                    //Assert.AreEqual(str, "true");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestStorageDevicePost failed. Couldn't post the device");
                Assert.Fail("WebException occurred.\n");
            }

            request = WebRequest.Create(URI + "/api/house/device/state");
            request.ContentType = "application/json";
            request.Method = "POST";

            jobjects = new JObject();
            jobjects["houseID"] = houseid;
            jobjects["roomID"] = roomId;
            jobjects["deviceID"] = deviceId;
            jobjects["Type"] = "Light";
            jobjects["Name"] = "BedroomLight";
            jobjects["state"] = "changed";
            json = jobjects.ToString();

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            }

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    //Assert.Fail(houseid + " " + deviceId + " " + roomId);
                    Assert.AreEqual(str, "true");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestStorageDeviceDelete failed.");
                Assert.Fail(houseid + " " + deviceId + " " + roomId);
            }

            //Now Try to Get the State Given Correct Data
            request = WebRequest.Create(URI + "/api/storage/device/" + houseid + "/" + roomId + "/" + deviceId + "/" + "changed");
            request.ContentType = "application/json";
            request.Method = "GET";

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.Equals(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.Equals(str, "true");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestStorageDeviceDelete failed.");
                Assert.Fail();
            }
        } 
    }

}
