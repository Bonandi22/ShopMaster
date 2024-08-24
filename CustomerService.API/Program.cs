using CustomerService.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Adiciona a configura��o de depend�ncias
builder.Services.AddProjectDependencies(builder.Configuration);

var app = builder.Build();

// Configura��o do middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();