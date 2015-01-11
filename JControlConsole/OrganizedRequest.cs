using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JControlConsole
{
    /// <summary>
    /// This OrganizedRequest Class will parse the contents of the json and put it neatly in here :) 
    /// </summary>
    public class OrganizedRequest
    {
        private string Player;
        private string Request;
        private string[] Params;
        public OrganizedRequest(string Player, string Request, string[] Params)
        {
            this.Player = Player;
            this.Request = Request;
            this.Params = Params;
        }
        /// <summary>
        /// I know this was stupid of me to call this player, follow up on the source code and you'll see what im talking about
        /// </summary>
        /// <returns>int</returns>
        public string getPlayer()
        {
            return Player;
        }
        /// <summary>
        /// Gets the param at a specific index
        /// </summary>
        /// <param name="index">gets param at a certain index</param>
        /// <returns>string</returns>
        public string getParam(int index)
        {
            return Params[index];
        }
        /// <summary>
        /// Gets request, ex: Connect, Attach, BlowUp Player
        /// </summary>
        /// <returns>string</returns>
        public string getRequest()
        {
            return Request;
        }
        /// <summary>
        /// Gets all the params
        /// </summary>
        /// <returns>string[]</returns>
        public string[] getParams()
        {
            return Params;
        }
    }
}
