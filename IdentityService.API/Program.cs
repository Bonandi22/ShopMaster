using IdentityService.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Adicione servi�os ao container
builder.Services.AddControllers();

// Configura��o do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adicione depend�ncias de identidade e outros servi�os
builder.Services.AddIdentityDependencies(builder.Configuration);

var app = builder.Build();

// Configura��o do pipeline de solicita��o HTTP
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