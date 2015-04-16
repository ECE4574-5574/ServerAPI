using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace HomeAutomationTest
{

    [TestClass]
    public class TestSimController
    {
        private string URI = "http://52.5.95.215";
        private string uri1 = "http://52.5.95.215";

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
            jobject["time"] = "2015-05-06 12:15:35";
            jobject["userId"] = username;
            jobject["lat"] = "37.874342";
            jobject["long"] = "-86.342234";
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

            //Testing update location with wrong username
            /*
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
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestPostUpdateLocation failed.");
                Assert.Fail();
            }
             */
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
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.AreEqual(str, "true");
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
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK); 
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
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
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
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.AreEqual(str, null);
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
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.AreEqual(str, "true");
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
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.AreEqual(str, "false");
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
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.AreEqual(str, "true");
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
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.AreEqual(str, "true");
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
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.AreEqual(str, "false");
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
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.AreEqual(str, "true");
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
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.AreEqual(str, "true");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestGetHouse failed. Couldn't get a house");
                Assert.Fail();
            }
        }

        //////////////////////////BEGINS STORAGE TESTS/////////////////////////
        //Tests Posts a device with the JSON object data given.
        //Also Tests GET devices in specific houseID
        [TestMethod]
        public void TestPostDevice()
        {

            WebRequest request = WebRequest.Create(uri1 + "/api/storage/device");
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
                Console.WriteLine("TestStorageDevicePost failed. Couldn't post the device");
                Assert.Fail();
            }


            //Now Trying to get a Device inside a house
            request = WebRequest.Create(URI + "api/storage/device/?{houseid=" + 567 + "}");
            request.ContentType = "application/json";
            request.Method = "GET";

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
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
            WebRequest request = WebRequest.Create(uri1 + "/api/storage/device");
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
                    Assert.Equals(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.Equals(str, "true");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("TestStorageDevicePost failed.");
                Assert.Fail();
            }

            //Now try to GET DeviceId w/ wrong Data

            request = WebRequest.Create(uri1 + "/api/storage/device/?{houseid=" + 8732 + "/&{roomid=" + 21 + "/&{deviceid=" + 3 + "}");
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
                Console.WriteLine("StorageGetDeviceID failed.");
                Assert.Fail();
            }

            // NOW Try to get the CORRECT Device ID Data

            request = WebRequest.Create(uri1 + "/api/storage/device/?{houseid=" + 567 + "/&{roomid=" + 890 + "/&{deviceid=" + 5 + "}");
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
            //FIRST POST Data

            WebRequest request = WebRequest.Create(uri1 + "/api/storage/device");
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
                Console.WriteLine("TestStorageDevicePost failed.");
                Assert.Fail();
            }

            //Testing with giving FALSE DATA 
            request = WebRequest.Create(uri1 + "api/storage/device/?{houseid=" + 000 + "/&{spaceid=" + 000 + "}");
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
                Console.WriteLine("TestStorageGetRoomId failed.");
                Assert.Fail();
            }

            //NOW TRY TO GET THE CORRECT ROOM ID DATA
            request = WebRequest.Create(uri1 + "api/storage/device/?{houseid=" + 567 + "/&{spaceid=" + 890 + "}");
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
                Console.WriteLine("StorageGetDevicesInRoomofType failed. Couldn't post a user");
                Assert.Fail();
            }

            //TEST GETTING FALSE DEVICE TYPES
            request = WebRequest.Create(URI + "/api/storage/device/?{houseid=" + 6511 + "/&{spaceid=" + 21 + "/&{type=unknown}");
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
                Console.WriteLine("StorageGetDevicesInRoomofType failed.");
                Assert.Fail();
            }
            //TEST GETTING CORRECT DEVICE TYPES
            request = WebRequest.Create(URI + "/api/storage/device/?{houseid=" + 567 + "/&{spaceid=" + 890 + "/&{type=Light}");
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
                    Assert.Equals(response.StatusCode, HttpStatusCode.OK);
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    Assert.Equals(str, "true");
                }
            }

            catch (WebException we)
            {
                Console.WriteLine("StorageGetDevicesInHouseType failed.");
                Assert.Fail();
            }

            //TEST Getting CORRECT DATA 
            request = WebRequest.Create(uri1 + "/api/storage/device/?{houseid=" + 567 + "/&{type=Light}");
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
            request = WebRequest.Create(uri1 + "api/storage/device/?{houseid=" + 991 + "/&{type=IDontKnow}");
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

            //Test Deleting invalid Data		
            WebRequest request = WebRequest.Create(URI + "/api/storage/device/?{houseid=" + 111 + "/&{roomid=" + 222 + "/&{deviceid=" + 5 + "}"); request.ContentType = "application/json";
            request.Method = "DELETE";

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
                Console.WriteLine("TestStorageDeviceDelete failed.");
                Assert.Fail();
            }

            //Now Actually Post a Device
            request = WebRequest.Create(URI + "/api/storage/device");
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
                Console.WriteLine("TestStorageDeviceDelete failed.");
                Assert.Fail();
            }
            //Now Try to Delete the Device Given Correct Data
            request = WebRequest.Create(URI + "/api/storage/device/?{houseid=" + 567 + "/&{roomid=" + 890 + "/&{deviceid=" + 5 + "}"); request.ContentType = "application/json";
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
                Console.WriteLine("TestStorageDeviceDelete failed.");
                Assert.Fail();
            }
        }

        /////////////ENDS TEST FROM STORAGE API///////////////////////////////////////

        /////////////////////BEGINS TEST FOR SPACE//////////////////////////////
        //POST api/storage/space	
        //Posts the space with the JSON object data information provided.
        //Also Test GETS Room
        [TestMethod]
        public void TestPostSpace()
        {

            WebRequest request = WebRequest.Create(URI + "/api/storage/space");
            request.ContentType = "application/json";
            request.Method = "POST";

            JObject jobjects = new JObject();
            jobjects["houseid"] = "1";
            jobjects["roomid"] = "1234";
            jobjects["type"] = "Light";
            jobjects["name"] = "BedroomLight";
            jobjects["x"] = "100";
            jobjects["y"] = "300";

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
                Console.WriteLine("TestStoragePostSpace failed, Didnt Post.");
                Assert.Fail();
            }

            //Now Try to Get That POST (Getting House and Room/Space ID)
            request = WebRequest.Create(URI + "/api/storage/space/?{houseid=" + 1 + "/&{roomid=" + 1234 + "}");
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
                Console.WriteLine("TestStoragePostSpace failed, Didnt Get the Data.");
                Assert.Fail();
            }

            //NOW Try to Get Incorrect Data From Post

            request = WebRequest.Create(URI + "/api/storage/space/?{houseid=" + 55 + "/&{roomid=" + 65 + "}");
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
                    Assert.Equals(str, "false");
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
        public void TestDeleteRoom()
        {
            //FIRST TRY DELETING A ROOM BEFORE POSTING ONE
            WebRequest request = WebRequest.Create(URI + "/api/storage/space/?{houseid=" + 5 + "/&{roomid=" + 6532 + "}");
            request.ContentType = "application/json";
            request.Method = "DELETE";

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
                Console.WriteLine("TestDeleteRoom failed. ");
                Assert.Fail();
            }
            //POST A ROOM TO BE DELETED 
            request = WebRequest.Create(URI + "/api/storage/space");
            request.ContentType = "application/json";
            request.Method = "POST";

            JObject jobjects = new JObject();
            jobjects["houseid"] = "1";
            jobjects["roomid"] = "1234";
            jobjects["type"] = "Light";
            jobjects["name"] = "BedroomLight";
            jobjects["x"] = "100";
            jobjects["y"] = "300";

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
                Console.WriteLine("TestStoragePostSpace failed, Didnt Post.");
                Assert.Fail();
            }

            //DELETE THE Room

            request = WebRequest.Create(URI + "/api/storage/space/?{houseid=" + 1 + "/&{roomid=" + 1234 + "}");
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
    }

}
