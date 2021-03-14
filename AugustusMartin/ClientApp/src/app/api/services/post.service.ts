import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiConfiguration as __Configuration } from '../api-configuration';
import { BaseService } from './base.service';
import { BehaviorSubject, Observable } from 'rxjs';
import { Post } from '../models/post';
import { map, switchMap, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})

export class PostService extends BaseService {

  constructor(config: __Configuration, http: HttpClient) {
    super(config, http);
  }
  public userPost(): Observable<Post[]> {
        return this.postSubject.asObservable();
  }

  GetPostByUser(userId: string): Observable<Post[]> {
    return this.http.get<Post[]>(`${this.rootUrl}post/getPost?userId=${userId}`).pipe(
      switchMap(res=>{
        this.postSubject.next(res);
        return this.userPost();
      })
    );     
  }
  private  postSubject = new BehaviorSubject<Post[]>(new Array<Post>());
  
}