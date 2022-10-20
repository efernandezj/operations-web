import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class ProfileService extends BaseService {

  public getProfiles(data: any, endPointResource: string): Observable<Object> {
    let headers = [
      { key: 'name', value: data.name },
      { key: 'workday', value: data.workday.toString() },
      { key: 'username', value: data.username }];

    return this.httpGet(endPointResource, headers);
  }
}
