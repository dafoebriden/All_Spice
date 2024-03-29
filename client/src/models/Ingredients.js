export class Ingredient{
    constructor(data){
        this.id = data.id
        this.name = data.name
        this.quantity = data.quantity
        this.createdAt = data.createdAt
        this.updatedAt = data.updatedAt
        this.recipeId = data.recipeId
    }
}