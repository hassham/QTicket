import { Component, OnInit } from '@angular/core';
import { InMemoryDataService } from '../../services/in-memory-data.service';
import { Ticket } from '../../classes/tickets';


@Component({
  selector: 'app-right-content',
  templateUrl: './right-content.component.html',
  styleUrls: ['./right-content.component.css']
})
export class RightContentComponent implements OnInit {
  ticketList;

  constructor(private _dataService: InMemoryDataService) { }

  ngOnInit(): void {
    this.showConfig();
  }

  showConfig() {
    this._dataService.getConfig()
      .subscribe((data: Ticket) => this.ticketList = {
          heroesUrl: data.id,
          ticketNumber:  data.ticketNumber,
          dateCreated: data.dateCreated,
      });
  }

}
