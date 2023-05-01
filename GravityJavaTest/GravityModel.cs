using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GravityJavaTest
{
    public class GravityModel
    {
        public static string OID;
        public static string endpointURL;
        public static string assetsCheckoutURL;
        public static string startTransactionToken;
        public static string processTransactionToken;

        public static void GetFormattedRequest(string databaseName)
        {
            if (databaseName.ToUpper() == "IBSEA")
            {
                OID = "1175256991";
                endpointURL = "https://api.emergepay-sandbox.chargeitpro.com/virtualterminal/v1";
                assetsCheckoutURL = "https://assets.emergepay-sandbox.chargeitpro.com";
                startTransactionToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ0aWQiOjkzMywib2lkIjoxMTc1MjU2OTkxLCJ0b2tlbl91c2UiOiJvcnQiLCJybmQiOjE5OTMxMTM2NTAuOTE1NTE1LCJncm91cHMiOlsiT3JnQVBJVXNlcnMiXSwiaWF0IjoxNjgwODk2MDczfQ.wiscESppOHcEjDjkzByJF6qtgVLHIdGILTewnPo44pk";
                processTransactionToken = "(none)";
            }
            else
            {
                OID = "1175256991";
                endpointURL = "https://api.emergepay-sandbox.chargeitpro.com/virtualterminal/v1";
                assetsCheckoutURL = "https://assets.emergepay-sandbox.chargeitpro.com";
                startTransactionToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ0aWQiOjkzMywib2lkIjoxMTc1MjU2OTkxLCJ0b2tlbl91c2UiOiJvcnQiLCJybmQiOjE5OTMxMTM2NTAuOTE1NTE1LCJncm91cHMiOlsiT3JnQVBJVXNlcnMiXSwiaWF0IjoxNjgwODk2MDczfQ.wiscESppOHcEjDjkzByJF6qtgVLHIdGILTewnPo44pk";
                processTransactionToken = "(none)";
            }

            //using (StreamWriter writer = new StreamWriter("CCLog.txt", true))
            //{
            //    writer.WriteLine(databaseName + "," + UserName);
            //    writer.Close();
            //}
        }
    }


    // Start Transaction - Request
    public class TransactionDataRoot
    {
        public TransactionData transactionData { get; set; }
    }

    public class TransactionData
    {
        public string transactionType { get; set; }
        public string method { get; set; }
        public string submissionType { get; set; }
    }

    // Start Transaction - Response
    public class StartTransactionResponse
    {
        public string transactionToken { get; set; }
    }

    // Process Sale (Checkout) - Request
    public class AccountDataRoot
    {
        public AccountData accountData { get; set; }
    }

    public class AccountData
    {
        public string amount { get; set; }
        public string externalTransactionId { get; set; }
        public string billingAddress { get; set; }
        public string billingName { get; set; }
        public string billingPostalCode { get; set; }
        public string cashierId { get; set; }
        public string tipAmount { get; set; }
        public string transactionReference { get; set; }
    }

    // Process Sale (Checkout) - Response
    public class TransactionResponseRoot
    {
        public TransactionResponse transactionResponse { get; set; }
    }

    public class TransactionResponse
    {
        public string uniqueTransId { get; set; }
        public string batchNumber { get; set; }
        public string resultMessage { get; set; }
        public string resultStatus { get; set; }
        public string approvalNumberResult { get; set; }
        public string amountProcessed { get; set; }
        public string amount { get; set; }
        public string amountTaxed { get; set; }
        public string amountTipped { get; set; }
        public string amountBalance { get; set; }
        public string transactionReference { get; set; }
        public string avsResponseCode { get; set; }
        public string avsResponseText { get; set; }
        public string cvvResponseCode { get; set; }
        public string cvvResponseText { get; set; }
        public string accountCardType { get; set; }
        public string accountExpiryDate { get; set; }
        public string transactionType { get; set; }
        public string billingName { get; set; }
        public string maskedAccount { get; set; }
        public string accountEntryMethod { get; set; }
        public string externalTransactionId { get; set; }
        public string cashier { get; set; }
        public Emv emv { get; set; }
        public string signature { get; set; }
        public Level2 level2 { get; set; }
        public bool isPartialApproval { get; set; }
        public string createdOn { get; set; }
    }

    public class Emv
    {
        public string aid { get; set; }
    }

    public class Level2
    {
        public bool isTaxExempt { get; set; }
        public string purchaseId { get; set; }
        public string purchaseOrderNumber { get; set; }
        public string customerTaxId { get; set; }
        public string destinationPostalCode { get; set; }
        public string productDescription { get; set; }
    }



}