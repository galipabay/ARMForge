using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Kernel.Entities
{
    public class Driver : BaseEntity
    {
		private String firstname;

		public String Firstname
		{
			get { return firstname; }
			set { firstname = value; }
		}

		private int employeeId;

		public int EmployeeId
        {
			get { return employeeId; }
			set { employeeId = value; }
		}
		private int licenseType;

		public int LicenseType
        {
			get { return licenseType; }
			set { licenseType = value; }
		}
		private bool isOnDuty;

		public bool IsOnDuty
        {
			get { return isOnDuty; }
			set { isOnDuty = value; }
		}
		private DateTime? lastInspectionDate;

		public DateTime? LastInspectionDate
        {
			get { return lastInspectionDate; }
			set { lastInspectionDate = value; }
		}
		private bool isAvailable;

		public bool IsAvailable
		{
			get { return isAvailable; }
			set { isAvailable = value; }
		}
		private ICollection<Shipment>? shipments;

		public ICollection<Shipment>? Shipments
		{
			get { return shipments; }
			set { shipments = value; }
		}

	}
}
