using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VotingBackend.Dtos;

namespace VotingBackend.Services
{
    public interface ICategoryService
    {
        public Task<CreateCategoryResponseDto> CreateCategory(CreateCategoryRequestDto request);
        public Task<UpdateCategoryResponseDto> UpdateCategory(UpdateCategoryRequestDto request);
        public Task<CategoryDto> DeleteCategory(Guid id);
        public Task<List<CategoryDto>> GetAllCategory();
    }
}