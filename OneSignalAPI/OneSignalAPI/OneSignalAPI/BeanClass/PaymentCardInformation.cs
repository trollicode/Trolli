using System;
using System.Collections.Generic;
using System.Text;

namespace OneSignalAPI.BeanClass
{
   public class PaymentCardInformation
    {
        public string cardType { get; set; }
        public string cardHolderName { set; get; }
        public string cardNumber { set; get; }
        public string experationDate { set; get; }
        public string cvvNumber { get; set; }

    }
}
