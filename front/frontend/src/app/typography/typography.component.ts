import { Component, OnInit } from '@angular/core';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-typography',
  templateUrl: './typography.component.html',
  styleUrls: ['./typography.component.scss']
})
export class TypographyComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }
  test: boolean = true
  showToast() {
    while(!this.test) {
    const Toast = Swal.mixin({
      toast: true,
      position: 'top-end',
      showConfirmButton: false,
      timer: 4000,
      timerProgressBar: true,
      didOpen: (toast) => {
        toast.addEventListener('mouseenter', Swal.stopTimer)
   
        toast.addEventListener('mouseleave', Swal.resumeTimer)
      } ,
      html:
      '  <div>'+
     ' loading now . . .'+
     '</div>',
    });
    Toast.fire({
    })
  }
   
  
  
    
  }
  
  reloadCurrentPage() {
    window.location.reload();
   }

}
