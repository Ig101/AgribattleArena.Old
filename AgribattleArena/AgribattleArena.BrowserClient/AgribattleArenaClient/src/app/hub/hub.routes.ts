import { Routes } from '@angular/router';
import { HubComponent } from './hub.component';
import { StartPageComponent } from './start-page.component';

const hubChildRoutes: Routes = [
    {path: '', component: StartPageComponent}
];

export const hubRoutes: Routes = [
    {path: '', component: HubComponent, children: hubChildRoutes},
];
