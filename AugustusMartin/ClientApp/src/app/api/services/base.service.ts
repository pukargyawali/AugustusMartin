/* tslint:disable */
import { HttpClient, HttpParameterCodec, HttpParams } from '@angular/common/http';
import { ApiConfiguration } from '../api-configuration';

export class BaseService {
  constructor(
    protected config: ApiConfiguration,
    protected http: HttpClient
  ) {
  }

  private _rootUrl: string = '';

  /**
   * Returns the root url for API operations. If not set directly in this
   * service, will fallback to ApiConfiguration.rootUrl.
   */
  get rootUrl(): string {
    return this._rootUrl || this.config.rootUrl;
  }

  /**
   * Sets the root URL for API operations in this service.
   */
  set rootUrl(rootUrl: string) {
    this._rootUrl = rootUrl;
  }
}
