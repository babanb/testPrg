using System;
using System.Threading;
using ADOPets.Web.Common.Payment.Model;
using ADOPets.Web.ViewModels.Account;
using ADOPets.Web.ViewModels.Econsultation;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using ADOPets.Web.FDPaymentservice;
using AuthorizeNet;
using Transaction = ADOPets.Web.FDPaymentservice.Transaction;
using ADOPets.Web.ViewModels.SMO;


namespace ADOPets.Web.Common.Helpers
{
    public static class PaymentHelper
    {
        public static PaymentResult USPayment(ConfirmationViewModel model)
        {
            PaymentResult objResult = new PaymentResult();

            try
            {
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;

                FDGGWSApiOrderService oFDGGWSApiOrderService = new FDGGWSApiOrderService();
                oFDGGWSApiOrderService.Url = WebConfigHelper.FirstDataWebReferenceOrder;
                oFDGGWSApiOrderService.ClientCertificates.Add(X509Certificate.CreateFromCertFile(WebConfigHelper.FirstDataClientCertificate));

                //Set the Authentication Credentials
                string username = WebConfigHelper.FirstDataNetworkCredentialUsername;
                string password = WebConfigHelper.FirstDataNetworkCredentialPassword;
                NetworkCredential nc = new NetworkCredential(username, password);
                oFDGGWSApiOrderService.Credentials = nc;

                FDGGWSApiOrderRequest oOrderRequest = new FDGGWSApiOrderRequest();
                Transaction oTransaction = new Transaction();
                CreditCardTxType oCreditCardTxType = new CreditCardTxType();
                oCreditCardTxType.Type = CreditCardTxTypeType.sale;
                CreditCardData oCreditCardData = new CreditCardData();

                oCreditCardData.ItemsElementName = new[] { ItemsChoiceType.CardNumber, ItemsChoiceType.ExpMonth, ItemsChoiceType.ExpYear };

                //split expiration date in month and year(required last 2 digites only)
                string[] expdat = model.ExpirationDate.Split('/');

                //enter card data here
                oCreditCardData.Items = new[] { model.CreditCardNumber, expdat[0], expdat[1].Substring(expdat[1].Length - 2) };
                oTransaction.Items = new object[] { oCreditCardTxType, oCreditCardData };
                FDPaymentservice.Payment oPayment = new FDPaymentservice.Payment();
                oPayment.ChargeTotal = Convert.ToDecimal(model.Price);
                oTransaction.Payment = oPayment;
                oOrderRequest.Item = oTransaction;

                FDGGWSApiOrderResponse oResponse = null;

                oResponse = oFDGGWSApiOrderService.FDGGWSApiOrder(oOrderRequest);

                objResult.OrderId = oResponse.OrderId;
                objResult.TransactionResult = oResponse.TransactionResult;
                objResult.ErrorMessage = oResponse.ErrorMessage;
                objResult.TransactionDate = oResponse.TDate;
                objResult.CalculatedShipping = oResponse.CalculatedShipping;
                objResult.CalculatedTax = oResponse.CalculatedTax;
                objResult.TransactionID = oResponse.TransactionID;
                objResult.TransactionScore = oResponse.TransactionScore;
                objResult.TransactionTime = oResponse.TransactionTime;
                if (string.IsNullOrEmpty(oResponse.ErrorMessage))
                {
                    objResult.Success = true;
                }
                else
                {
                    objResult.Success = false;
                }

            }
            catch (Exception ex)
            {
                objResult.ErrorMessage = ex.Message;
                objResult.Success = false;
            }

            return objResult;
        }

