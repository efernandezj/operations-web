import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from 'src/app/common/services/base.service';

@Injectable({
  providedIn: 'root'
})
export class RoleService extends BaseService {

  public getInitData(): Observable<Object> {
    return this.httpGet('role/init');
  }
}
