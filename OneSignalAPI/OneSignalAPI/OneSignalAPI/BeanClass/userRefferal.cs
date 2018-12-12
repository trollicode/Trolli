using System;
using System.Collections.Generic;
using System.Text;

namespace OneSignalAPI.BeanClass
{
    public class userReferral
    {
        public string userId { get; set; }
        public string referralCode { get; set; }
        public string referredByCode { get; set; }
        public int rewardBalance { get; set; }
        public bool availReward { get; set; }
    }
}