        public static PaymentResult USAuthorizePayment(ConfirmationViewModel model)
        {
            var request = new AuthorizationRequest(model.CreditCardNumber, model.ExpirationDate, model.Price, model.Plan);

            //user information
            request.Email = model.Email;
            request.FirstName = model.FirstName;
            request.LastName = model.LastName;

            //billing address
            request.Address = string.Format("{0} {1}", model.BillingAddress1, model.BillingAddress2);
            request.City = model.BillingCity;
            request.State = EnumHelper.GetResourceValueForEnumValue(model.BillingState);
            request.Country = EnumHelper.GetResourceValueForEnumValue(model.BillingCountry);
            request.Zip = model.BillingZip.HasValue ? model.BillingZip.ToString() : null;

            var gateWay = new Gateway(WebConfigHelper.AuthorizeLogin, WebConfigHelper.AuthorizeKey, WebConfigHelper.UseAuthorizeTestMode);

            var response = gateWay.Send(request);

            var result = new PaymentResult
            {
                OrderId = response.TransactionID,
                TransactionResult = response.Message,
                Success = response.Approved
            };

            return result;
        }

        public static PaymentResult PTAuthorizePayment(ConfirmationViewModel model)
        {
            var request = new AuthorizationRequest(model.CreditCardNumber, model.ExpirationDate, model.Price, model.Plan);

            //user information
            request.Email = model.Email;
            request.FirstName = model.FirstName;
            request.LastName = model.LastName;

            //billing address
            request.Address = string.Format("{0} {1}", model.BillingAddress1, model.BillingAddress2);
            request.City = model.BillingCity;
            request.State = EnumHelper.GetResourceValueForEnumValue(model.BillingState);
            request.Country = EnumHelper.GetResourceValueForEnumValue(model.BillingCountry);
            request.Zip = model.BillingZip.HasValue ? model.BillingZip.ToString() : null;

            var gateWay = new Gateway(WebConfigHelper.PTAuthorizeLogin, WebConfigHelper.PTAuthorizeKey, WebConfigHelper.UseAuthorizeTestMode);

            var response = gateWay.Send(request);

            var result = new PaymentResult
            {
                OrderId = response.TransactionID,
                TransactionResult = response.Message,
                Success = response.Approved
            };

            return result;
        }

        //.........for econsultationpayment......
        public static PaymentResult USAuthorizePayment1(ConfirmationViewModel1 model)
        {
            var request = new AuthorizationRequest(model.CreditCardNumber, model.ExpirationDate, model.Price, model.Plan);

            //user information
            request.Email = model.Email;
            request.FirstName = model.FirstName;
            request.LastName = model.LastName;

            //billing address
            request.Address = string.Format("{0} {1}", model.BillingAddress1, model.BillingAddress2);
            request.City = model.BillingCity;
            request.State = EnumHelper.GetResourceValueForEnumValue(model.BillingState);
            request.Country = EnumHelper.GetResourceValueForEnumValue(model.BillingCountry);
            request.Zip = model.BillingZip;

            var gateWay = new Gateway(WebConfigHelper.AuthorizeLogin, WebConfigHelper.AuthorizeKey, WebConfigHelper.UseAuthorizeTestMode);

            var response = gateWay.Send(request);

            var result = new PaymentResult
            {
                OrderId = response.TransactionID,
                TransactionResult = response.Message,
                Success = response.Approved
            };

            return result;
        }

        public static PaymentResult SMOPayment(ViewModels.SMO.AddBillingViewModel model)
        {
            var request = new AuthorizationRequest(model.CreditCardNumber, model.ExpirationDate.ToString(), model.Price, model.Plan);


            //billing address
            request.Address = string.Format("{0} {1}", model.Address1, model.Address2);
            request.City = model.City;
            request.State = EnumHelper.GetResourceValueForEnumValue(model.State);
            request.Country = EnumHelper.GetResourceValueForEnumValue(model.Country);
            request.Zip = model.Zip == "" ? model.Zip.ToString() : null;

            var gateWay = new Gateway(WebConfigHelper.AuthorizeLogin, WebConfigHelper.AuthorizeKey, WebConfigHelper.UseAuthorizeTestMode);

            var response = gateWay.Send(request);

            var result = new PaymentResult
            {
                OrderId = response.TransactionID,
                TransactionResult = response.Message,
                Success = response.Approved
            };

            return result;
        }

