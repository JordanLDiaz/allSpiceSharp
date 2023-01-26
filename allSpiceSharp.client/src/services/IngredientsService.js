import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class IngredientsService {
  async getIngredients(recipeId) {
    const res = await api.get('api/recipes/' + recipeId + '/ingredients')
    logger.log('[GETTING INGREDIENTS]', res.data)
    AppState.ingredients = res.data
  }
}
export const ingredientsService = new IngredientsService();