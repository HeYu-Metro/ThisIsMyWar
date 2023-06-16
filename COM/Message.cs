using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThisIsMyWar.COM
{
    //消息类
    public class Message
    {

        public string Code()
        {
            string verificationCode = GenerateVerificationCode();
            return verificationCode;
        }

        static string GenerateVerificationCode()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string verificationCode = "";

            for (int i = 0; i < 5; i++)
            {
                int index = random.Next(chars.Length);
                verificationCode += chars[index];
            }

            return verificationCode;
        }
    }
}
