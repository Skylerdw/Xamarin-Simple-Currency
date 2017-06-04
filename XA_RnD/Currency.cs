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
using System.Globalization;
using System.Collections;

namespace XA_RnD
{
    class Currency : IComparable
    {
        public decimal USDExchangeRate { get; set; }

        private string _code;
        public string Code
        {
            get
            {
                if(_code == null)
                {
                    throw new ArgumentNullException("Code property was not initialised properly.");
                }
                return _code;
            }
            set
            {
                _code = value;
            }
        }

        private CultureInfo _culture;
        public CultureInfo Culture
        {
            get
            {
                if (_culture == default(CultureInfo))
                {
                    string symbol = string.Empty;
                    CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
                    IList Result = new ArrayList();
                    foreach (CultureInfo ci in cultures)
                    {
                        RegionInfo ri = new RegionInfo(ci.LCID);
                        if (ri.ISOCurrencySymbol == Code)
                        {
                            _culture = ci;
                        }
                    }
                    if(_culture == null)
                    {
                        throw new ArgumentException($"Currency code {_code} is not a proper currency code.");
                    }
                }
                return _culture;
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                if(_name == null)
                {
                    RegionInfo region = new RegionInfo(Culture.LCID);
                    _name = region.CurrencyEnglishName;
                }
                return _name;
            }
        }

        public string GetValueInCurrency(Currency anotherCurrency)
        {
            var exchangeRate = USDExchangeRate / anotherCurrency.USDExchangeRate;
            return exchangeRate.ToString("C", Culture);
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Currency otherCurrency = obj as Currency;
            if (otherCurrency != null)
                return Code.CompareTo(otherCurrency.Code);
            else
                throw new ArgumentException("Can only compare with other Currency objects.");
        }
    }
}