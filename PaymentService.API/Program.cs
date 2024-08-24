using PaymentService.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços necessários ao contêiner.
builder.Services.AddControllers();

// Adiciona a configuração de injeção de dependências personalizadas
builder.Services.AddProjectDependencies(builder.Configuration);

// Adiciona suporte ao OpenAPI/Swagger.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cria o aplicativo.
var app = builder.Build();

// Configura o pipeline de solicitação HTTP.
if (app.Environment.IsDevelopment())
{
    // Habilita o Swagger e a UI do Swagger na configuração de desenvolvimento
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Ativa a autenticação e autorização
app.UseAuthentication();
app.UseAuthorization();

// Mapeia os controladores
app.MapControllers();

// Inicia a aplicação.
app.Run();