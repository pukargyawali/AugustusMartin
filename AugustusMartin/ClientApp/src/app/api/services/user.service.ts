import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiConfiguration as __Configuration } from '../api-configuration';
import { BaseService } from './base.service';
import { BehaviorSubject, Observable } from 'rxjs';
import { User } from '../models/user'
import { switchMap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})

export class UserService extends BaseService {

  constructor(config: __Configuration, http: HttpClient) {
    super(config, http);
  }
  public user(): Observable<User> {
    return this.userSubject.asObservable();
  }


  getAllUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${this.rootUrl}User/GetAllUsers`);     
  }

  getUser(userId): Observable<User>{
      return this.http.get<User>(`${this.rootUrl}User/GetUserById?userId=${userId}`).pipe(
          switchMap(res=>{
              this.userSubject.next(res);
              return this.user();
          })
      )
  }
  private userSubject = new BehaviorSubject<User>(new User());
}