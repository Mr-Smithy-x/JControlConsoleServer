using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JControlConsole
{
    /// <summary>
    /// returns 2 types, maybe useful for true and false and a message to display
    /// </summary>
    public class SpecialReturn
    {
        int returnInt;
        string returnStr;
        public int getReturnedInt(){
            return returnInt;
        }
        public string getReturnStr(){
            return returnStr;
        }
        public SpecialReturn(int returnInt)
        {
            this.returnInt = returnInt;
        }
        public SpecialReturn(string returnStr)
        {
            this.returnStr = returnStr;
        }
        public SpecialReturn(int retInt, string retStr)
        {
            this.returnInt = retInt;
            this.returnStr = retStr;
        }
    }
}
