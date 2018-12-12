using System;
using System.Collections.Generic;
using System.Text;

namespace RealTrolli.ValidationClass
{
    class RegistrationValidation
    {

        public static bool validation = true;
        public static void IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                if (addr.Address == email)
                {
                    validation = false;
                }
                else
                {
                    validation = true;
                }
            }
            catch
            {
                validation = true;
            }
        }
    }
}
