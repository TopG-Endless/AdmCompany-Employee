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

        //Get: Pick up Companies by Id
        app.MapGet("/Company/{Id}", (int Id) =>
        {
            var company = Company.FirstOrDefault(p => p.Id == Id);
            return company != null ? Results.Ok(company) : Results.NotFound();
        });

        // newCompany.Id = companies.Max(c => c.Id) + 1; // Simular auto-incremento de ID
        //     newCompany.Employees = new List<Employee>();
        //     companies.Add(newCompany);
        //     return CreatedAtAction(nameof(GetCompany), new { id = newCompany.Id }, newCompany);

        //Post: Create a new company
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

        //Get: 


app.Run();



public class Company
{
    public int Id { get; set; }
    public required string Name  { get; set; }
    
    //public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}



public class Employee
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int CompanyId { get; set; }
    
}


   
