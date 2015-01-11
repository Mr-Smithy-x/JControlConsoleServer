using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JControlConsole;

namespace JControlConsoleSample
{
    public partial class Form1 : Form, OnRequest
    {
        public Form1()
        {
            InitializeComponent();
        }

        JControlConsoleServer jccs;
        private void button1_Click(object sender, EventArgs e)
        {
            jccs = new JControlConsoleServer(this, 1337);
            this.Text = "Accepting Connections At: " + jccs.getIP() + ":" + jccs.getPort().ToString();
            jccs.Initialize();

        }

        public ResponseTemplate sendResponse(OrganizedRequest organized)
        {
            string message = "";
            string response = "";
            switch (organized.getRequest())
            {
                case "console":
                    switch (organized.getPlayer())
                    {

                        case "Connect":
                            message = GlobalAPI.Connect(organized.getParam(0)).getReturnStr();
                            response = "Connected";
                            return new ResponseTemplate(response, message, null);
                        case "Attach":
                            message = GlobalAPI.Attach().getReturnStr();
                            response = "Attached";
                            return new ResponseTemplate(response, message, null);
                        case "ShutDown":
                            message = GlobalAPI.sendBoot(PS3Lib.CCAPI.RebootFlags.ShutDown).getReturnStr();
                            response = "Turned Off";
                            return new ResponseTemplate(response, message, null);
                        case "SoftReboot":
                            message = GlobalAPI.sendBoot(PS3Lib.CCAPI.RebootFlags.SoftReboot).getReturnStr();
                            response = "Soft Rebooted";
                            return new ResponseTemplate(response, message, null);
                        case "HardBoot":
                            message = GlobalAPI.sendBoot(PS3Lib.CCAPI.RebootFlags.HardReboot).getReturnStr();
                            response = "Hard Rebooted";
                            return new ResponseTemplate(response, message, null);
                        case "GetConsoles":
                            message = "Consoles Loaded";
                            response = "Consoles";
                            return new ResponseTemplate(response, message, GlobalAPI.getConsoles());
                        case "Notify":
                            message = GlobalAPI.Notify(organized.getParam(0)).getReturnStr();
                            response = "Notified";//
                            return new ResponseTemplate(response, message, null);
                        case "Buzzer":
                            message = GlobalAPI.RingBuzzer().getReturnStr();
                            response = "Buzzed";
                            return new ResponseTemplate(response, message, null);

                    }
                    break;
            }
            return null;

        }
       
    }
}
