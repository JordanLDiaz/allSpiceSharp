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

  internal Recipe GetOneRecipe(int id)
  {
    string sql = @"
    SELECT
    recipes.*,
    accounts.*
    FROM recipes
    JOIN accounts ON accounts.id = recipes.creatorId
    WHERE recipes.id = @id;
    ";
    return _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
    {
      recipe.Creator = account;
      return recipe;
    }, new { id }).FirstOrDefault();
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

  internal bool EditRecipe(Recipe original)
  {
    string sql = @"
    UPDATE recipes
      SET
      title = @title,
      instructions = @instructions,
      img = @img,
      category = @category
      WHERE id = @id;
    ";
    int rows = _db.Execute(sql, original);
    return rows > 0;
  }

  internal string RemoveRecipe(int id)
  {
    string sql = @"
      DELETE FROM recipes
      WHERE id = @id;
    ";
    _db.Execute(sql, new { id });
    return "Recipe was deleted";
  }
}
