import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { BaseService } from 'src/app/common/services/base.service';

@Injectable({
  providedIn: 'root'
})
export class SecurityService extends BaseService {

  public getUsers(data: any): Observable<Object> {
    let headers = [
      { key: 'name', value: data.name },
      { key: 'workday', value: data.workday.toString() },
      { key: 'username', value: data.username }];

    return this.httpGet('user', headers);
  }

  public getActiveUsers(data: any): Observable<Object> {
    let headers = [
      { key: 'name', value: data.name },
      { key: 'workday', value: data.workday?.toString() },
      { key: 'username', value: data.username }];

    return this.httpGet('user/active', headers);
  }

  public processUser(isNew: boolean, data: any): Observable<Object> {
    return isNew ? this.httpPost('user', data) : this.httpPut('user', data, data.workdayNumber);
  }
}
