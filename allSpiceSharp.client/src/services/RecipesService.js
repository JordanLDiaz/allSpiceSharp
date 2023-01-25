import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class RecipesService {
  async getRecipes() {
    const res = await api.get('api/recipes')
    logger.log('[GETTING ALL RECIPES]', res.data)
    AppState.recipes = res.data
  }

  async getFavorites() {
    const res = await api.get('account/favorites')
    logger.log('[GETTING MY FAVORITES]', res.data)
    AppState.recipes = res.data
  }

  async createRecipe(formData) {
    const res = await api.post('api/recipes', formData)
    logger.log(['CREATING RECIPE'], res.data)
    AppState.activeRecipe = res.data
    AppState.recipes.push(res.data)
    return res.data
  }

  async searchRecipes(query) {
    const res = await api.get('api/recipes')
    AppState.recipes = res.data
    logger.log('[SEARCHING RECIPES]', res.data)
    AppState.recipes = AppState.recipes.filter(r => r.category.toString().toUpperCase().includes(query.toUpperCase()))
  }
}

export const recipesService = new RecipesService();
