export class Recipe{
    constructor(data){
        this.id = data.id
        this.title = data.title
        this.instructions = data.instructions
        this.img = data.img
        this.category = data.category
        this.creator = data.creator
        this.createdAt = data.createdAt
        this.updatedAt = data.updatedAt
    }
}