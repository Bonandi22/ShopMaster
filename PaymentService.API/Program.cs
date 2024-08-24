using PaymentService.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os servi�os necess�rios ao cont�iner.
builder.Services.AddControllers();

// Adiciona a configura��o de inje��o de depend�ncias personalizadas
builder.Services.AddProjectDependencies(builder.Configuration);

// Adiciona suporte ao OpenAPI/Swagger.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cria o aplicativo.
var app = builder.Build();

// Configura o pipeline de solicita��o HTTP.
if (app.Environment.IsDevelopment())
{
    // Habilita o Swagger e a UI do Swagger na configura��o de desenvolvimento
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Ativa a autentica��o e autoriza��o
app.UseAuthentication();
app.UseAuthorization();

// Mapeia os controladores
app.MapControllers();

// Inicia a aplica��o.
app.Run();