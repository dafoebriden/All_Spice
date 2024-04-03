<template>
  <div class="container">
    <div class="row d-flex justify-content-evenly flex-wrap">
      <div @click="getRecipe(recipe.id)" v-for="recipe in recipes" :key="recipe.id" class="recipe" role="button"
        type="button" data-bs-toggle="modal" data-bs-target="#activeRecipe"
        :style="{ backgroundImage: `url(${recipe.img})` }">
        <div>
          <p class="recipe-category rounded-pill">{{ recipe.category }}</p>
        </div>
        <div>
          <h4 class="recipe-title">{{ recipe.title }}</h4>
        </div>
      </div>
    </div>
  </div>






  <!-- NOTE Modal -->
  <div class="modal fade" id="activeRecipe" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1"
    aria-labelledby="activeRecipeLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered modal-xl ">
      <div v-if="recipe" class="modal-content">
        <div class="container-fluid modal-body modal-recipe">
          <div class="modal-recipe-picture" :style="{ backgroundImage: `url(${recipe.img})` }">
            <div><i class="mdi mdi-heart text-danger"></i></div>
          </div>
          <div class="row modal-recipe-content">
            <div class="p-0">
              <div class="col-12 d-flex justify-content-end">
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
              </div>
              <div class="col-12 d-flex align-items-center mb-3">
                <h1 class="m-0 ms-2">{{ recipe.title }}</h1>
                <p class="ms-2  recipe-title rounded-pill">{{ recipe.category }}</p>
              </div>
            </div>
            <div class="p-0 d-flex justify-content-center justify-content-md-evenly flex-wrap">
              <div class="col-11 col-lg-5 mb-3">
                <div class="recipe-card">
                  <div class="recipe-card-header">
                    <h2 class="m-0">Instructions</h2>
                  </div>
                  <div class="recipe-card-body">
                    <p class="m-0">{{ recipe.instructions }}</p>
                  </div>
                </div>
              </div>
              <div class="col-11 col-lg-5">
                <div class="recipe-card">
                  <div class="recipe-card-header">
                    <h2 class="m-0 text-align-center">Ingredients <span class="add-button" role="button">+</span>
                    </h2>
                  </div>
                  <div class="recipe-card-body">
                    <p v-for="ingredient in ingredients" :key="ingredient.id" class="m-0">{{ ingredient.quantity }} {{
        ingredient.name }}</p>
                  </div>
                </div>
              </div>
            </div>

            <div class="col-12">
              <p class="mb-1 text-end">published by: {{ recipe.creator.name }}</p>
            </div>
          </div>
        </div>

      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue';
import Pop from '../utils/Pop';
import { recipesService } from '../services/RecipesService';
import { AppState } from '../AppState';

export default {
  setup() {
    onMounted(() => {
      getRecipes()
    })
    async function getRecipes() {
      try {
        await recipesService.getRecipes()
      } catch (error) {
        Pop.error(error)
      }
    }
    async function getRecipe(id) {
      try {
        // router.push(`/${id}`)
        await recipesService.getRecipe(id)
        await recipesService.getIngredients(id)
      } catch (error) {
        Pop.error(error)
      }
    }
    return {
      recipes: computed(() => AppState.recipes),
      recipe: computed(() => AppState.activeRecipe),
      ingredients: computed(() => AppState.ingredients),
      getRecipe
    }
  }
}
</script>

<style scoped lang="scss">
.recipe {
  height: 300px;
  width: 300px;
  box-shadow: 0px 0px 10px black;
  margin: 25px;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  border-radius: 5px;
  background-size: cover;
  background-position: center;
}

.recipe:hover {
  box-shadow: 0px 0px 15px black;
  border-radius: 5px;
  border: 2px solid white;
  background-size: cover;
  background-position: center;
}

.recipe-category {
  background-color: rgba(0, 0, 0, 0.4);
  border: 1px solid white;
  width: fit-content;
  padding-left: 8px;
  padding-right: 8px;
  margin: 10px;
  font-weight: bold;
  color: white;
}

.recipe-title {
  background-color: rgba(0, 0, 0, 0.4);
  border-radius: 5px;
  border: 1px solid white;
  padding-left: 8px;
  padding-right: 8px;
  margin: 10px;
  font-weight: bold;
  color: white;
  ;
}

.modal-recipe {
  min-height: 600px;
  padding: 0px;
}

.modal-recipe-picture {
  height: 300px;
  width: 100%;
  display: flex;
  justify-content: end;
  background-size: cover;
  background-position: center;
  border-top-left-radius: 7px;
  border-top-right-radius: 7px;
}

.modal-recipe-content {
  width: 100%;
  flex-direction: column;

  justify-content: space-between;
  padding-left: 15px;
}

@media screen and (min-width: 1200px) {
  .modal-recipe {
    min-height: 600px;
    display: flex;
    padding: 0px;
  }

  .modal-recipe-picture {
    height: auto;
    width: 40%;
    border-top-left-radius: 7px;
    border-bottom-left-radius: 7px;
    border-top-right-radius: 0px;
  }

  .modal-recipe-content {
    width: 61%;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    padding-left: 15px;
  }
}

.recipe-card {
  box-shadow: 0px 0px 8px black;
  border-radius: 8px;
}

.recipe-card-header {
  text-align: center;
  background-color: rgb(82 115 96);
  border-top-right-radius: 8px;
  border-top-left-radius: 8px;
}

.recipe-card-body {
  padding: 5px;
}

.add-button {
  text-shadow: 0px 0px 5px white;
}
</style>
