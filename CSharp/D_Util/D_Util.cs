using System.Text.RegularExpressions;

namespace D_Util
{
    namespace D_InterNet
    {
        //소켓 에러코드
        public enum SOCKET_ERROR_CODE
        {
            NOT_FOUND_SERVER = 10061,
            DISCONNECTED = 10054,
            CLOSE_INTERRUPT = 10004,
        }
    }


    static class ValidData
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
