namespace All_Spice.controllers;

[ApiController]
[Route("api/[controller]")]
public class FavoritesController : ControllerBase
{
    private readonly FavoritesService _favoritesService;
    private readonly Auth0Provider _auth0Provider;
    public FavoritesController(FavoritesService favoritesService, Auth0Provider auth0Provider)
    {
        _favoritesService = favoritesService;
        _auth0Provider = auth0Provider;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Favorite>> CreateFavorite([FromBody] Favorite favoriteData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            favoriteData.CreatorId = userInfo.Id;
            Favorite favorite = _favoritesService.Create(favoriteData);
            return Ok(favorite);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpDelete]
    [Authorize]
    public async Task<ActionResult<string>> Remove(int favoriteId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            _favoritesService.Remove(favoriteId, userInfo.Id);
            return Ok($"Removed Favorite: {favoriteId}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}
