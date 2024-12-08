﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusicSchool.Commands;
using MusicSchool.Models;
using MusicSchool.Queries;
using MusicSchool.Requests.CategoryRequests;
using MusicSchool.Responses;

namespace MusicSchool.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryResponse>>> GetAllCategories()
    {
        var categories = await _mediator.Send(new GetAllCategoriesQuery());

        return Ok(categories);
    }

    // GET: api/Category/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoryResponse>> GetCategoryById(int id)
    {
        var category = await _mediator.Send(new GetCategoryByIdQuery(id));

        return category == null
            ? NotFound("Id not found")
            : Ok(category);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Category>> UpdateCategory(int id, [FromBody] UpdateCategory request)
    {
        var response = await _mediator.Send(new UpdateCategoryCommand(id, request.NewCategoryName));

        return HandleApiResponse(response);
    }

    // POST: api/category
    [HttpPost]
    public async Task<ActionResult<Category>> CreateCategory([FromBody] CreateCategoryRequest request)
    {
        var response = await _mediator.Send(new CreateCategoryCommand(request.Name));

        return HandleApiResponse(response);
    }

    // DELETE: api/category/{id}
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> RemoveCategory(int id)
    {
        var response = await _mediator.Send(new DeleteCategoryByIdCommand(id));

        return HandleApiResponse(response);
    }

    private ObjectResult HandleApiResponse<T>(ApiResult<T> response) where T : class
    {
        return response.IsSuccess
            ? StatusCode(response.StatusCode, response.Data)
            : StatusCode(response.StatusCode, response.Message);
    }
}
