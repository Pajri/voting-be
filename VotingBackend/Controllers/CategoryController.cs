using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VotingBackend.Constants;
using VotingBackend.Dtos;
using VotingBackend.Services;

namespace VotingBackend.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [Authorize(Roles = Constant.ROLE_ADMIN)]
        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetAll()
        {
            try
            {
                var categoryList = await _service.GetAllCategory();
                return categoryList;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [Authorize(Roles = Constant.ROLE_ADMIN)]
        [HttpPost]
        public async Task<ActionResult<CreateCategoryResponseDto>> Create(CreateCategoryRequestDto request)
        {
            CreateCategoryResponseDto response;
            try
            {
                response = await _service.CreateCategory(request);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

            return Created("", response);
        }

        [Authorize(Roles = Constant.ROLE_ADMIN)]
        [HttpPost("update")]
        public async Task<ActionResult<UpdateCategoryResponseDto>> Update(UpdateCategoryRequestDto request)
        {
            try
            {
                var category = await _service.UpdateCategory(request);
                return Ok(category);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode((int) HttpStatusCode.InternalServerError, ex);
            }
        }

        [Authorize(Roles = Constant.ROLE_ADMIN)]
        [HttpPost("delete")]
        public async Task<ActionResult<CategoryDto>> Delete(Guid id)
        {
            try
            {
                var category = await _service.DeleteCategory(id);
                return Ok(category);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
