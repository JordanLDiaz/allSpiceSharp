<template>
  <div class="row elevation-5 rounded">
    <div class="col-12">
      <img :src="recipe.img" alt="" class="img-fluid selectable recipe-img pt-2">
    </div>

    <h5 class="col-12">{{ recipe.category }}</h5>

    <div class="col-11">
      <h4>{{ recipe.title }}</h4>
    </div>
    <h3 class="col-1 p-1"><i class="mdi mdi-heart text-success"></i></h3>

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

    }
  }
};
</script>


<style lang="scss" scoped>
.recipe-img {
  max-height: 35vh;
  min-height: 35vh;
  width: 100%;
  object-fit: cover;
}
</style>