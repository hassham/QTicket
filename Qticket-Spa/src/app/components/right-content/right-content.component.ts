import { Component, OnInit } from '@angular/core';
import { InMemoryDataService } from '../../services/in-memory-data.service';
import { Ticket } from '../../classes/tickets';


@Component({
  selector: 'app-right-content',
  templateUrl: './right-content.component.html',
  styleUrls: ['./right-content.component.css']
})
export class RightContentComponent implements OnInit {
  ticketList: any;

  constructor(private _dataService: InMemoryDataService) { }

  ngOnInit(): void {
    this.showConfig();
  }

  showConfig() {
    this._dataService.getConfig().subscribe((result) => {
        this.ticketList = result;
      });
      
  }

  onAssign(item) {
    console.log(item);
  }

}
