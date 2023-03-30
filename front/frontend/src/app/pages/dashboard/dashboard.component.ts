import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Chart } from 'chart.js';
import { catchError, of, tap } from 'rxjs';
import { DataaService } from 'src/app/dataa.service';
import { ServerData } from 'src/app/models/ServerData';
import { ServerService } from 'src/app/services/server.service';
import Swal from 'sweetalert2';

declare var multiSelect: any;
@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'
]
})
export class DashboardComponent  {
  [x: string]: any;
      databases: any ;
      servers : ServerData[];
      serverDisplays : any ;
      databaseDisplays : any ;
      tablesDisplay : any;
      selectedServerId : string;
      selectedDatabase: string;
      tables: string[] = [];
      selectedTables: string;
      selectedTablesForNext: string[] = [];
    
   lodaing : boolean = false;
      constructor(private serverService:ServerService) {}
    
      async ngOnInit()  {
        this.databaseDisplays =new Map<string, string>();
        this.databaseDisplays.set("0","select value")
         this.getServers();
         this.multiSelect();
      }
   

      onDatabaseChange() {
        this.getTables();
      }

      onNext() {
     
        
      }
   
      async getServers(){
          this.serverDisplays =new Map<string, number>();
          const data =  this.serverService.getServers("b995b0f3-f7da-4878-ae1a-d3a667b79906")
            .pipe(
              catchError(err => of(null)),
              tap(() => this.lodaing == false)
            ).subscribe( data  => {
              if (data) {
                for(let s of data.servers)
                {
                  this.serverDisplays.set(s.id,s.name);
                  this.showServerPopup()
                }
              }
            });
      }
      showServerPopup()
      {
        (async () => {

          const { value: Serveurs } = await Swal.fire({
            title: 'Select Serveur ',
            input: 'select',
            inputOptions: {
              'Serveurs': this.serverDisplays
             
            },
            inputPlaceholder: 'Select a Serveur',
            confirmButtonText: 'Connect'
            
          })
          
          if (Serveurs) {
            this.selectedServerId = Serveurs;
            this.getDatabase()
          }
          
        })();
      }
      getDatabase()
      {
        
        const data =  this.serverService.getBbs(this.selectedServerId)
        .pipe(
          catchError(err => of(null)),
          tap(() => this.lodaing == false)
        ).subscribe( data  => {
          if (data) {
            for(let s of data.dataBases)
            {
              this.databaseDisplays.set(s.id,s.name);
            }
            
          }
        });
 
      }
      getTables()
      {
        this.tablesDisplay =new Map<string, string>();
        const data =  this.serverService.getTables(this.selectedServerId,this.selectedDatabase)
        .pipe(
          catchError(err => of(null)),
          tap(() => this.lodaing == false)
        ).subscribe( data  => {
          if (data) {
            for(let s of data.tables)
            {
              this.tablesDisplay.set(s.id,s.name);
            }
          }
        });
 
      }
    
     
}
