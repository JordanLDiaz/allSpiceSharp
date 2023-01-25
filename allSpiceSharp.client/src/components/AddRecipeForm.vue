<template>
  <div class="modal-header bg-success text-light">
    <h1>New Recipe</h1>
    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
  </div>

  <div class="modal-body">
    <form @submit.prevent="createRecipe">
      <div class="row">
        <div class="col-6">
          <label for="title" class="form-label">Title</label>
          <input v-model="editable.title" type="text" maxlength="50" class="form-control" id="title"
            placeholder="Title..." required>
        </div>
        <div class="col-6">
          <label for="category" class="form-label">Category</label>
          <select v-model="editable.category" class="form-select" id="category" aria-label="Default select example"
            required>
            <option selected default>Choose Category</option>
            <option value="american">American</option>
            <option value="asian">Asian</option>
            <option value="breakfast">Breakfast</option>
            <option value="cheese">Cheese</option>
            <option value="dessert">Dessert</option>
            <option value="italian">Italian</option>
            <option value="mexican">Mexican</option>
            <option value="salad">Salad</option>
            <option value="seafood">Seafood</option>
            <option value="soup">Soup</option>
            <option value="misc">Misc.</option>
          </select>
        </div>
        <div class="col-12">
          <label for="img" class="form-label">Image URL</label>
          <input v-model="editable.img" type="text" maxlength="250" class="form-control" id="img"
            placeholder="Image Url..." required>
        </div>
      </div>
      <div class="row">
        <p class="text-center mt-3">You will add ingredients and instructions after your recipe is created!</p>
      </div>
      <div class="modal-footer">
        <button type="submit" class="btn btn-success bg-success">Add Recipe</button>
        <button type="button" class="btn" data-bs-dismiss="modal">Close</button>
      </div>
    </form>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import { recipesService } from "../services/RecipesService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
export default {
  setup() {
    const editable = ref({})
    return {
      editable,

      async createRecipe() {
        try {
          await recipesService.createRecipe(editable.value)
          editable.value = {}
          Modal.getOrCreateInstance('#addRecipeModal').hide()
          // Modal.getOrCreateInstance('#exampleModal').show()
        } catch (error) {
          logger.error(error)
          Pop.error(error.message)
        }
      }
    }
  }
};
</script>


<style lang="scss" scoped>

</style>