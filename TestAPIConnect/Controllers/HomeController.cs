using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestAPIConnect.Common;
using TestAPIConnect.Models;
using TestAPIConnect.Services;

namespace TestAPIConnect.Controllers
{
    public class HomeController : Controller
    {

        public void InitUser()
        {
            LoginModel user = new LoginModel
            {
                apiTokenUrl = "https://api-sit.ocb.com.vn/ocbsit/developerhub/ocb-sit-oauth/oauth2/token",
                apiUrl = "https://api-sit.ocb.com.vn/ocbsit/developerhub/",
                Certificate = "MIIEOzCCAyOgAwIBAgIJAJTFvdpeKAEDMA0GCSqGSIb3DQEBCwUAMIGuMQswCQYDVQQGEwJWTjEPMA0GA1UECAwGSGEgTm9pMQ8wDQYDVQQHDAZIYSBOb2kxEjAQBgNVBAoMCVNlYXRlY2hpdDEnMCUGA1UECwweQ09ORyBOR0hFIFRIT05HIFRJTiBEQU5HIE5BTSBBMRkwFwYDVQQDDBBzZWF0ZWNoaXQuY29tLnZuMSUwIwYJKoZIhvcNAQkBFhZ0YW5kbkBzZWF0ZWNoaXQuY29tLnZuMB4XDTE5MDYwNTA5NDAwOVoXDTI0MDYwMzA5NDAwOVowgZQxCzAJBgNVBAYTAlZOMQwwCgYDVQQIDANIQ00xDDAKBgNVBAcMA0hDTTEMMAoGA1UECgwDT0NCMSMwIQYDVQQLDBpOR0FOIEhBTkcgQ1BUTSBQSFVPTkcgRE9ORzETMBEGA1UEAwwKb2NiLmNvbS52bjEhMB8GCSqGSIb3DQEJARYSdGFuZG5nb2NAZ21haWwuY29tMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAo//zbAPzdwZow0+x6tf/O6ilctGFXm7XZmOPJwKSXEgMc/vud8sRhztK9uBhFBmDykA9Vky0twmoLLqkSz0aU351OAyEiNttX2ykYzJWsUXn9R9qK7BkMlBX8ork+BPGNgHjdJKJ77fjL8VEXStfINx/QT3Ox+QQLAstMSfIPPHAmWFNvki64uUQlffCClbNMVXPhwZncqTBh/ZMv3HPXixgORDNwyv5n9oxd8CTb1y0ezcg3Nv7xcSwSJPfyFw0IEM1wYQAg13KyIKv4wIfH/y3V4bslgME6xXQGwy/pwTwwR7nSKsdOC5JFlorPAgjLjtqlse9ub0/Cr2bAjtRUwIDAQABo3QwcjAfBgNVHSMEGDAWgBSMiEw3M7yeSzYZ6fF4W9SamxzuWzAJBgNVHRMEAjAAMAsGA1UdDwQEAwIE8DA3BgNVHREEMDAughJhcGktc2l0Lm9jYi5jb20udm6CGGFwaS1nd2FkbS1zaXQub2NiLmNvbS52bjANBgkqhkiG9w0BAQsFAAOCAQEARgrxqLK5YN7DtWAush5RY/ebDCekxu/8O90nwnaIiFdY9ShdfAFmvtCs1X+siBDpxx3Co2bhYKuaXT42X0Wuu/fVWOEpD2+18w3Sj22n6uZTq4vCZfvdX7N1nuSY6YBHvvqu6/A1eS0IEalEqYobp2CyF4AazP5O3u7dqQaElusqmddioDvVDiJcMbNkdOuQec4NMX6/u4lQaYk4RF0JoCF1Sa22lsWOCIc/HEGaeAlaLgRCFhPA+Vk0f54tOi+yF9zRnjYZO8HL9sT6d0UETcNuh5QUvfcK4Rhhq/oHsN6rvkA8Y3MYkvxWal1WpyTPlnUdfjrepzKID6jMYIFxIA==",
                PrivateKey = "MIIEpAIBAAKCAQEAo//zbAPzdwZow0+x6tf/O6ilctGFXm7XZmOPJwKSXEgMc/vud8sRhztK9uBhFBmDykA9Vky0twmoLLqkSz0aU351OAyEiNttX2ykYzJWsUXn9R9qK7BkMlBX8ork+BPGNgHjdJKJ77fjL8VEXStfINx/QT3Ox+QQLAstMSfIPPHAmWFNvki64uUQlffCClbNMVXPhwZncqTBh/ZMv3HPXixgORDNwyv5n9oxd8CTb1y0ezcg3Nv7xcSwSJPfyFw0IEM1wYQAg13KyIKv4wIfH/y3V4bslgME6xXQGwy/pwTwwR7nSKsdOC5JFlorPAgjLjtqlse9ub0/Cr2bAjtRUwIDAQABAoIBAQCVRKdIjygQE7NS4bysZcCXil5cbTuYwgYn2UI4XWzdtW4wOwPH4PqpPVxz67IwWzDK60FoxRRO7Ok3HQHgwVKu4BDM3QfckOuxyO6uouipHVmMj/VQopHwAZSq26Sf70+fZISkW6RUneiYWFJrAsjo3gitVxZYdcoKbHnLncvxOxvMW6BDO9vjZ7CNrRs4YSYtUrfn+RIemYOPJf0BzmZPJFCdYp5Uf8tYChrj/vcMjIsXAZ/dtgid01ZC9K/RMloqzJsrbNnIfQbjmxj3oQuDdW+ozOMnlZwKzjaDoj4S92HudRz3J1X03XYKTnJ4qeQzHWhMkOBSKuBcFY8HwJmBAoGBANMANK6FRelCYrY1HbFgIHCQ02uFgal9YX1Smpd0xNF8aVZcM8cCUXt18rZ8UdHanZeTXpdHbIraekTctg4hmYzUjEgbf9Ubp+DsYzMGm8O4l3A3hx9b9PpC3zeM6Fj9uoPtKFI5Cej7t8YS55N81VckUzatrFEnOu9OaJWK6P8zAoGBAMb5rhL98IOkL0h1eqyEFVJpJH3UlruBLrD7qZxH0ez1FsmvmQnY3ot8YDx+mtYHXv0qiSvqeLWy0hjbOTYyqRhIYYGktiZaMmSH+KvPHNgIYcD7iNpiE3NmCLza6K3QARN5tnvgTuA4kY3fWjIFKzShuS926JJ9Qo07TFSFruVhAoGBAJz12jq5CXir2aKRgLUiPP9/vMaPWhUrIAqKGFXylzb+xZ1omVvBbbvZ0ePON09UwUawaf0/NI9WVv5C8Wsxs3f/5Rr+2ek92XSIZILgt56xAnaH2AyL64D/ne1E9NK+bLEXCpeftq+KEPtXtM0SX+GjNAPIzhbQiBbczQ/xdcHhAoGAB8Z//+v+dxZ2Zo14sr8imirTqzsgfMlKis36zcmcsXbOYilDgLgB0k+U7yg/Yre9BYWhAJ9UAj2vqhr+/Fg0dWd2r/tAxvTlXTpXBFe+l86UC1eI/IeynOLS2pZvW0Nyl1E9SU/1pRtwzKt6udOr4Y2kT++EnRzZ+ezkSbVDpWECgYBkPKnepwJ1xHQ5cB9lj0LbX6tGnkTnOf/dxkAJfM3d65xCKMhuSYR0drgzYzcjbLMx7L8fKp3yroQVCQh8vCziFsnNx1gf8tjInFnL7q0JxxnipehWnmVIs7XtoXtpK8e+ZFRS/xMelhxBmaqAao8iV6Kf8q8WDSnhOPXPb2hbAg==",
                ClientId = "f9f601eac7bd8310360a79051abc333b",
                ClientSecret = "42b97a6246555a2b642a5522c2a9b720",
                Scope = "OCBSIT",
                Username = "jka3375",
                Password = "hpp4481"

            };
            var loginresult = ServiceAPI.Current.LoginAccess(user);
            if (loginresult.IsOk)
            {
                user.Authentication = ((tokenresponse)loginresult.dataObj).access_token;
                Session[Constants.LoginSession] = user;
            }
        }

