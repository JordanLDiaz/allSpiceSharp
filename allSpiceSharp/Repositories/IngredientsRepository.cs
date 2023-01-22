namespace allSpiceSharp.Repositories;

public class IngredientsRepository
{
  private readonly IDbConnection _db;
  public IngredientsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Ingredient GetIngredientsById(int id)
  {
    string sql = @"
    SELECT
    *
    FROM ingredients
    WHERE id = @id;
    ";
    return _db.QueryFirstOrDefault<Ingredient>(sql, new { id });
  }
  internal Ingredient CreateIngredient(Ingredient ingredientData)
  {
    string sql = @"
    INSERT INTO ingredients
    (name, quantity, recipeId)
    VALUES
    (@name, @quantity, @recipeId);
    SELECT LAST_INSERT_ID();
    ";
    int id = _db.ExecuteScalar<int>(sql, ingredientData);
    ingredientData.Id = id;
    return ingredientData;
  }


  internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
  {
    string sql = @"
    SELECT
    *
    FROM ingredients
    WHERE recipeId = @recipeId;
    ";
    return _db.Query<Ingredient>(sql, new { recipeId }).ToList();
  }

  internal bool RemoveIngredient(int id)
  {
    string sql = @"
    DELETE FROM ingredients
    WHERE id = @id;
    ";
    int rows = _db.Execute(sql, new { id });
    return rows > 0;
  }
}
