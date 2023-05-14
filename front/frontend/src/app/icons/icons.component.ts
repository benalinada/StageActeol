import { Component, OnInit } from '@angular/core';
import { ServerService } from 'app/services/server.service';
import { UserAppService } from 'app/services/userApp.service';
import { catchError, of, tap } from 'rxjs';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-icons',
  templateUrl: './icons.component.html',
  styleUrls: ['./icons.component.css']
})
export class IconsComponent implements OnInit {

   [x: string]: any;
  
  liste_des_bd: any = []; // liste de base de donne de serveur 
  liste_des_table: any = [];// liste de fact table  de base de donné slecté 
  liste_des_dim: any = []; // liste de dim de fact table 
  serveurid: any; // servur id slecté 
  bd_name: any; // dbname slecte 
  lodaing: boolean = false;
  selectedServer :any;
  selectedServerSourceId : any;
  newCubename: any;
   selectedDatabase : any;
   responseDate : Date;
   isLoading = false;
  constructor(private serverService: ServerService, private userService: UserAppService) { }

  async ngOnInit() {
    
   this.getServers()  
   this.Show_Calculation();
  }
          // serveur Engine 
          getServers() {
            this.liste_des_serveur = []
            this.isLoading = true;
            this.serverDisplays = new Map<string, number>();
              let progress = 0;
            const data = this.serverService.getServers(this.userService.user.Id)

              .pipe(
                catchError(err => of(null)),
                tap(() => this.lodaing == false)
          
              ).subscribe(data => {
                this.liste_des_serveur = data.Servers
                if (data) {
                  for (let s of data.Servers) {
                    this.serverDisplays.set(s.Id, s.Name);
                    this.getServers
                  }
                }
                this.isLoading = false;
              });


          }
           
          //  get db a partir de id serveur 
          setserveur(id: any) {
            this.serveurid = id;
            this.isLoading = true;
            const data = this.serverService.getBbs(id)
              .pipe(
                catchError(err => of(null)),
                tap(() => this.lodaing == false)
              ).subscribe(data => {

                if (data) {
                  this.liste_des_bd = data.DataBases

                }
                this.isLoading = false;
              });
          }
// get messure 
getmessures (name : any) {
  this.liste_messure = []
  this.isLoading = true;
  this.MessureDisplays = new Map<string, number>();
    let progress = 0;
  const data = this.serverService.getMessures(this.serveurid,  name)

  .pipe(
    catchError(err => of(null)),
    tap(() => this.lodaing == false)
  ).subscribe((data: any) => {
           this.liste_messure = data.messures
           this.isLoading = false;
    });


}
 /*Add_Calculations'*/
 async Show_Calculation(){
  const { value: accept } = await Swal.fire({
    title:'<h2 style ="text-align: center;">Welcome To Add Calculatio(s) ! </h2>',
    
    width: '950px',
    html: '<html>'+
    '<div style="padding: 10px;text-align: center;margin-top: 0">'+ 
    'To add calculation(s), you typically follow these steps:'+  '<br>'+
    '<br>'+
'<ul> 1-Select a cube source: This involves choosing the server source name and the cube name.</ul>'+
'<ul> 2-You should create operation(s):This involves selecting mesures , .</ul> '+
'<ul> 3-Select a destination server and set a name for the new cube: After selecting the database source, you need to choose the destination server where you want to'+
' dispatch the cube. You also need to provide a name for the new cube that will be created on the destination server.</ul> '+
'<br>'+
'--> By following these steps, you can successfully dispatch an OLAP cube and'+ 'create a new cube on the destination server. '+
'</p>'+
'</div>'+

         '</html>',
    confirmButtonText:
      'Continue <i class="fa fa-arrow-right"></i>',
   
  })
}

//sammary confirm
confirm(){
  const swalWithBootstrapButtons = Swal.mixin({
    customClass: {
      confirmButton: 'btn btn-success',
      cancelButton: 'btn btn-danger'
    },
    buttonsStyling: false
  })
  
  swalWithBootstrapButtons.fire({
    title: 'Are you sure?',
    showCancelButton: true,


    confirmButtonText: 'Yes, add !',
    cancelButtonText: 'No, cancel!',
    reverseButtons: true
  }).then((result) => {
    if (result.isConfirmed) {
      swalWithBootstrapButtons.fire(
 
        'success'
      )
    } else if (
      /* Read more about handling dismissals below */
      result.dismiss === Swal.DismissReason.cancel
    ) {}
 
  })
}


}


