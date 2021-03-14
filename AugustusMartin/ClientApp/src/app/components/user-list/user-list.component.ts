import { Component, Inject, OnDestroy } from '@angular/core';
import{ UserService } from 'src/app/api/services/user.service'
import {User} from 'src/app/api/models/user';
import { Router } from '@angular/router';
import { pipe, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './user-list.component.html',
  styleUrls: ['user-list.component.css'],
})
export class UserComponent implements OnDestroy{
 
  public users: User[];
  _destroyed$ = new Subject<boolean>();
  
  constructor(private userService: UserService,
              private router: Router  ) {
   this.userService = userService;
  }

  ngOnInit() {
    let apiUsers = [];
    this.userService.getAllUsers().pipe(takeUntil(this._destroyed$)).subscribe((user: User[]) => {            
      user.map(u=>{        
        apiUsers.push(u);
      })
    })
    this.users = apiUsers;
  }

 userPost(userId){
     console.log("test"+ userId);
     this.router.navigate(['/user-post-list', { userId: userId }]);
 }
 ngOnDestroy(){
   this.users=[];
   this._destroyed$.next(true);
 }
}
