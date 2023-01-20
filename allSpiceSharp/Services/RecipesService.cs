namespace allSpiceSharp.Services;

public class RecipesService
{
  private readonly RecipesRepository _repo;
  public RecipesService(RecipesRepository repo)
  {
    _repo = repo;
  }

  internal Recipe Create(Recipe recipeData)
  {
    Recipe recipe = _repo.Create(recipeData);
    return recipe;
  }

}
