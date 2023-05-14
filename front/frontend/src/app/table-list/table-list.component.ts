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
  lodaing: boolean = false;
  ServerAnalyseSorceId : any ; // server fih lcube l9dima 
  slecteDdcube : any ; // fih lcube l9dim
  ServerEngineSorceId : any ; // serveur engine fih dw jdida
  selectedDatabase : any ; // dw jdida
  selectedtargtServerId : any = []; //targt serveur analysis
  newCubename : any ; //cube name
  looodaing : boolean = true ;
  envoi : boolean = true;
   responseDate : Date;
   step : number = 0;
   isLooading = false;
  constructor(private serverService: ServerService, private userService: UserAppService) { }

  async ngOnInit() {
   
   this.getServers()  
   this.Show();
  }
// serveur Engine 
getServers() {
  this.liste_des_serveur = []
  this.serverDisplays = new Map<string, number>();
     let progress = 0; 
     this.isLooading = true;
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
        this.isLooading = false;
      }
      this.isLooading = false;
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
  this.isLooading = true;
  const data = this.serverService.getBbs(id)
    .pipe(
      catchError(err => of(null)),
      tap(() => this.lodaing == false)
    ).subscribe(data => {

      if (data) {
        this.liste_des_bd = data.DataBases
        this.islooading = false;
      }
      this.isLooading = false;
    });
}
setserveur2(id: any) {
  this.serveuridAnalysis = id;
  this.isLooading = true;
  const data = this.serverService.getBbs(id)
    .pipe(
      catchError(err => of(null)),
      tap(() => this.lodaing == false)
    ).subscribe(data => {

      if (data) {
        this.liste_des_bdcube = data.DataBases
        this.islooading = false;
      }
      this.isLooading = false;
    });
}


//envoyer data
Table_Reponse: any[] = []; 
Dispatch() {
  let cubedispatch : DispatchData = new DispatchData();
  
  this.isLoading = true;
  cubedispatch.sourceServerEngineId = this.ServerEngineSorceId.Id ;  // fih base l9dima
  cubedispatch.targetServerEngineId = "CAEC2EBB-A150-45F1-996F-7E89EC5F4028" ;// fih base jdida
  cubedispatch.sourceServerAnalyseId = this.ServerAnalyseSorceId.Id ;//fih cube 
  cubedispatch.targetEngineDb = this.selectedDatabase.Name; //dw jdida
  cubedispatch.soureceAnalyserDb= this.slecteDdcube.Name; //lcube l9dim
  this.selectedtargtServerId.forEach( s => {

   cubedispatch.targetServerAnalyseId.push(s.Id)

  }); // win chn7ot lcube 
  console.log( cubedispatch )
  console.log(this.looodaing )
  const data = this.serverService.postCubedispatch(cubedispatch)
  .pipe(
    catchError(err => of(null)),
    tap(() => this.lodaing == false

    )
  ).subscribe(async (data: any) => {
       
    if (data) {
      this.MeassageError = []
     
    }else{
      this.MeassageError = []
      this.MeassageError.push("Error Dispatch cube")
    }
    
  });

}
/*Dispatch'*/
async Show(){
  const { value: accept } = await Swal.fire({
    title:'<h2 style ="text-align: center;">Welcome To Dispatch ! </h2>',
    width: '950px',
    html: '<html>'+
    '<div style="padding: 10px;text-align: center;margin-top: 0">'+ 
    'To dispatch an  OLAP cube, you typically follow these steps:'+  '<br>'+
    '<br>'+
'<ul> 1-Select a cube source: This involves choosing the server source name and the database source where the cube data resides.</ul>'+
'<ul> 2-Select a database source: Once you have chosen the cube source, you need to select the database source by choosing the server engine and the specific database that contains the data for the cube.</ul> '+

'<ul> 3-You should click "Next",then select a destination server and set a name for the new cube: After selecting the database source, you need to choose the destination server where you want to'+
' dispatch the cube. You also need to provide a name for the new cube that will be created on the destination server.</ul> '+
'<br>'+
'--> By following these steps, you can successfully dispatch an OLAP cube and'+ 'create a new cube on the destination server. '+
'</p>'+
'</div>'+

         '</html>',
   
  })
  
 
}

async sammary(){
  Swal.fire({
    title: 'Are you sure?',
    text: "You won't be able to revert this!",

    showCancelButton: true,
 
   
    confirmButtonColor: '#3085d6',
    cancelButtonColor: '#d33',
    confirmButtonText: 'Yes, delete it!'
  }).then((result) => {
    if (result.isConfirmed) {
     
       this.Dispatch();
       this. envoi = false
     
    }
  })
  
 
}

 

}







