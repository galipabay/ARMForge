using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Kernel.Entities
{
    public class Invoice : BaseEntity
    {
		private int orderId;

		public int OrderId
        {
			get { return orderId; }
			set { orderId = value; }
		}
		private int customerId;

		public int CustomerId
        {
			get { return customerId; }
			set { customerId = value; }
		}
		private DateTime invoiceDate;

		public DateTime InvoiceDate
		{
			get { return invoiceDate; }
			set { invoiceDate = value; }
		}
		private DateTime dueDate;

		public DateTime DueDate
		{
			get { return dueDate; }
			set { dueDate = value; }
		}
		private int totalAmounts;

		public int TotalAmounts
		{
			get { return totalAmounts; }
			set { totalAmounts = value; }
		}
		private String status;

		public String Status
        {
			get { return status; }
			set { status = value; }
		}
		private DateTime paymentDate;

		public DateTime PaymentDate
		{
			get { return paymentDate; }
			set { paymentDate = value; }
		}

	}
}
