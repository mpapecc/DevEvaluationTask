export interface Tag{
    id:string,
    value:string
}

export interface PostTag extends Omit<Tag, 'id'> {}