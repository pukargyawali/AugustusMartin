import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from 'src/app/components/nav-menu/nav-menu.component';
import {UserComponent} from 'src/app/components/user-list/user-list.component'
import {UserService} from '../app/api/services/user.service';
import {ApiConfiguration} from './api/api-configuration';
import {MatCardModule} from '@angular/material/card';
import {BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatPaginatorModule} from '@angular/material/paginator';
import {MatListModule} from '@angular/material/list';
import {MatSidenavModule} from '@angular/material/sidenav';
import {UserPostComponent} from 'src/app/components/user-post-list/user-post-list.component';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatIconModule} from '@angular/material/icon';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    UserComponent,
    UserPostComponent  
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: UserComponent, pathMatch: 'full' },
      {path: 'user-post-list', component: UserPostComponent}
    ]),
    MatCardModule,
    MatPaginatorModule,
    BrowserAnimationsModule,
    MatListModule,
    MatSidenavModule,
    MatGridListModule,
    MatIconModule
  ],
  providers: [UserService,ApiConfiguration],
  bootstrap: [AppComponent]
})
export class AppModule { }
