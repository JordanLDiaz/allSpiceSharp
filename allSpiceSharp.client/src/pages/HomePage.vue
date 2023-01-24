<template>
  <div class="container-fluid">
    <!-- SECTION Banner -->
    <section class="row mt-2">
      <div class="col-12 text-center">
        <img src="../assets/img/AllSpice_Banner.png" alt="banner" class="elevation-5 rounded">
      </div>
    </section>


    <!-- SECTION Home | My Recipes | Favorites -->
    <section class="row mt-2 justify-content-center my-2">
      <div class="col-4 d-flex justify-content-between">
        <button @click="getRecipes()" class="btn btn-success bg-success text-light mx-1">Home</button>
        <button @click="getMyRecipes()" class="btn btn-success bg-success text-light mx-1">My Recipes</button>
        <button @click="getFavorites()" class="btn btn-success bg-success text-light mx-1">Favorites</button>
      </div>
    </section>

    <!-- SECTION Recipes -->
    <section class="row justify-content-around">
      <div v-for="r in recipes" :key="r.id" class="col-4 p-5">
        <RecipeCard :recipe="r" />
      </div>
    </section>
  </div>

  <!-- SECTION Add Recipe Button -->
  <section class="position-fixed bottom-0 end-0 p-2">
    <h1>
      <i class="mdi mdi-plus-circle selectable text-success" title="=Add Recipe"></i>
    </h1>

  </section>
</template>

<script>
import { onMounted, computed } from "vue";
import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { recipesService } from '../services/RecipesService.js'

export default {
  setup() {
    async function getRecipes() {
      try {
        await recipesService.getRecipes();
      } catch (error) {
        logger.error(error)
        Pop.error(error.message)
      }
    }
    onMounted(() => getRecipes())
    return {
      recipes: computed(() => AppState.recipes),
      getRecipes,
      getMyRecipes() {
        AppState.recipes = AppState.recipes.filter(r => r.creatorId == AppState.account.id);
      },

      //   async getFavorites() {
      //     try {
      //       await recipesService.getFavorites();
      //     } catch (error) {
      //       logger.error(error)
      //       Pop.error(error.message)
      //     }
      // }
    }
  }
}
</script>

<style scoped lang="scss">
img {
  height: 35vh;
  width: 95vw;
}

button {
  width: 40vw;
}
</style>
