import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Header } from '../types/common.types';



@Injectable({
  providedIn: 'root'
})
export class BaseService {
  private readonly _endPointDominio: string;

  constructor(private http: HttpClient) {
    this._endPointDominio = environment.apiPath;
  }

  private _headers(_headers?: Header[]): HttpHeaders {
    let headers = new HttpHeaders();
    if (_headers != null && _headers.length > 0)
      (_headers ?? []).filter(h => h.value != '').forEach(h => headers = headers.append(h.key, h.value));
    return headers;
  }



  protected httpGetByKey(endPointResource: string, id: string, headersOptions?: any): Observable<object> {
      return this.http.get(`${this._endPointDominio}/api/${endPointResource}/${id}`, { headers: this._headers(headersOptions) });
  }

  protected httpGet(endPointResource: string, headersOptions?: any): Observable<object> {
    return this.http.get(`${this._endPointDominio}/api/${endPointResource}`, { headers: this._headers(headersOptions) });
  }

  protected httpPost(endPointResource: string, data: any, headersOptions?: any): Observable<object> {
    return this.http.post(`${this._endPointDominio}/api/${endPointResource}`, data, { headers: this._headers(headersOptions) });
  }

  protected httpPut(endPointResource: string, data: any, id: number, headersOptions?: any): Observable<object> {
    return this.http.put(`${this._endPointDominio}/api/${endPointResource}/${id}`, data, { headers: this._headers(headersOptions) });
  }

  protected httpDelete(endPointResource: string, id: number, headersOptions?: any): Observable<object> {
    return this.http.delete(`${this._endPointDominio}/api/${endPointResource}/${id}`, { headers: this._headers(headersOptions) });
  }
}

