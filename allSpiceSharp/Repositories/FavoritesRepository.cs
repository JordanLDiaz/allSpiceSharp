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

  //   internal List<MyFavorite> GetFavoritesByAccountId(string userId)
  //   {
  //     string sql = @"
  //     SELECT
  // r.*,
  // f.*,
  // cr.* 
  // FROM favorites f
  // JOIN recipes r ON r.id = f.recipeId
  // JOIN accounts cr ON r.creatorId = cr.id
  // WHERE f.accountId = @userId;
  //     ";
  //     List<MyFavorite> myfavorites = _db.Query<MyFavorite, Favorite, Account, MyFavorite>(sql, (reciple, favorite, creator) =>
  //     {
  //       recipes.FavoriteId = favorite.Id;
  //       recipes.Creator = creator;
  //       return Recipe;
  //     }, new Object { userId }).ToList();
  //     return myfavorites;
  //   }
}
