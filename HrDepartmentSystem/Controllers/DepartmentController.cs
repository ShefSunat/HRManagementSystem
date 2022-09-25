namespace WebApi.Controllers;
using Domain.Models;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("[Controller]")]
public class DepartmentController : ControllerBase
{
    private GetDepatrmentDto _getDepartmentDto;
    public DepartmentController(GetDepatrmentDto getDepatrmentDto)
    {
        _getDepartmentDto = getDepatrmentDto;
    }
  

    [HttpGet("GetDepartments")]
    public async Task<Response<List<GetDepatrmentDto>>> GetDepartment()
    {
        return await _getDepartmentDto.GetDepartment();
    }
 [HttpGet("GetDepartmentById")]
    public async Task<Response<List<GetDepatrmentDto>>> GetDepartmentById(Department department)
    {
        return await _getDepartmentDto.GetDepartmentById(department);
    }

    [HttpPost("AddDepartment")]
    public async Task<Response<Department>> AddDepartment(Department department)
    {
        return await _getDepartmentDto.AddDepartment(department);
    }
    [HttpPut("UpdateDepartment")]
    public async Task<Response<Department>> UpdateDepartment(Department department)
    {
       return await _getDepartmentDto.UpdateDepartment(department);
    }
}