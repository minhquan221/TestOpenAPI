using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using TestAPIConnect.Common;
using TestAPIConnect.Models;

namespace TestAPIConnect.Services
{
    public class ServiceAPI
    {
        private static ServiceAPI _current = new ServiceAPI();

        public static ServiceAPI Current
        {
            get
            {
                return _current != null ? _current : new ServiceAPI();
            }
        }

        #region InitTrace

        public trace InitCurrentTrace()
        {
            var trace = new trace
            {
                clientTransId = APIHelper.RandomTransId(),
                clientTimestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff")
            };
            return trace;
        }

        #endregion

        #region HubFunction

        public JsonResultData HubFunction(Dictionary<string, object> info, object request, string functionUrl)
        {

            var ResultFunct = new JsonResultData();
            try
            {
                var authentoken = LoginUser.Current.Authentication;
                var clientId = info.ContainsKey("ClientId") ? info["ClientId"].ToString() : LoginUser.Current.ClientId;
                var clientSecret = info.ContainsKey("ClientSecret") ? info["ClientSecret"].ToString() : LoginUser.Current.ClientSecret;
                var certificate = info.ContainsKey("Certificate") ? info["ClientId"].ToString() : LoginUser.Current.Certificate;
                authentoken = AuthenticateData(clientId, clientSecret, certificate);
                int status = -1;
                string Msg = string.Empty;
                object Err = new object();
                var responseConnect = APIHelper.CallAPI2(ref status, ref Msg, ref Err, functionUrl, "POST", 0, "application/json", null, request, authentoken, clientId, clientSecret, info.ContainsKey("PrivateKey") ? info["PrivateKey"].ToString() : "", certificate);

                if (status == 0)
                {
                    ResultFunct.IsOk = false;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataErr = Err;
                }
                else
                {
                    var resultdata = responseConnect;
                    ResultFunct.IsOk = true;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataObj = resultdata;

                }
                return ResultFunct;
            }
            catch (Exception ex)
            {
                ResultFunct.IsOk = false;
                ResultFunct.Msg = ex.ToString();
                return ResultFunct;
            }
        }

        #endregion

        #region Get Token

        public JsonResultData LoginAccess(LoginModel user)
        {
            var ResultFunct = new JsonResultData();
            try
            {
                int status = -1;
                string Msg = string.Empty;
                object Err = new object();
                var responseConnect = APIHelper.AccessToken<tokenresponse>(ref status, ref Msg, ref Err, user);
                return responseConnect;
            }
            catch (Exception ex)
            {
                ResultFunct.IsOk = false;
                ResultFunct.Msg = ex.ToString();
                return ResultFunct;
            }
        }

        #endregion

        #region Sign On Data

        public JsonResultData ProcessGenerate(certificate data)
        {
            var ResultFunct = new JsonResultData();
            try
            {
                var signature = CommonJoseHelper.Current.Sign2(data.datastring, data.PrivateKey);
                if (signature != null)
                {
                    ResultFunct.IsOk = true;
                    ResultFunct.Msg = "";
                    ResultFunct.dataObj = signature;
                }
                return ResultFunct;
            }
            catch (Exception ex)
            {
                ResultFunct.IsOk = false;
                ResultFunct.Msg = ex.ToString();
                return ResultFunct;
            }
        }

        public JsonResultData WriteSignature(certificate data)
        {
            var ResultFunct = new JsonResultData();
            try
            {
                var signature = CommonSignature.Current.SignWithLibrary<object>(data.datastring, data.PrivateKey);
                if (CommonSignature.Current.Verify(signature.ToString(), data.Certificate))
                {
                    var resultDecode = CommonSignature.Current.Decode<object>(signature.ToString(), data.Certificate);
                }
                if (signature != null)
                {
                    ResultFunct.IsOk = true;
                    ResultFunct.Msg = "";
                    ResultFunct.dataObj = signature.ToString();
                }
                return ResultFunct;
            }
            catch (Exception ex)
            {
                ResultFunct.IsOk = false;
                ResultFunct.Msg = ex.ToString();
                return ResultFunct;
            }
        }

        #endregion

        //payment/externalDomesticTransfers
        #region External Domestic Transfers

        public JsonResultData ProcessPayment(externalTransferViewModel payment)
        {

            var ResultFunct = new JsonResultData();
            try
            {
                var authentoken = LoginUser.Current.Authentication;
                var clientId = !string.IsNullOrEmpty(payment.ClientId) ? payment.ClientId : LoginUser.Current.ClientId;
                var clientSecret = !string.IsNullOrEmpty(payment.ClientSecret) ? payment.ClientSecret : LoginUser.Current.ClientSecret;
                var certificate = !string.IsNullOrEmpty(payment.Certificate) ? payment.ClientId : LoginUser.Current.Certificate;
                authentoken = AuthenticateData(clientId, clientSecret, certificate);
                var request = new RequestBodyModel<external>
                {
                    trace = InitCurrentTrace(),
                    data = new external
                    {
                        externalTransfer = ConvertHelper.Current.ConvertObjToObj<externalTransfer, externalTransferViewModel>(payment)
                    }
                };
                int status = -1;
                string Msg = string.Empty;
                object Err = new object();
                var responseConnect = APIHelper.CallAPI<ResponseBodyModel<dataresponsedata<OBJtransactionpaymentResponse>>>(ref status, ref Msg, ref Err, "v1/payment/localTransfers", "POST", 0, "application/json", null, request, authentoken, clientId, clientSecret, payment.PrivateKey, certificate);

                if (status == 0)
                {
                    ResultFunct.IsOk = false;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataErr = Err;
                }
                else
                {
                    var resultdata = responseConnect.data.transactions;
                    ResultFunct.IsOk = true;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataObj = resultdata;

                }
                return ResultFunct;
            }
            catch (Exception ex)
            {
                ResultFunct.IsOk = false;
                ResultFunct.Msg = ex.ToString();
                return ResultFunct;
            }
        }

        #endregion

        //accounts/details
        #region accountsDetails

