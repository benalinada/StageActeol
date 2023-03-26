import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { catchError, finalize, Observable, of, tap } from "rxjs";
import { BaseData, BasesData } from "../models/BaseData";
import { ServerData, ServersData } from '../models/ServerData';
import { TableData, TablesData } from "../models/TableData";
@Injectable()
export class ServerService {
    dataserver: ServerData[] ;
    databases: BaseData[] ;
    Tables: TableData[] ;
    loading : boolean = false;
    constructor(private http: HttpClient,) {

    }
    getServers(userid: string): Observable<ServersData> {
        this.loading = true;
        const res = this.http.get<ServersData | null>(`https://localhost:44362/api/servers/${userid}`).pipe(
            tap( data => {
                this.dataserver = data.servers;
              }),
              catchError(err => {
                return of(null);
              }),
              finalize(() =>this.loading = false)
            );
            return res;
    }
    getBbs(id: string): Observable<BasesData> {
      this.loading = true;
      const res = this.http.get<BasesData | null>(`https://localhost:44362/api/DataBases/${id}`).pipe(
          tap( data => {
              this.databases = data.dataBases;
            }),
            catchError(err => {
              return of(null);
            }),
            finalize(() =>this.loading = false)
          );
          return res;
  }
  getTables(id: string, dbName:string): Observable<TablesData> {
    this.loading = true;
    const res = this.http.get<TablesData | null>(`https://localhost:44362/api/tables/${id}/${dbName}`).pipe(
        tap( data => {
            this.databases = data.tables;
          }),
          catchError(err => {
            return of(null);
          }),
          finalize(() =>this.loading = false)
        );
        return res;
}
}

