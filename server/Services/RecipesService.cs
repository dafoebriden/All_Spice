namespace All_Spice.Services;

public class RecipesService
{
    private readonly RecipesRepository _repository;
    public RecipesService(RecipesRepository repository)
    {
        _repository = repository;
    }

    // NOTE Creating recipe
    internal Recipe Create(Recipe recipeData)
    {
        Recipe recipe = _repository.Create(recipeData);
        return recipe;
    }

    // NOTE Getting all recipes
    internal List<Recipe> Get()
    {
        List<Recipe> recipes = _repository.Get();
        return recipes;
    }

    // NOTE Getting one recipe by its Id
    internal Recipe GetById(int recipeId)
    {
        Recipe recipe = _repository.GetById(recipeId) ?? throw new Exception($"There is no recipe with the the id: {recipeId}");
        return recipe;
    }

    //NOTE Editing recipe by Id
    internal Recipe Edit(int recipeId, string userId, Recipe recipeData)
    {
        Recipe recipeToUpdate = _repository.GetById(recipeId) ?? throw new Exception($"There is no recipe with the the id: {recipeId}");
        if (recipeToUpdate.CreatorId != userId)
        {
            throw new Exception("That recipe does not belong to you. Give it back you thief!");
        }
        recipeToUpdate.Title = recipeData.Title ?? recipeToUpdate.Title;
        recipeToUpdate.Instructions = recipeData.Instructions ?? recipeToUpdate.Instructions;
        recipeToUpdate.Img = recipeData.Img ?? recipeToUpdate.Img;
        recipeToUpdate.Category = recipeData.Category ?? recipeToUpdate.Category;
        Recipe recipe = _repository.Edit(recipeToUpdate);
        return recipe;
    }
    // NOTE Deleting recipe by Id
    internal void Delete(int recipeId, string userId)
    {
        Recipe RecipeToDelete = _repository.GetById(recipeId);
        if (RecipeToDelete.CreatorId != userId)
        {
            throw new Exception("You do not have authorization to delete this Recipe");
        }
        _repository.Delete(recipeId);
    }
}
