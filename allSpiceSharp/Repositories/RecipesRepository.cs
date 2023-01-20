namespace allSpiceSharp.Repositories;

public class RecipesRepository
{
  private readonly IDbConnection _db;
  public RecipesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal List<Recipe> GetAllRecipes()
  {
    string sql = @"
    SELECT 
    recipes.*,
    accounts.*
    FROM recipes
    JOIN accounts ON accounts.id = recipes.creatorId;
    ";
    List<Recipe> recipes = _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
    {
      recipe.Creator = account;
      return recipe;
    }).ToList();
    return recipes;
  }

  internal Recipe Create(Recipe recipeData)
  {
    string sql = @"
    INSERT INTO recipes
    (title, instructions, img, category, creatorId)
    VALUES
    (@title, @instructions, @img, @category, @creatorId);
    SELECT LAST_INSERT_ID();
    ";
    int id = _db.ExecuteScalar<int>(sql, recipeData);
    recipeData.Id = id;
    return recipeData;
  }
}
