using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json;
using EnumsNET;

namespace Tracker_Api.Helper
{
    public class HttpWebResponseHelper
    {
        public static IActionResult CreateBadHttpResponse(Exception e)
        {
            CustomStatusCode statusCode;
            // Here you could manage each exception indivudually
            Console.WriteLine(e.GetType().ToString());
            switch (e.GetType().ToString())
            {
                case "MIC_Library.Utilities.Helper.NullOrEmptyResponseException":
                    statusCode = CustomStatusCode.NullOrEmptyResponse;
                    break;
                case "MIC_Library.Utilities.ExceptionError.TooMuchDifferenceOnPurchaseException":
                    statusCode = CustomStatusCode.TooMuchDifferenceOnPurchase;
                    break;
                default:
                    statusCode = CustomStatusCode.Default;
                    break;
            }
            var st = new StackTrace(e, true);
            var frame = st.GetFrame(0);
            var fileName = frame?.GetFileName();
            var line = frame?.GetFileLineNumber();
            return new BadRequestObjectResult(e.Message + "||" + e.InnerException?.Message + " #$" + fileName + ":" + line) { StatusCode = (int)statusCode };
        }
        public static IActionResult CreateGoodHttpResponse(object? value = null)
        {
            CustomStatusCode statusCode = CustomStatusCode.Ok;
            value ??= statusCode.AsString(EnumFormat.Description);
            return new OkObjectResult(JsonSerializer.Serialize(value, new JsonSerializerOptions
            {
                // Set the property to ignore null values
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            }));
        }

        public static IActionResult Execute(object? obj = null)
        {
            try
            {
                return CreateGoodHttpResponse(obj);
            }
            catch (Exception e)
            {
                return CreateBadHttpResponse(e);
            }
        }
    }

    public enum CustomStatusCode : short
    {
        [Description("Success")]
        Ok = 200,
        [Description("Response is empty or null")]
        NullOrEmptyResponse = 542,
        [Description("SAPbobsCOM Specific error")] // https://www.sap-business-one-tips.com/indonesian-daftar-error-code-uidi-api-sap-business-one-dan-penjelasannya/
        SAPbobsCOM = 569,

        [Description("Too much difference between DB quantity and entered quantity")]
        TooMuchDifferenceOnPurchase = 598,

        [Description("Default custom error, not very specific")]
        Default = 599,
    }

    public class NullOrEmptyResponseException : Exception
    {
        MethodInfo MethodWhereExceptionOccured { get; }
        public NullOrEmptyResponseException(MethodInfo methodWhereExceptionOccured) : base($"Response is empty or null; ExpectedAnswer: {methodWhereExceptionOccured.ReturnType}; MethodWhereExceptionOccured: {methodWhereExceptionOccured}")
        {
            MethodWhereExceptionOccured = methodWhereExceptionOccured;
        }
    }
}
