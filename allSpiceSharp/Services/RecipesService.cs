namespace allSpiceSharp.Services;

public class RecipesService
{
  private readonly RecipesRepository _repo;
  public RecipesService(RecipesRepository repo)
  {
    _repo = repo;
  }

  internal List<Recipe> GetAllRecipes(string userId)
  {
    List<Recipe> recipes = _repo.GetAllRecipes();
    return recipes;
  }
  internal Recipe Create(Recipe recipeData)
  {
    Recipe recipe = _repo.Create(recipeData);
    return recipe;
  }

}
