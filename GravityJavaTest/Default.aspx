<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GravityJavaTest.Default" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

  <head>
    <title>Gravity JAVA Test</title>
    <meta charset="UTF-8" />

    <script src="https://assets.emergepay-sandbox.chargeitpro.com/cip-hosted-modal.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.4/jquery.min.js"></script>
    <script src="<% =assetsCheckoutURL %>/cip-hosted-fields.js"></script>

    <!-- Use the emergepay library -->
    <script>
        $(document).ready(function () {
            getToken().then(function (transactionToken) {
                // Initialize the hosted fields
                var hosted = emergepayFormFields.init({
                    // (required) Used to set up each field
                    transactionToken: transactionToken,
                    // (required) The type of transaction to run
                    transactionType: "CreditSale",
                    // (optional) Configure which fields to use and the id's of the elements to append each field to
                    fieldSetUp: {
                        // These fields are valid for credit card transactions
                        cardNumber: {
                            appendToSelector: "cardNumberContainer",
                            useField: true,
                            // optional, automatically sets the height of the iframe to the height of the
                            // contents within the iframe. Useful when using the styles object
                            autoIframeHeight: true,
                            // optional, see styles section above for more information
                            styles: { "background-color": "blue" }
                        },
                        cardExpirationDate: {
                            appendToSelector: "expirationDateContainer",
                            useField: true,
                            // optional, automatically sets the height of the iframe to the height of the
                            // contents within the iframe. Useful when using the styles object
                            autoIframeHeight: true,
                            // optional, see styles section above for more information
                            styles: { "border": "1px solid red" }
                        },
                        cardSecurityCode: {
                            appendToSelector: "securityCodeContainer",
                            useField: true,
                            // optional, automatically sets the height of the iframe to the height of the
                            // contents within the iframe. Useful when using the styles object
                            autoIframeHeight: true,
                            // optional, see styles section above for more information
                            styles: {}
                        },
                        // These fields are valid for ACH transactions
                        accountNumber: {
                            appendToSelector: "accountNumberContainer",
                            useField: true,
                            // optional, automatically sets the height of the iframe to the height of the
                            // contents within the iframe. Useful when using the styles object
                            autoIframeHeight: true,
                            // optional, see styles section above for more information
                            styles: {}
                        },
                        routingNumber: {
                            appendToSelector: "routingNumberContainer",
                            useField: true,
                            // optional, automatically sets the height of the iframe to the height of the
                            // contents within the iframe. Useful when using the styles object
                            autoIframeHeight: true,
                            // optional, see styles section above for more information
                            styles: {}
                        },
                        accountHolderName: {
                            appendToSelector: "accountHolderNameContainer",
                            useField: true,
                            // optional, automatically sets the height of the iframe to the height of the
                            // contents within the iframe. Useful when using the styles object
                            autoIframeHeight: true,
                            // optional, see styles section above for more information
                            styles: {}
                        },
                        // These fields are valid for all transaction types
                        totalAmount: {
                            useField: false
                        },
                        externalTranId: {
                            useField: false
                        }
                    },
                    // (optional) If there is a validation error for a field, the styles set in this object will be applied to the field
                    fieldErrorStyles: {
                        "border": "none",
                        "box-shadow": "0px 0px 4px 1px red"
                    },
                    // (optional) This callback function will be called when there is a validation error for a field.
                    onFieldError: function (data) {
                        console.log(data);
                    },
                    // (optional) This callback function will be called when a field validation error has been cleared.
                    onFieldErrorCleared: function (data) {
                        console.log(data);
                    },
                    // (optional) This callback function will be called when all of the requested fields have loaded
                    // and are ready to accept user input. This can be useful for things like toggling the status
                    // of loading indicators or ignoring clicks on a button until all of the fields are fully loaded
                    onFieldsLoaded: function () {
                        console.log('All fields loaded');
                    },
                    // (required) Callback function that gets called after user successfully enters their information into the form fields and triggers the execution of the `process` function
                    onUserAuthorized: function (transactionToken) {
                        console.log('transactionToken', transactionToken);

                        var details = {
                            transactionToken,
                            // any other transaction details you may need to send to your server
                        };

                        $.ajax({
                            url: 'http://localhost:5555/submit',
                            type: 'POST',
                            dataType: 'json',
                            contentType: 'application/json',
                            data: JSON.stringify(details)
                        })
                            .done(function (data) {
                                // your server successfully ran the transaction
                                location = 'https://www.gravitypayments.com';
                            })
                            .fail(function (error) {
                                console.error(error);
                            });
                    },
                    // (optional) Callback function that gets called after the user enters the first 6 digits of their card number.
                    // This can be useful for showing a card brand icon to the user, or for determining if the card entered
                    // is a credit card or a debit card.
                    onBinChange: function (binData) {
                        console.log('bin', binData.bin);
                        console.log('card type', binData.cardType);
                    }
                });

                $('#payBtn').on("click", function (event) {
                    event.preventDefault();
                    // run the transaction if all fields are valid
                    hosted.process();
                });
            });
        });

        // This function makes a call to your server to get a transaction token
        function getToken() {
            return new Promise(function (resolve, reject) {
                $.ajax({
                    url: 'http://localhost:5555/start-transaction',
                    type: 'POST',
                    dataType: 'json',
                    contentType: 'application/json'
                })
                    .done(function (data) {
                        if (data.transactionToken) resolve(data.transactionToken);
                        else reject('Error getting transaction token');
                    })
                    .fail(function (err) {
                        reject(err);
                    });
            });
        }
    </script>

  </head>

  <body>

    <form id="GravityForm" runat="server">
        token: <% =transactionToken %> <br />
        assetsCheckoutURL: <% =assetsCheckoutURL %> <br /> <br />

        <!-- Elements that fields will be appended to for credit card transactions -->
        <div id="cardNumberContainer">
            <iframe src="http://winjewel.com" title="Iframe Example"></iframe>
        </div>
        <div id="expirationDateContainer"></div>
        <div id="securityCodeContainer"></div>

        <!-- Elements that fields will be appended to for ACH transactions -->
        <div id="accountNumberContainer"></div>
        <div id="routingNumberContainer"></div>
        <div id="accountHolderNameContainer"></div>

        <button id="payBtn">Pay</button>
    </form>

  </body> 


</html>