        public JsonResultData AccountsDetails(DataCertificate info)
        {

            var ResultFunct = new JsonResultData();
            try
            {
                var authentoken = LoginUser.Current.Authentication;
                var clientId = !string.IsNullOrEmpty(info.ClientId) ? info.ClientId : LoginUser.Current.ClientId;
                var clientSecret = !string.IsNullOrEmpty(info.ClientSecret) ? info.ClientSecret : LoginUser.Current.ClientSecret;
                var certificate = !string.IsNullOrEmpty(info.Certificate) ? info.ClientId : LoginUser.Current.Certificate;
                authentoken = AuthenticateData(clientId, clientSecret, certificate);
                var stringList = info.accountNumbers.Split(',');
                List<string> data = new List<string>();
                foreach (var item in stringList)
                {
                    data.Add(item);
                }
                var request = new RequestBodyModel<datarequestAccountNumber>
                {
                    trace = InitCurrentTrace(),
                    data = new datarequestAccountNumber
                    {
                        accountNumbers = data
                    }
                };
                int status = -1;
                string Msg = string.Empty;
                object Err = new object();
                var responseConnect = APIHelper.CallAPI<ResponseBodyModel<dataresponsedata<string>>>(ref status, ref Msg, ref Err, "v1/accounts/details", "POST", 0, "application/json", null, request, authentoken, clientId, clientSecret, info.PrivateKey, certificate);

                if (status == 0)
                {
                    ResultFunct.IsOk = false;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataErr = Err;
                }
                else
                {
                    var resultdata = responseConnect.data.accounts;
                    ResultFunct.IsOk = true;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataObj = resultdata;

                }
                return ResultFunct;
            }
            catch (Exception ex)
            {
                ResultFunct.IsOk = false;
                ResultFunct.Msg = ex.ToString();
                return ResultFunct;
            }
        }

        #endregion


        //account/transactions
        #region transactions

        public JsonResultData GetAllTransactions(inputSearchTrans info)
        {

            var ResultFunct = new JsonResultData();
            try
            {
                var authentoken = LoginUser.Current.Authentication;
                var clientId = !string.IsNullOrEmpty(info.ClientId) ? info.ClientId : LoginUser.Current.ClientId;
                var clientSecret = !string.IsNullOrEmpty(info.ClientSecret) ? info.ClientSecret : LoginUser.Current.ClientSecret;
                var certificate = !string.IsNullOrEmpty(info.Certificate) ? info.ClientId : LoginUser.Current.Certificate;
                authentoken = AuthenticateData(clientId, clientSecret, certificate);

                var request = new 
                {
                    trace = InitCurrentTrace(),
                    data = new 
                    {
                        conditions = new 
                        {
                            transactionTime = new 
                            {
                                fromDate = info.fromDate.HasValue ? info.fromDate.Value.ToString("yyyy-MM-dd") : DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd"),
                                toDate = info.toDate.HasValue ? info.toDate.Value.ToString("yyyy-MM-dd") : DateTime.Now.ToString("yyyy-MM-dd"),
                            },
                            AccountNumber = info.debitAccountNumber
                        }
                    }
                };
                int status = -1;
                string Msg = string.Empty;
                object Err = new object();
                var responseConnect = APIHelper.CallAPI<ResponseBodyModel<dataresponsedata<List<OBJTransactionResponse>>>>(ref status, ref Msg, ref Err, "v1/account/transactions", "POST", 0, "application/json", null, request, authentoken, clientId, clientSecret, info.PrivateKey, certificate);

                if (status == 0)
                {
                    ResultFunct.IsOk = false;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataErr = Err;
                }
                else
                {
                    var resultdata = responseConnect.data.transactions;
                    ResultFunct.IsOk = true;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataObj = resultdata;

                }
                return ResultFunct;
            }
            catch (Exception ex)
            {
                ResultFunct.IsOk = false;
                ResultFunct.Msg = ex.ToString();
                return ResultFunct;
            }
        }

        #endregion


        #region accounts
        //accounts
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public JsonResultData GetListAccounts(DataCertificate info)
        {

            var ResultFunct = new JsonResultData();
            try
            {
                var authentoken = LoginUser.Current.Authentication;
                var clientId = !string.IsNullOrEmpty(info.ClientId) ? info.ClientId : LoginUser.Current.ClientId;
                var clientSecret = !string.IsNullOrEmpty(info.ClientSecret) ? info.ClientSecret : LoginUser.Current.ClientSecret;
                var certificate = !string.IsNullOrEmpty(info.Certificate) ? info.ClientId : LoginUser.Current.Certificate;
                authentoken = AuthenticateData(clientId, clientSecret, certificate);

                var request = new
                {
                    trace = InitCurrentTrace()
                };
                int status = -1;
                string Msg = string.Empty;
                object Err = new object();
                var responseConnect = APIHelper.CallAPI<ResponseBodyModel<dataresponsedata<accounts>>>(ref status, ref Msg, ref Err, "v1/accounts", "POST", 0, "application/json", null, request, authentoken, clientId, clientSecret, info.PrivateKey, certificate);
                if (status == 0)
                {
                    ResultFunct.IsOk = false;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataErr = Err;
                }
                else
                {
                    var resultdata = responseConnect.data.accounts;
                    ResultFunct.IsOk = true;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataObj = resultdata;

                }
                return ResultFunct;
            }
            catch (Exception ex)
            {
                ResultFunct.IsOk = false;
                ResultFunct.Msg = ex.ToString();
                return ResultFunct;
            }
        }

        #endregion


        #region Truy vấn thông tin các khoản tiền gửi

        //createDepositAccount
        /// <summary>
        /// 
        /// </summary>
        /// <param name="depositInfo"></param>
        /// <returns></returns>
        public JsonResultData createDepositAccount(depositInfoViewModel depositInfo)
        {

            var ResultFunct = new JsonResultData();
            try
            {
                var authentoken = LoginUser.Current.Authentication;
                var clientId = !string.IsNullOrEmpty(depositInfo.ClientId) ? depositInfo.ClientId : LoginUser.Current.ClientId;
                var clientSecret = !string.IsNullOrEmpty(depositInfo.ClientSecret) ? depositInfo.ClientSecret : LoginUser.Current.ClientSecret;
                var certificate = !string.IsNullOrEmpty(depositInfo.Certificate) ? depositInfo.ClientId : LoginUser.Current.Certificate;
                authentoken = AuthenticateData(clientId, clientSecret, certificate);

                var request = new RequestBodyModel<datarequestDepositInfo>
                {
                    trace = InitCurrentTrace(),
                    data = new datarequestDepositInfo
                    {
                        depositInfo = ConvertHelper.Current.ConvertObjToObj<depositInfo,
                        depositInfoViewModel>(depositInfo)
                    }
                };
                int status = -1;
                string Msg = string.Empty;
                object Err = new object();
                var responseConnect = APIHelper.CallAPI<ResponseBodyModel<dataresponsedata<string>>>(ref status, ref Msg, ref Err, "v1/createDepositAccount", "POST", 0, "application/json", null, request, authentoken, clientId, clientSecret, depositInfo.PrivateKey, certificate);
                if (status == 0)
                {
                    ResultFunct.IsOk = false;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataErr = Err;
                }
                else
                {
                    var resultdata = responseConnect.data.depositAcctNumber;
                    ResultFunct.IsOk = true;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataObj = resultdata;

                }
                return ResultFunct;
            }
            catch (Exception ex)
            {
                ResultFunct.IsOk = false;
                ResultFunct.Msg = ex.ToString();
                return ResultFunct;
            }
        }

        #endregion


