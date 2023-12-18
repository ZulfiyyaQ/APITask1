using APITask1.DTOs.Tag;

namespace APITask1.Services.Interfaces
{
    public interface ITagService
    {
        Task<ICollection<GetTagDto>> GetAllAsync(int page, int take);
        Task<GetTagDto> GetByIdAsync(int id);
        Task CreateAsync(CreateTagDTO tagDto);
        Task UpdateAsync(int id, UpdateTagDto updateTagDto);
        Task DeleteAsync(int id);
    }
}
