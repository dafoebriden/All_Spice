namespace All_Spice.Services;

public class IngredientsService
{

    private readonly IngredientsRepository _repository;
    public IngredientsService(IngredientsRepository repository)
    {
        _repository = repository;
    }
    internal Ingredient Create(Ingredient ingredientData)
    {
        Ingredient ingredient = _repository.Create(ingredientData);
        return ingredient;
    }
    internal List<Ingredient> Get()
    {
        List<Ingredient> ingredients = _repository.Get();
        return ingredients;
    }
    internal Ingredient Delete(int ingredientId, string userId)
    {
        Ingredient ingredientToDelete = _repository.GetIngredientById(IngredientId);
        if (ingredientToDelete.CreatorId != userId)
        {
            throw new Exception("You do not have authorization to delete this ingredient");
        }
        _repository.Delete(ingredientId);
    }
}
