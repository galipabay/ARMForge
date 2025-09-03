using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Kernel.Entities
{
    public class Product : BaseEntity
    {
		private String name;

		public String Name
		{
			get { return name; }
			set { name = value; }
		}
		private String stockKeepingUnit;

		public String StockKeepingUnit
        {
			get { return stockKeepingUnit; }
			set { stockKeepingUnit = value; }
		}
		private String description;

		public String Description
        {
			get { return description; }
			set { description = value; }
		}
		private int unitPrice;

		public int UnitPrice
        {
			get { return unitPrice; }
			set { unitPrice = value; }
		}
		private int stockQuantity;

		public int StockQuantity
        {
			get { return stockQuantity; }
			set { stockQuantity = value; }
		}
		private String category;

		public String Category
        {
			get { return category; }
			set { category = value; }
		}
		private bool isActive;

		public bool IsActive
        {
			get { return isActive; }
			set { isActive = value; }
		}
	}
}
