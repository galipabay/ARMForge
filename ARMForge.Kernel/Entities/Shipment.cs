using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Todo: Add relationships to Order, Driver, and Vehicle entities
namespace ARMForge.Kernel.Entities
{
    public class Shipment :BaseEntity
    {
		private int orderId;

		public int OrderId
        {
			get { return orderId; }
			set { orderId = value; }
		}
		private String trackingNumber;

		public String TrackingNumber
        {
			get { return trackingNumber; }
			set { trackingNumber = value; }
		}
		private int driverId;

		public int DriverId
        {
			get { return driverId; }
			set { driverId = value; }
		}
		private int vehicleId;

		public int VehicleId
        {
			get { return vehicleId; }
			set { vehicleId = value; }
		}
		private DateTime estimatedDeliveryDate;

		public DateTime EstimatedDeliveryDate
        {
			get { return estimatedDeliveryDate; }
			set { estimatedDeliveryDate = value; }
		}
		private DateTime? actualDeliveryDate;

		public DateTime? ActualDeliveryDate
        {
			get { return actualDeliveryDate; }
			set { actualDeliveryDate = value; }
		}
		private String status;

		public String Status
		{
			get { return status; }
			set { status = value; }
		}
		private Order order;

		public Order Order
		{
			get { return order; }
			set { order = value; }
		}
		private Driver driver;

		public Driver Driver
		{
			get { return driver; }
			set { driver = value; }
		}
		private Vehicle vehicle;

		public Vehicle Vehicle
		{
			get { return vehicle; }
			set { vehicle = value; }
		}
	}
}
