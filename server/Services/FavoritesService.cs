namespace All_Spice.Services;

public class FavoritesService
{

    private readonly FavoritesRepository _repository;
    public FavoritesService(FavoritesRepository repository)
    {
        _repository = repository;
    }
    // NOTE Create a Favorite
    internal Favorite Create(Favorite favoriteData)
    {
        Favorite favorite = _repository.Create(favoriteData);
        return favorite;
    }


    // NOTE Getting Favorite by Id
    internal Favorite GetById(int favoriteId)
    {
        Favorite favorites = _repository.GetById(favoriteId);
        return favorites;
    }

    // NOTE Getting favorites by accountId
    internal List<Recipe> Get(string userId)
    {
        List<Recipe> favorites = _repository.Get(userId);
        return favorites;
    }

    // NOTE Deleting Favorite
    internal void Remove(int FavoriteId, string userId)
    {
        Favorite FavoriteToDelete = _repository.GetById(FavoriteId);
        if (FavoriteToDelete.CreatorId != userId)
        {
            throw new Exception("You do not have authorization to delete this Favorite");
        }
        _repository.Remove(FavoriteId);
    }
}