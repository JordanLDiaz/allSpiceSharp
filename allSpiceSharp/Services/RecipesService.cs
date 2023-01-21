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
  internal Recipe GetOneRecipe(int recipeId, string userId)
  {
    Recipe recipe = _repo.GetOneRecipe(recipeId);
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

  internal Recipe EditRecipe(Recipe recipeEdit, int recipeId, string userId)
  {
    Recipe original = GetOneRecipe(recipeId, userId);
    original.Title = recipeEdit.Title ?? original.Title;
    original.Instructions = recipeEdit.Instructions ?? original.Instructions;
    original.Img = recipeEdit.Img ?? original.Img;
    original.Category = recipeEdit.Category ?? original.Category;

    bool edited = _repo.EditRecipe(original);
    if (edited == false)
    {
      throw new Exception("Recipe was not edited.");
    }
    return original;
  }

  internal string RemoveRecipe(int recipeId, string userId)
  {
    Recipe original = GetOneRecipe(recipeId, userId);
    if (original.CreatorId != userId)
    {
      throw new Exception("You don't have permission to delete.");
    }

    _repo.RemoveRecipe(recipeId);
    return $"{original.Title} was deleted.";
  }
}
