import { Injectable } from "@angular/core";
import { OAuthService } from "angular-oauth2-oidc";

@Injectable()
export class UserService {
   user : any;
    constructor(private oauthService: OAuthService) {

    }
    
       
}


