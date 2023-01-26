<template>
  <div class="container-fluid">
    <!-- SECTION Banner -->
    <section class="row mt-2 justify-content-center">
      <div class="col-12 text-center banner elevation-5 rounded">
        <div class="row mt-2 justify-content-end">
          <div class="col-2">
            <div class="d-flex justify-content-end search">
              <form @submit.prevent="searchRecipes" class="input-group">
                <input v-model="search" class="form-control rounded" type="text" name="recipe" id="recipe"
                  placeholder="Search recipes by category">
                <button type="submit" title="Search Recipes by Category" class="btn btn-success w-20">
                  <i class="mdi mdi-magnify"></i>
                </button>
              </form>
            </div>
          </div>
        </div>
        <div class="row justify-content-center">
          <div class="col-4 text-white text-shadow">
            <h1>ALL SPICE</h1>
            <h4>Cherish Your Family</h4>
            <h4>And Their Cooking</h4>
          </div>
        </div>
        <!-- <img src="../assets/img/AllSpice_Banner.png" alt="banner" class="elevation-5 rounded banner"> -->
      </div>
    </section>

    <!-- SECTION Home | My Recipes | Favorites -->
    <section class="row mt-2 justify-content-center my-2">
      <div class="col-4 d-flex justify-content-between">
        <button @click="getRecipes()" class="btn btn-success bg-success text-light mx-1 elevation-5">Home</button>
        <button @click="getMyRecipes()" class="btn btn-success bg-success text-light mx-1 elevation-5">My
          Recipes</button>
        <button @click="getFavorites()"
          class="btn btn-success bg-success text-light mx-1 elevation-5">Favorites</button>
      </div>
    </section>

    <!-- SECTION Recipes -->
    <section class="row justify-content-around">
      <div v-for="r in recipes" :key="r.id" class="col-4 px-4 my-3">
        <RecipeCard :recipe="r" />
      </div>
    </section>
  </div>

  <!-- SECTION Add Recipe Button -->
  <section class="position-fixed bottom-0 end-0 p-2">
    <button class="btn btn-success bg-success elevation-5 rounded-circle" style="height: 60px; width: 60px"
      data-bs-toggle="modal" data-bs-target="#addRecipeForm">
      <i class="mdi mdi-plus-circle selectable text-light fs-2" title="Add Recipe"></i>
    </button>
  </section>

  <ModalComponent id="addRecipeForm">
    <AddRecipeForm />
  </ModalComponent>

</template>

<script>
import { onMounted, computed, ref } from "vue";
import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { recipesService } from '../services/RecipesService.js'
import ModalComponent from "../components/ModalComponent.vue";

export default {
  setup() {
    const search = ref('')

    async function getRecipes() {
      try {
        await recipesService.getRecipes();
      }
      catch (error) {
        logger.error(error);
        Pop.error(error.message);
      }
    }
    onMounted(() => getRecipes());
    return {
      recipes: computed(() => AppState.recipes),
      getRecipes,
      search,

      async getMyRecipes() {
        await recipesService.getRecipes()
        AppState.recipes = AppState.recipes.filter(r => r.creatorId == AppState.account.id);
      },

      async getFavorites() {
        try {
          await recipesService.getFavorites();
        }
        catch (error) {
          logger.error(error);
          Pop.error(error.message);
        }
      },

      async searchRecipes() {
        try {
          await recipesService.searchRecipes(search.value)
          this.search = ''
        } catch (error) {
          logger.error(error)
          Pop.error(error.message)
        }
      }
    };
  },
  components: { ModalComponent }
}
</script>

<style scoped lang="scss">
.banner {
  background-image: url('https://images.unsplash.com/photo-1564149504298-00c351fd7f16?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80');
  background-position: center;
  background-size: cover;
  height: 35vh;
  width: 95vw;
}

button {
  width: 40vw;
}

.text-shadow {
  text-shadow: 2px 2px 4px #484848;
}
</style>
