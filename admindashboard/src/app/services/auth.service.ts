import { Injectable, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { User } from '../models/user';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})


export class AuthService implements OnInit {
  private urlUser = "User";
  private Logout = "logout";
  private urlCheckToken = "validate-refresh-token";
  date: number = 0;
  
  ngOnInit(): void {
  }
  
  constructor(private http: HttpClient, private router: Router) { 
  }
  
  // Lấy api authentication user
  public AuthenticationUser(user: User) {
    return this.http.post<any>(`${environment.apiUrl}/${this.urlUser}`, user); //
  }

  public ValidateRefreshToken(): Observable<any> {
    return this.http.post<any>(`${environment.apiUrl}/${this.urlUser}/${this.urlCheckToken}`, {});
  }

   public LogoutByRefreshToken(): Observable<any> {
    return this.http.get<any>(`${environment.apiUrl}/${this.urlUser}/${this.Logout}`);
  }
  
  public storeToken(tokenValue: string) {
    localStorage.setItem('token', tokenValue);
  }

  public getToken() {
    return localStorage.getItem('token');
  }

  public isLoggedIn() {
    return !!localStorage.getItem('token');
  }

  public logOut() {
    localStorage.removeItem("token");
    this.router.navigate(["/login"]);
  }

  public clearLocalStorage() {
    localStorage.clear();
    this.router.navigate(["/login"]);
  }

  public autoLogout(expirationDate: number) {
    setTimeout(() => {   
      this.logOut();
    }, expirationDate);
  } 
}
