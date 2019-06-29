using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopSi.Common
{
    [Serializable]
    public class CommonLogin
    {
        public long ID { get; set; }
        public string UserName { get; set; }
    }
}