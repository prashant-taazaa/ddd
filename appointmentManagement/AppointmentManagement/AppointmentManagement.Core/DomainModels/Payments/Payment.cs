using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentManagement.Core.DomainModels.Payments
{
    public class Payment
    {
        public Guid Id { get; protected set; }
        public virtual decimal Amount { get; protected set; }
        public virtual DateTime Date { get; protected set; }
        public virtual string RefrenceNumber { get; protected set; }
        public virtual PaymentStatus PaymentStatus { get; protected set; }

        public static Payment Create(decimal amount,string refrenceNumber,
            PaymentStatus paymentStatus)
        {
            return Create(Guid.NewGuid(), amount, refrenceNumber, paymentStatus);
        }

        public static Payment Create(Guid id, decimal amount, string refrenceNumber,
            PaymentStatus paymentStatus)
        {
            if (string.IsNullOrEmpty(refrenceNumber))
                throw new ArgumentNullException("refrenceNumber");

            return new Payment()
            {
                Id = id,
                Amount = amount,
                RefrenceNumber = refrenceNumber,
                PaymentStatus = paymentStatus
            };
        }

        public void UpdateStatus(PaymentStatus paymentStatus)
        {
            this.PaymentStatus = paymentStatus;
        }
    }
}
