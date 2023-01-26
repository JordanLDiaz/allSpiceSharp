import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class FavoritesService {
  async favoriteRecipe(recipeId) {
    const res = await api.post('api/favorites', recipeId)
    logger.log('[FAVORITED RECIPE]', res.data)

  }
}

export const favoritesService = new FavoritesService();