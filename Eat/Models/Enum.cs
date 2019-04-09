using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eat.Models
{
    
    public static class DeliveryType
    {
        public const string InPlace = "in place";
        public const string Pickup = "pickup";
        public const string Courier = "courier";
        public static List<string> DeliveryTypeList = new List<string>()
        { InPlace , Pickup , Courier};
    }

    public static class StatusDishOrder
    {
        public const string Delivered = "delivered";
        public const string Sent = "sent";
        public const string New = "new";
        public const string Confirmed = "confirmed";
        public const string Rejected = "rejected";
        public const string Canceled = "canceled";
        public static List<string> StatusDishOrderList = new List<string>()
        { Delivered , Sent , New , Confirmed, Rejected, Canceled };
    }

    public static class StatusPayment
    {
        public const string FullyPaid = "fully paid";
        public const string NotPaid = "not paid";
        public const string ReceivedPrepayment = "received prepayment";
        public const string WaitingForPayment = "waiting for payment";
        public static List<string> StatusPaymentList = new List<string>()
        { FullyPaid , NotPaid , ReceivedPrepayment , WaitingForPayment };

    }

    public static class StatusTableOrder
    {
        public const string Request = "Request";
        public const string RejectedR = "RejectedR";
        public const string DoneR = "DoneR";
        public const string Cancel = "Cancel";
        public const string ConfirmedR = "ConfirmedR";
        public static List<string> StatusTableOrderList = new List<string>()
        { Request , RejectedR , DoneR , Cancel, ConfirmedR };
    }
   
    
	
}