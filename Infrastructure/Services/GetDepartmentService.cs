
namespace Infrastructure.Services;
using Npgsql;
using Dapper;
using Domain.Wrapper;
using Domain.Models;
using Infrastructure.DataContext;
public class GetDepatrmentDto
{
     private DataContext _context;
     public GetDepatrmentDto (DataContext context)
    {
        _context = context;
    }

     public async Task<Response<List<GetDepatrmentDto>>> GetDepartment()
    {
        await using var connection = _context.CreateConnection();
     var sql = $"SELECT d.Id,d.Name,concat (em.FirstName, ' ',em.lastname) as fullname,em.Id FROM department as d Left JOIN department_manager  as dm ON dm.DepartmentId=d.ID Left JOIN employee as em ON em.Id=dm.EmployeeId;";
                var result = await connection.QueryAsync<GetDepatrmentDto>(sql);
        return  new Response<List<GetDepatrmentDto>>(result.ToList());
    }
     public async Task<Response<List<GetDepatrmentDto>>> GetDepartmentById(Department department)
    {
        try
        {
             using var connection = _context.CreateConnection();
        {
            string sql = $"SELECT d.Id,d.Name,concat (em.FirstName, ' ',em.lastname) as fullname,em.Id FROM department as d Left JOIN department_manager  as dm ON dm.DepartmentId=d.ID Left JOIN employee as em ON em.Id=dm.EmployeeId where id = {department.Id};";
            var response  = await connection.ExecuteScalarAsync<List<GetDepatrmentDto>>(sql);
            return new Response<List<GetDepatrmentDto>>(response);
        }
        }
         catch (Exception ex)
        {
           return new Response<List<GetDepatrmentDto>>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public Task<Response<Department>> UpdateDepartment(object department)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<Department>> AddDepartment(Department department)
    {
        using var connection = _context.CreateConnection();
        try
        {
            var sql = $"insert into department (name,id) values({department.Name},{department.Id} )returning id);";
            var result = await connection.ExecuteScalarAsync<Department>(sql);
            return new Response<Department>(department);
        }
        catch (Exception ex)
        {

return new Response<Department>(System.Net.HttpStatusCode.InternalServerError, ex.Message);        }
    
    }

    
    public async Task<Response<Department>> UpdateDepartment(Department department)
    {
        try
        {
             using var connection = _context.CreateConnection();
        {
            string sql = $"update Department set DepartmentName = {department.Name},DepartmentId = {department.Id}   where Id = {department.Id}";
                 var result = await connection.ExecuteScalarAsync<int>(sql);
            return new Response<Department>(department); 
        }
        }
         catch (Exception ex)
        {     
return new Response<Department>(System.Net.HttpStatusCode.InternalServerError, ex.Message);               }  
       
    }

    private Response<T> Response<T>(T department, int id)
    {
        throw new NotImplementedException();
    }
}