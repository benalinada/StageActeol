import { Component } from '@angular/core';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'argon-dashboard-angular';
  ngOnInit() {
    (async () => {

      const { value: Serveurs } = await Swal.fire({
        title: 'Select Serveur ',
        input: 'select',
        inputOptions: {
          'Serveurs': {
           Serveurname: 'Serveur id',
           Serveurname2: 'Serveur id2',

       
          },
         
        },
        inputPlaceholder: 'Select a Serveur',
        confirmButtonText: 'Connect'
        
      })
      
      if (Serveurs) {
        Swal.fire(`You selected: ${Serveurs}`)
      }
      
    })();
};
}