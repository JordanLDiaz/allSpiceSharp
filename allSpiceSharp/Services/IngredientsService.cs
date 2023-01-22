namespace allSpiceSharp.Services;

public class IngredientsService
{
  private readonly IngredientsRepository _repo;
  private readonly RecipesService _recipesService;
  public IngredientsService(IngredientsRepository repo, RecipesService recipesService)
  {
    _repo = repo;
    _recipesService = recipesService;
  }

  internal Ingredient GetIngredientsById(int id)
  {
    Ingredient selected = _repo.GetIngredientsById(id);
    if (selected == null)
    {
      throw new Exception("Invalid id.");
    }
    return selected;
  }

  internal Ingredient CreateIngredient(Ingredient ingredientData)
  {
    Ingredient ingredient = _repo.CreateIngredient(ingredientData);
    return ingredient;
  }


  internal List<Ingredient> GetIngredientsByRecipeId(int recipeId, string userId)
  {
    Recipe recipe = _recipesService.GetOneRecipe(recipeId);
    List<Ingredient> ingredients = _repo.GetIngredientsByRecipeId(recipeId);
    return ingredients;
  }

  internal string RemoveIngredient(int ingredientId, string userId)
  {
    Ingredient ingredient = GetIngredientsById(ingredientId);
    bool deleted = _repo.RemoveIngredient(ingredientId);
    if (deleted == false)
    {
      throw new Exception("No ingredient was deleted.");
    }
    return $"{ingredient.Name} was deleted.";
  }
}
