using System;
using System.Collections.Generic;

namespace CryptoCurrency
{
    public class Converter
    {
        private readonly IDictionary<string, double> currencies = new Dictionary<string, double>();
        /// <summary>
        /// Angiver prisen for en enhed af en kryptovaluta. Prisen angives i dollars.
        /// Hvis der tidligere er angivet en værdi for samme kryptovaluta, 
        /// bliver den gamle værdi overskrevet af den nye værdi
        /// </summary>
        /// <param name="currencyName">Navnet på den kryptovaluta der angives</param>
        /// <param name="price">Prisen på en enhed af valutaen målt i dollars. Prisen kan ikke være negativ</param>
        public void SetPricePerUnit(String currencyName, double price)
        {
            if (currencies.ContainsKey(currencyName))
                currencies.Remove(currencyName);
            currencies.Add(currencyName, price);
        }

        /// <summary>
        /// Konverterer fra en kryptovaluta til en anden. 
        /// Hvis en af de angivne valutaer ikke findes, kaster funktionen en ArgumentException
        /// 
        /// </summary>
        /// <param name="fromCurrencyName">Navnet på den valuta, der konverterers fra</param>
        /// <param name="toCurrencyName">Navnet på den valuta, der konverteres til</param>
        /// <param name="amount">Beløbet angivet i valutaen angivet i fromCurrencyName</param>
        /// <returns>Værdien af beløbet i toCurrencyName</returns>
        public double Convert(String fromCurrencyName, String toCurrencyName, double amount)
        {
            if (!currencies.ContainsKey(fromCurrencyName))
                throw new Exception("From Currency not found");
            if (!currencies.ContainsKey(toCurrencyName))
                throw new Exception("To Currency not found");
            var fromCurrencyPrice = currencies[fromCurrencyName];
            var toCurrencyPrice = currencies[toCurrencyName];
            var priceInFromCurrency = fromCurrencyPrice * amount;
            var amountOfToCurrency = priceInFromCurrency / toCurrencyPrice;
            return amountOfToCurrency;
        }
    }
}
