import { Component, OnInit } from '@angular/core';
import { UserData } from 'app/models/UserData';
import { ServerService } from 'app/services/server.service';
import { UserAppService } from 'app/services/userApp.service';
import { catchError, of, tap } from 'rxjs';

@Component({
  selector: 'app-table-list',
  templateUrl: './table-list.component.html',
  styleUrls: ['./table-list.component.css']
})
export class TableListComponent implements OnInit {

  [x: string]: any;
  user: UserData; // user 
  liste_des_bd: any = []; // liste de base de donne de serveur 
  liste_des_table: any = [];// liste de fact table  de base de donné slecté 
  liste_des_dim: any = []; // liste de dim de fact table 
  serveurid: any; // servur id slecté 
  bd_name: any; // dbname slecte 
  dim: any;  // dim slecté 
  fact_name : any;
  liste_des_attribute: any = [];
  lodaing: boolean = false;

  constructor(private serverService: ServerService, private userService: UserAppService) { }

  async ngOnInit() {
   this.getUser2;   
  console.log("rr")
  }
  async getUser2() {

    (await this.userService.getUser())
      .pipe(
        catchError(err => of(null)),
        tap(() => this.lodaing == false)
      ).subscribe(data => {
        if (data) {
          this.getServers2(data);
          console.log(data)
          console.log("rr")
        }
      });
  }
getServers2(user:UserData) {
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
          this.getServers
        }
      }
    });


}
//  get db a partir de id serveur 
setserveur(id: any) {
  this.serveurid = id;
  const data = this.serverService.getBbs(id)
    .pipe(
      catchError(err => of(null)),
      tap(() => this.lodaing == false)
    ).subscribe(data => {

      if (data) {
        this.liste_des_bd = data.DataBases

      }
    });
}
 //  get fact table a partir de id serveur et db name
 setbasedonnees(name: any) {
  this.bd_name = name
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
      }
    });
}
// get dim  tables a partir de id serveur et db name et factname
settable(name: any) {
  this.fact_name = name
  this.tablesDisplay = new Map<string, string>();
  const data = this.serverService.getColumns(this.serveurid, this.bd_name, name)
    .pipe(
      catchError(err => of(null)),
      tap(() => this.lodaing == false)
    ).subscribe((data: any) => {


      this.liste_des_dim = data.Columns
      
    });
}


}
