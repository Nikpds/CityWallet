import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Http, Headers, Response, RequestOptions } from '@angular/http';
import { tokenNotExpired, AuthHttp, JwtHelper } from 'angular2-jwt';
import { Observable, BehaviorSubject } from 'rxjs/Rx';

import { User } from '../appModel';

import { environment } from '../../environments/environment';
import { UserService } from '../user/user.service';

@Injectable()
export class AuthService {
  private usersUrl = environment.api + 'users';
  private authUrl = environment.auth + 'token';
  private jwtHelper = new JwtHelper();

  private userSubject$ = new BehaviorSubject<User>(new User());
  user$ = this.userSubject$.asObservable();

  get user(): User {
    return this.userSubject$.getValue();
  }

  set user(value: User) {
    this.userSubject$.next(value);
  }

  constructor(
    private router: Router,
    private http: Http,
    private authHttp: AuthHttp
  ) {
    if (this.authenticated) {
      this.initUserData();
    } else {
      this.user = null;
    }
  }

  private initUserData(_token?: any) {
    const token = _token ? _token : localStorage.getItem('token');
    const tokeninfo = this.parseJwt(token);
    var _user = new User
    _user.id = tokeninfo.Id
    _user.name = tokeninfo.Name;
    _user.lastname = tokeninfo.Lastname;
    this.user = _user;
  }

  get authenticated() {
    return tokenNotExpired('token');
  }

  public login(username: string, password: string): Observable<boolean> {

    const body: any = { "Username": username, "Password": password };
    const headers = new Headers({ 'Content-Type': 'application/json' });
    const options = new RequestOptions({ headers: headers });

    return this.http.post(this.authUrl, body, options)
      .map((response: Response) => {
        const token = response.json().token;
        if (token) {
          localStorage.setItem('token', token);
          this.initUserData(token);
          return true;
        } else {
          return false;
        }
      })
      .catch((error: any) => {
        return Observable.throw(error);
      });
  }

  public logOut() {
    localStorage.removeItem("token");
    this.user = null;
    this.router.navigate(['/login']);
  }


  parseJwt(token) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace('-', '+').replace('_', '/');
    return JSON.parse(window.atob(base64));
  };

  private encodeParams(params: any): string {
    let body: string = "";
    for (const key in params) {
      if (body.length) {
        body += "&";
      }
      body += key + "=";
      body += encodeURIComponent(params[key]);
    }
    return body;
  }

  private authJsonHeaders(): Observable<Headers> {
    const header = new Headers();

    header.append('Content-Type', 'application/json');
    header.append('Accept', 'application/json');

    const token = localStorage.getItem('token');
    if (token) {
      header.append('Authorization', 'Bearer ' + token);
      return Observable.of(header);
    } else {
      return Observable.of(header);
    }
  }

  //Rest API HTTP methods
  public get(url: string): Observable<any> {
    return this.authJsonHeaders().flatMap(res => {
      return this.http.get(url, { headers: res })
        .map(res => res)
        .catch(err => this.handleError(err));
    });
  }

  public post(url: string, data: any): Observable<any> {
    return this.authJsonHeaders().flatMap(res => {
      return this.http.post(url, data, { headers: res })
        .map(res => res)
        .catch(err => this.handleError(err));
    });
  }

  public put(url: string, data: any): Observable<any> {
    return this.authJsonHeaders().flatMap(res => {
      return this.http.put(url, data, { headers: res })
        .map(res => res)
        .catch(err => this.handleError(err));
    });
  }

  public delete(url: string): Observable<any> {
    return this.authJsonHeaders().flatMap(res => {
      return this.http.delete(url, { headers: res })
        .map(res => res)
        .catch(err => this.handleError(err));
    });
  }

  private handleError(error: Response) {
    if (error.status == 401) {
      this.router.navigateByUrl('/login');
      return Observable.throw(error.statusText);
    } else {
      return Observable.throw(error.text());
    }
  }
}
