import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AgendaServiceService {

  apiUrl: string = "http://localhost:5152/api/";

  constructor(private http: HttpClient) {  
  }
  setHeaders(): any {
    return new HttpHeaders({
      'Content-Type':'application/json',
      'Access-Control-Allow-Origin': '*'
    });
  }

  getAgendaTypes(): Observable<any>{
    const headers = this.setHeaders()
    return this.http.get(this.apiUrl + "AgendaTypes", { headers: headers})
  }

  getAgendaMatrix(): Observable<any>{
    const headers = this.setHeaders()
    return this.http.get(this.apiUrl + "AgendaRoleMatrix", { headers: headers})
  }

  createAgendaItem(requestPayload:any): Observable<any>{
    const headers = this.setHeaders()
    return this.http.post(this.apiUrl + "AgendaItems", requestPayload, {  headers})
  }

  updateAgendaItem(id:String, requestPayload:any): Observable<any>{
    const headers = this.setHeaders()
    return this.http.put(this.apiUrl + "AgendaItems/"+id, requestPayload, {  headers})
  }

  getUserRoles(): Observable<any>{
    const headers = this.setHeaders()
    return this.http.get(this.apiUrl + "ApproverRoles", { headers: headers})
  }

  getApprovers(): Observable<any>{
    const headers = this.setHeaders()
    return this.http.get(this.apiUrl + "Approvers", { headers: headers})
  }

}