        #region Internal Transfer
        /// <summary>
        /// 
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        public JsonResultData ProcessInternalPayment(internalTransferViewModel payment)
        {

            var ResultFunct = new JsonResultData();
            try
            {
                var authentoken = LoginUser.Current.Authentication;
                var clientId = !string.IsNullOrEmpty(payment.ClientId) ? payment.ClientId : LoginUser.Current.ClientId;
                var clientSecret = !string.IsNullOrEmpty(payment.ClientSecret) ? payment.ClientSecret : LoginUser.Current.ClientSecret;
                var certificate = !string.IsNullOrEmpty(payment.Certificate) ? payment.ClientId : LoginUser.Current.Certificate;
                authentoken = AuthenticateData(clientId, clientSecret, certificate);

                var request = new RequestBodyModel<internals>
                {
                    trace = InitCurrentTrace(),
                    data = new internals
                    {
                        internalTransfer = ConvertHelper.Current.ConvertObjToObj<internalTransfer, internalTransferViewModel>(payment)
                    }
                };
                int status = -1;
                string Msg = string.Empty;
                object Err = new object();
                var responseConnect = APIHelper.CallAPI<ResponseBodyModel<dataresponsedata<OBJtransactionpaymentResponse>>>(ref status, ref Msg, ref Err, "v1/payment/internalTransfers", "POST", 0, "application/json", null, request, authentoken, clientId, clientSecret, payment.PrivateKey, certificate);

                if (status == 0)
                {
                    ResultFunct.IsOk = false;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataErr = Err;
                }
                else
                {
                    var resultdata = responseConnect.data.transactions;
                    ResultFunct.IsOk = true;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataObj = resultdata;

                }
                return ResultFunct;
            }
            catch (Exception ex)
            {
                ResultFunct.IsOk = false;
                ResultFunct.Msg = ex.ToString();
                return ResultFunct;
            }
        }

        #endregion

        #region retrievePartnerBill

        //payment/retrievePartnerBill
        public JsonResultData retrievePartnerBill(billInfoModelView info)
        {

            var ResultFunct = new JsonResultData();
            try
            {
                var authentoken = LoginUser.Current.Authentication;
                var clientId = !string.IsNullOrEmpty(info.ClientId) ? info.ClientId : LoginUser.Current.ClientId;
                var clientSecret = !string.IsNullOrEmpty(info.ClientSecret) ? info.ClientSecret : LoginUser.Current.ClientSecret;
                var certificate = !string.IsNullOrEmpty(info.Certificate) ? info.ClientId : LoginUser.Current.Certificate;
                authentoken = AuthenticateData(clientId, clientSecret, certificate);

                var request = new RequestBodyModel<billInfoHub>
                {
                    trace = InitCurrentTrace(),
                    data = new billInfoHub
                    {
                        billInfo = new billInfoRequest
                        {
                            billCode = info.billCode,
                            providerCode = info.providerCode,
                            serviceCode = info.serviceCode
                        }
                    }
                };
                int status = -1;
                string Msg = string.Empty;
                object Err = new object();
                var responseConnect = APIHelper.CallAPI2(ref status, ref Msg, ref Err, "v1/payment/retrievePartnerBill", "POST", 0, "application/json", null, request, authentoken, clientId, clientSecret, info.PrivateKey, certificate);

                if (status == 0)
                {
                    ResultFunct.IsOk = false;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataErr = Err;
                }
                else
                {
                    var resultdata = responseConnect;
                    ResultFunct.IsOk = true;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataObj = resultdata;

                }
                return ResultFunct;
            }
            catch (Exception ex)
            {
                ResultFunct.IsOk = false;
                ResultFunct.Msg = ex.ToString();
                return ResultFunct;
            }
        }


        #endregion


        #region fastTransfers

        //payment/fastTransfers
        public JsonResultData fastTransfers(fastTransferViewModel info)
        {

            var ResultFunct = new JsonResultData();
            try
            {
                var authentoken = LoginUser.Current.Authentication;
                var clientId = !string.IsNullOrEmpty(info.ClientId) ? info.ClientId : LoginUser.Current.ClientId;
                var clientSecret = !string.IsNullOrEmpty(info.ClientSecret) ? info.ClientSecret : LoginUser.Current.ClientSecret;
                var certificate = !string.IsNullOrEmpty(info.Certificate) ? info.ClientId : LoginUser.Current.Certificate;
                authentoken = AuthenticateData(clientId, clientSecret, certificate);

                var request = new RequestBodyModel<fast>
                {
                    trace = InitCurrentTrace(),
                    data = new fast
                    {
                        fastTransfer = ConvertHelper.Current.ConvertObjToObj<fastTransfer, fastTransferViewModel>(info)
                    }
                };
                int status = -1;
                string Msg = string.Empty;
                object Err = new object();
                var responseConnect = APIHelper.CallAPI<ResponseBodyModel<dataresponsedataTransaction>>(ref status, ref Msg, ref Err, "v1/payment/fastTransfers", "POST", 0, "application/json", null, request, authentoken, clientId, clientSecret, info.PrivateKey, certificate);

                if (status == 0)
                {
                    ResultFunct.IsOk = false;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataErr = Err;
                }
                else
                {
                    var resultdata = responseConnect.data.transaction;
                    ResultFunct.IsOk = true;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataObj = resultdata;

                }
                return ResultFunct;
            }
            catch (Exception ex)
            {
                ResultFunct.IsOk = false;
                ResultFunct.Msg = ex.ToString();
                return ResultFunct;
            }
        }


        #endregion


        #region Bill Payment
        /// <summary>
        /// 
        /// </summary>
        /// <param name="billInfo"></param>
        /// <returns></returns>
        public JsonResultData ProcessBillPayment(billPaymentInfoViewModel billInfo)
        {

            var ResultFunct = new JsonResultData();
            try
            {
                var authentoken = LoginUser.Current.Authentication;
                var clientId = !string.IsNullOrEmpty(billInfo.ClientId) ? billInfo.ClientId : LoginUser.Current.ClientId;
                var clientSecret = !string.IsNullOrEmpty(billInfo.ClientSecret) ? billInfo.ClientSecret : LoginUser.Current.ClientSecret;
                var certificate = !string.IsNullOrEmpty(billInfo.Certificate) ? billInfo.ClientId : LoginUser.Current.Certificate;
                authentoken = AuthenticateData(clientId, clientSecret, certificate);

                var request = new RequestBodyModel<billPayment>
                {
                    trace = InitCurrentTrace(),
                    data = new billPayment
                    {
                        billPaymentInfo = ConvertHelper.Current.ConvertObjToObj<billPaymentInfo, billPaymentInfoViewModel>(billInfo)
                    }
                };
                int status = -1;
                string Msg = string.Empty;
                object Err = new object();
                var responseConnect = APIHelper.CallAPI<ResponseBodyModel<dataresponsedataTransaction>>(ref status, ref Msg, ref Err, "v1/payment/billPayment", "POST", 0, "application/json", null, request, authentoken, clientId, clientSecret, billInfo.PrivateKey, certificate);

                if (status == 0)
                {
                    ResultFunct.IsOk = false;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataErr = Err;
                }
                else
                {
                    var resultdata = responseConnect.data.transaction;
                    ResultFunct.IsOk = true;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataObj = resultdata;

                }
                return ResultFunct;
            }
            catch (Exception ex)
            {
                ResultFunct.IsOk = false;
                ResultFunct.Msg = ex.ToString();
                return ResultFunct;
            }
        }

