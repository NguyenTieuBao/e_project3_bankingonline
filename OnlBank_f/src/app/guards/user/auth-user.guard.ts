import { CanActivateFn, Router } from '@angular/router';
import { ApiService } from '../../services/api.service';
import { inject } from '@angular/core';
import { NgToastService } from 'ng-angular-popup';

export const authUserGuard: CanActivateFn = (route, state) => {
  const apiService = inject(ApiService);
  const router = inject(Router);
  const toast = inject(NgToastService);
  if (apiService.isLoggedInUser())
  {
    return true;
  } else
  {
    toast.danger("Please Login First!", "ERROR", 5000);
    router.navigate(['/login']);
    return false;
  }
};
