import { Routes } from '@angular/router';
import { MainComponent } from './main.component';
import { MainResolver } from './main.resolver';

export const mainRoutes: Routes = [
    {path: '', component: MainComponent, resolve: {profile: MainResolver}},
];