        #endregion

        #region Partner Provider Code

        public JsonResultData PartnerProviderCode(PartnerProviderCodeViewModel infoModel)
        {

            var ResultFunct = new JsonResultData();
            try
            {
                var authentoken = LoginUser.Current.Authentication;
                var clientId = !string.IsNullOrEmpty(infoModel.ClientId) ? infoModel.ClientId : LoginUser.Current.ClientId;
                var clientSecret = !string.IsNullOrEmpty(infoModel.ClientSecret) ? infoModel.ClientSecret : LoginUser.Current.ClientSecret;
                var certificate = !string.IsNullOrEmpty(infoModel.Certificate) ? infoModel.ClientId : LoginUser.Current.Certificate;
                authentoken = AuthenticateData(clientId, clientSecret, certificate);
                var request = new RequestBodyModel<datarequestserviceCode>
                {
                    trace = InitCurrentTrace(),
                    data = new datarequestserviceCode
                    {
                        serviceCode = infoModel.serviceCode
                    }
                };
                int status = -1;
                string Msg = string.Empty;
                object Err = new object();
                var responseConnect = APIHelper.CallAPI<ResponseBodyModel<dataresponseServiceProviderList>>(ref status, ref Msg, ref Err, "v1/payment/selectPartnerProviderCode", "POST", 0, "application/json", null, request, authentoken, clientId, clientSecret, infoModel.PrivateKey, certificate);

                if (status == 0)
                {
                    ResultFunct.IsOk = false;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataErr = Err;
                }
                else
                {
                    var resultdata = responseConnect.data.serviceProviderList;
                    ResultFunct.IsOk = true;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataObj = resultdata;

                }
                return ResultFunct;
            }
            catch (Exception ex)
            {
                ResultFunct.IsOk = false;
                ResultFunct.Msg = ex.ToString();
                return ResultFunct;
            }
        }

        #endregion

        #region Authenticate Before Call

        public string AuthenticateData(string ClientId = "", string ClientSecret = "", string Certificate = "")
        {
            var authentoken = LoginUser.Current.Authentication;
            var clientId = !string.IsNullOrEmpty(ClientId) ? ClientId : LoginUser.Current.ClientId;
            var clientSecret = !string.IsNullOrEmpty(ClientSecret) ? ClientSecret : LoginUser.Current.ClientSecret;
            var certificate = !string.IsNullOrEmpty(Certificate) ? ClientId : LoginUser.Current.Certificate;
            if (clientId != LoginUser.Current.ClientId || clientSecret != LoginUser.Current.ClientSecret)
            {
                int statusToken = -1;
                string MsgToken = string.Empty;
                object ErrToken = new object();
                var getNewToken = APIHelper.AccessToken<tokenresponse>(ref statusToken, ref MsgToken, ref ErrToken, new LoginModel
                {
                    Username = LoginUser.Current.Username,
                    Password = LoginUser.Current.Password,
                    ClientId = ClientId,
                    ClientSecret = ClientSecret
                });
                if (statusToken != 0 && getNewToken != null)
                {
                    authentoken = ((tokenresponse)getNewToken.dataObj).access_token;
                }
            }
            return authentoken;
        }

        #endregion

        #region account/creation/result
        //account/creation/result
        public JsonResultData AccountCreationResult(conditionsViewModel info)
        {

            var ResultFunct = new JsonResultData();
            try
            {
                var authentoken = LoginUser.Current.Authentication;
                var clientId = !string.IsNullOrEmpty(info.ClientId) ? info.ClientId : LoginUser.Current.ClientId;
                var clientSecret = !string.IsNullOrEmpty(info.ClientSecret) ? info.ClientSecret : LoginUser.Current.ClientSecret;
                var certificate = !string.IsNullOrEmpty(info.Certificate) ? info.ClientId : LoginUser.Current.Certificate;
                authentoken = AuthenticateData(clientId, clientSecret, certificate);
                var request = new
                {
                    trace = InitCurrentTrace(),
                    data = new
                    {
                        conditions = new
                        {
                            transactionId = info.transactionId
                        }
                    }
                };
                int status = -1;
                string Msg = string.Empty;
                object Err = new object();
                var responseConnect = APIHelper.CallAPI<ResponseBodyModel<dataresponsedata<transactionStatusResponse>>>(ref status, ref Msg, ref Err, "v1/account/creation/result", "POST", 0, "application/json", null, request, authentoken, clientId, clientSecret, info.PrivateKey, certificate);

                if (status == 0)
                {
                    ResultFunct.IsOk = false;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataErr = Err;
                }
                else
                {
                    var resultdata = responseConnect.data.transactions;
                    ResultFunct.IsOk = true;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataObj = resultdata;

                }
                return ResultFunct;
            }
            catch (Exception ex)
            {
                ResultFunct.IsOk = false;
                ResultFunct.Msg = ex.ToString();
                return ResultFunct;
            }
        }


        #endregion

        #region card/creation/result
        //card/creation/result
        public JsonResultData CardCreationResult(Dictionary<string, object> info)
        {

            var request = new
            {
                trace = InitCurrentTrace(),
                data = new
                {
                    conditions = new
                    {
                        transactionId = info["transactionId"]
                    }
                }
            };
            return HubFunction(info, request, "v1/card/creation/result");
        }

        #region account/overdraft/creation/result

        public JsonResultData AccountOverdraftCreationResult(Dictionary<string, object> info)
        {
            //"data": {
            //    "conditions": {
            //        "transactionId": "2019022812005359"
            //    }
            //}
            var request = new
            {
                trace = InitCurrentTrace(),
                data = new
                {
                    conditions = new
                    {
                        transactionId = info["transactionId"]
                    }
                }
            };
            return HubFunction(info, request, "v1/account/overdraft/creation/result");
        }

        #endregion

        #region LoanCreationResult

        public JsonResultData LoanCreationResult(Dictionary<string, object> info)
        {
            var request = new
            {
                trace = InitCurrentTrace(),
                data = new
                {
                    conditions = new
                    {
                        transactionId = info["transactionId"]
                    }
                }
            };
            return HubFunction(info, request, "v1/loan/creation/result");
        }

        #endregion

        #region loan/creation

