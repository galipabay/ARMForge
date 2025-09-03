using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Kernel.Entities
{
    public class OrderItem : BaseEntity
    {
		private int orderId;

		public int OrderId
        {
			get { return orderId; }
			set { orderId = value; }
		}
		private int productId;

		public int ProductId
        {
			get { return productId; }
			set { productId = value; }
		}
		private int quantity;

		public int Quantity
        {
			get { return quantity; }
			set { quantity = value; }
		}
		private int unitPrice;

		public int UnitPrice
        {
			get { return unitPrice; }
			set { unitPrice = value; }
		}
		private int subtotal;

		public int Subtotal
        {
			get { return subtotal; }
			set { subtotal = value; }
		}

	}
}
