using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace XA_RnD
{
    static class CurrencyLoader
    {
        public static List<Currency> LoadCurrencies()
        {
            JObject jsonResult;
            string url = @"http://api.fixer.io/latest?base=USD";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Get;
            request.ContentType = "application/json; charset=utf-8";
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                string jsonResultString = string.Empty;
                jsonResultString = reader.ReadToEnd();
                jsonResult = JObject.Parse(jsonResultString);
            }

            var currencies = new List<Currency>();
            foreach(JProperty currencyRate in jsonResult["rates"])
            {
                currencies.Add(new Currency() { Code = currencyRate.Name, USDExchangeRate = currencyRate.Value.ToObject<decimal>() });
            }
            return currencies;
        }
    }
}