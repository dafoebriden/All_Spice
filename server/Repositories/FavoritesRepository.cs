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
        favorite.*,
        recipe.*,
        account.*
        FROM favorites favorite
        JOIN recipes recipe ON favorite.recipeId = recipe.id
        JOIN accounts account ON recipe.creatorId = account.id
        WHERE recipe.id = favorite.recipeId AND favorite.creatorId = @userId;";

        List<FavoriteRecipe> recipes = _db.Query<Favorite, FavoriteRecipe, Account, FavoriteRecipe>(sql, (favorite, recipe, userId) =>
        {
            favoriteRecipe.Creator = userId;
            return favoriteRecipe;
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
