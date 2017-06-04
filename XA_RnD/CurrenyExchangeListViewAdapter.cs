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

namespace XA_RnD
{
    class CurrencyExchangeViewAdapter : BaseAdapter<Currency>
    {
        private List<Currency> _items;
        private Context _context;

        public CurrencyExchangeViewAdapter(Context context, List<Currency> items, int currencySelectedId)
        {
            _items = items;
            _context = context;
            CurrencySelected = items[currencySelectedId];
        }

        public Currency CurrencySelected { get; set; }

        public override int Count
        {
            get { return _items.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Currency this[int position]
        {
            get { return _items[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if(row == null)
            {
                row = LayoutInflater.From(this._context).Inflate(Resource.Layout.ListView_row, root: null, attachToRoot: false);
            }

            Currency rowCurrency = _items[position];

            TextView currencyCodeTextView = row.FindViewById<TextView>(Resource.Id.currencyCode);
            currencyCodeTextView.Text = rowCurrency.Code;

            TextView currencyNameTextView = row.FindViewById<TextView>(Resource.Id.currencyName);
            currencyNameTextView.Text = rowCurrency.Name;

            TextView currencySellTextView = row.FindViewById<TextView>(Resource.Id.youPay);
            currencySellTextView.Text = CurrencySelected.GetValueInCurrency(rowCurrency).ToString();

            TextView currencyBuyTextView = row.FindViewById<TextView>(Resource.Id.youGet);
            currencyBuyTextView.Text = rowCurrency.GetValueInCurrency(CurrencySelected).ToString();

            return row;
        }
    }
}