        public JsonResultData LoanCreation(Dictionary<string, object> info)
        {
            string[] email = !string.IsNullOrEmpty(info["email"].ToString()) ? info["email"].ToString().Split(',') : new string[] { "" };
            string[] mobile = !string.IsNullOrEmpty(info["mobile"].ToString()) ? info["mobile"].ToString().Split(',') : new string[] { "" };
            long totalIncomePerMonth = 0;
            long collateralAmount = 0;
            long loanAmount = 0;
            var request = new
            {
                trace = InitCurrentTrace(),
                data = new
                {
                    customer = new
                    {
                        fullname = info["customer.fullname"],
                        birthday = string.IsNullOrEmpty(info["customer.birthday"].ToString()) ? "" : info["customer.birthday"].ToString().Replace("/", ""),
                        maritalStatus = info["customer.maritalStatus"],
                        nationality = info["customer.nationality"],
                        branchCode = info["customer.branchCode"],
                        jobInfo = new
                        {
                            professionalCode = info["customer.jobInfo.professionalCode"],
                        },
                        legal = new
                        {
                            legalNumber = info["customer.legal.legalNumber"],
                            lssuedDate = string.IsNullOrEmpty(info["customer.legal.lssuedDate"].ToString()) ? "" : info["customer.legal.lssuedDate"].ToString().Replace("/", ""),
                            lssuedPlace = info["customer.legal.lssuedPlace"],
                        },
                        contactInfo = new
                        {
                            contactAddress = new
                            {
                                address1 = info["customer.contactInfo.contactAddress.address1"],
                                address2 = info["customer.contactInfo.contactAddress.address2"],
                                address3 = info["customer.contactInfo.contactAddress.address3"],
                                ward = info["customer.contactInfo.contactAddress.ward"],
                                district = info["customer.contactInfo.contactAddress.district"],
                                city = info["customer.contactInfo.contactAddress.city"],
                            },
                            permanentAddress = new
                            {
                                address1 = info["customer.contactInfo.permanentAddress.address1"],
                                address2 = info["customer.contactInfo.permanentAddress.address2"],
                                address3 = info["customer.contactInfo.permanentAddress.address3"],
                                ward = info["customer.contactInfo.permanentAddress.ward"],
                                district = info["customer.contactInfo.permanentAddress.district"],
                                city = info["customer.contactInfo.permanentAddress.city"],
                            },
                            email = email,
                            mobile = mobile
                        }
                    },
                    spouse = new
                    {
                        fullname = info["spouse.fullname"],
                        birthday = string.IsNullOrEmpty(info["spouse.birthday"].ToString()) ? "" : info["spouse.birthday"].ToString().Replace("/", ""),
                        maritalStatus = info["spouse.maritalStatus"],
                        nationality = info["spouse.nationality"],
                        jobInfo = new
                        {
                            professionalCode = info["spouse.jobInfo.professionalCode"],
                        },
                        legal = new
                        {
                            legalNumber = info["spouse.legal.legalNumber"],
                            lssuedDate = string.IsNullOrEmpty(info["spouse.legal.lssuedDate"].ToString()) ? "" : info["spouse.legal.lssuedDate"].ToString().Replace("/", ""),
                            lssuedPlace = info["spouse.legal.lssuedPlace"],
                        },
                    },
                    loan = new
                    {
                        totalIncomePerMonth = long.TryParse(info["loan.totalIncomePerMonth"].ToString(), out totalIncomePerMonth) ? totalIncomePerMonth : 0,
                        realEstateProject = info["loan.realEstateProject"],
                        collateralAmount = long.TryParse(info["loan.collateralAmount"].ToString(), out collateralAmount) ? collateralAmount : 0,
                        loanAmount = long.TryParse(info["loan.loanAmount"].ToString(), out loanAmount) ? loanAmount : 0,
                    }
                }
            };

            return HubFunction(info, request, "v1/loan/creation");
        }

        #endregion


        #endregion

        #region v1/payment/salary/excuteBatchPayment

        public JsonResultData OcbSalaryExcuteBatchPayment(Dictionary<string, object> info)
        {
            var request = new
            {
                trace = InitCurrentTrace(),
                data = new
                {
                    batchPayment = new
                    {
                        batchPaymentId = info["batchPaymentId"]
                    }
                }
            };
            return HubFunction(info, request, "v1/payment/salary/excuteBatchPayment");
        }

        #endregion

        #region payment/selectPartnerServiceCode

        //payment/selectPartnerServiceCode
        public JsonResultData selectPartnerServiceCode(DataCertificate info)
        {

            var ResultFunct = new JsonResultData();
            try
            {
                var authentoken = LoginUser.Current.Authentication;
                var clientId = !string.IsNullOrEmpty(info.ClientId) ? info.ClientId : LoginUser.Current.ClientId;
                var clientSecret = !string.IsNullOrEmpty(info.ClientSecret) ? info.ClientSecret : LoginUser.Current.ClientSecret;
                var certificate = !string.IsNullOrEmpty(info.Certificate) ? info.ClientId : LoginUser.Current.Certificate;
                authentoken = AuthenticateData(clientId, clientSecret, certificate);
                var request = new RequestBodyModelDataNull
                {
                    trace = InitCurrentTrace()
                };
                int status = -1;
                string Msg = string.Empty;
                object Err = new object();
                var responseConnect = APIHelper.CallAPI<ResponseBodyModel<dataresponsedata<string>>>(ref status, ref Msg, ref Err, "v1/payment/selectPartnerServiceCode", "POST", 0, "application/json", null, request, authentoken, clientId, clientSecret, info.PrivateKey, certificate);

                if (status == 0)
                {
                    ResultFunct.IsOk = false;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataErr = Err;
                }
                else
                {
                    var resultdata = responseConnect.data.serviceList;
                    ResultFunct.IsOk = true;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataObj = resultdata;

                }
                return ResultFunct;
            }
            catch (Exception ex)
            {
                ResultFunct.IsOk = false;
                ResultFunct.Msg = ex.ToString();
                return ResultFunct;
            }
        }

        #endregion

        #region branch/locator

        //branch/locator
        public JsonResultData BranchLocator(DataCertificate info)
        {

            var ResultFunct = new JsonResultData();
            try
            {
                var authentoken = LoginUser.Current.Authentication;
                var clientId = !string.IsNullOrEmpty(info.ClientId) ? info.ClientId : LoginUser.Current.ClientId;
                var clientSecret = !string.IsNullOrEmpty(info.ClientSecret) ? info.ClientSecret : LoginUser.Current.ClientSecret;
                var certificate = !string.IsNullOrEmpty(info.Certificate) ? info.ClientId : LoginUser.Current.Certificate;
                authentoken = AuthenticateData(clientId, clientSecret, certificate);
                var request = new RequestBodyModelDataNull
                {
                    trace = InitCurrentTrace()
                };
                int status = -1;
                string Msg = string.Empty;
                object Err = new object();
                var responseConnect = APIHelper.CallAPI<ResponseBodyModel<dataresponsedata<string>>>(ref status, ref Msg, ref Err, "v1/branch/locator", "POST", 0, "application/json", null, request, authentoken, clientId, clientSecret, info.PrivateKey, certificate);

                if (status == 0)
                {
                    ResultFunct.IsOk = false;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataErr = Err;
                }
                else
                {
                    var resultdata = responseConnect.data.branchDetailList;
                    ResultFunct.IsOk = true;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataObj = resultdata;

                }
                return ResultFunct;
            }
            catch (Exception ex)
            {
                ResultFunct.IsOk = false;
                ResultFunct.Msg = ex.ToString();
                return ResultFunct;
            }
        }

