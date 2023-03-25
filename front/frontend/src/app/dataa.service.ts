import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataaService {

  private baseUrl = 'http://localhost:3000'; // URL de l'API qui renvoie les tables de dimensions et de fait
  constructor(private http: HttpClient) { }
  getDimensions(): Observable<string[]> {
    return this.http.get<string[]>(`${this.baseUrl}/dimensions`);
  }

  getFacts(): Observable<string[]> {
    return this.http.get<string[]>(`${this.baseUrl}/facts`);
  }

  getCubeData(dimensions: string, fact: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/cube?dimensions=${dimensions}&fact=${fact}`);
  }
}

