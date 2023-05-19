import { Component, OnInit } from '@angular/core';
import {  DispatchData } from 'app/models/DispatchData';
import { ServerService } from 'app/services/server.service';
import { UserAppService } from 'app/services/userApp.service';
import { catchError, of, tap } from 'rxjs';
import Swal from 'sweetalert2'
@Component({
  selector: 'app-table-list',
  templateUrl: './table-list.component.html',
  styleUrls: ['./table-list.component.css']
})
export class TableListComponent implements OnInit {

  [x: string]: any;
  
  liste_des_bd: any = []; // liste de base de donne de serveur 
  liste_des_table: any = [];// liste de fact table  de base de donné slecté 
  liste_des_dim: any = []; // liste de dim de fact table 
  serveurid: any; // servur id slecté 
  bd_name: any; // dbname slecte 
  ServerAnalyseSorceId : any ; // server fih lcube l9dima 
  slecteDdcube : any ; // fih lcube l9dim
  ServerEngineSorceId : any ; // serveur engine fih dw jdida
  selectedDatabase : any ; // dw jdida
  selectedtargtServerId : any = []; //targt serveur analysis
  newCubename : any ; //cube name
  looodaing : boolean = true ;
  envoi : boolean = true;
   responseDate : Date;
   MeassageError :any;
   isLoading = false;
   step : number = 0;

  constructor(private serverService: ServerService, private userService: UserAppService) { }

  async ngOnInit() {
   
   this.getServers()  
   this.show_dispatch();
  }
// serveur Engine 
getServers() {
  this.liste_des_serveur = []
  this.serverDisplays = new Map<string, number>();
     let progress = 0; 
     this.isLoading = true;
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
        this.isLoading = false;
      }
      this.isLoading = false;
    });


}

nextStep()
{
  this.step = this.step +1;
}
previousStep()
{
  this.step = this.step -1;
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
        this.isLoading = false;
      }
      this.isLoading = false;
    });
}
setserveur2(id: any) {
  this.serveuridAnalysis = id;
  this.isLoading = true;
  const data = this.serverService.getBbs(id)
    .pipe(
      catchError(err => of(null)),
      tap(() => this.lodaing == false)
    ).subscribe(data => {

      if (data) {
        this.liste_des_bdcube = data.DataBases
        this.isLoading = false;
      }
      this.isLoading = false;
    });
}


//envoyer data
Table_Reponse: any[] = []; 
Dispatch() {
  let cubedispatch : DispatchData = new DispatchData();
  this.loading = true;
 
  cubedispatch.sourceServerEngineId = this.ServerEngineSorceId.Id ;  
  cubedispatch.targetServerEngineId = "CAEC2EBB-A150-45F1-996F-7E89EC5F4028" ;
  cubedispatch.sourceServerAnalyseId = this.ServerAnalyseSorceId.Id ;
  cubedispatch.targetEngineDb = this.selectedDatabase.Name; 
  cubedispatch.soureceAnalyserDb= this.slecteDdcube.Name; 
  cubedispatch.newdbName = this.newCubename;
  cubedispatch.targetServerAnalyseId =[];
  this.selectedtargtServerId.forEach( s => {
   cubedispatch.targetServerAnalyseId.push(s.Id)
  }); 
  
  const data = this.serverService.postCubedispatch(cubedispatch)
  .pipe(
    catchError(err => of(null)),
    tap(() => this.lodaing == false)
  ).subscribe(async (data: any) => {
     
    if (data) {
      this.MeassageError = []
     
    }else{
      this.MeassageError = []
      this.MeassageError.push("Error creaing cube")
      console.log(this.MeassageError)
    }
    
  });



}
/*Dispatch'*/
async show_dispatch(){Swal.fire({
  title:'<h2 style ="text-align: center;"> Dispatch ! </h2>',

  width: '950px',
  html: '<html>'+
'<div style="padding: 10px;">'+
'<p>'+
  'To dispatch an OLAP cube, you should typically follow these steps:'+
   
  
 '<p style="text-align:left;">1-Select a cube source: Choose the server source name and the existing cube   .</p>'+
 
    '<p style="text-align:left;">2- Select the database source: Choose the server engine and the specific database to the cube creation </p>'+
   
  
      '<p style="text-align:left;">4- You should choose a new cube name  </p>'+
   
      '<p style="text-align:left;">5- When you click  "Dispatch", a preview of the summary appears</p>'+
   
  ' <p style="text-align:left;"> 6- You confirme the cube dispatcher</p>'+

      
    '<h3 >By following these steps, you can successfully dispatch an OLAP cube</h3>'+
  '</p>'+
'</div>'+
       '</html>',
})}
async sammary(){
  Swal.fire({
    title: "You want to dispatch this cube !",
  

    showCancelButton: true,
    html: '<html>'+
    '<div style="padding: 10px;">'+
    '<p style="text-align:left;">-  database source: '+this.selectedDatabase.Name+ '</p>'+
      
     '<p style="text-align:left;">- cube source: '+ this.slecteDdcube.Name+'.</p>'+
    
          '<p style="text-align:left;">-  new cube name '+this.newCubename+' </p>'+
              
      '</p>'+
    '</div>'+
           '</html>',
   
    confirmButtonColor: '#3085d6',
    cancelButtonColor: '#d33',
    confirmButtonText: 'Confirm !'
  }).then((result) => {
    if (result.isConfirmed) {
     
       this.Dispatch();
      
     
    }
  })
 
}
refreche_page() {
  window.location.reload();
}
reset(step: number) {
  this.step = this.step -1;
}
 

}







