﻿using System;
using System.Web;
using System.Collections;
using System.Collections.Specialized;
using System.Web.Security;

namespace AppLogic
{
    public class CookiesHelper
    {
        private HttpContext ctx = HttpContext.Current;
        private string _cookieName = null;
        private HybridDictionary _data;

        public HybridDictionary CookieData
        {
            get
            {
                if (CookieData == null)
                {
                    this.GetCookie();
                }
                return _data;
            }
            set
            {
                _data = value;
            }
        }

        public void SetValue(string Key, string Value)
        {
            if (_data == null)
                _data = new HybridDictionary();
            _data.Add(Key, Value);
        }

        public string GetValue(string Key)
        {
            string retValue = string.Empty;

            if (_data != null)
            {
                retValue = _data[Key].ToString();
            }
            return retValue;
        }

        private CookiesHelper()
        {
        }

        public CookiesHelper(string cookieName)
        {
            _cookieName = cookieName;
        }

        public void Save()
        {
            // Setting a cookie's value and/or subvalue using the HttpCookie class
            HttpCookie cookie;

            if (ctx.Request.Cookies[_cookieName] != null)
                ctx.Request.Cookies.Remove(_cookieName);
            cookie = new HttpCookie(_cookieName);
            if (_data.Count > 0)
            {
                IEnumerator cookieData = _data.GetEnumerator();
                DictionaryEntry item;
                while (cookieData.MoveNext())
                {
                    item = (DictionaryEntry)cookieData.Current;
                    cookie.Values.Add(item.Key.ToString(), item.Value.ToString());
                }
            }
            ctx.Response.AppendCookie(cookie);
        }

        public void GetCookie()
        {
            // Retrieving a cookie's value(s)
            if (ctx.Request.Cookies[_cookieName] != null)
            {
                NameValueCollection values = ctx.Request.Cookies[_cookieName].Values;
                if (values.Count > 0)
                {
                    _data = new HybridDictionary(values.Count);
                    foreach (string key in values.Keys)
                    {
                        _data.Add(key, values[key]);
                    }
                }
            }
        }

        public int GetCookieCount()
        {
            int iCount = 0;
            if (ctx.Request.Cookies[_cookieName] != null)
            {
                
                NameValueCollection values = ctx.Request.Cookies[_cookieName].Values;
                iCount = values.Count;

            }
            return iCount;

        }

        public void Delete()
        {
            // Set the value of the cookie to null and
            // set its expiration to some time in the past
            if (ctx.Response.Cookies[_cookieName] != null)
            {
                ctx.Response.Cookies[_cookieName].Value = null;
                ctx.Response.Cookies[_cookieName].Expires =
                 System.DateTime.Now.AddMonths(-1); // last month
            }
        }
    }
}