<template>
  <div v-if="ingredient">
    <div class=""><span>{{ ingredient.quantity }}</span> {{ ingredient.name }}</div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';

export default {
  props: {
    ingredient: { type: Object, required: true }
  },

  setup() {
    return {
    }
  }
};
</script>


<style lang="scss" scoped>

</style>