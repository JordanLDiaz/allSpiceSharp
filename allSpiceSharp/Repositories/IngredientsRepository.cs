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

  internal bool RemoveIngredient(int ingredientId)
  {
    string sql = @"
    DELETE FROM ingredients
    WHERE ingredientId = @ingredientId;
    ";
    int rows = _db.Execute(sql, new { ingredientId });
    return rows > 0;
  }
}
