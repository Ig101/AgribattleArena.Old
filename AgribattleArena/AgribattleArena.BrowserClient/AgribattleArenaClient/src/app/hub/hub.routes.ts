import { Routes } from '@angular/router';
import { HubComponent } from './hub.component';

const hubChildRoutes: Routes = [
];

export const hubRoutes: Routes = [
    {path: '', component: HubComponent, children: hubChildRoutes},
];
