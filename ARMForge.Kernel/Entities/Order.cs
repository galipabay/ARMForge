using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Kernel.Entities
{
    public class Order : BaseEntity
    {
		private int customerId;

		public int CustomerId
        {
			get { return customerId; }
			set { customerId = value; }
		}
		private DateTime orderDate;

		public DateTime OrderDate
		{
			get { return orderDate; }
			set { orderDate = value; }
		}
		private DateTime requiredDate;

		public DateTime RequiredDate
        {
			get { return requiredDate; }
			set { requiredDate = value; }
		}
		private DateTime shippedDate;

		public DateTime ShippedDate
		{
			get { return shippedDate; }
			set { shippedDate = value; }
		}
		private String status;

		public String Status
        {
			get { return status; }
			set { status = value; }
		}
		private int totalAmount;

		public int TotalAmount
        {
			get { return totalAmount; }
			set { totalAmount = value; }
		}

	}
}
