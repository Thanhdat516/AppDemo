import { Injectable, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { User } from '../models/user';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})

export class AuthService implements OnInit {
  private urlUser = "User";
  date: number = 0;
  
  ngOnInit(): void {
  }
  
  constructor(private http: HttpClient, private router: Router) { 
  }
  
  // Lấy api authentication user
  public AuthenticationUser(user: User) {
    return this.http.post<any>(`${environment.apiUrl}/${this.urlUser}`, user);
  }
  
  public storeToken(tokenValue: string) {
    localStorage.setItem('token', tokenValue);
  }

  public getToken() {
    return localStorage.getItem('token');
  }

  public issLoggedIn() {
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
