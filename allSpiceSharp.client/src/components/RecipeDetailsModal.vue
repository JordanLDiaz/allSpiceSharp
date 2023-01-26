<template>
  <div v-if="recipe">
    <div class="container-fluid py-0 ps-0 modal-body">
      <div class="row">
        <div class="col-4">
          <img :src="activeRecipe.img" alt="" class="img-fluid selectable recipe-img rounded">
        </div>
        <div class="col-8">
          <div class="row justify-content-end">
            <div class="col-1">
              <button @click="favoriteRecipe" class="btn text-success fs-5" title="Favorite Recipe"><i
                  class="mdi mdi-heart"></i></button>
            </div>
            <div class="col-1">
              <button @click="deleteRecipe" class="btn text-danger fs-5" title="Delete Recipe"><i
                  class="mdi mdi-delete"></i></button>
            </div>
            <button type="button" class="btn-close me-2 mt-2" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="row">
            <h2 class="text-success">{{ activeRecipe.title }}</h2>
            <h4>{{ activeRecipe.category }}</h4>
          </div>
          <div class="row">
            <div class="col-6 card p-0">
              <h5 class="bg-success text-center py-1">Recipe Ingredients</h5>
              <div v-for="i in ingredients" :key="i.id">
                <Ingredients :ingredient="i" />
              </div>
            </div>
            <div class="col-6 card p-0">
              <h5 class="bg-success text-center py-1">Recipe Instructions</h5>
              <div class="">{{ activeRecipe.instructions }}</div>
            </div>
          </div>
          <div class="row">
            <p class="text-end mt-5">Recipe published by: {{ activeRecipe.creator.name }}</p>
          </div>
        </div>
      </div>
    </div>
  </div>

</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, watchEffect, ref } from 'vue';
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { favoritesService } from '../services/FavoritesService.js';
import { recipesService } from '../services/RecipesService.js';
import { Modal } from 'bootstrap';

export default {

  setup() {

    return {
      activeRecipe: computed(() => AppState.activeRecipe),
      ingredients: computed(() => AppState.ingredients),
      recipe: computed(() => AppState.activeRecipe),
      account: computed(() => AppState.account),

      async deleteRecipe() {
        try {
          // logger.log('click delete', AppState.activeRecipe.id)
          const recipeId = AppState.activeRecipe.id
          await recipesService.deleteRecipe(recipeId)
          Modal.getOrCreateInstance('#recipeDetailModal').hide()
        } catch (error) {
          logger.error(error)
          Pop.error(error.message)
        }
      },

      async favoriteRecipe() {
        try {
          logger.log('clicked heart', AppState.activeRecipe.id)
          const favoriteData = { recipeId: AppState.activeRecipe.id }
          await favoritesService.favoriteRecipe(favoriteData)
        } catch (error) {
          logger.error(error)
          Pop.error(error.message)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.recipe-img {
  max-height: 50vh;
  min-height: 35vh;
  width: 100%;
  object-fit: cover;
}
</style>