import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class InMemoryDataService {
  configUrl = 'http://localhost:61598/api/tickets/GetUnassignedTickets';

  constructor(private http: HttpClient) {
  }

  getConfig() {

    return this.http.get(this.configUrl);
  }
}
