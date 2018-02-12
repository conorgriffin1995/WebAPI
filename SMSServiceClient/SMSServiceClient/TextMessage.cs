using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSService.Models
{
    class TextMessage
    {
        public string PhoneNumberSender
        {
            get;
            set;
        }
        public string PhoneNumberReceiver
        {
            get;
            set;
        }
        public string Content
        {
            get;
            set;
        }
    }
}
