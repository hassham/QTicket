import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class InMemoryDataService {
  configUrl = 'https://localhost:44337/api/tickets';

  constructor(private http: HttpClient) {
  }

  getConfig() {
    return this.http.get(this.configUrl);
  }
}
