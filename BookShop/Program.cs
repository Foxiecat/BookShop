using BookShop.Books;
using BookShop.Books.Interfaces;
using BookShop.Books.Repositories;
using BookShop.Middleware;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddSingleton<IBookRepository, BookMySqlDatabase>()
        .AddExceptionHandler<GlobalExceptionHandling>()
        .AddEndpointsApiExplorer()
        .AddSwaggerGen();
}


WebApplication app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    
    app.UseExceptionHandler(_ => { })
        .UseHttpsRedirection();
    
    // Our own endpoint
    app.MapBookEndpoints();
}

app.Run();