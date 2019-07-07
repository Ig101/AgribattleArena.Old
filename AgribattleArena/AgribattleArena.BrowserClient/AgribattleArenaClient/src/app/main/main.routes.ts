import { Routes } from '@angular/router';
import { MainComponent } from './main.component';
import { MainResolver } from './main.resolver';
import { StartPageComponent } from './start-page.component';

const mainChildRoutes: Routes = [
    {path: '', component: StartPageComponent}
];

export const mainRoutes: Routes = [
    {path: '', component: MainComponent, resolve: {error: MainResolver}, children: mainChildRoutes},
];
