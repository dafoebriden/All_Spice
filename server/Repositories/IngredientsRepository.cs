namespace All_Spice.Repositories;
public class IngredientsRepository
{
    private readonly IDbConnection _db;
    public IngredientsRepository(IDbConnection db)
    {
        _db = db;
    }
    internal Ingredient Create(Ingredient ingredientData)
    {
        string sql = @"
        INSERT INTO
        ingredients(name, quantity, creatorId, recipeId)
        VALUES(@Name, @Quantity, @CreatorId, @RecipeId);

        SELECT ingredient.*,
        account.*
        FROM ingredients ingredient
        JOIN accounts account ON ingredient.creatorId = account.id
        WHERE ingredient.id = LAST_INSERT_ID();";
        Ingredient ingredient = _db.Query<Ingredient, Account, Ingredient>(sql, (ingredient, account) =>
        {
            ingredient.Creator = account;
            return ingredient;
        }, ingredientData).FirstOrDefault();
        return ingredient;
    }

    // NOTE Getting an ingredients by recipeId
    internal List<Ingredient> GetIngredients(int recipeId)
    {
        string sql = @"
        SELECT 
        ingredient.*,
        account.*
        FROM ingredients ingredient
        JOIN accounts account ON ingredient.creatorId = account.id
        WHERE ingredient.recipeId = @recipeId;";

        List<Ingredient> ingredients = _db.Query<Ingredient, Account, Ingredient>(sql, (ingredient, account) =>
        {
            ingredient.Creator = account;
            return ingredient;
        }, new { recipeId }).ToList();
        return ingredients;
    }

    // NOTE Getting an ingredient by its Id
    internal Ingredient GetById(int ingredientId)
    {
        string sql = @"
        SELECT 
        ingredient.*,
        account.*
        FROM ingredients ingredient
        JOIN accounts account ON ingredient.creatorId = account.id
        WHERE ingredient.id = @ingredientId;";

        Ingredient ingredient = _db.Query<Ingredient, Account, Ingredient>(sql, (ingredient, account) =>
        {
            ingredient.Creator = account;
            return ingredient;
        }, new { ingredientId }).FirstOrDefault();
        return ingredient;
    }

    // NOTE Deleting an ingredient
    internal void Delete(int Id)
    {
        string sql = @"
        DELETE FROM ingredients
        WHERE id = @Id LIMIT 1
        ;";
        _db.Query(sql, new { Id });
    }
}
