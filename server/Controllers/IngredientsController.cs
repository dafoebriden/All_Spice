namespace All_Spice.Controllers;

[ApiController]
[Route("[controller]")]
public class IngredientsController : ControllerBase
{
    private readonly IngredientsService _ingredientsService;
    private readonly Auth0Provider _auth0Provider;
    public IngredientsController(IngredientsService ingredientsService, Auth0Provider auth0Provider)
    {
        _ingredientsService = ingredientsService;
        _auth0Provider = auth0Provider;
    }

    //NOTE Creating an Ingredient
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Ingredient>> Create([FromBody] Ingredient ingredientData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            ingredientData.CreatorId = userInfo.Id;
            Ingredient ingredient = _ingredientsService.Create(ingredientData);
            return Ok(ingredient);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    //NOTE Getting all Ingredients
    [HttpGet]
    public ActionResult<List<Ingredient>> Get()
    {
        try
        {
            List<Ingredient> ingredients = _ingredientsService.Get();
            return Ok(ingredients);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    //NOTE Getting a Ingredient by its Id
    [HttpGet("{IngredientId}")]
    public ActionResult<Ingredient> GetById(int ingredientId)
    {
        try
        {
            Ingredient ingredient = _ingredientsService.GetById(ingredientId);
            return Ok(ingredient);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    //NOTE Deleting a Ingredient by its Id
    [HttpDelete("{IngredientId}")]
    [Authorize]
    public async Task<ActionResult<string>> DeleteById(int ingredientId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            await _ingredientsService.DeleteById(ingredientId, userInfo.Id);
            return Ok($"Deleted Ingredient: {ingredientId}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }




}