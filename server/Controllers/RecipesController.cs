namespace All_Spice.Controllers;

[ApiController]
[Route("api[controller]")]
public class RecipesController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<Recipe>> Get()
    {
        try
        {
            return Ok(new List<Recipe>)
        }
        catch (System.Exception)
        {

            throw;
        }
    }
}