        //For PT econsultationpayment......
        public static PaymentResult PTAuthorizePaymentForEC(ConfirmationViewModel1 model)
        {
            var request = new AuthorizationRequest(model.CreditCardNumber, model.ExpirationDate, model.Price, model.Plan);

            //user information
            request.Email = model.Email;
            request.FirstName = model.FirstName;
            request.LastName = model.LastName;

            //billing address
            request.Address = string.Format("{0} {1}", model.BillingAddress1, model.BillingAddress2);
            request.City = model.BillingCity;
            request.State = EnumHelper.GetResourceValueForEnumValue(model.BillingState);
            request.Country = EnumHelper.GetResourceValueForEnumValue(model.BillingCountry);
            request.Zip = model.BillingZip;

            var gateWay = new Gateway(WebConfigHelper.PTAuthorizeLogin, WebConfigHelper.PTAuthorizeKey, WebConfigHelper.UseAuthorizeTestMode);

            var response = gateWay.Send(request);

            var result = new PaymentResult
            {
                OrderId = response.TransactionID,
                TransactionResult = response.Message,
                Success = response.Approved
            };

            return result;
        }

        //For PT SMO Payment
        public static PaymentResult PTSMOPayment(ViewModels.SMO.AddBillingViewModel model)
        {
            var request = new AuthorizationRequest(model.CreditCardNumber, model.ExpirationDate.ToString(), model.Price, model.Plan);


            //billing address
            request.Address = string.Format("{0} {1}", model.Address1, model.Address2);
            request.City = model.City;
            request.State = EnumHelper.GetResourceValueForEnumValue(model.State);
            request.Country = EnumHelper.GetResourceValueForEnumValue(model.Country);
            request.Zip = model.Zip == "" ? model.Zip.ToString() : null;

            var gateWay = new Gateway(WebConfigHelper.PTAuthorizeLogin, WebConfigHelper.PTAuthorizeKey, WebConfigHelper.UseAuthorizeTestMode);

            var response = gateWay.Send(request);

            var result = new PaymentResult
            {
                OrderId = response.TransactionID,
                TransactionResult = response.Message,
                Success = response.Approved
            };

            return result;
        }

        public static PaymentResult FrenchPayment(ConfirmationViewModel model)
        {
            throw new NotImplementedException();
        }


        public static PaymentResult FakePayment(ConfirmationViewModel model)
        {
            //faking payment, waiting 3 seconds
            Thread.Sleep(3000);

            return new PaymentResult
            {
                OrderId = "OrderIdTest",
                Success = true,
                TransactionID = "TransactionIdTest"
            };
        }

        public static PaymentResult FakePaymentForUpgradePlan(ViewModels.Profile.PlanConfirmationViewModel model)
        {
            //faking payment, waiting 3 seconds
            Thread.Sleep(3000);

            return new PaymentResult
            {
                OrderId = "OrderIdTest",
                Success = true,
                TransactionID = "TransactionIdTest"
            };
        }


        public static PaymentResult USAuthorizePaymentForUpgradePlan(ViewModels.Profile.PlanConfirmationViewModel model)
        {
            var request = new AuthorizationRequest(model.CreditCardNumber, model.BillingExpirationDate, model.Price, model.PlanName);

            var gateWay = new Gateway(WebConfigHelper.AuthorizeLogin, WebConfigHelper.AuthorizeKey, WebConfigHelper.UseAuthorizeTestMode);

            var response = gateWay.Send(request);

            var result = new PaymentResult
            {
                OrderId = response.TransactionID,
                TransactionResult = response.Message,
                Success = response.Approved
            };

            return result;
        }


        public static PaymentResult PTAuthorizePaymentForUpgradePlan(ViewModels.Profile.PlanConfirmationViewModel model)
        {
            var request = new AuthorizationRequest(model.CreditCardNumber, model.BillingExpirationDate, model.Price, model.PlanName);

            var gateWay = new Gateway(WebConfigHelper.PTAuthorizeLogin, WebConfigHelper.PTAuthorizeKey, WebConfigHelper.UseAuthorizeTestMode);

            var response = gateWay.Send(request);

            var result = new PaymentResult
            {
                OrderId = response.TransactionID,
                TransactionResult = response.Message,
                Success = response.Approved
            };

            return result;
        }

    }
}