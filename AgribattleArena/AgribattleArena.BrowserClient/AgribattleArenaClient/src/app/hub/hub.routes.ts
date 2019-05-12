import { Routes } from '@angular/router';
import { HubComponent } from './hub.component';
import { LoginComponent } from './login.component';

export const hubRoutes: Routes = [
    {path: '', component: HubComponent},
    {path: 'login', component: LoginComponent},
];
