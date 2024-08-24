using CustomerService.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Adiciona a configuração de dependências
builder.Services.AddProjectDependencies(builder.Configuration);

var app = builder.Build();

// Configuração do middleware
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