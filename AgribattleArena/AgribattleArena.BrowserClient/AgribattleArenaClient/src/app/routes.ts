import { Routes } from '@angular/router';
import { mainMatcher } from './main.matcher';
import { battleMatcher } from './battle.matcher';

export const appRoutes: Routes = [
    {path: '', matcher: mainMatcher, loadChildren: './main/main.module#MainModule' },
    {path: '', matcher: battleMatcher, loadChildren: './battle/battle.module#BattleModule'},
    {path: '**', redirectTo: ''}
];
