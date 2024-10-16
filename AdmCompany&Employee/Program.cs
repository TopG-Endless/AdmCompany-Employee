using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



        // Crear una lista de compañías en memoria
        var Company = new List<Company>
        {
            new Company
            {
                Id = 1,
                Name = "TechCorp",
                
            },
            new Company
            {
                Id = 2,
                Name = "Innovate Inc.",
                
            },
            new Company
            {
                Id = 3,
                Name = "NextGen Solutions",
                
            }
        };

        // Crear una lista de empleados en memoria
        var Employee = new List<Employee>
        {
            new Employee
            {
                Id = 1,
                Name = "Rudolf Casimiro",
                CompanyId = 1
            },
            new Employee
            {
                Id = 2,
                Name = "Jackie Chang",
                CompanyId = 2
            },
            new Employee
            {
                Id = 3,
                Name = "Juan Soto",
                CompanyId = 3
            }
        };

        //GET: Pick up all the companies
        app.MapGet("/Company", () => 
        {
            return Results.Ok(Company);
        });

        //GetById: Pick up Companies by Id
        app.MapGet("/Company/{Id}", (int Id) =>
        {
            var company = Company.FirstOrDefault(p => p.Id == Id);
            return company != null ? Results.Ok(company) : Results.NotFound();
        });
        //Post: Create a new Company
        app.MapPost("/Company", (Company newCompany) =>
        {
            newCompany.Id = Company.Max(p => p.Id) +1;
            Company.Add(newCompany);
            return Results.Created($"/Company/{newCompany.Id}", newCompany);
        });

        //Put: Update company
        app.MapPut("/Company/{Id}", (int Id, Company updatedCompany) =>
        {
            var company = Company.FirstOrDefault(p => p.Id == Id);
            if (company == null)
            {
                return Results.NotFound();
            }

            company.Name = updatedCompany.Name;

            return Results.NoContent();
        });

        //Delete: Delete company by id
        app.MapDelete("/Company/{Id}", (int Id) =>
        {
            var company = Company.FirstOrDefault(c => c.Id == Id);
            if (company is null) return Results.NotFound();

            var hasEmployees = Employee.Any(e => e.CompanyId == Id);
            if (hasEmployees)
            return Results.BadRequest("Cannot delete company because it has assigned employees.");

            Company.Remove(company);
            return Results.NoContent();
        });


        //Get: Pick up all Employees
        
        app.MapGet("/Employee", () => 
        {
            return Results.Ok(Employee);
        });

        //GetById: Pick up Employee by id
        app.MapGet("/Employee/{Id}", (int Id) =>
        {
            var employee = Employee.FirstOrDefault(p => p.Id == Id);
            return Employee != null ? Results.Ok(employee) : Results.NotFound();
        });

        //Post: Create a new Employee
        app.MapPost("/Employee", (Company newEmployee) =>
        {
            newEmployee.Id = Company.Max(p => p.Id) +1;
            Company.Add(newEmployee);
            return Results.Created($"/Employee/{newEmployee.Id}", newEmployee);
        });

        //Put: Update Employee
          app.MapPut("/Employee/{Id}", (int Id, Employee updatedEmployee) =>
        {
            var employee = Employee.FirstOrDefault(p => p.Id == Id);
            if (employee == null)
            {
                return Results.NotFound();
            }

            employee.Name = updatedEmployee.Name;

            return Results.NoContent();
        });

        //DeletebyID: Delete employee by id
        app.MapDelete("/Employee/{Id}", (int Id) =>
        {
            var employee = Employee.FirstOrDefault(e => e.Id == Id);
            if (employee is null) return Results.NotFound();

            Employee.Remove(employee);
            return Results.NoContent();
        });

        //DeleteAll: Delete company with employees
        app.MapDelete("/Company/{id}/with-Employee", (int Id) =>
        {
            var company = Company.FirstOrDefault(c => c.Id == Id);
            if (company is null) return Results.NotFound();

            Employee.RemoveAll(e => e.CompanyId == Id);
            Company.Remove(company);

            return Results.NoContent();
        });



app.Run();



public class Company
{
    public int Id { get; set; }
    public required string Name  { get; set; }
    
}



public class Employee
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int CompanyId { get; set; }
    
}


   