        #endregion


        #region customer/creation/result
        //customer/creation/result
        public JsonResultData CustomerCreationResult(Dictionary<string, object> info)
        {
            //"data": {
            //    "conditions": {
            //        "transactionId": "2019022812005359"
            //    }
            //}
            var request = new
            {
                trace = InitCurrentTrace(),
                data = new
                {
                    conditions = new
                    {
                        transactionId = info["transactionId"]
                    }
                }
            };
            return HubFunction(info, request, "v1/customer/creation/result");
        }
        #endregion

        #region v1/payment/salary/initBatchPayment

        public JsonResultData PaymentSalaryInitBatchPayment(Dictionary<string, object> info)
        {
            var batchPayment = new List<BatchItem>();
            int countBatch = 0;
            bool ContentBatch = info.ContainsKey("batchPayment[" + countBatch.ToString() + "][payeeAccountNumber]");
            while (ContentBatch)
            {
                var batch = new BatchItem();
                batch.payeeAccountNumber = info["batchPayment[" + countBatch.ToString() + "][payeeAccountNumber]"].ToString();
                batch.payeeName = info["batchPayment[" + countBatch.ToString() + "][payeeName]"].ToString();
                batch.paymentDescription = info["batchPayment[" + countBatch.ToString() + "][paymentDescription]"].ToString();
                batch.amount = Convert.ToInt64(info["batchPayment[" + countBatch.ToString() + "][amount]"].ToString());
                batchPayment.Add(batch);
                countBatch++;
                ContentBatch = info.ContainsKey("batchPayment[" + countBatch.ToString() + "][payeeAccountNumber]");
            }
            var request = new
            {
                trace = InitCurrentTrace(),
                data = new
                {
                    batchPayment = new
                    {
                        //currency = info["currency"].ToString(),
                        sourceAccountNumber = info["sourceAccountNumber"].ToString(),
                        sourceSubAccountNumber = info["sourceSubAccountNumber"].ToString(),
                        batchItem = batchPayment
                    },
                }
            };
            return HubFunction(info, request, "v1/payment/salary/initBatchPayment");
        }

        #endregion

        #region depositAccounts

        //depositAccounts
        public JsonResultData depositAccounts(DataCertificate info)
        {

            var ResultFunct = new JsonResultData();
            try
            {
                var authentoken = LoginUser.Current.Authentication;
                var clientId = !string.IsNullOrEmpty(info.ClientId) ? info.ClientId : LoginUser.Current.ClientId;
                var clientSecret = !string.IsNullOrEmpty(info.ClientSecret) ? info.ClientSecret : LoginUser.Current.ClientSecret;
                var certificate = !string.IsNullOrEmpty(info.Certificate) ? info.ClientId : LoginUser.Current.Certificate;
                authentoken = AuthenticateData(clientId, clientSecret, certificate);
                var request = new RequestBodyModelDataNull
                {
                    trace = InitCurrentTrace(),
                };
                int status = -1;
                string Msg = string.Empty;
                object Err = new object();
                var responseConnect = APIHelper.CallAPI<ResponseBodyModel<dataresponsedata<string>>>(ref status, ref Msg, ref Err, "v1/depositAccounts", "POST", 0, "application/json", null, request, authentoken, clientId, clientSecret, info.PrivateKey, certificate);

                if (status == 0)
                {
                    ResultFunct.IsOk = false;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataErr = Err;
                }
                else
                {
                    var resultdata = responseConnect.data.accountList;
                    ResultFunct.IsOk = true;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataObj = resultdata;

                }
                return ResultFunct;
            }
            catch (Exception ex)
            {
                ResultFunct.IsOk = false;
                ResultFunct.Msg = ex.ToString();
                return ResultFunct;
            }
        }

        #endregion

        #region topup/transactions

        //topup/transactions
        public JsonResultData TopupTransactions(conditionsViewModel info)
        {

            var ResultFunct = new JsonResultData();
            try
            {
                var authentoken = LoginUser.Current.Authentication;
                var clientId = !string.IsNullOrEmpty(info.ClientId) ? info.ClientId : LoginUser.Current.ClientId;
                var clientSecret = !string.IsNullOrEmpty(info.ClientSecret) ? info.ClientSecret : LoginUser.Current.ClientSecret;
                var certificate = !string.IsNullOrEmpty(info.Certificate) ? info.ClientId : LoginUser.Current.Certificate;
                authentoken = AuthenticateData(clientId, clientSecret, certificate);
                var request = new
                {
                    trace = InitCurrentTrace(),
                    data = new
                    {
                        conditions = new
                        {
                            transactionTime = new
                            {
                                fromDate = info.fromDate.HasValue ? info.fromDate.Value.ToString("yyyyMMdd") : DateTime.Now.AddDays(-30).ToString("yyyyMMdd"),
                                toDate = info.toDate.HasValue ? info.toDate.Value.ToString("yyyyMMdd") : DateTime.Now.ToString("yyyyMMdd"),
                            },
                        }

                    }
                };
                int status = -1;
                string Msg = string.Empty;
                object Err = new object();
                var responseConnect = APIHelper.CallAPI2(ref status, ref Msg, ref Err, "v1/topup/transactions", "POST", 0, "application/json", null, request, authentoken, clientId, clientSecret, info.PrivateKey, certificate);

                if (status == 0)
                {
                    ResultFunct.IsOk = false;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataErr = Err;
                }
                else
                {
                    var resultdata = responseConnect;
                    ResultFunct.IsOk = true;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataObj = resultdata;

                }
                return ResultFunct;
            }
            catch (Exception ex)
            {
                ResultFunct.IsOk = false;
                ResultFunct.Msg = ex.ToString();
                return ResultFunct;
            }
        }

        #endregion

        #region customerInfo

