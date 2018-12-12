using System;
using System.Collections.Generic;
using System.Text;

namespace OneSignalAPI.BeanClass
{
    class UserRating
    {
        public string trolliId { get; set; }
        public string userId { get; set; }
        public string ratingByClient { get; set; }
        public string reviewByClient { get; set; }
        public string ratingBySmartBuyer { get; set; }
        public string reviewBySmartBuyer { get; set; }
    }
}
