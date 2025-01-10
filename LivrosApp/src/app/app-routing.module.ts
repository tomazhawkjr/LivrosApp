import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginModule } from './pages/login/login.module';
import { HomeModule } from './pages/home/home.module';
import { LayoutModule } from './pages/layout/layout.module';
import { RedirectGuard } from './guards/redirect.guard';
import { LoginComponent } from './pages/login/login.component';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./pages/layout/layout.module').then((m) => m.LayoutModule)
  },
  {
    path: 'login',
    loadChildren: () => import('./pages/login/login.module').then((m) => m.LoginModule)
  },
  {
    path: '**',
    component: LoginComponent,
    canActivate: [RedirectGuard]
  },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { onSameUrlNavigation: 'reload' }),
    LayoutModule,
    LoginModule
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
