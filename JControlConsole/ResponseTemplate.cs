using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JControlConsole
{
    /// <summary>
    /// Use this template to serialize the json
    /// </summary>
    public class ResponseTemplate
    {
        public object res;
        public object mes;
        public object pair;
        public ResponseTemplate(object res, object mes, object pair)
        {
            this.res = res;
            this.mes = mes;
            this.pair = pair;
        }
    }

}
