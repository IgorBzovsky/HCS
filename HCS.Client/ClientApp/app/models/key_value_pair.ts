export class KeyValuePair {
    id: number;
    name: string;

    constructor(id?: number, name?: string) {
        this.id = id ? id : 0;
        this.name = name ? name : '';
    }
}