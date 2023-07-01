using crud.Data;
using Microsoft.EntityFrameworkCore;
using crud.Models;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");
builder.Services.AddDbContext<DirectoryDb>(options =>
       options.UseNpgsql(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());




//create contact
app.MapPost("/contacts", async (Contact e, DirectoryDb db) =>
{
    var validationContext = new ValidationContext(e, serviceProvider: null, items: null);
    var validationResults = new List<ValidationResult>();
    if (!Validator.TryValidateObject(e, validationContext, validationResults, validateAllProperties: true))
    {
        var errors = validationResults.Select(vr => vr.ErrorMessage);
        return Results.BadRequest(errors);
    }
    db.Contacts.Add(e);
    await db.SaveChangesAsync();
    return Results.Created($"/contacts/{e.Id}", e);
});


//get contact by id 
app.MapGet("/contacts/{id:int}", async (int id, DirectoryDb db) =>
{
    return id <= 0
        ? Results.BadRequest("The ID must be a positive integer.")
        : await db.Contacts.FindAsync(id) is Contact e
            ? Results.Ok(e)
            : Results.NotFound("No contact found with this id");
});


//get all contacts
app.MapGet("/contacts", async (DirectoryDb db) =>
{
    var contacts = await db.Contacts.ToListAsync();

    return contacts.Count == 0
        ? Results.NotFound("No contacts were found.")
        : Results.Ok(contacts);
});


//update contact by id
app.MapPut("/contacts/{id:int}", async (int id, Contact e, DirectoryDb db) =>
{
    return e.Id != id
        ? Results.BadRequest("The id dosn`t same")
        : (await db.Contacts.FindAsync(id)) is Contact contact
            ? await UpdateContactAndReturnResult(contact, e, db)
            : Results.NotFound("No contact found with this id");
});

async Task<IResult> UpdateContactAndReturnResult(Contact contact, Contact updatedContact, DirectoryDb db)
{
    contact.FirstName = updatedContact.FirstName;
    contact.LastName = updatedContact.LastName;
    contact.Phone = updatedContact.Phone;
    contact.Comments = updatedContact.Comments;

    await db.SaveChangesAsync();
    return Results.Ok(contact);
}


//delete contact by id
app.MapDelete("/contacts/{id:int}", async (int id, DirectoryDb db) =>
{
    var contact = await db.Contacts.FindAsync(id);

    return contact is null
        ? Results.NotFound("No contact found with this id")
        : (await DeleteContactAndReturnResult(contact, db));
});

async Task<IResult> DeleteContactAndReturnResult(Contact contact, DirectoryDb db)
{
    db.Contacts.Remove(contact);
    await db.SaveChangesAsync();
    return Results.NoContent();
}


app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

