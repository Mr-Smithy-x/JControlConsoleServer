using PS3Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JControlConsole
{
    /// <summary>
    /// I left you with a little preset, to connect to the console and attach shutdown
    /// </summary>
    public class GlobalAPI 
    {
        public static PS3API ps3Api = new PS3API(SelectAPI.ControlConsole);

        public static List<KeyValuePair<string, string>> getConsoles()
        {
            List<KeyValuePair<string, string>> Consoles = new List<KeyValuePair<string, string>>();
            List<CCAPI.ConsoleInfo> consoles = ps3Api.CCAPI.GetConsoleList();
            foreach (CCAPI.ConsoleInfo c in consoles)
            {
                Consoles.Add(new KeyValuePair<string, string>(c.Name, c.Ip));
            }
            return Consoles;
        }

        public static SpecialReturn Notify(string message, CCAPI.NotifyIcon icon = CCAPI.NotifyIcon.CAUTION){
            ps3Api.CCAPI.Notify(icon,message);
            return new SpecialReturn(1,"sent notification");
        }

        public static SpecialReturn RingBuzzer(CCAPI.BuzzerMode buzzer = CCAPI.BuzzerMode.Single)
        {
            ps3Api.CCAPI.RingBuzzer(buzzer);
            return new SpecialReturn(1, "Ringed Buzzer");
        }
        public static SpecialReturn Connect(string ip = null)
        {
            
            if (ip != null)
            {
                if (ps3Api.ConnectTarget(ip) == true)
                {

                    return new SpecialReturn("Connected!");
                }
                else
                {
                    return new SpecialReturn("Not Connected!");
                }
            }
            else
            {
                if (ps3Api.ConnectTarget() == true)
                {

                    return new SpecialReturn("Connected!");
                }
                else
                {
                    return new SpecialReturn("Not Connected!");
                }
            }
            
           
        }
        public static SpecialReturn Attach()
        {

            if (ps3Api.AttachProcess() == true)
            {
                return new SpecialReturn(1,"Attached!");
            }
            else
            {
                return new SpecialReturn(1,"Could not attach?");
            }


        }

        public static SpecialReturn Disconnect()
        {
            ps3Api.DisconnectTarget();
            return new SpecialReturn(1,"Disconnected");
        }
        
        public static SpecialReturn sendBoot(CCAPI.RebootFlags boot)
        {

            if (boot == CCAPI.RebootFlags.SoftReboot)
            {
                ps3Api.CCAPI.ShutDown(CCAPI.RebootFlags.SoftReboot);
                return new SpecialReturn(1,"SoftBoot Initiated!");
            }
            else if (boot == CCAPI.RebootFlags.HardReboot)
            {

                ps3Api.CCAPI.ShutDown(CCAPI.RebootFlags.HardReboot);
                return new SpecialReturn(1,"HardBoot Initiated!");
            }
            else if (boot == CCAPI.RebootFlags.ShutDown)
            {

                ps3Api.CCAPI.ShutDown(CCAPI.RebootFlags.ShutDown);
                return new SpecialReturn(1,"Shutdown Initiated!");
            }
            else
            {
                return new SpecialReturn(0,"Could Not Understand");
            }

        }
    }
}
