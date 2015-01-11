using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace JControlConsole
{
    /// <summary>
    /// This is the handler that is passes info from the thread to the main UI and back
    /// It is also where you read the content to tell the server what to do to the ps3
    /// </summary>
    public interface OnRequest
    {
        /// <summary>
        /// Based on the organized request you need to use switch & case to tell the app what to do to the ps3, i left you with a sample
        /// </summary>
        /// <param name="organized">The Android Request</param>
        /// <returns>Response Template</returns>
        ResponseTemplate sendResponse(OrganizedRequest organized);
    }
}
