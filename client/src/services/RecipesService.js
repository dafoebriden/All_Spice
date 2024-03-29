import { AppState } from "../AppState"
import { Ingredient } from "../models/Ingredients"
import { Recipe } from "../models/Recipe"
import { api } from "./AxiosService"

class RecipesService{
    async getRecipes(){
        AppState.recipes = []
        const res = await api.get('api/recipes')
        AppState.recipes = res.data.map(rec=> new Recipe(rec))
}
    async getRecipe(id){
        AppState.activeRecipe = null
        const res = await api.get(`api/recipes/${id}`)
        AppState.activeRecipe = new Recipe(res.data)
    }
    async getIngredients(id){
        AppState.ingredients = []
        const res = await api.get(`api/recipes/${id}/ingredients`)
        AppState.ingredients = res.data.map(i=> new Ingredient(i))
    }
}
export const recipesService = new RecipesService()