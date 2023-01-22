<template>
  <div class="col-12 recipe-img elevation-5">
    <img :src="recipe.img" alt="" class="img-fluid selectable">
    <div class="row">
      <h5 class="col-10">{{ recipe.category }}</h5>
      <h4 class="col-1"><i class="mdi mdi-heart text-success"></i></h4>
    </div>
    <div class="row">
      <div class="col-12">
        <h4>{{ recipe.title }}</h4>
      </div>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';

export default {
  props: {
    recipe: { type: Object, required: true }
  },

  setup() {
    return {
      // recipeImg: computed(() => `url('${props.recipe.img}')`)
    }
  }
};
</script>


<style lang="scss" scoped>
.recipe-img {
  background-position: center;
  background-size: cover;
  min-height: 50vh;
}
</style>