        //customerInfo
        public JsonResultData customerInfo(DataCertificate info)
        {

            var ResultFunct = new JsonResultData();
            try
            {
                var authentoken = LoginUser.Current.Authentication;
                var clientId = !string.IsNullOrEmpty(info.ClientId) ? info.ClientId : LoginUser.Current.ClientId;
                var clientSecret = !string.IsNullOrEmpty(info.ClientSecret) ? info.ClientSecret : LoginUser.Current.ClientSecret;
                var certificate = !string.IsNullOrEmpty(info.Certificate) ? info.ClientId : LoginUser.Current.Certificate;
                authentoken = AuthenticateData(clientId, clientSecret, certificate);
                var request = new RequestBodyModelDataNull
                {
                    trace = InitCurrentTrace()
                };
                int status = -1;
                string Msg = string.Empty;
                object Err = new object();
                var responseConnect = APIHelper.CallAPI<ResponseBodyModel<dataresponseCustomer>>(ref status, ref Msg, ref Err, "v1/customerInfo", "POST", 0, "application/json", null, request, authentoken, clientId, clientSecret, info.PrivateKey, certificate);

                if (status == 0)
                {
                    ResultFunct.IsOk = false;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataErr = Err;
                }
                else
                {
                    var resultdata = responseConnect.data;
                    ResultFunct.IsOk = true;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataObj = resultdata;

                }
                return ResultFunct;
            }
            catch (Exception ex)
            {
                ResultFunct.IsOk = false;
                ResultFunct.Msg = ex.ToString();
                return ResultFunct;
            }
        }

        #endregion


        #region card/creation

        //card/creation
        public JsonResultData CardCreation(customerCardViewModel info)
        {

            var ResultFunct = new JsonResultData();
            try
            {
                var authentoken = LoginUser.Current.Authentication;
                var clientId = !string.IsNullOrEmpty(info.ClientId) ? info.ClientId : LoginUser.Current.ClientId;
                var clientSecret = !string.IsNullOrEmpty(info.ClientSecret) ? info.ClientSecret : LoginUser.Current.ClientSecret;
                var certificate = !string.IsNullOrEmpty(info.Certificate) ? info.ClientId : LoginUser.Current.Certificate;
                authentoken = AuthenticateData(clientId, clientSecret, certificate);
                var request = new
                {
                    trace = InitCurrentTrace(),
                    data = new
                    {
                        customer = ConvertHelper.Current.ConvertNullStringEmpty(ConvertHelper.Current.ConvertObjToObj<customerCard, customerCardViewModel>(info))
                    }
                };
                request.data.customer.birthday = string.IsNullOrEmpty(request.data.customer.birthday) ? "" : request.data.customer.birthday.Replace("/", "");
                request.data.customer.legal.lssuedDate = string.IsNullOrEmpty(request.data.customer.legal.lssuedDate) ? "" : request.data.customer.legal.lssuedDate.Replace("/", "");
                int status = -1;
                string Msg = string.Empty;
                object Err = new object();
                var responseConnect = APIHelper.CallAPI<ResponseBodyModel<dataresponsedata<string>>>(ref status, ref Msg, ref Err, "v1/card/creation", "POST", 0, "application/json", null, request, authentoken, clientId, clientSecret, info.PrivateKey, certificate);

                if (status == 0)
                {
                    ResultFunct.IsOk = false;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataErr = Err;
                }
                else
                {
                    var resultdata = responseConnect;
                    ResultFunct.IsOk = true;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataObj = resultdata;

                }
                return ResultFunct;
            }
            catch (Exception ex)
            {
                ResultFunct.IsOk = false;
                ResultFunct.Msg = ex.ToString();
                return ResultFunct;
            }
        }


        #endregion

        #region account/overdraft/creation

        //account/overdraft/creation
        public JsonResultData AccountOverdraftCreation(customerViewModel info)
        {

            var ResultFunct = new JsonResultData();
            try
            {
                var authentoken = LoginUser.Current.Authentication;
                var clientId = !string.IsNullOrEmpty(info.ClientId) ? info.ClientId : LoginUser.Current.ClientId;
                var clientSecret = !string.IsNullOrEmpty(info.ClientSecret) ? info.ClientSecret : LoginUser.Current.ClientSecret;
                var certificate = !string.IsNullOrEmpty(info.Certificate) ? info.ClientId : LoginUser.Current.Certificate;
                authentoken = AuthenticateData(clientId, clientSecret, certificate);
                var request = new RequestBodyModel<customerHub>
                {
                    trace = InitCurrentTrace(),
                    data = new customerHub
                    {
                        customer = ConvertHelper.Current.ConvertNullStringEmpty(ConvertHelper.Current.ConvertObjToObj<customer, customerViewModel>(info))
                    }
                };
                request.data.customer.birthday = string.IsNullOrEmpty(request.data.customer.birthday) ? "" : request.data.customer.birthday.Replace("/", "");
                request.data.customer.legal.lssuedDate = string.IsNullOrEmpty(request.data.customer.legal.lssuedDate) ? "" : request.data.customer.legal.lssuedDate.Replace("/", "");
                int status = -1;
                string Msg = string.Empty;
                object Err = new object();
                var responseConnect = APIHelper.CallAPI<ResponseBodyModel<dataresponsedata<string>>>(ref status, ref Msg, ref Err, "v1/account/overdraft/creation", "POST", 0, "application/json", null, request, authentoken, clientId, clientSecret, info.PrivateKey, certificate);

                if (status == 0)
                {
                    ResultFunct.IsOk = false;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataErr = Err;
                }
                else
                {
                    var resultdata = responseConnect;
                    ResultFunct.IsOk = true;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataObj = resultdata;

                }
                return ResultFunct;
            }
            catch (Exception ex)
            {
                ResultFunct.IsOk = false;
                ResultFunct.Msg = ex.ToString();
                return ResultFunct;
            }
        }

        #endregion


        #region customer/creation

        //customer/creation
        public JsonResultData CustomerCreation(customerViewModel info)
        {

            var ResultFunct = new JsonResultData();
            try
            {
                var authentoken = LoginUser.Current.Authentication;
                var clientId = !string.IsNullOrEmpty(info.ClientId) ? info.ClientId : LoginUser.Current.ClientId;
                var clientSecret = !string.IsNullOrEmpty(info.ClientSecret) ? info.ClientSecret : LoginUser.Current.ClientSecret;
                var certificate = !string.IsNullOrEmpty(info.Certificate) ? info.ClientId : LoginUser.Current.Certificate;
                authentoken = AuthenticateData(clientId, clientSecret, certificate);
                var request = new RequestBodyModel<customerHub>
                {
                    trace = InitCurrentTrace(),
                    data = new customerHub
                    {
                        customer = ConvertHelper.Current.ConvertNullStringEmpty(ConvertHelper.Current.ConvertObjToObj<customer, customerViewModel>(info))
                    }
                };
                request.data.customer.birthday = string.IsNullOrEmpty(request.data.customer.birthday) ? "" : request.data.customer.birthday.Replace("/", "");
                request.data.customer.legal.lssuedDate = string.IsNullOrEmpty(request.data.customer.legal.lssuedDate) ? "" : request.data.customer.legal.lssuedDate.Replace("/", "");
                int status = -1;
                string Msg = string.Empty;
                object Err = new object();
                var responseConnect = APIHelper.CallAPI<ResponseBodyModel<dataresponsedata<string>>>(ref status, ref Msg, ref Err, "v1/customer/creation", "POST", 0, "application/json", null, request, authentoken, clientId, clientSecret, info.PrivateKey, certificate);

                if (status == 0)
                {
                    ResultFunct.IsOk = false;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataErr = Err;
                }
                else
                {
                    var resultdata = responseConnect;
                    ResultFunct.IsOk = true;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataObj = resultdata;

                }
                return ResultFunct;
            }
            catch (Exception ex)
            {
                ResultFunct.IsOk = false;
                ResultFunct.Msg = ex.ToString();
                return ResultFunct;
            }
        }

