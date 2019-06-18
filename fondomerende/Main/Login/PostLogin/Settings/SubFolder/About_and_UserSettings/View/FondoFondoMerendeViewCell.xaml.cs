using fondomerende.Main.Services.RESTServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.About_and_UserSettings.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FondoFondoMerendeViewCell : ViewCell
    {
        UserFundsServiceManager userFundsService = new UserFundsServiceManager();
        public FondoFondoMerendeViewCell()
        {
            InitializeComponent();
            GetFunds();
        }

        private async void GetFunds()
        {
            var result = await userFundsService.GetFundsFundAsync();
            if (result != null)
            {
                if (result.response.success)
                {
                    FundFund.Text = "il Fondo merende ha €" + Convert.ToString(result.data.fund_funds_amount) + "a disposizione";
                    if (result.data.fund_funds_amount <= 0)
                    {
                        var fs = new FormattedString();
                        fs.Spans.Add(new Span { Text = "il Fondo merende ha €", TextColor = Color.Black });
                        fs.Spans.Add(new Span { Text = Convert.ToString(result.data.fund_funds_amount), TextColor = Color.Red });
                        fs.Spans.Add(new Span { Text = " a disposizione", TextColor = Color.Black });
                        FundFund.FormattedText = fs;
                    }
                }
                else
                {
                    FundFund.Text = "Errore";
                }
            }
        }
    }
}
