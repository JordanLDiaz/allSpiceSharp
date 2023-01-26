import { api } from "./AxiosService.js";

class FavoritesService {
  async favoriteRecipe(favoriteData) {
    const res = await api.post('api/favorites', favoriteData)

  }
}

export const favoritesService = new FavoritesService();