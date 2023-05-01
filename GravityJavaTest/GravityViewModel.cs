using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GravityJavaTest
{
    public class GravityViewModel
    {

        public async Task<TransactionResponseRoot> ProcessSalesCheckoutAsync()
        {
            var response = new object();

            string transactionToken = GravityModel.processTransactionToken;

            AccountDataRoot accountDataRoot = new AccountDataRoot();
            AccountData accountData = new AccountData();

            accountDataRoot.accountData = accountData;

            accountData.amount = "1.00";
            accountData.externalTransactionId = Guid.NewGuid().ToString();
            // Optional
            accountData.billingAddress = "123 Main St";
            accountData.billingName = "John Smith";
            accountData.billingPostalCode = "90210";
            accountData.cashierId = "My Cashier";
            accountData.tipAmount = "0.25";
            accountData.transactionReference="inv#2344";

            TransactionResponseRoot transactionResponseRoot = new TransactionResponseRoot();

            try
            {
                using (var client = new HttpClient())
                {
                    // Start Transaction
                    string transactionJson = JsonConvert.SerializeObject(accountData);

                    string url = $"{GravityModel.endpointURL}/orgs/{GravityModel.OID}/transactions/checkout/{GravityModel.processTransactionToken}";
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);

                    request.Headers.Add("Authorization", $"Bearer {GravityModel.startTransactionToken}");
                    request.Content = new StringContent(transactionJson, Encoding.UTF8, "application/json");

                    var httpResponse = await client.SendAsync(request).ConfigureAwait(false);
                    var data = await httpResponse.Content.ReadAsStringAsync();

                    transactionResponseRoot = JsonConvert.DeserializeObject<TransactionResponseRoot>(data);
                    
                    return transactionResponseRoot;
                }
            }
            catch (Exception exc)
            {
                return transactionResponseRoot;
            }
        }

        public async Task<StartTransactionResponse> StartTransactionCheckoutAsync()
        {
            var response = new object();

            TransactionDataRoot transactionDataRoot = new TransactionDataRoot();
            TransactionData transactionData = new TransactionData();

            transactionDataRoot.transactionData = transactionData;

            transactionData.transactionType = "CreditSale";
            transactionData.method = "hostedFields";
            transactionData.submissionType = "manual";

            StartTransactionResponse startTransactionResponse = new StartTransactionResponse();

            try
            {
                using (var client = new HttpClient())
                {
                    // Start Transaction
                    string transactionJson = JsonConvert.SerializeObject(transactionDataRoot);

                    string url = $"{GravityModel.endpointURL}/orgs/{GravityModel.OID}/transactions/start";
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);

                    request.Headers.Add("Authorization", $"Bearer {GravityModel.startTransactionToken}");
                    //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(GravityModel.startTransactionToken);
                    request.Content = new StringContent(transactionJson, Encoding.UTF8, "application/json");

                    var httpResponse = await client.SendAsync(request).ConfigureAwait(false);
                    var data = await httpResponse.Content.ReadAsStringAsync();

                    startTransactionResponse = JsonConvert.DeserializeObject<StartTransactionResponse>(data);

                    GravityModel.processTransactionToken = startTransactionResponse.transactionToken;

                    return startTransactionResponse;
                }
            }
            catch (Exception exc)
            {
                GravityModel.processTransactionToken = "error: " + exc.Message;

                return startTransactionResponse;
            }
        }
    }
}
