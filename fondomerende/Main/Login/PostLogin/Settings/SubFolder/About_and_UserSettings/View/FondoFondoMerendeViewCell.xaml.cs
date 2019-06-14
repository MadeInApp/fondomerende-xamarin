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
        }

        private async void GetFunds()
        {
            var result = await userFundsService.GetFundsFundAsync();
            if (result.response.success)
            {
                FundFund.Text = Convert.ToString(result.data.fund_funds_amount);
                if(result.data.fund_funds_amount <= 0)
                {
                    FundFund.TextColor = Color.Red;
                }
            }
            else
            {
                FundFund.Text = "Errore";
            }
        }
    }
}
