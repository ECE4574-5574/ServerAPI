using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    class NotificationManager
    {
        private string mAccessKey = "";
        private string mSecretKey = "";
        private string API_KEY = "AIzaSyC0CNX3zlfOMcz7RBYYfaycp7wD_oipFZs";
        private RegionEndpoint mEndpoint;

        private AmazonSimpleNotificationServiceClient snsClient;

        public NotificationManager(string acck, string seck, RegionEndpoint endpoint)
        {
            mAccessKey = acck;
            mSecretKey = seck;
            mEndpoint = endpoint;
        }

        public bool init()
        {
            try
            {
                snsClient = new AmazonSimpleNotificationServiceClient(mAccessKey, mSecretKey, mEndpoint);
            }

            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public void PublishNotification(string topic, string message)
        {
            snsClient.Publish(topic, message);
        }

        public string createPlatformApplicationAndAttachToTopic(string deviceToken, string username)
        {
            if (deviceToken == null || username == null)
            {
                throw new ArgumentException("Passing null device token or username");
            }

            var topicRequest = new CreateTopicRequest
            {
                Name = username + "_myTopic"
            };

            var topicResponse = snsClient.CreateTopic(topicRequest);


            var createPlatformApplicationRequest = new CreatePlatformApplicationRequest
            {
                // Platform Credential is the SERVER API KEY FOR GCM
                Attributes = new Dictionary<string, string>() { { "PlatformCredential", API_KEY }, { "EventEndpointCreated", topicResponse.TopicArn } },
                Name = username + "_platform",
                Platform = "GCM"
            };

            var createPlatformResponse = snsClient.CreatePlatformApplication(createPlatformApplicationRequest);

            string platformApplicationArn = createPlatformResponse.PlatformApplicationArn;

            var request1 = new CreatePlatformEndpointRequest
            {
                CustomUserData = "",
                PlatformApplicationArn = platformApplicationArn,
                Token = deviceToken
            };

            // Getting the endpoint result
            // It contains Endpoint ARN that needs to be subscripted to a topic
            var createPlatformEndpointResult = snsClient.CreatePlatformEndpoint(request1);


            try
            {
                snsClient.Subscribe(new SubscribeRequest
                {
                    Protocol = "application",
                    TopicArn = topicResponse.TopicArn,
                    Endpoint = createPlatformEndpointResult.EndpointArn
                });
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                return null;
            }

            return topicResponse.TopicArn;
        }

        public string getTopicName(string username)
        {
            return username + "_myTopic";
        }

        public void removeNotification(string username)
        {
            //arn:aws:sns:us-west-2:336632281456:app/GCM/mobileapp
            //_platform
            var request = new DeletePlatformApplicationRequest
            {
                PlatformApplicationArn = "arn:aws:sns:us-east-1:80398EXAMPLE:" +
                  "app/GCM/TimeCardProcessingApplication"
            };

            snsClient.DeletePlatformApplication(request);
        }
    }
}
