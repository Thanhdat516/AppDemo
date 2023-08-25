import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

export const authGuard: CanActivateFn = (route, state) => {
  // const currentMenu = route.url[0].path;
  const router = inject(Router);
  const authService = inject(AuthService);
  // const token = authService.getToken();

  if (authService.issLoggedIn())
  {
    return true;
  }
  else {
    router.navigate(["/accessdenid"]);
    return false;
  }
};
