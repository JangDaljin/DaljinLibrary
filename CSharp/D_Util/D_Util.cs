using System.Text.RegularExpressions;

namespace D_Util
{
    public class D_InterNet
    {
        //IPv4 유효성 체크
        public static bool IsIP(string IP)
        {
            Regex rex = new Regex(@"^(([0-9]|[1-9][0-9]|1([0-9]{2})|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1([0-9]{2})|2[0-4][0-9]|25[0-5])$");
            bool res = false;
            if (rex.IsMatch(IP))
            {
                res = true;
            }
            return res;
        }

        //포트 유효성 체크
        public static bool IsPORT(int PORT)
        {
            bool res = false;
            if(PORT > 0 && PORT < 65536)
            {
                res = true;
            }
            return res;
        }

        //포트 유효성 체크
        public static bool IsPORT(string PORT)
        {
            try
            {
                return IsPORT(int.Parse(PORT));
            }
            catch {
                return false;
            }
        }
    }
    
}
