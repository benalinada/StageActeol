import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

declare interface RouteInfo {
    path: string;
    title: string;
    icon: string;
    class: string;
}
export const ROUTES: RouteInfo[] = [
    { path: '/dashboard', title: 'Add Cube',  icon: 'fa fa-cubes', class: '' },
    { path: '/icons', title: 'Add Calculation ',  icon:'fa fa-calculator', class: '' },
    { path: '/maps', title: 'Dispatch',  icon:'fa fa-paper-plane', class: '' },
    //{ path: '/user-profile', title: 'Repporting', icon:'fa fa-line-chart', class: '' },
   
];

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {

  public menuItems: any[];
  public isCollapsed = true;

  constructor(private router: Router) { }

  ngOnInit() {
    this.menuItems = ROUTES.filter(menuItem => menuItem);
    this.router.events.subscribe((event) => {
      this.isCollapsed = true;
   });
  }
}
