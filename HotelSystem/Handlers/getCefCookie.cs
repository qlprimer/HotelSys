using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSystem.Handlers
{
  
        public class CookieVisitor : CefSharp.ICookieVisitor
        {
            public event Action<CefSharp.Cookie> SendCookie;
            public bool Visit(CefSharp.Cookie cookie, int count, int total, ref bool deleteCookie)
            {
                deleteCookie = false;
                if (SendCookie != null)
                {
                    SendCookie(cookie);
                }

                return true;
            }
        }
    

    class CookieMonster : ICookieVisitor
    {
        readonly List<Tuple<string, string>> cookies = new List<Tuple<string, string>>();
        readonly Action<IEnumerable<Tuple<string, string>>> useAllCookies;

        public CookieMonster(Action<IEnumerable<Tuple<string, string>>> useAllCookies)
        {
            this.useAllCookies = useAllCookies;
        }

        public bool Visit(Cookie cookie, int count, int total, ref bool deleteCookie)
        {
            cookies.Add(new Tuple<string, string>(cookie.Name, cookie.Value));

            if (count == total - 1)
                useAllCookies(cookies);

            return true;
        }
    }
}
