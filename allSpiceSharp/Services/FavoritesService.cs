namespace allSpiceSharp.Services;

public class FavoritesService
{
  private readonly FavoritesRepository _repo;
  private readonly RecipesService _recipesService;
  public FavoritesService(FavoritesRepository repo, RecipesService recipesService)
  {
    _repo = repo;
    _recipesService = recipesService;
  }

  internal Favorite Create(Favorite favoriteData)
  {
    Favorite favorite = _repo.Create(favoriteData);
    return favorite;
  }

  internal List<MyFavorite> GetFavoritesByAccountId(string userId)
  {
    List<MyFavorite> myFavorites = _repo.GetFavoritesByAccountId(userId);
    return myFavorites;
  }

  internal Favorite GetOneFavorite(int id)
  {
    Favorite favorite = _repo.GetOneFavorite(id);
    if (favorite == null)
    {
      throw new Exception("No favorite at this id.");
    }
    return favorite;
  }

  internal string RemoveFavorite(int id, string userId)
  {
    Favorite original = GetOneFavorite(id);
    bool deleted = _repo.RemoveFavorite(id);
    if (deleted == false)
    {
      throw new Exception("No favorite was removed.");
    }
    return "Favorite was removed.";
  }
}
