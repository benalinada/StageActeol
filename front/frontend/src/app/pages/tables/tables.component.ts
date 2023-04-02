import { Component, OnInit } from '@angular/core';
import { IDropdownSettings } from 'ng-multiselect-dropdown';

@Component({
  selector: 'app-tables',
  templateUrl: './tables.component.html',
  styleUrls: ['./tables.component.scss'],
})
export class TablesComponent  implements OnInit  {
  dropdownList = [];
  selectedItems = [];
  dropdownSettings:IDropdownSettings = {};
  ngOnInit() {}
    
 
}




