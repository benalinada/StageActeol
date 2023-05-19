import { Component, OnInit } from '@angular/core';
import { CalculationData } from 'app/models/CalculationData';
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
  selectedServerSorceId : any ; //serveur analysis Sélectionne. 
  value :any ; // operation Sélectionne. 
  selectedDatabase : any ; //DB cube Sélectionne. 
  mesure2 : any ; //messure 2
  mesure1 : any ; //messure 1
  selectedServerSourceId : any;
  newCubename: any;
   responseDate : Date;
   nameCalculation : any;
   isLoading = false;
  constructor(private serverService: ServerService, private userService: UserAppService) { }

  async ngOnInit() {
    
   this.getServers()  
  
  }

  step : number = 0;
  nextStep()
  {
    this.step = this.step +1;
  }
  previousStep()
  {
    this.step = this.step -1;
  }
  goToStep(step: number) {
    this.step = this.step -2;
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
                this.show_Calculations();
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
          
           this.liste_messure = data.Messure
           console.log(this.liste_messure);
           this.isLoading = false;

    });


}
  //  envoyer  les dooneé 
  envoyer_data() {
    let Calculation : CalculationData = new CalculationData ();
    Calculation.sourceServerAnalyseId = this.selectedServerSorceId.Id;
    Calculation.sourceServerAnalyseName = this.selectedServerSorceId.Name;
    Calculation.soureceAnalyserDb = this.selectedDatabase.Name;
    Calculation.provider = "msolap";
    Calculation.cubeName ="SampleCube";
    Calculation.mes1 = this.mesure1.Name;
    Calculation.mes2 = this.mesure1.Name;
     Calculation.opr  = this.value;
     Calculation.namecalculation = this.nameCalculation;
     this.loading = true;

    const data = this.serverService.postCalculation(Calculation)
    .pipe(
      catchError(err => of(null)),
      tap(() => this.lodaing == false)
    ).subscribe(async (data: any) => {
       
      if (data) {
        this.MeassageError = []
       
      }else{
        this.MeassageError = []
        this.MeassageError.push("Error add calculation")
      }
      
    });
  
  }
 /*Add_Calculations'*/
 async show_Calculations(){Swal.fire({
  title:'<h2 style ="text-align: center;"> Add calculations  ! </h2>',

  width: '800px',
  html: '<html>'+
'<div style="padding: 10px;">'+
'<p>'+
  'To add caculations to an OLAP cube, you should typically follow these steps:'+
 
   
'<p style="text-align:left;">1- Select a cube source: This involves choosing the server source name and the cube name.</p>'+
      '<br>'+
    '<p style="text-align:left;">2- Then you should create operation(s):this involves selecting mesures, opertions and set a name for each calculation</p>'+
      '<br>'+

      
    '<h3 >By following these steps, you can successfully add calculations to an OLAP cube</h3>'+
  '</p>'+
'</div>'+
      '</html>',
})};
resultat_de_selection:any=[]
obj_select:any={}
add() {

  this.obj_select={name:this.nameCalculation , messure : this.mesure1.Name, messure2 :this.mesure2.Name , operation:this.value }
  this.resultat_de_selection.push(this.obj_select)
 
 console.log(this.resultat_de_selection)
 console.log(this.resultat_de_selection.messure)


}
//sammary confirm
confirm(){
  Swal.fire({
    title: "You want to add this measure calculation to the cube : "+this.selectedDatabase.Name+ "",
    text: "",
    showCancelButton: true,
    html: '<html>'+
    '<div style="padding: 10px;">'+
    '<p style="text-align:left;">- Expression  : ' + this.mesure1.Name + '  '+this.value+'  ' + this.mesure2.Name+ '</p>'+
    '<p style="text-align:left;">- Name to calculation: ' + this.nameCalculation+ '</p>'+
    '</div>'+
    '</html>',
    confirmButtonColor: '#3085d6',
    cancelButtonColor: '#d33',
    confirmButtonText: 'Confirm!'
  }).then((result) => {
    if (result.isConfirmed) {
      this.add();
      
    }
  });
}
reset(step: number) {
  this.step = this.step -2;
}
refreche_page() {
  window.location.reload();
}

}


