import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Chart } from 'chart.js';
import { DataaService } from 'src/app/dataa.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent  {
      databases: string[] = ['database1', 'database2', 'database3'];
      selectedDatabase: string;
      tables: string[] = [];
      selectedTables: {[key: string]: boolean} = {};
      selectedTablesForNext: string[] = [];

      constructor(private http: HttpClient) {}

      onDatabaseChange() {
        this.http.get(`/api/tables?database=${this.selectedDatabase}`).subscribe((tables: string[]) => {
          this.tables = tables;
        });
      }

      onNext() {
        this.selectedTablesForNext = Object.keys(this.selectedTables).filter((table) => {
          return this.selectedTables[table];
        });
        this.selectedTables = {};
      }
}
