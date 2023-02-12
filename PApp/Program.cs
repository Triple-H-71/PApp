
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyPolicy",
                      policy =>
                      {
                          policy.WithOrigins("http://206.189.197.156/",
                                              "http://localhost:4200/",
                                              "http://67.191.212.110/")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowAnyOrigin()
                          ;

                      });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<PApp.IDBConnector, PApp.DBConnector>();

var app = builder.Build();
app.UseSwagger();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{    
    app.UseSwaggerUI();
}

app.UseCors("MyPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
