import { Attribute, Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ServerData } from 'app/models/ServerData';
import { UserData } from 'app/models/UserData';
import { BehaviorSubject, catchError, delay, of, tap } from 'rxjs';
import { ServerService } from 'app/services/server.service';
import { UserAppService } from 'app/services/userApp.service';
import { HttpErrorResponse, HttpEventType } from '@angular/common/http';
import { CubeData } from 'app/models/CubeData';
import {MatSnackBar } from '@angular/material/snack-bar'
import Swal from 'sweetalert2';
//import { threadId } from 'worker_threads';


@Component({
  selector: 'app-dashboard',
  templateUrl:'./dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  [x: string]: any;
  user: UserData; // user 
  liste_des_bd: any = []; // liste de base de donne de serveur 
  liste_des_table: any = [];// liste de fact table  de base de donné slecté 
  liste_des_dim: any = []; // liste de dim de fact table 
  serveurid: any; // servur id slecté 
  bd_name: any; // dbname slecte 
  dim: any;  // dim slecté 
  fact_name : any;
  liste_des_Atr_fact: any = [];
  liste_des_attribute: any = [];
  liste_des_attribute_grouper: any = {};
  liste_des_attribute_grouper1: any = [];
  liste_des_attribute_grouper1_value: any = [];
  liste_des_attribute_grouper1_key: any = [];
  liste_des_attribute_grouper_KEY: any = [];
  res: any = [];
  variable_res: any = {};
  liste_var_res: any = [];
  Cubename: string
  MeassageError :any;
  isLoading = false;
  step : number = 0;
  selectedMesures :any
   nameMessure : any
   value : any
  constructor(private serverService: ServerService, private userService: UserAppService) {

  }

  async ngOnInit() {
    
    await this.getUser();
    this.Show_Cube();
   // await this.getServers();
  }
  //get user 

  
  async getUser() {
       this.isLoading = true;
    (await this.userService.getUser())
      .pipe(
        catchError(err => of(null)),
        tap(() => this.lodaing == false)
      ).subscribe(data => {
        if (data) {
          this.getServers(data);
          this.loading = false;
        }
        this.isLoading = false;
        console.log(this.loading)
      });
  }
 
  ngOnChanges() : void {
    // logique du composant
}
  //get serveur 
  liste_des_serveur: any = [];
  getServers(user:UserData) { 
    this.isLoading = true;
    this.liste_des_serveur = []
    this.user = user;
    this.serverDisplays = new Map<string, number>();
       let progress = 0;
    const data = this.serverService.getServers(user.Id)
 
      .pipe(
        catchError(err => of(null)),
        tap(() => this.lodaing == false)
   
      ).subscribe(data => {
        this.liste_des_serveur = data.Servers
        if (data) {
          for (let s of data.Servers) {
            this.serverDisplays.set(s.Id, s.Name);
            
          }
          this.lodaing == false
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
  goToStep(step: number) {
    this.step = this.step -2;
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
  //  get fact table a partir de id serveur et db name
  setbasedonnees(name: any) {
    this.bd_name = name
    this.isLoading = true;
    this.tablesDisplay = new Map<string, string>();
    const data = this.serverService.getTables(this.serveurid, name)
      .pipe(
        catchError(err => of(null)),
        tap(() => this.lodaing == false)
      ).subscribe(data => {
        this.liste_des_table = data.Tables
        if (data) {
          for (let s of data.Tables) {
            this.tablesDisplay.set(s.Id, s.Name);
            
          }
          this.isLoading = false;
        }
        this.isLoading = false;
      });
  }
  // get dim  tables a partir de id serveur et db name et factname
  settable(name: any) {
    this.isLoading = true;
    this.fact_name = name
    this.tablesDisplay = new Map<string, string>();
    const data = this.serverService.getColumns(this.serveurid, this.bd_name, name)
      .pipe(
        catchError(err => of(null)),
        tap(() => this.lodaing == false)
      ).subscribe((data: any) => {


        this.liste_des_dim = data.Columns
        this.setAtrFact2(name)
        this.isLoading = false;
      });
      this.isLoading = false;
  }

  // affiche le nom de attribute !!!!!!!!!!!!!!!!!!!
  // set attribute de dim slecte 
  deleteRow(event: any) {
    // Récupérer l'élément parent direct de l'icône de suppression
    let row = event.target.closest('tr');
  
    // Supprimer la ligne de la table
    row.remove();
  }
  ch: any = []
  setchoix(ev: any) {
    this.ch = []
    for (let i = 0; i < this.selectedObjects.length; i++) {
      this.ch.push(this.selectedObjects[i].name)
      this.setAtrFact(this.selectedObjects[i].name);
    }
    /* this.liste_des_attribute = []
 
     this.tablesDisplay = new Map<string, string>();
     const data = this.serverService.getAttributs(this.serveurid, this.bd_name, this.dim, this.ch)
       .pipe(
         catchError(err => of(null)),
         tap(() => this.lodaing == false)
       ).subscribe((data: any) => {
         this.liste_des_attribute = data.attribute
          
        // this.setAtrFact("dim_gender");
         //this.regrouper();
          
       });*/

  }
  liste_atr: any = []
  setchoix_atr(ev: any) {
    this.liste_atr = []
    for (let i = 0; i < this.slecte_Atr.length; i++) {
      this.ch.push(this.slecte_Atr[i].name)
    }
  }

  // liste de attribute  pour les dim slecte 
  regrouper() {
    this.res = []
    for (let i = 0; i < this.liste_des_attribute.length; i++) {
      let index
      for (let j = 0; j < this.liste_des_attribute.length; j++) {
        if (this.liste_des_attribute[j].tableName == this.liste_des_attribute[i].tableName && i != j) {
          index = i;
        }
      }

      let elmenet_exsiste_on_table_res = -1
      for (let k = 0; k < this.res.length; k++) {
        if (this.res[k].key == this.liste_des_attribute[index].tableName) {
          elmenet_exsiste_on_table_res = k
        }
      }

      if (elmenet_exsiste_on_table_res == -1) {
        this.liste_var_res = []
        this.liste_var_res.push(this.liste_des_attribute[i])
        this.variable_res = {}
        this.variable_res.liste = this.liste_var_res;
        this.variable_res.key = this.liste_des_attribute[i].tableName;
        this.res.push(this.variable_res)

      } else {

        this.res[elmenet_exsiste_on_table_res].liste.push(this.liste_des_attribute[index])
      }
    }

  }


  liste1: any = []
  liste2: any = []

  // liste de attribute slecte
  setAtrFact(name: any) {
    this.liste1 = []
    this.dim = name
    this.tablesDisplay = new Map<string, string>();
    const data = this.serverService.getAttributFacts(this.serveurid, this.bd_name, name)
      .pipe(
        catchError(err => of(null)),
        tap(() => this.lodaing == false)
      ).subscribe((data: any) => {

        if (data) {
          this.liste1 = data.AttributFact
        }
      });
  }

  // liste de attribute slecte
  setAtrFact2(name: any) {
    this.dim = name
    this.liste2 = []
    this.tablesDisplay = new Map<string, string>();
    const data = this.serverService.getAttributFacts(this.serveurid, this.bd_name, name)
      .pipe(
        catchError(err => of(null)),
        tap(() => this.lodaing == false)
      ).subscribe((data: any) => {

        this.liste2 = data.AttributFact

      });
  }

  //  envoyer  les dooneé 
  envoyer_data() {
    let cube : CubeData = new CubeData();
    cube.DBName = this.bd_name;
    cube.cubeDbName= this.Cubename;
    cube.DBEngineServer= this.serveurid;
    cube.CubeDataSourceName='Data';
    cube.CubeDataSourceViewName='dataView'
    cube.FactTableName =this.fact_name;
    cube.DBAnalyserServer = this.selectedServertargtId.Id;
    cube.DimensionTableCount= this.resultat_de_selection.length;
    cube.TableNamesAndKeys =[];
    this.loading = true;
    this.resultat_de_selection.forEach(element => {
      var item  = [];
      item.push(this.dim);
      item.push(element.obj1.Name);
      item.push(this.fact_name );
      item.push(element.obj2.Name);

      cube.TableNamesAndKeys.push(item)
    });
    const data = this.serverService.postCube(cube)
    .pipe(
      catchError(err => of(null)),
      tap(() => this.lodaing == false)
    ).subscribe(async (data: any) => {
       
      if (data) {
        this.MeassageError = []
       
      }else{
        this.MeassageError = []
        this.MeassageError.push("Error creaing cube")
      }
      
    });
  
  }
  reset(){
    this.MeassageError = null;
    this.loading =false;
  }
  obj1: any;
  obj2: any;
  setselect1(obj: any) {
    this.obj1 = obj
  }
  setselect2(obj: any) {
    this.obj2 = obj
  }

  resultat_de_selection:any=[]
  obj_select:any={}

  match() {
    for (let i = 0; i < this.liste1.length; i++) {
      if (this.obj1.name == this.liste1[i].name) {
      //this.liste1.splice(i, 1)
      }
    }
    for (let i = 0; i < this.liste2.length; i++) {
      if (this.obj2.name == this.liste2[i].name) {
       // this.liste2.splice(i, 1)
      }
    }
    for (let i = 0; i < this.liste_des_dim.length; i++) {
      if (this.dim == this.liste_des_dim[i].name) {
     //   this.liste_des_dim.splice(i, 1)
      }
    }
    this.obj_select={dim:this.dim,obj1:this.obj1 , obj2:this.obj2 , Fact:this.fact_name }
    this.resultat_de_selection.push(this.obj_select)
   



  }
    mesureliste :any=[]
  nouvelle_mesure :any= {}
  addMessure(){
   
      this.nouvelle_mesure = {mesure: this.selectedMesures.Name ,operation : this.value , nom: this.nameMessure };
      this.mesureliste.push(this.nouvelle_mesure) 
      console.log(this.mesureliste)
    }
    refreche_page() {
      window.location.reload();
    }


    async Show_Cube(){
        const {} = Swal.fire({
        title: 'ACTEOL ',
        text: 'OLAP Builder and dispatcher : You can generate automatically an OLAP cube and dispatch to diffrent Client/servers .',
         imageUrl : 'https://media.istockphoto.com/id/1294997127/vector/welcome-concept-team-of-people.jpg?s=1024x1024&w=is&k=20&c=HlP5N80R0f96WjnkKuPVcI_Vxglr_YuWeMykXYCf-F4%3D&fbclid=IwAR0HJfRGcJJj17OxMJVp0S3-sU8DOPGrgjgvVTN5iPtpAFDu-5MpcIQJxB8',
        imageWidth: 400,
        imageHeight: 200,
        imageAlt: 'Custom image',
      }).then((result) => {
        if (result.isConfirmed) {
          Swal.fire({
            title:'<h2 style ="text-align: center;">Welcome To Add cube  ! </h2>',
       
            width: '950px',
            html: '<html>'+
            '<div style="padding: 10px;">'+
      '<p style= font-family: "Times New Roman", serif; >'+
      'To add an  OLAP cube, you typically follow these steps:'+
      '<ul> 1- Select informations to create the cube structure : This involves'+ 'choosing the server source name , a data warehouse , fact_table ,Dimensions tables , their attributs and  you sould define relations between dimension attributs and fact attributs</ul>'+
      '<ul> 2- You should click "Next", then you have the freedom to choose the'+ 'measures and operations to apply, and then you assign a corresponding name to each measure </ul> '+
      '<ul> 3- Select a destination Analysis server '+
      '<ul> 4 - set a name to the new cube.</ul> '+
      '<ul> 5 -When you click on the Generate button, a preview of the summary appears.</ul> '+
      '<ul> 6 -You should chek the summary  then you click on "Are You Sure!". '+ 'the cube creation will be initiate</ul> '+
      '<br>'+
      '<H2> =>By following these steps, you can successfully create an OLAP cube</H2>'+ 
      
                 '</html>',
            confirmButtonText:
              'Continue <i class="fa fa-arrow-right"></i>',
           
          })
          
         
        }})
    

      }

    }



