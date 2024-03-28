<template>
  <div class="container">
    <div class="row d-flex flex-wrap">
      <div v-for="recipe in recipes" :key="recipe.id" class="col-11 col-sm-8 col-md-6 col-lg-4">

      </div>
    </div>
  </div>
</template>

<script>
import { guardReactiveProps, onMounted } from 'vue';
import Pop from '../utils/Pop';

export default {
  setup() {
    onMounted(() => {
      getRecipes()
    })
    return {
      async getRecipes() {
        try {
          const recipes = await recipesService.getRecipes()
          return recipes
        } catch (error) {
          Pop.error(error)
        }
      }
    }
  }
}
</script>

<style scoped lang="scss"></style>
