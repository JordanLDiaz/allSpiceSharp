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
}
