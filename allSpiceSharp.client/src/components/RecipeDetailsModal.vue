<template>
  <div v-if="recipe">
    <div class="container-fluid py-0 ps-0 modal-body">
      <div class="row">
        <div class="col-4">
          <img :src="activeRecipe.img" alt="" class="img-fluid selectable recipe-img rounded">
        </div>
        <div class="col-8">
          <div class="row">
            <div class="col-9">
              <h2 class="text-success">{{ activeRecipe.title }}</h2>
              <h5 class="text-primary">{{ activeRecipe.category }}</h5>
            </div>
            <div class="col-1">
              <button v-if="!recipe.favoriteId" @click="favoriteRecipe" class="btn text-success fs-5"
                title="Favorite Recipe"><i class="mdi mdi-heart"></i></button>
              <button v-else class="btn text-success fs-5" title="Unfavorite Recipe"><i
                  class="mdi mdi-heart-broken"></i></button>
            </div>
            <div class="col-1">
              <button v-if="account.id == recipe.creatorId" @click="deleteRecipe" class="btn text-danger fs-5"
                title="Delete Recipe"><i class="mdi mdi-delete"></i></button>
            </div>
            <button type="button" class="btn-close me-2 mt-2" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>

          <div class="row justify-content-evenly">
            <div class="col-5 card p-2 m-1">
              <h5 class="bg-success text-center py-1">Recipe Ingredients</h5>
              <div v-for="i in ingredients" :key="i.id">
                <Ingredients :ingredient="i" />
              </div>
            </div>
            <div class="col-5 card p-2 m-1">
              <h5 class="bg-success text-center py-1">Recipe Instructions</h5>
              <div class="">{{ activeRecipe.instructions }}</div>
              <form>
                <input placeholder="Add Next Step" />
                <button @click="" class="text-success" title="add instruction"><i class="mdi mdi-plus"></i></button>
              </form>
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
          if (await Pop.confirm('Are you sure you want to delete this recipe?')) {
            const recipeId = AppState.activeRecipe.id
            await recipesService.deleteRecipe(recipeId)
            Modal.getOrCreateInstance('#recipeDetailModal').hide()
            Pop.success('Recipe has been deleted.')
          }

        } catch (error) {
          logger.error(error)
          Pop.error(error.message)
        }
      },

      async favoriteRecipe() {
        try {
          // logger.log('clicked heart', AppState.activeRecipe.id)
          const recipeId = { recipeId: AppState.activeRecipe.id }
          await favoritesService.favoriteRecipe(recipeId)
          Pop.success('Recipe is now in your favorites!')
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