        #region Account Page

        [CommonAuthen]
        public ActionResult Index()
        {
            //InitUser();
            ViewBag.Account = LoginUser.Current;
            return View();
        }

        [HttpPost]
        [CommonAction]
        public JsonResult GetListAccounts(DataCertificate info)
        {
            try
            {
                return Json(ServiceAPI.Current.GetListAccounts(info), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonResultData
                {
                    IsOk = false,
                    Msg = ex.ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Payment Page

        [CommonAuthen]
        public ActionResult Payments(string accountnumber)
        {
            ViewBag.AccountNumber = accountnumber;
            return View();
        }

        [HttpPost]
        [CommonAction]
        public JsonResult ProcessPayment(externalTransferViewModel info)
        {
            try
            {
                return Json(ServiceAPI.Current.ProcessPayment(info), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonResultData
                {
                    IsOk = false,
                    Msg = ex.ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Generate Signature

        public ActionResult GenerateSignature()
        {
            return View();
        }

        public ActionResult TestSign()
        {
            return View();
        }

        [HttpPost]
        public JsonResult WriteSignature(certificate datasign)
        {
            try
            {
                return Json(ServiceAPI.Current.WriteSignature(datasign), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonResultData
                {
                    IsOk = false,
                    Msg = ex.ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ProcessGenerate(certificate datasign)
        {
            try
            {
                return Json(ServiceAPI.Current.ProcessGenerate(datasign), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonResultData
                {
                    IsOk = false,
                    Msg = ex.ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Transaction Page

        [CommonAuthen]
        public ActionResult DetailTransaction(string accountnumber)
        {
            ViewBag.AccountNumber = accountnumber;
            return View();
        }

        [HttpPost]
        [CommonAction]
        public JsonResult GetAllTransactions(inputSearchTrans info)
        {
            try
            {
                return Json(ServiceAPI.Current.GetAllTransactions(info), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonResultData
                {
                    IsOk = false,
                    Msg = ex.ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion


        #region Internal Transfer

        [CommonAuthen]
        public ActionResult InternalTransfer()
        {
            return View();
        }

        [HttpPost]
        [CommonAction]
        public JsonResult ProcessInternalPayment(internalTransferViewModel info)
        {
            try
            {
                return Json(ServiceAPI.Current.ProcessInternalPayment(info), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonResultData
                {
                    IsOk = false,
                    Msg = ex.ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Create Deposit Account

        [CommonAuthen]
        public ActionResult DepositAccount()
        {
            return View();
        }

        [HttpPost]
        [CommonAction]
        public JsonResult CreateDepositAccount(depositInfoViewModel info)
        {
            try
            {
                return Json(ServiceAPI.Current.createDepositAccount(info), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonResultData
                {
                    IsOk = false,
                    Msg = ex.ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Bill Payment

        [CommonAuthen]
        public ActionResult BillPayment()
        {
            return View();
        }

        [HttpPost]
        [CommonAction]
        public JsonResult ProcessBillPayment(billPaymentInfoViewModel info)
        {
            try
            {
                if (!string.IsNullOrEmpty(info.billCodeItemString))
                {
                    string[] dataSplit = info.billCodeItemString.Split(',');
                    info.billCodeItemNo = dataSplit;
                }
                return Json(ServiceAPI.Current.ProcessBillPayment(info), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonResultData
                {
                    IsOk = false,
                    Msg = ex.ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Partner Provider Code

        [CommonAuthen]
        public ActionResult PartnerProviderCode()
        {
            return View();
        }

        [HttpPost]
        [CommonAction]
        public JsonResult GetPartnerProviderCode(PartnerProviderCodeViewModel info)
        {
            try
            {
                return Json(ServiceAPI.Current.PartnerProviderCode(info), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonResultData
                {
                    IsOk = false,
                    Msg = ex.ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region selectPartnerServiceCode

        [CommonAuthen]
        public ActionResult selectPartnerServiceCode()
        {
            return View();
        }

        [HttpPost]
        [CommonAction]
        public JsonResult selectPartnerServiceCode(DataCertificate info)
        {
            try
            {
                return Json(ServiceAPI.Current.selectPartnerServiceCode(info), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonResultData
                {
                    IsOk = false,
                    Msg = ex.ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [CommonAction]
        public JsonResult selectPartnerServiceCodeBuildCombobox(DataCertificate info)
        {
            try
            {
                var result = ServiceAPI.Current.selectPartnerServiceCode(info);
                if (!result.IsOk)
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (result.dataObj == null)
                {
                    string rs = CommonBuildString<service>.Current.BuildTextbox("serviceCode", "serviceCode", "", new List<string[]> { new string[2] { "class", "form-control" } });
                    return Json(new { IsOk = true, dataObj = rs }, JsonRequestBehavior.AllowGet);
                }
                var svList = (serviceList)result.dataObj;
                if (svList.service == null)
                {
                    string rs = CommonBuildString<service>.Current.BuildTextbox("serviceCode", "serviceCode", "", new List<string[]> { new string[2] { "class", "form-control" } });
                    return Json(new { IsOk = true, dataObj = rs }, JsonRequestBehavior.AllowGet);
                }
                string resultString = CommonBuildString<service>.Current.BuildCombobox(svList.service, "serviceCode", "serviceCode", "serviceCode", "serviceName", new List<string[]> { new string[2] { "class", "form-control" } });
                return Json(new { IsOk = true, dataObj = resultString }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonResultData
                {
                    IsOk = false,
                    Msg = ex.ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [CommonAction]
        public JsonResult selectPartnerProviderCodeBuildCombobox(PartnerProviderCodeViewModel info, string extraparam = "")
        {
            try
            {
                var result = ServiceAPI.Current.PartnerProviderCode(info);
                string attrName = string.IsNullOrEmpty(extraparam) ? "providerCode" : extraparam;
                if (!result.IsOk)
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (result.dataObj == null)
                {
                    string rs = CommonBuildString<serviceProvider>.Current.BuildTextbox(attrName, attrName, "", new List<string[]> { new string[2] { "class", "form-control" } });
                    return Json(new { IsOk = true, dataObj = rs }, JsonRequestBehavior.AllowGet);
                }
                var svList = (serviceProviderList)result.dataObj;
                if (svList.serviceProvider == null)
                {
                    string rs = CommonBuildString<serviceProvider>.Current.BuildTextbox(attrName, attrName, "", new List<string[]> { new string[2] { "class", "form-control" } });
                    return Json(new { IsOk = true, dataObj = rs }, JsonRequestBehavior.AllowGet);
                }
                string resultString = CommonBuildString<serviceProvider>.Current.BuildCombobox(svList.serviceProvider, attrName, attrName, "providerCode", "providerName", new List<string[]> { new string[2] { "class", "form-control" } });
                return Json(new { IsOk = true, dataObj = resultString }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonResultData
                {
                    IsOk = false,
                    Msg = ex.ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region BranchLocator

        [CommonAuthen]
        public ActionResult BranchLocator()
        {
            return View();
        }

        [HttpPost]
        [CommonAction]
        public JsonResult BranchLocator(DataCertificate info)
        {
            try
            {
                return Json(ServiceAPI.Current.BranchLocator(info), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonResultData
                {
                    IsOk = false,
                    Msg = ex.ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region account/creation/result

        [CommonAuthen]
        public ActionResult AccountCreationResult()
        {
            return View();
        }

        [HttpPost]
        [CommonAction]
        public JsonResult AccountCreationResult(conditionsViewModel info)
        {
            try
            {
                return Json(ServiceAPI.Current.AccountCreationResult(info), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonResultData
                {
                    IsOk = false,
                    Msg = ex.ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        


        #region customer/creation/result

        //customer/creation/result
        [CommonAuthen]
        public ActionResult CustomerCreationResult()
        {
            return View();
        }

        [HttpPost]
        [CommonAction]
        public JsonResult CustomerCreationResult(FormCollection info)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                foreach (var item in info.AllKeys)
                {
                    data.Add(item, info[item]);
                }
                return Json(ServiceAPI.Current.CustomerCreationResult(data), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonResultData
                {
                    IsOk = false,
                    Msg = ex.ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region depositAccounts

        //depositAccounts
        [CommonAuthen]
        public ActionResult depositAccounts()
        {
            return View();
        }

        [HttpPost]
        [CommonAction]
        public JsonResult depositAccounts(DataCertificate info)
        {
            try
            {
                return Json(ServiceAPI.Current.depositAccounts(info), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonResultData
                {
                    IsOk = false,
                    Msg = ex.ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion


        #region topup/transactions

        //topup/transactions
        [CommonAuthen]
        public ActionResult TopupTransactions()
        {
            return View();
        }

        [HttpPost]
        [CommonAction]
        public JsonResult TopupTransactions(conditionsViewModel info)
        {
            try
            {
                return Json(ServiceAPI.Current.TopupTransactions(info), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonResultData
                {
                    IsOk = false,
                    Msg = ex.ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion


        #region customerInfo

        //customerInfo
        [CommonAuthen]
        public ActionResult customerInfo()
        {
            return View();
        }

        [HttpPost]
        [CommonAction]
        public JsonResult customerInfo(DataCertificate info)
        {
            try
            {
                return Json(ServiceAPI.Current.customerInfo(info), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonResultData
                {
                    IsOk = false,
                    Msg = ex.ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion


        #region card/creation

        //card/creation
        [CommonAuthen]
        public ActionResult CardCreation()
        {
            return View();
        }

        [HttpPost]
        [CommonAction]
        public JsonResult CardCreation(customerCardViewModel info)
        {
            try
            {
                return Json(ServiceAPI.Current.CardCreation(info), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonResultData
                {
                    IsOk = false,
                    Msg = ex.ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region card/creation/result

        [CommonAuthen]
        public ActionResult CardCreationResult()
        {
            return View();
        }

        [HttpPost]
        [CommonAction]
        public JsonResult CardCreationResult(FormCollection info)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                foreach(var item in info.AllKeys)
                {
                    data.Add(item, info[item]);
                }
                return Json(ServiceAPI.Current.CardCreationResult(data), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonResultData
                {
                    IsOk = false,
                    Msg = ex.ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region ocb-salary-excutebatchpayment/payment/salary/excuteBatchPayment

        [CommonAuthen]
        public ActionResult excuteBatchPayment()
        {
            return View();
        }

        [HttpPost]
        [CommonAction]
        public JsonResult excuteBatchPayment(FormCollection info)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                foreach (var item in info.AllKeys)
                {
                    data.Add(item, info[item]);
                }
                return Json(ServiceAPI.Current.OcbSalaryExcuteBatchPayment(data), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonResultData
                {
                    IsOk = false,
                    Msg = ex.ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion


        #region account/overdraft/creation

        //account/overdraft/creation
        [CommonAuthen]
        public ActionResult AccountOverdraftCreation()
        {
            return View();
        }

        [HttpPost]
        [CommonAction]
        public JsonResult AccountOverdraftCreation(customerViewModel info)
        {
            try
            {
                return Json(ServiceAPI.Current.AccountOverdraftCreation(info), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonResultData
                {
                    IsOk = false,
                    Msg = ex.ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }


        #endregion


        #region customer/creation

        //customer/creation
        [CommonAuthen]
        public ActionResult CustomerCreation()
        {
            return View();
        }

        [HttpPost]
        [CommonAction]
        public JsonResult CustomerCreation(customerViewModel info)
        {
            try
            {
                return Json(ServiceAPI.Current.CustomerCreation(info), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonResultData
                {
                    IsOk = false,
                    Msg = ex.ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion


        #region atms/locator

        //atms/locator
        [CommonAuthen]
        public ActionResult AtmsLocator()
        {
            return View();
        }

        [HttpPost]
        [CommonAction]
        public JsonResult AtmsLocator(DataCertificate info)
        {
            try
            {
                return Json(ServiceAPI.Current.AtmsLocator(info), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonResultData
                {
                    IsOk = false,
                    Msg = ex.ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion


        #region account/creation

        //account/creation
        [CommonAuthen]
        public ActionResult AccountCreation()
        {
            return View();
        }

        [HttpPost]
        [CommonAction]
        public JsonResult AccountCreation(customerViewModel info)
        {
            try
            {
                return Json(ServiceAPI.Current.AccountCreation(info), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonResultData
                {
                    IsOk = false,
                    Msg = ex.ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region fastTransfers

        [CommonAuthen]
        public ActionResult fastTransfers()
        {
            return View();
        }

        [HttpPost]
        [CommonAction]
        public JsonResult fastTransfers(fastTransferViewModel info)
        {
            try
            {
                return Json(ServiceAPI.Current.fastTransfers(info), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonResultData
                {
                    IsOk = false,
                    Msg = ex.ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region retrievePartnerBill

        [CommonAuthen]
        public ActionResult retrievePartnerBill()
        {
            return View();
        }

        [CommonAction]
        [HttpPost]
        public JsonResult retrievePartnerBill(billInfoModelView info)
        {
            try
            {
                return Json(ServiceAPI.Current.retrievePartnerBill(info), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonResultData
                {
                    IsOk = false,
                    Msg = ex.ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [CommonAction]
        [HttpPost]
        public JsonResult retrievePartnerBillExportInfo(billInfoModelView info)
        {
            try
            {
                return Json(ServiceAPI.Current.retrievePartnerBill(info), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonResultData
                {
                    IsOk = false,
                    Msg = ex.ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion


        #region AccountsDetails

        [CommonAuthen]
        public ActionResult GetAccountsDetails()
        {
            return View();
        }


        [HttpPost]
        [CommonAuthen]
        public JsonResult GetAccountsDetails(DataCertificate info)
        {
            try
            {
                return Json(ServiceAPI.Current.AccountsDetails(info), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonResultData
                {
                    IsOk = false,
                    Msg = ex.ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [CommonAuthen]
        public JsonResult GetAccountsDetailsFieldTextbox(DataCertificate info)
        {
            try
            {
                var result = ServiceAPI.Current.AccountsDetails(info);
                if (!result.IsOk)
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (result.dataObj == null)
                {
                    return Json(new { IsOk = true, dataObj = "" }, JsonRequestBehavior.AllowGet);
                }
                var accountN = (List<objaccount>)result.dataObj;
                if (accountN == null)
                {
                    return Json(new { IsOk = true, dataObj = "" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsOk = true, dataObj = accountN[0].account.accountShortName }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new JsonResultData
                {
                    IsOk = false,
                    Msg = ex.ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region payment/salary/initBatchPayment

        [CommonAuthen]
        public ActionResult initBatchPayment()
        {
            return View();
        }

        [HttpPost]
        [CommonAction]
        public JsonResult initBatchPayment(FormCollection info)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                foreach (var item in info.AllKeys)
                {
                    data.Add(item, info[item]);
                }
                return Json(ServiceAPI.Current.PaymentSalaryInitBatchPayment(data), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonResultData
                {
                    IsOk = false,
                    Msg = ex.ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region account/overdraft/creation/result

        [CommonAuthen]
        public ActionResult AccountOverdraftCreationResult()
        {
            return View();
        }

        [HttpPost]
        [CommonAction]
        public JsonResult AccountOverdraftCreationResult(FormCollection info)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                foreach (var item in info.AllKeys)
                {
                    data.Add(item, info[item]);
                }
                return Json(ServiceAPI.Current.AccountOverdraftCreationResult(data), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonResultData
                {
                    IsOk = false,
                    Msg = ex.ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
        public ActionResult AddNewBatch(string id = "")
        {
            ViewBag.CountNew = id;
            return PartialView();
        }

        #region Loan Registe

        [CommonAuthen]
        public ActionResult LoanCreation()
        {
            return View();
        }

        [HttpPost]
        [CommonAction]
        public JsonResult LoanCreation(FormCollection info)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                foreach (var item in info.AllKeys)
                {
                    data.Add(item, info[item]);
                }
                return Json(ServiceAPI.Current.LoanCreation(data), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonResultData
                {
                    IsOk = false,
                    Msg = ex.ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region LoanResult

        [CommonAuthen]
        public ActionResult LoanCreationResult()
        {
            return View();
        }

        [HttpPost]
        [CommonAction]
        public JsonResult LoanCreationResult(FormCollection info)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                foreach (var item in info.AllKeys)
                {
                    data.Add(item, info[item]);
                }
                return Json(ServiceAPI.Current.LoanCreationResult(data), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonResultData
                {
                    IsOk = false,
                    Msg = ex.ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
    }
}