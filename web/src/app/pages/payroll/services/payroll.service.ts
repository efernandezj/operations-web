import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from 'src/app/common/services/base.service';


@Injectable({
  providedIn: 'root'
})
export class PayrollService extends BaseService {

  public getInitData(): Observable<Object> {
    return this.httpGet('employee/init');
  }

  public getEmployees(data: any): Observable<Object> {
    let headers = [
      { key: 'name', value: data.name },
      { key: 'workday', value: data.workday.toString() },
      { key: 'username', value: data.username }];

    return this.httpGet('employee', headers);
  }



  public getEmployee(data: any): Observable<Object> {
    return this.httpGetByKey("employee",data);
  }

  public postEmployee(data: any, isNew: boolean): Observable<object> {
    return isNew ? this.httpPost('employee', data) : this.httpPut('employee', data, data.workdayNumber);
  }
}