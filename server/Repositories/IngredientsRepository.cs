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
        }, ingredientId).FirstOrDefault();
        return ingredient;
    }

    // NOTE Deleting an ingredient
    internal void Delete(int id)
    {
        string sql = @"
        DELETE * FROM ingredients ingredient
        WHERE ingredient.id = @id LIMIT 1
        ;";
        _db.Query(sql, id);
    }
}
