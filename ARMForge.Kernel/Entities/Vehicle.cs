using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Kernel.Entities
{
    public class Vehicle : BaseEntity
    {
		private String plateNumber;

		public String PlateNumber
        {
			get { return plateNumber; }
			set { plateNumber = value; }
		}
		private String vehicleType;

		public String VehicleType
        {
			get { return vehicleType; }
			set { vehicleType = value; }
		}
		private int capacityKg;

		public int CapacityKg
        {
			get { return capacityKg; }
			set { capacityKg = value; }
		}
		private int capacityM3;

		public int CapacityM3
        {
			get { return capacityM3; }
			set { capacityM3 = value; }
		}
		private bool isAvailable;

		public bool IsAvailable
        {
			get { return isAvailable; }
			set { isAvailable = value; }
		}

	}
}
