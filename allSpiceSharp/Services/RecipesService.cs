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
  internal Recipe GetOneRecipe(int id)
  {
    Recipe recipe = _repo.GetOneRecipe(id);
    if (recipe == null)
    {
      throw new Exception("No recipe at this id.");
    }
    return recipe;
  }
  internal Recipe Create(Recipe recipeData)
  {
    Recipe recipe = _repo.Create(recipeData);
    return recipe;
  }

  internal Recipe EditRecipe(Recipe recipeData, int id, string userId)
  {
    Recipe original = GetOneRecipe(id);
    if (original.CreatorId != userId)
    {
      throw new Exception("Not your recipe to edit.");
    }
    original.Title = recipeData.Title ?? original.Title;
    original.Instructions = recipeData.Instructions ?? original.Instructions;
    original.Img = recipeData.Img ?? original.Img;
    original.Category = recipeData.Category ?? original.Category;

    bool edited = _repo.EditRecipe(original);
    if (edited == false)
    {
      throw new Exception("Recipe was not edited.");
    }
    return original;
  }

  internal string RemoveRecipe(int id, string userId)
  {
    Recipe original = GetOneRecipe(id);
    if (original.CreatorId != userId)
    {
      throw new Exception("You don't have permission to delete.");
    }

    string message = _repo.RemoveRecipe(id);
    return message;
  }
}
