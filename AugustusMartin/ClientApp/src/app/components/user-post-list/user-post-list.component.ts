import { Component, Inject, OnDestroy } from '@angular/core';
import { UserService } from 'src/app/api/services/user.service'
import { PostService } from 'src/app/api/services/post.service'
import { User } from 'src/app/api/models/user';
import { Post } from 'src/app/api/models/post';
import { Observable, Subject } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { takeUntil } from 'rxjs/operators';


@Component({
    selector: 'app-user-post-list',
    templateUrl: './user-post-list.component.html',
    styleUrls: ['user-post-list.component.css'],
})
export class UserPostComponent implements OnDestroy {
   
    public user$: Observable<User>;
    public posts$: Observable<Post[]>;
    public posts: Post[] = [];
    private userId :string;
    _destroyed$ = new Subject<boolean>();

    constructor(private postService: PostService,
                private userService: UserService,
                private route: ActivatedRoute
               ) {
        
       this.populateUserPosts();
    }

    ngOnInit() { 
        this.userId = this.route.snapshot.paramMap.get('userId');
        this.userService.getUser(this.userId).pipe(takeUntil(this._destroyed$)).subscribe();
        this.postService.GetPostByUser(this.userId).pipe(takeUntil(this._destroyed$)).subscribe()
    }    

    populateUserPosts() {
        this.user$ = this.userService.user();
        this.posts$ = this.postService.userPost();       
    }
    ngOnDestroy(){
        this.user$= null;
        this.posts$ = null;
        this._destroyed$.next(true);
    }
}
