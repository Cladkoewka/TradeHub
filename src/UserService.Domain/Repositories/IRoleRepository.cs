using UserService.Domain.Entities;

namespace UserService.Domain.Repositories;

public interface IRoleRepository
{
    Task<Role?> GetByIdAsync(Guid id);
    Task<Role?> GetByNameAsync(string name);
    Task<IEnumerable<Role>> GetAllAsync();
    Task CreateAsync(Role role);
    Task UpdateAsync(Role role);
    Task DeleteAsync(Role role);
}