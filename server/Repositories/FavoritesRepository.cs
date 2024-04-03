namespace All_Spice.Repositories;
public class FavoritesRepository
{
    private readonly IDbConnection _db;
    public FavoritesRepository(IDbConnection db)
    {
        _db = db;
    }
    internal Favorite Create(Favorite favoriteData)
    {
        string sql = @"
        INSERT INTO
        favorites(creatorId, recipeId)
        VALUES(@CreatorId, @RecipeId);

        SELECT recipe.*,
        account.*
        FROM recipes recipe
        JOIN accounts account ON recipe.creatorId = account.id
        WHERE recipe.id = @RecipeId;";
        Favorite favorite = _db.Query<Favorite, Account, Favorite>(sql, (favorite, account) =>
        {
            favorite.Creator = account;
            return favorite;
        }, favoriteData).FirstOrDefault();
        return favorite;
    }

    // NOTE Getting an Favorites by userId
    internal List<Recipe> Get(string userId)
    {
        string sql = @" 
        SELECT 
        recipe.*,
        account.*,
        favorite.*
        FROM recipes recipe
        JOIN accounts account ON recipe.creatorId = account.id
        WHERE recipe.id = favorite.RecipeId AND favorite.creatorId = @userId;";

        List<Recipe> recipes = _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
        {
            recipe.Creator = account;
            return recipe;
        }, new { userId }).ToList();
        return recipes;
    }

    // NOTE Getting an Favorite by its Id
    internal Favorite GetById(int favoriteId)
    {
        string sql = @"
        SELECT FROM favorites
        WHERE id = @favoriteId;";

        Favorite favorite = _db.Query<Favorite>(sql, new { favoriteId }).FirstOrDefault();
        return favorite;
    }

    // NOTE Deleting an Favorite
    internal void Remove(int Id)
    {
        string sql = @"
        DELETE FROM favorites
        WHERE id = @Id 
        LIMIT 1
        ;";
        _db.Query(sql, new { Id });
    }
}
