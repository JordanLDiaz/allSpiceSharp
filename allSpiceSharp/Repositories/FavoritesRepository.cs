namespace allSpiceSharp.Repositories;

public class FavoritesRepository
{
  private readonly IDbConnection _db;
  public FavoritesRepository(IDbConnection db)
  {
    _db = db;
  }
  internal Favorite Create(Favorite favoriteData)
  {
    string sql = @"
    INSERT INTO favorites
    (accountId, recipeId)
    VALUES
    (@accountId, @recipeId);
    SELECT LAST_INSERT_ID();
    ";
    int id = _db.ExecuteScalar<int>(sql, favoriteData);
    favoriteData.Id = id;
    return favoriteData;
  }

  internal List<MyFavorite> GetFavoritesByAccountId(string userId)
  {
    string sql = @"
    SELECT
    recipes.*,
    favorites.*,
    creator.* 
    FROM favorites
    JOIN recipes ON recipes.id = favorites.recipeId
    JOIN accounts creator ON recipes.creatorId = creator.id
    WHERE favorites.accountId = @userId;
    ";
    List<MyFavorite> myFavorites = _db.Query<MyFavorite, Favorite, Account, MyFavorite>(sql, (recipes, favorites, creator) =>
    {
      recipes.FavoriteId = favorites.Id;
      recipes.Creator = creator;
      return recipes;
    }, new { userId }).ToList();
    return myFavorites;
  }

  internal Favorite GetOneFavorite(int id)
  {
    string sql = @"
    SELECT
    *
    FROM favorites
    WHERE id = @id;
    ";
    return _db.QueryFirstOrDefault<Favorite>(sql, new { id });
  }

  internal bool RemoveFavorite(int id)
  {
    string sql = @"
    DELETE FROM favorites
    WHERE id = @id;
    ";
    int rows = _db.Execute(sql, new { id });
    return rows > 0;
  }
}
