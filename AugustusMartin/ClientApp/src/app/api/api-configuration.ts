/* tslint:disable */
import { Injectable } from '@angular/core';
import {Inject } from '@angular/core';

/**
 * Global configuration for Api services
 */
@Injectable({
  providedIn: 'root',
})
export class ApiConfiguration {
  constructor(@Inject('BASE_URL') baseurl: string){
    this.rootUrl = baseurl;    
  }
  rootUrl: string;
}
