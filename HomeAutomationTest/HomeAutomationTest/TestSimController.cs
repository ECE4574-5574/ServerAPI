using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HomeAutomationTest
{

    [TestClass]
    public class TestSimController
    {
        private string URI = "http://52.5.95.215";

        [TestMethod]
        public void TestPostTimeFrame()
        {
            Console.WriteLine("Starting TestPostTimeFrame.");

            WebRequest request = WebRequest.Create(URI + "/api/sim/timeframe");
            request.ContentType = "application/json";
            request.Method = "POST";

            JObject jobject = new JObject();
            jobject["wall"] = "1997-07-16T19:20:30+01:00";
            jobject["sim"] = "ISO 8601";
            jobject["rate"] = "double";

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

                    Assert.Equals(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.Equals(str, "true");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestPostOnSimHarness failed.");
                Assert.Fail();
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

                    Assert.Equals(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.Equals(str, "false");
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
            Console.WriteLine("Starting TestPostUpdateLocation...");

            string username = "test_post_update_username";
            string password = "password";

            // First create a user ...
            // POST api/storage/user	
            // Posts the users information provided by JSON object data.

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

                    Assert.Equals(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.Equals(str, "true");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestPostUpdateLocation failed. Couldn't post a user");
                Assert.Fail();
            }

            // Now post update location
            // POST api/app/user/updateposition/{username}

            request = WebRequest.Create(URI + "/api/app/user/updateposition/" + username);
            request.ContentType = "application/json";
            request.Method = "POST";

            jobject = new JObject();
            jobject["time"] = "2015-05-06 12:15:35";
            jobject["userId"] = username;
            jobject["lat"] = "37.874342";
            jobject["lon"] = "-86.342234";
            jobject["alt"] = "21.5452";

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

                    Assert.Equals(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.Equals(str, "true");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestPostUpdateLocation failed.");
                Assert.Fail();
            }

            //Testing update location with wrong username

            request = WebRequest.Create(URI + "/api/app/user/updateposition/" + "wrongusername");
            request.ContentType = "application/json";
            request.Method = "POST";

            jobject = new JObject();
            jobject["time"] = "2015-05-06 12:15:35";
            jobject["userId"] = username +"wrongusername";
            jobject["lat"] = "37.874342";
            jobject["lon"] = "-86.342234";
            jobject["alt"] = "21.5452";

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
                    // Should get a bad request
                    Assert.Equals(response.StatusCode, HttpStatusCode.BadRequest);
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestPostUpdateLocation failed.");
                Assert.Fail();
            }
        }

        // Testing delete user on 
        [TestMethod]
        public void TestDeleteUser()
        {
            // DELETE api/storage/api/stroage/user/{username}	
            // Deletes the user specified by the username.

            string username = "idontexist";

            WebRequest request = WebRequest.Create(URI + "/api/storage/user/" + username);
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
                    Assert.AreNotSame(response.StatusCode, HttpStatusCode.OK);
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestDeleteUser failed.");
                Console.Write(we.Message);
            }

            // ADDING A VALID USER TO THE SERVER 

            username = "test_delete_user_username";
            string password = "password";

            // First create a user ...
            // POST api/storage/user	
            // Posts the users information provided by JSON object data.

            request = WebRequest.Create(URI + "/api/storage/user/" + username);
            request.ContentType = "application/json";
            request.Method = "POST";

            JObject jobject = new JObject();
            jobject["UserID"] = username;
            jobject["Password"] = password;

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
                    Assert.Equals(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.Equals(str, "true");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestDeleteUser failed. Couldn't post a user");
                Assert.Fail();
            }

            // DELETEING THE USER NOW
            request = WebRequest.Create(URI + "/api/storage/user/" + username);
            request.ContentType = "application/json";
            request.Method = "DELETE";


            jobjects = new JObject();
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
                    Assert.AreNotSame(response.StatusCode, HttpStatusCode.OK);
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestDeleteUser failed.");
                Console.Write(we.Message);
            }

            //Now Trying to get that user

            request = WebRequest.Create(URI + "/api/storage/user/" + username);
            request.ContentType = "application/json";
            request.Method = "GET";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            }

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.Equals(response.StatusCode, HttpStatusCode.OK); 
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestDeleteUser failed. Couldn't post a user");
                Assert.Fail();
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

                    Assert.Equals(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.Equals(str, "true");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestPostUser failed. Couldn't post a user");
                Assert.Fail();
            }

            //Now Trying to get that user

            request = WebRequest.Create(URI + "/api/storage/user/" + username);
            request.ContentType = "application/json";
            request.Method = "GET";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();
            }

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.Equals(response.StatusCode, HttpStatusCode.OK);
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestPostUser failed. Couldn't get the user");
                Assert.Fail();
            }
        }

        // Test GET user
        [TestMethod]
        public void TestGetUser()
        {

            string username = "test_get_user_username";
            string password = "password";

            WebRequest request = WebRequest.Create(URI + "/api/storage/user/" + username);
            request.ContentType = "application/json";
            request.Method = "GET";

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
                    Assert.Equals(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.Equals(str, null);
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestGetUser failed. Couldn't post a user");
                Assert.Fail();
            }


            // Posting a user on the server
            request = WebRequest.Create(URI + "/api/storage/user/" + username);
            request.ContentType = "application/json";
            request.Method = "POST";

            jobject = new JObject();
            jobject["UserID"] = username;
            jobject["Password"] = password;

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

                    Assert.Equals(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.Equals(str, "true");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestPostUser failed. Couldn't post a user");
                Assert.Fail();
            }


            request = WebRequest.Create(URI + "/api/storage/user/" + username);
            request.ContentType = "application/json";
            request.Method = "GET";

            jobject = new JObject();
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
                    Assert.Equals(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.Equals(str, "true");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestGetUser failed. Couldn't post a user");
                Assert.Fail();
            }

        }

         // Test delete house
        [TestMethod]
        public void TestDeleteHouse()
        {
            // DELETE api/storage/house/{houseid}	
            // Deletes the house with the specified houseid.

            int houseid = 100;

            WebRequest request = WebRequest.Create(URI + "/storage/house/?{houseid=" + houseid + "}");
            request.ContentType = "application/json";
            request.Method = "DELETE";

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
                    Assert.Equals(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.Equals(str, "false");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestDeleteHouse failed. Couldn't delete a house");
                Assert.Fail("Got a web exception when deleting a house. Possibly can't communicate with Persistent Storage.");
            }

            // Posting a house
            // POST api/storage/house	
            // Posts the house with the JSON object information provided.

            request = WebRequest.Create(URI + "/storage/house/?{houseid=" + houseid + "}");
            request.ContentType = "application/json";
            request.Method = "POST";

            jobject = new JObject();
            jobject["houseId"] = houseid;
            jobject["name"] = "myhouse";
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
                    Assert.Equals(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.Equals(str, "true");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestDeleteHouse failed. Couldn't delete a house");
                Assert.Fail();
            }

            // Try deleteing that house again

            request = WebRequest.Create(URI + "/storage/house/?{houseid=" + houseid + "}");
            request.ContentType = "application/json";
            request.Method = "DELETE";

            jobject = new JObject();
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
                    Assert.Equals(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.Equals(str, "true");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestDeleteHouse failed. Couldn't delete a house");
                Assert.Fail();
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

            WebRequest request = WebRequest.Create(URI + "/storage/house/?{houseid=" + houseid + "}");
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
                    Assert.Equals(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.Equals(str, "true");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestPostHouse failed. Couldn't post a house");
                Assert.Fail();
            }

            // GET THAT HOUSE
            // GET api/storage/house/{houseid}

            request = WebRequest.Create(URI + "storage/house/?{houseid=" + houseid + "}");
            request.ContentType = "application/json";
            request.Method = "GET";

            jobject = new JObject();
            jobject["houseId"] = houseid;
            jobject["name"] = "myhouse";
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
                    Assert.Equals(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.Equals(str, "true");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestPostHouse failed. Couldn't post a house");
                Assert.Fail();
            }
        }

         // Test delete house
        [TestMethod]
        public void TestGetHouse()
        {
            int houseid = 102;

            WebRequest request = WebRequest.Create(URI + "/storage/house/?{houseid=" + houseid + "}");
            request.ContentType = "application/json";
            request.Method = "GET";

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
                    Assert.Equals(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.Equals(str, "false");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestGetHouse failed. Couldn't get a house");
                Assert.Fail();
            }

            // Posting a house
            // POST api/storage/house   
            // Posts the house with the JSON object information provided.


            request = WebRequest.Create(URI + "/storage/house/?{houseid=" + houseid + "}");
            request.ContentType = "application/json";
            request.Method = "POST";

            jobject = new JObject();
            jobject["houseId"] = houseid;
            jobject["name"] = "myhouse";
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
                    Assert.Equals(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.Equals(str, "true");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestGetHouse failed. Couldn't get a house");
                Assert.Fail();
            }

            // GET THAT HOUSE
            // GET api/storage/house/{houseid}

            request = WebRequest.Create(URI + "/storage/house/?{houseid=" + houseid + "}");
            request.ContentType = "application/json";
            request.Method = "GET";

            jobject = new JObject();
            jobject["houseId"] = houseid;
            jobject["name"] = "myhouse";
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
                    Assert.Equals(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.Equals(str, "true");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestGetHouse failed. Couldn't get a house");
                Assert.Fail();
            }
        }
    }

}
