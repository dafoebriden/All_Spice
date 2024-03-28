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


    // NOTE Getting ingredient by Id
    internal Ingredient GetById(int ingredientId)
    {
        Ingredient ingredients = _repository.GetById(ingredientId);
        return ingredients;
    }

    // NOTE Deleting Ingredient
    internal void Delete(int ingredientId, string userId)
    {
        Ingredient ingredientToDelete = _repository.GetById(ingredientId);
        if (ingredientToDelete.CreatorId != userId)
        {
            throw new Exception("You do not have authorization to delete this ingredient");
        }
        _repository.Delete(ingredientId);
    }
}
