namespace All_Spice.Services;

public class RecipesService
{
    private readonly RecipesRepository _repository;
    public RecipesService(RecipesRepository repository)
    {
        _repository = repository;
    }
    internal Recipe Create(Recipe RecipeData)
    {
        Recipe Recipe = _repository.Create(RecipeData);
        return Recipe;
    }
    internal List<Recipe> Get()
    {
        List<Recipe> Recipes = _repository.Get();
        return Recipes;
    }
    internal Recipe Delete(int RecipeId, string userId)
    {
        Recipe RecipeToDelete = _repository.GetRecipeById(RecipeId);
        if (RecipeToDelete.CreatorId != userId)
        {
            throw new Exception("You do not have authorization to delete this Recipe");
        }
        _repository.Delete(RecipeId);
    }
}
