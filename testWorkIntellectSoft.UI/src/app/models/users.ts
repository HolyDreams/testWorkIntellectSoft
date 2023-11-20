import { Phone } from "./phones";

export class User {
    id?: number;
    firstName = "";
    lastName = "";
    birthyear!: number;
    phones!: Phone[];
}