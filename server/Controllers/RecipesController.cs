namespace All_Spice.Controllers;

[ApiController]
[Route("api[controller]")]
public class RecipesController : ControllerBase
{
    private readonly RecipesService _recipesService;
    private readonly IngredientsService _ingredientsService;
    private readonly Auth0Provider _auth0Provider;
    public RecipesController(RecipesService recipesService, IngredientsService ingredientsService, Auth0Provider auth0Provider)
    {
        _recipesService = recipesService;
        _ingredientsService = ingredientsService;
        _auth0Provider = auth0Provider;
    }

    //NOTE Creating a recipe
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Recipe>> Create([FromBody] Recipe recipeData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            recipeData.CreatorId = userInfo.Id;
            Recipe recipe = _recipesService.Create(recipeData);
            return Ok(recipe);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    //NOTE Getting all recipes
    // [HttpGet]
    // public ActionResult<List<Recipe>> Get()
    // {
    //     try
    //     {
    //         List<Recipe> recipes = _recipesService.Get();
    //         return Ok(recipes);
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e.Message);
    //     }
    // }

    //NOTE Getting a recipe by its Id
    // [HttpGet("{recipeId}")]
    // public ActionResult<Recipe> GetById(int recipeId)
    // {
    //     try
    //     {
    //         Recipe recipe = _recipesService.GetById(recipeId);
    //         return Ok(recipe);
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e.Message);
    //     }
    // }

    //NOTE Deleting a recipe by its Id
    // [HttpDelete("{recipeId}")]
    // [Authorize]
    // public async Task<ActionResult<string>> DeleteById(int recipeId)
    // {
    //     try
    //     {
    //         Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
    //         await _recipesService.DeleteById(recipeId, userInfo.Id);
    //         return Ok($"Deleted recipe: {recipeId}");
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e.Message);
    //     }
    // }

    //NOTE getting ingredients for a recipe
    // [HttpGet("{recipeId}/ingredients")]
    // public ActionResult<List<Ingredient>> GetIngredients(int recipeId)
    // {
    //     try
    //     {
    //         List<Ingredient> ingredients = _ingredientsService.GetIngredients(recipeId);
    //         return Ok(ingredients);
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e.Message);
    //     }
    // }
}