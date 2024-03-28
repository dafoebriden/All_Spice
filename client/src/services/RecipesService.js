import { api } from "./AxiosService"

class RecipesService{
    async getRecipes(){
    const recipes = await api.get('api/recipes')
    return recipes
}
}
export const recipesService = new RecipesService()