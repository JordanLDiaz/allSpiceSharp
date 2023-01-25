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
              <button class="btn text-danger fs-5" title="Delete Recipe"><i class="mdi mdi-delete"></i></button>
            </div>
            <button type="button" class="btn-close me-2 mt-2" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="row">
            <h3 class="text-success">{{ activeRecipe.title }}</h3>
            <h5>{{ activeRecipe.category }}</h5>
          </div>
          <div class="row">
            <div class="col-6 card p-0">
              <div class="bg-success text-center">Recipe Ingredients</div>
              <div class="">{{ activeRecipe.ingredients }}</div>
            </div>
            <div class="col-6 card p-0">
              <div class="bg-success text-center">Recipe Instructions</div>
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
import { ingredientsService } from "../services/IngredientsService.js"


export default {

  setup() {
    // const ingredients = ref({});

    watchEffect(() => {
      if (AppState.activeRecipe) {
        getIngredients()
      }
    })


    async function getIngredients() {
      try {
        await ingredientsService.getIngredients()
      } catch (error) {
        logger.error(error)
        Pop.error(error.message)
      }
    }

    onMounted(() => getIngredients())

    return {
      getIngredients,
      activeRecipe: computed(() => AppState.activeRecipe),
      ingredients: computed(() => AppState.ingredients),
      recipe: computed(() => AppState.activeRecipe),

    }
  }
};
</script>


<style lang="scss" scoped>
.recipe-img {
  max-height: 50vh;
  min-height: 35vh;
  width: 100%;
  object-fit: cover;
}

// .ingredient-card,
// .instructions-card {}
</style>