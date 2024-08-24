using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Interfaces
{
    public interface IBaseService<TDto, TKey>
    {
        Task<TDto> GetByIdAsync(TKey id);

        Task<IEnumerable<TDto>> GetAllAsync();

        Task AddAsync(TDto dto);

        Task UpdateAsync(TDto dto);

        Task DeleteAsync(TKey id);
    }
}