namespace BookShop.Endpoints;

public static class BookEndpoints
{
    public static void MapBookEndpoints(this WebApplication app)
    {
        RouteGroupBuilder bookGroup = app.MapGroup("/books");
        
    }
    
    
}