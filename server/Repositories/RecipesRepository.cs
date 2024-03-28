using Microsoft.AspNetCore.Http.HttpResults;

namespace All_Spice.Repositories;

public class RecipesRepository
{
    private readonly IDbConnection _db;
    public RecipesRepository(IDbConnection db)
    {
        _db = db;
    }
    internal Recipe Create(Recipe RecipeData)
    {
        string sql = @"
        INSERT INTO 
        recipes(title, instructions, image, category, creatorId) 
        VALUES(@Title, @Instructions, @Image, @Category, @CreatorId);

        SELECT recipe.*,
        account.*
        FROM recipes recipe
        JOIN accounts account On recipe.creatorId = account.id
        WHERE recipe.id = LAST_INSERTED_ID();";
        Recipe recipe = _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
        {
            recipe.Creator = account;
            return recipe;
        }, RecipeData).FirstOrDefault();
        return recipe;
    }
    internal List<Recipe> Get()
    {
        string sql = @"
        SELECT
        recipe.*,
        account*
        FROM recipes recipe
        JOIN accounts account ON recipe.creatorId = account.id;";

        List<Recipe> recipes = _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
        {
            recipe.Creator = account;
            return recipe;
        }).ToList();
        return recipes;
    }
    internal Recipe GetById(int recipeId)
    {
        string sql = @"
        SELECT recipe.*,
        account.*
        FROM recipes recipe
        JOIN accounts account ON recipe.creatorId = account.id
        WHERE recipe.id = @recipeId
        ;";
        Recipe recipe = _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
        {
            recipe.Creator = account;
            return recipe;
        }, new { recipeId }).FirstOrDefault();
        return recipe;
    }
    internal void Delete(int id)
    {
        string sql = @"
        DELETE * IN recipes recipe
        WHERE recipe.id = @id
        ;";
        _db.Query<Recipe>(sql, id);
    }
}