        #endregion

        #region atms/locator

        //atms/locator
        public JsonResultData AtmsLocator(DataCertificate info)
        {

            var ResultFunct = new JsonResultData();
            try
            {
                var authentoken = LoginUser.Current.Authentication;
                var clientId = !string.IsNullOrEmpty(info.ClientId) ? info.ClientId : LoginUser.Current.ClientId;
                var clientSecret = !string.IsNullOrEmpty(info.ClientSecret) ? info.ClientSecret : LoginUser.Current.ClientSecret;
                var certificate = !string.IsNullOrEmpty(info.Certificate) ? info.ClientId : LoginUser.Current.Certificate;
                authentoken = AuthenticateData(clientId, clientSecret, certificate);
                var request = new RequestBodyModelDataNull
                {
                    trace = InitCurrentTrace()
                };
                int status = -1;
                string Msg = string.Empty;
                object Err = new object();
                var responseConnect = APIHelper.CallAPI<ResponseBodyModel<dataresponsedata<string>>>(ref status, ref Msg, ref Err, "v1/atms/locator", "POST", 0, "application/json", null, request, authentoken, clientId, clientSecret, info.PrivateKey, certificate);

                if (status == 0)
                {
                    ResultFunct.IsOk = false;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataErr = Err;
                }
                else
                {
                    var resultdata = responseConnect.data.bankATMList;
                    ResultFunct.IsOk = true;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataObj = resultdata;

                }
                return ResultFunct;
            }
            catch (Exception ex)
            {
                ResultFunct.IsOk = false;
                ResultFunct.Msg = ex.ToString();
                return ResultFunct;
            }
        }

        #endregion

        #region account/creation

        //account/creation
        public JsonResultData AccountCreation(customerViewModel info)
        {

            var ResultFunct = new JsonResultData();
            try
            {
                var authentoken = LoginUser.Current.Authentication;
                var clientId = !string.IsNullOrEmpty(info.ClientId) ? info.ClientId : LoginUser.Current.ClientId;
                var clientSecret = !string.IsNullOrEmpty(info.ClientSecret) ? info.ClientSecret : LoginUser.Current.ClientSecret;
                var certificate = !string.IsNullOrEmpty(info.Certificate) ? info.ClientId : LoginUser.Current.Certificate;
                authentoken = AuthenticateData(clientId, clientSecret, certificate);
                var request = new RequestBodyModel<customerHub>
                {
                    trace = InitCurrentTrace(),
                    data = new customerHub
                    {
                        customer = ConvertHelper.Current.ConvertNullStringEmpty(ConvertHelper.Current.ConvertObjToObj<customer, customerViewModel>(info))
                    }
                };
                request.data.customer.birthday = string.IsNullOrEmpty(request.data.customer.birthday) ? "" : request.data.customer.birthday.Replace("/", "");
                request.data.customer.legal.lssuedDate = string.IsNullOrEmpty(request.data.customer.legal.lssuedDate) ? "" : request.data.customer.legal.lssuedDate.Replace("/", "");
                int status = -1;
                string Msg = string.Empty;
                object Err = new object();
                var responseConnect = APIHelper.CallAPI<ResponseBodyModel<dataresponsedata<string>>>(ref status, ref Msg, ref Err, "v1/account/creation", "POST", 0, "application/json", null, request, authentoken, clientId, clientSecret, info.PrivateKey, certificate);

                if (status == 0)
                {
                    ResultFunct.IsOk = false;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataErr = Err;
                }
                else
                {
                    var resultdata = responseConnect;
                    ResultFunct.IsOk = true;
                    ResultFunct.Msg = Msg;
                    ResultFunct.dataObj = resultdata;

                }
                return ResultFunct;
            }
            catch (Exception ex)
            {
                ResultFunct.IsOk = false;
                ResultFunct.Msg = ex.ToString();
                return ResultFunct;
            }
        }

        #endregion



        #region Init Data Test

        public List<objaccount> InitTestData()
        {
            return new List<objaccount>
            {
               new objaccount{
                account = new account{
                    accountNumber = "12345",
                    accountShortName = "OCB1",
                    availableBalance = "1000000",
                    balance = "1000000",
                    currency = "VND",
                    interestRate = "1",
                    lastTransactionBookingDate = "20190321",
                    lockedAmount = "0",
                    postingRestriction = "",
                    productType = "TEST"
                }
               },
               new objaccount{
                account = new account{
                    accountNumber = "12345",
                    accountShortName = "OCB1",
                    availableBalance = "1000000",
                    balance = "1000000",
                    currency = "VND",
                    interestRate = "1",
                    lastTransactionBookingDate = "20190321",
                    lockedAmount = "0",
                    postingRestriction = "",
                    productType = "TEST"
                }
               },
               new objaccount{
                account = new account{
                    accountNumber = "12345",
                    accountShortName = "OCB1",
                    availableBalance = "1000000",
                    balance = "1000000",
                    currency = "VND",
                    interestRate = "1",
                    lastTransactionBookingDate = "20190321",
                    lockedAmount = "0",
                    postingRestriction = "",
                    productType = "TEST"
                }
               },
            };
        }

        public LoginModel InitLoginUser()
        {
            /*
                <add key="Client:Id" value="778df20d1b12eadddab2369e9b62e3ae"/>
                <add key="Client:Secret" value="2e6a07102b05606e730e8b78b6858fa1"/>
             */
            var user = new LoginModel
            {
                Username = "admin",
                Password = "admin",
                Authentication = "AAIgZWE2NmUzYzFlNzlmODI0ODA4N2UwY2VlNzJjMmJkZGWQc_fZnFW9_xMEdhv2_0uz_tpg9TN9VLnwyTPJuK_-DYk_0KowGgk3BBrSzrHWw0zxpuqOK_WcmamWeodjwlud",
                ClientId = "778df20d1b12eadddab2369e9b62e3ae",
                ClientSecret = "2e6a07102b05606e730e8b78b6858fa1"
            };
            return user;
            //Session[Constants.LoginSession] = user;
        }

        #endregion


    }
}