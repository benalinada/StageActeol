import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { State } from '@popperjs/core';
import { UserService } from 'angular-auth-oidc-client/lib/user-data/user.service';
import { Chart } from 'chart.js';
import { BehaviorSubject, catchError, of, tap } from 'rxjs';
import { DataaService } from 'src/app/dataa.service';
import { ServerData } from 'src/app/models/ServerData';
import { UserData } from 'src/app/models/UserData';
import { ServerService } from 'src/app/services/server.service';
import { UserAppService } from 'src/app/services/userApp.service';
import Swal from 'sweetalert2';

declare var multiSelect: any;
@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss',
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
      selectedTables: string ;
      selectedColumn: string[];
      selectedTablesForNext: string[] = [];
      columnsDisplay : any;
      user: UserData;
    
   lodaing : boolean = false;
  
    private stateChange = new BehaviorSubject<State>(this.virtualState);
      constructor(private serverService:ServerService, private userService: UserAppService) {}
    
      async ngOnInit()  {
        this.databaseDisplays =new Map<string, string>();
        this.databaseDisplays.set("0","select value");
         await this.getUser();
        
         this.multiSelect();

      }
      
          
        
      

      onDatabaseChange() {
        this.getTables();
        
      }

      onNext() {
     
        
      }
      
      async getUser()
      {

        (await this.userService.getUser())
        .pipe(
          catchError(err => of(null)),
          tap(() => this.lodaing == false)
        ).subscribe( data  => {
          if (data) {
          this.user = data;
          this.getServers();
          }
        });
      }
   
      getServers(){
          this.serverDisplays =new Map<string, number>();
          const data =  this.serverService.getServers(this.user.id)  
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
        Swal.fire({
          title: 'Loading...',
          html: `
            <div class="loader-section section-left"></div>
            <div class="loader-section section-right"></div>
          `,
          allowOutsideClick: false,
          showConfirmButton: false,
          didOpen: () => {
            setTimeout(() => {
              Swal.close();
            }, 3500);
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

        Swal.fire({
          title: 'Loading...',
          html: `
            <div class="loader-section section-left"></div>
            <div class="loader-section section-right"></div>
          `,
          allowOutsideClick: false,
          showConfirmButton: false,
          didOpen: () => {
            setTimeout(() => {
              Swal.close();
            }, 3500);
          }
        });
        
      }
      getColumns() {
        this.columnsDisplay = new Map<string, string[]>();
        const data = this.serverService.getColumns(this.selectedServerId, this.selectedDatabase,this.tablesDisplay).pipe(
          catchError(err => of(null)),
          tap(() => this.loading = false)
        ).subscribe(data => {
          if (data) {
            for(let s of data.Columns)
            {
              this.columnsDisplay.set(s.id,s.name);
            }
          }
        });
        Swal.fire({
          title: 'Loading...',
          html: `
            <div class="loader-section section-left"></div>
            <div class="loader-section section-right"></div>
          `,
          allowOutsideClick: false,
          showConfirmButton: false,
          didOpen: () => {
            setTimeout(() => {
              Swal.close();
            }, 3500);
          }
        });
      } getAttributs() {
        this.AttributDisplay = new Map<string, string[]>();
        const data = this.serverService.getAttributs(this.selectedServerId, this.selectedDatabase,this.selectedTables).pipe(
          catchError(err => of(null)),
          tap(() => this.loading = false)
        ).subscribe(data => {
          if (data) {
            for(let s of data.Attributes)
            {
              this.AttributDisplay.set(s.id,s.name);
            }
          }
        });
      }
      openSwal() {
        Swal.fire({
          title: 'Multiple inputs',
          html:
            '<input id="swal-input1" class="swal2-input">',
          focusConfirm: false,
          preConfirm: () => {
            return [
              document.getElementById('swal-input1')
           
            ]
          }
        })
      }
    
     
}
