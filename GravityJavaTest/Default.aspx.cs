using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GravityJavaTest
{
    public partial class Default : System.Web.UI.Page
    {
        public string transactionToken;
        public string assetsCheckoutURL;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // Firsts load all of the setting for location
                GravityModel.GetFormattedRequest("IBPortland");

                // Start transaction - Get transaction token to process sales
                GravityViewModel gravityViewModel = new GravityViewModel();

                Task<StartTransactionResponse> startTransactionResponse = gravityViewModel.StartTransactionCheckoutAsync();
                startTransactionResponse.Wait();

                // Process sale - Checkout version
                //Task<TransactionResponseRoot> processSalesCheckoutResponse = gravityViewModel.ProcessSalesCheckoutAsync();
                //processSalesCheckoutResponse.Wait();
            }

            assetsCheckoutURL = GravityModel.assetsCheckoutURL;
            transactionToken = GravityModel.processTransactionToken;
        }
    }
}