import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegisterFormComponent } from './components/register-form/register-form.component';
import { LoginFormComponent } from './components/login-form/login-form.component';
import { CoverComponent } from './components/cover/cover.component';
import { LoginAdminFormComponent } from './components/login-admin-form/login-admin-form.component';
import { MainAdminPageComponent } from './components/main-admin-page/main-admin-page.component';
import { MainPageComponent } from './components/main-page/main-page.component';
import { authUserGuard } from './guards/user/auth-user.guard';

const routes: Routes = [
  {
    path: '',
    component: CoverComponent
  },
  
  {
    path: 'register',
    component: RegisterFormComponent
  },
  {
    path: 'login',
    component: LoginFormComponent
  },
  {
    path: 'login_admin',
    component: LoginAdminFormComponent
  },
  {
    path: 'customer',
    component: MainPageComponent,
    canActivate:[authUserGuard],
    children: [
      {
        path: '',
        loadChildren: () => import('./components/home-section/home-section.module').then(c => c.HomeSectionModule)
      },
      {
        path: 'home',
        loadChildren: () => import('./components/home-section/home-section.module').then(c => c.HomeSectionModule)
      },
      {
        path: 'my_bank',
        loadChildren: () => import('./components/bank-account/bank-account.module').then(c => c.BankAccountModule)
      },
      {
        path: 'transaction',
        loadChildren: () => import('./components/payment-transfer/payment-transfer.module').then(c => c.PaymentTransferModule)
      },
      {
        path: 'history',
        loadChildren: () => import('./components/history/history.module').then(c => c.HistoryModule)
      },
      {
        path: 'bank_account',
        loadChildren: () => import('./components/add-bank-account/add-bank-account.module').then(c => c.AddBankAccountModule)
      },
      {
        path: 'dashboard',
        loadChildren: () => import('./components/dashboard/dashboard.module').then(c => c.DashboardModule)
      },
      {
        path: 'contact',
        loadChildren: () => import('./components/contact-us/contact-us.module').then(c => c.ContactUsModule)
      },
      {
        path: 'profile',
        loadChildren: () => import('./components/customer-profile/customer-profile.module').then(c => c.CustomerProfileModule)
      }
    ]
  },
  {
    path: 'admin',
    component: MainAdminPageComponent,
    children: [
      {
        path: 'admin_profile',
        loadChildren: () => import('./components/admin-profile/admin-profile.module').then(c => c.AccountProfileModule)
      },
      {
        path: 'customer_transaction_request',
        loadChildren: () => import('./components/customer-transaction-request/customer-transaction-request.module').then(c => c.CustomerTransactionRequestModule)
      },
      {
        path: 'customer_help_request',
        loadChildren: () => import('./components/customer-help-request/customer-help-request.module').then(c => c.CustomerHelpRequestModule)
      },
      {
        path: 'customer_revenue',
        loadChildren: () => import('./components/customer-revenue/customer-revenue.module').then(c => c.CustomerRevenueModule)
      },
      {
        path: 'customer_account',
        loadChildren: () => import('./components/customer-account-management/customer-account-management.module').then(c => c.CustomerAccountManagementModule)
      },
      {
        path: 'staff_account',
        loadChildren: () => import('./components/staff-manager-account-management/staff-manager-account-management.module').then(c => c.StaffManagerAccountManagementModule)
      },
      {
        path: 'account',
        loadChildren: () => import('./components/account-management/account-management.module').then(c => c.AccountManagementModule)
      },
      { path: 'admin', redirectTo: '/admin_profile', pathMatch: 'full' }
    ]
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
