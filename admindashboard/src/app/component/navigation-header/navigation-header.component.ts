import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { AlertifyService } from '../../../alertify.service'

@Component({
  selector: 'app-navigation-header',
  templateUrl: './navigation-header.component.html',
  styleUrls: ['./navigation-header.component.css']
})
export class NavigationHeaderComponent {
  message: string = '';
  constructor(private authService: AuthService, private alertify: AlertifyService) {}

  handleLogOut() {
    this.authService.LogoutByRefreshToken().subscribe({next: () => {this.authService.logOut()}});
    this.alertify.success("Logout successful")
  }
}
