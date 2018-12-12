using System;
using System.Collections.Generic;
using System.Text;

namespace RealTrolli.BeanClass
{
    class SignupBean
    {
        public string name { set; get; }
        public string email { set; get; }
        public string subrub { set; get; }
        public string states { set; get; }
        public string postCodes { set; get; }
        public string country { set; get; }
        public string userType { set; get; }
        public string latitude { set; get; }
        public string longitude { set; get; }
        public string phoneNumber { set; get; }
        public string deviceId { get; set; }
        public string UUID { get; set; } 
        public string folderId { get; set; }
        public string imageId { get; set; }
        public string rewardCard1 { get; set; }
        public string rewardCard2 { get; set; }
        public string oneSignalId { get; set; }
        public string stripeCustomerId { get; set; }
    }
}
