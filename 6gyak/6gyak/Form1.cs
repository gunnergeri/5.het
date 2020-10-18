using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using _6gyak.Entities;

namespace _6gyak
{
    public partial class Form1 : Form
    {
        BindingList<Ratedata> rates = new BindingList<Ratedata>();

        private string result;

        public Form1()
        {

            InitializeComponent();

            Harmadik();
            Negyedik();

        }

        private void Harmadik()
        {
           

            var mnbService = new MNBServiceReference.MNBArfolyamServiceSoapClient();

            var request = new MNBServiceReference.GetExchangeRatesRequestBody()
            {
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30"
            };
            var response = mnbService.GetExchangeRates(request);

            result = response.GetExchangeRatesResult;

            dataGridView1.DataSource = rates;
        }

        private void Negyedik()
        {
            var xml = new XmlDocument();
            xml.LoadXml(result);

            // Végigmegünk a dokumentum fő elemének gyermekein
            foreach (XmlElement element in xml.DocumentElement)
            {
                // Létrehozzuk az adatsort és rögtön hozzáadjuk a listához
                // Mivel ez egy referencia típusú változó, megtehetjük, hogy előbb adjuk a listához és csak később töltjük fel a tulajdonságait
                var rate = new Ratedata();
                rates.Add(rate);

                // Dátum
                rate.Date = DateTime.Parse(element.GetAttribute("date"));

                // Valuta
                var childElement = (XmlElement)element.ChildNodes[0];
                rate.Currency = childElement.GetAttribute("curr");

                // Érték
                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0)
                    rate.Value = value / unit;
            }
        }



}
}
