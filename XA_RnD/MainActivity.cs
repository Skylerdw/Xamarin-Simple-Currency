using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System;

namespace XA_RnD
{
    [Activity(Label = "XA_RnD", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private List<Currency> currencies;
        private ListView mainView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            mainView = FindViewById<ListView>(Resource.Id.mainListView);
            currencies = CurrencyLoader.LoadCurrencies();
            currencies.Sort();

            var adapter = new CurrencyExchangeViewAdapter(this, currencies, 0);

            mainView.Adapter = adapter;
            mainView.ItemLongClick += currencies_ItemLongClick;
            // SetContentView (Resource.Layout.Main);
        }

        private void currencies_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            CurrencyExchangeViewAdapter adapter = (CurrencyExchangeViewAdapter) ((ListView)sender).Adapter;
            adapter.CurrencySelected = currencies[e.Position];
            adapter.NotifyDataSetChanged();
            Console.WriteLine(adapter.CurrencySelected.Name);
        }
    }
}

