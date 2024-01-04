import { Token } from "./token";

export class Response {
    success?: boolean ;
    message? = "";
    data: Token = {};
}