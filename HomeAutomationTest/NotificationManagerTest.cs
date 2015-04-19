using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    public class NotificationManagerTest
    {
        [TestMethod]
        public void TestCreatePlatformApplication()
        {
            // of the array is one line of the file. 
            string[] lines = System.IO.File.ReadAllLines(@"C:\keys.txt");
            string accK = lines[0];
            string secK = lines[1];

            NotificationManager nm = new NotificationManager(accK, secK, Amazon.RegionEndpoint.USEast1);

            nm.init();
            string topicArn1 = nm.createPlatformApplicationAndAttachToTopic("exampletoken", "username");
            string topicArn2 = nm.createPlatformApplicationAndAttachToTopic("exampletoken1", "username1");

            AmazonSimpleNotificationServiceClient snsClient = new AmazonSimpleNotificationServiceClient(accK, secK, Amazon.RegionEndpoint.USEast1);

            var topic = snsClient.FindTopic(nm.getTopicName("username"));
            Assert.AreEqual(topic.TopicArn, topicArn1);

            var topic1 = snsClient.FindTopic(nm.getTopicName("username1"));
            Assert.AreEqual(topic1.TopicArn, topicArn2);

            string topicArn3 = nm.createPlatformApplicationAndAttachToTopic("exampletoken2", "username");
            string topicArn4 = nm.createPlatformApplicationAndAttachToTopic("exampletoken3", "username1");

            Assert.AreEqual(topicArn1, topicArn3);
            Assert.AreEqual(topicArn2, topicArn4);

            try
            {
                topicArn1 = nm.createPlatformApplicationAndAttachToTopic("exampletoken", "username");
                Assert.Fail("Created a platform for same username and device token");
            }

            catch (Exception e)
            {
                Assert.IsTrue(e != null);
            }

            var appsResponse = snsClient.ListPlatformApplications();

            foreach (var app in appsResponse.PlatformApplications)
            {

                var appAttrsRequest = new GetPlatformApplicationAttributesRequest
                {
                    PlatformApplicationArn = app.PlatformApplicationArn
                };

                var appAttrsResponse = snsClient.GetPlatformApplicationAttributes(appAttrsRequest);
                System.Diagnostics.Trace.WriteLine(app.PlatformApplicationArn);

            }
        }
    }
}
