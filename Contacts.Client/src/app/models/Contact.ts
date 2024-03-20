export interface Contact{
    id:string,
    firstName:string,
    lastName:string,
    address:string,
    isBookmarked:boolean,
    contactTags:any[]
    emails:any[],
    numbers:any[]
}

export interface PostContact extends Omit<Contact, 'id'> {}

export interface SearchContact extends Omit<Contact, 'id'|'address'|'isBookmarked' |'emails' | 'numbers'> {}