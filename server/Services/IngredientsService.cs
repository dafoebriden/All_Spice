namespace All_Spice.Services;

public class IngredientsService
{

    private readonly IngredientsRepository _repository;
    public IngredientsService(IngredientsRepository repository)
    {
        _repository = repository;
    }

    // NOTE Creating ingredient
    internal Ingredient Create(Ingredient ingredientData)
    {
        Ingredient ingredient = _repository.Create(ingredientData);
        return ingredient;
    }

    // NOTE Getting all ingredients
    internal List<Ingredient> Get()
    {
        List<Ingredient> ingredients = _repository.Get();
        return ingredients;
    }

    // NOTE Getting ingredient by Id
    internal Ingredient GetById(int ingredientId)
    {
        Ingredient ingredients = _repository.GetById(ingredientId);
        return ingredients;
    }

    // NOTE Deleting Ingredient
    internal Ingredient Delete(int ingredientId, string userId)
    {
        Ingredient ingredientToDelete = _repository.GetById(IngredientId);
        if (ingredientToDelete.CreatorId != userId)
        {
            throw new Exception("You do not have authorization to delete this ingredient");
        }
        _repository.Delete(ingredientId);
    }
}
