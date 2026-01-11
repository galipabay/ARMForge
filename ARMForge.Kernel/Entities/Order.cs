using ARMForge.Kernel.Enums;
using ARMForge.Kernel.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Kernel.Entities
{
    public class Order : BaseEntity
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public DateTimeOffset OrderDate { get; private set; } = DateTimeOffset.UtcNow;

        public DateTimeOffset RequiredDate { get; set; }
        public DateTimeOffset? ShippedDate { get; set; }
        
        [Required]
        public OrderStatus Status { get; private set; } = OrderStatus.Draft;

        [Range(0.01, double.MaxValue)]
        public decimal TotalAmount { get; private set; }

        [Required, MaxLength(32)]
        public string OrderNumber { get; set; } = string.Empty;

        [MaxLength(32)]
        public string? PaymentMethod { get; set; } // "Kredi Kartı", "Havale", "Nakit"
        
        [Required]
        public PaymentStatus PaymentStatus { get; private set; } = PaymentStatus.Unpaid;

        public DateTimeOffset? PaymentDate { get; set; }

        [Required, MaxLength(500)]
        public string DeliveryAddress { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string DeliveryCity { get; set; } = string.Empty;

        [Required, MaxLength(20)]
        public string DeliveryPostalCode { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string DeliveryCountry { get; set; } = string.Empty;

        [Required]
        public OrderPriority Priority { get; private set; } = OrderPriority.Medium;

        [MaxLength(500)]
        public string? SpecialInstructions { get; set; }

        [Range(0, double.MaxValue)]
        public decimal TotalWeight { get; private set; }

        [Range(0, double.MaxValue)]
        public decimal TotalVolume { get; private set; }

        [MaxLength(50)]
        public string? InternalReference { get; set; }

        public int? SalesPersonId { get; set; }
        public User? SalesPerson { get; set; }

        public ICollection<Shipment> Shipments { get; set; } = [];
        public ICollection<OrderItem> OrderItems { get; set; } = [];

        public void SetRequiredDate(DateTimeOffset requiredDate)
        {
            if (requiredDate < OrderDate)
                throw new DomainException("Required date cannot be earlier than order date");

            RequiredDate = requiredDate;
        }
        public void MarkAsPaid(string paymentMethod)
        {
            if (PaymentStatus != PaymentStatus.Unpaid)
                throw new DomainException("Payment already processed");

            PaymentStatus = PaymentStatus.Paid;
            PaymentMethod = paymentMethod;
            PaymentDate = DateTimeOffset.UtcNow;

            if (Status == OrderStatus.Draft)
                Status = OrderStatus.Confirmed;
        }

        public void Ship()
        {
            if (Status != OrderStatus.Confirmed)
                throw new DomainException("Only confirmed orders can be shipped");

            if (PaymentStatus != PaymentStatus.Paid)
                throw new DomainException("Order must be paid before shipping");

            Status = OrderStatus.Shipped;
            ShippedDate = DateTimeOffset.UtcNow;
        }
        public void Cancel()
        {
            if (Status == OrderStatus.Shipped || Status == OrderStatus.Completed)
                throw new DomainException("Order cannot be cancelled after shipping");
            
            if (PaymentStatus == PaymentStatus.Paid)
                PaymentStatus = PaymentStatus.Refund;

            Status = OrderStatus.Cancelled;
        }

        public void SetTotals(decimal amount, decimal weight, decimal volume)
        {
            TotalAmount = amount;
            TotalWeight = weight;
            TotalVolume = volume;
        }

        public void Deactivate()
        {
            if (Status == OrderStatus.Confirmed || Status == OrderStatus.Shipped)
                throw new DomainException("Active order cannot be deactivated");

            IsActive = false;
        }

        public void ChangePriority(OrderPriority priority)
        {
            Priority = priority;
        }
        public void DecreaseTotals(decimal amount, decimal weight, decimal volume)
        {
            TotalAmount -= amount;
            TotalWeight -= weight;
            TotalVolume -= volume;

            if (TotalAmount < 0) TotalAmount = 0;
            if (TotalWeight < 0) TotalWeight = 0;
            if (TotalVolume < 0) TotalVolume = 0;
        }
        public void IncreaseTotals(decimal amount, decimal weight, decimal volume)
        {
            TotalAmount += amount;
            TotalWeight += weight;
            TotalVolume += volume;
        }
    }
}
