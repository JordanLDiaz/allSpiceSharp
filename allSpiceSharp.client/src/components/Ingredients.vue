<template>
  <div class="row">
    <div class="col-9"><span>{{ ingredient?.quantity }}</span> {{ ingredient?.name }}</div>
    <h5 v-if="account.id == recipe.creatorId" @click="" class="col-1"><i class="mdi mdi-pen selectable text-primary"
        title="edit ingredient"></i> </h5>
    <h5 v-if="account.id == recipe.creatorId" @click="" class="col-1"><i
        class="mdi mdi-delete-forever-outline selectable text-danger" title="edit ingredient"></i>
    </h5>
    <form v-if="account.id == recipe?.creatorId" @submit.prevent="">
      <input placeholder="Add Quantity" />
      <input placeholder="Add Ingredient Name" />
      <button class="text-success" title="add ingredient" type="submit"><i class="mdi mdi-plus"></i></button>
    </form>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';

export default {
  props: {
    ingredient: { type: Object, required: true }
  },

  setup() {
    const newIngredient = ref({})
    return {
      account: computed(() => AppState.account),
      recipe: computed(() => AppState.activeRecipe),
      newIngredient,

      // async addIngredient() {

      // }
    }
  }
};
</script>


<style lang="scss" scoped>

</style>