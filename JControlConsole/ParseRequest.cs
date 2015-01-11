using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace JControlConsole
{
    public class ParseRequest
    {
        /// <summary>
        /// Makes the json request after filling the parameters
        /// </summary>
        /// <param name="res">Stands for response, The Android app will do based on the response</param>
        /// <param name="mes">Stands for message, The Android app will pop a message up showing this message</param>
        /// <param name="pair">Stands for pair, If this is filled, the android app will put this in a json array for the android to parse</param>
        /// <returns>This Returns the json String</returns>
        public static string MakeJsonWithPair(string res, string mes,List<KeyValuePair<string, string>> pair)
        {
            ResponseTemplate c = new ResponseTemplate(res, mes, pair);
            return JsonConvert.SerializeObject(c, Formatting.Indented);
        }
        /// <summary>
        /// Makes the json request using the template.
        /// </summary>
        /// <param name="resTemp"></param>
        /// <returns>Returns the Json String of this response template</returns>
        public static string MakeJson(ResponseTemplate resTemp)
        {
            return JsonConvert.SerializeObject(resTemp, Formatting.Indented);
        }

        /// <summary>
        /// This parses the json that the android sent to the server, this will parse it in contents and send the parsed content to the main UI, for TODO to take place
        /// </summary>
        /// <param name="Request">The Android Request Json</param>
        /// <param name="sock">The Socket passed from the JControlSample</param>
        /// <param name="gets">This will be pointed to the main ui through the JControlControl</param>
        /// <returns></returns>
        public static ResponseTemplate getResponse(string Request, Socket sock, OnRequest gets)
        {
            if (Request == null || sock == null || gets == null) return null;
            try
            {
                JObject j = (JObject)JsonConvert.DeserializeObject(Request);
                string request = j["request"].ToString();
                string player = j["data"]["player"].ToString();
                string p1 = j["params"]["p1"].ToString();
                string p2 = j["params"]["p2"].ToString();
                string p3 = j["params"]["p3"].ToString();
                string p4 = j["params"]["p4"].ToString();
                string p5 = j["params"]["p5"].ToString();
                string ip = sock.RemoteEndPoint.ToString();
                OrganizedRequest Organized = new OrganizedRequest(request, player, new string[] { p1, p2, p3, p4, p5 });
                Console.WriteLine("---------------\nRequest: {0}\nPlayer: {1}\nParam 1: {2}\nParam 2: {3} \nParam 3: {4}\nParam 4: {5}\nParam 5: {6}\n From: {7}", Organized.getRequest(), Organized.getPlayer(),Organized.getParam(0),Organized.getParam(1),Organized.getParam(2), Organized.getParam(3), Organized.getParam(4), ip + "\n----------------\n");
                return gets.sendResponse(Organized);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

    }
}
