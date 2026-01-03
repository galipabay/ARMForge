using ARMForge.Types.DTOs;

public interface IDriverService
{
    Task<IEnumerable<DriverDto>> GetAllDriversAsync();
    Task<DriverDto?> GetDriverByIdAsync(int id);
    Task<DriverDto> AddDriverAsync(DriverCreateDto driver);
    Task<DriverDto?> UpdateDriverAsync(int id, DriverUpdateDto driver);
    Task<bool> DeleteDriverAsync(int id);
}
