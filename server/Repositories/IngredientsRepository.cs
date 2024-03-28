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

;";
        Ingredient ingredient = _db.Query<Ingredient, Account, Ingredient>(sql, (ingredient, account) =>
        {
            ingredient.Creator = account;
            return ingredient;
        }, ingredientData).FirstOrDefault();
        return ingredient;
    }
}
