using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _6gyak
{
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            var mnbService = new MNBServiceReference.MNBArfolyamServiceSoapClient();

            var request = new MNBServiceReference.GetExchangeRatesRequestBody()
            {
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30"
            };

            var response = mnbService.GetExchangeRates(request);

            var result = response.GetExchangeRatesResult;


            InitializeComponent();
        }

        
    }
}
