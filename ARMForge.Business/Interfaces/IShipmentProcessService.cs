using ARMForge.Kernel.Entities;
using ARMForge.Types.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Business.Interfaces
{
    public interface IShipmentProcessService
    {
        // Sevkiyat oluşturma işlemini yönetir ve oluşturulan Sevkiyat varlığını döner.
        Task<Shipment> CreateShipmentAsync(CreateShipmentRequestDto request);
    }
}
