using IdentityService.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Adicione serviços ao container
builder.Services.AddControllers();

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adicione dependências de identidade e outros serviços
builder.Services.AddIdentityDependencies(builder.Configuration);

var app = builder.Build();

// Configuração do pipeline de solicitação HTTP
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