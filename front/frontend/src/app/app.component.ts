import { Component} from '@angular/core';
import { authConfig } from './config/auth.config';
import { JwksValidationHandler, OAuthService } from 'angular-oauth2-oidc';
import Swal from 'sweetalert2';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  globalVariable: string;

  title = 'Automatic OLAP Cube ';
  constructor(private oauthService: OAuthService) {
    this.configure();
     this.welcomme();
  }

private configure() {
  this.oauthService.configure(authConfig);
  this.oauthService.tokenValidationHandler = new JwksValidationHandler();
  this.oauthService.loadDiscoveryDocumentAndTryLogin();
}
async welcomme(){
      const {} = Swal.fire({
      title: 'ACTEOL ',
      text: 'OLAP Builder and dispatcher : You can generate automatically an OLAP cube and dispatch to diffrent Client/servers .',
       imageUrl : 'https://media.istockphoto.com/id/1294997127/vector/welcome-concept-team-of-people.jpg?s=1024x1024&w=is&k=20&c=HlP5N80R0f96WjnkKuPVcI_Vxglr_YuWeMykXYCf-F4%3D&fbclid=IwAR0HJfRGcJJj17OxMJVp0S3-sU8DOPGrgjgvVTN5iPtpAFDu-5MpcIQJxB8',
      imageWidth: 400,
      imageHeight: 200,
      imageAlt: 'Custom image',
    }).then((result) => {
      /* Read more about isConfirmed, isDenied below */
      if (result.isConfirmed) {
        this.globalVariable = "true";
      }})
    }
}
