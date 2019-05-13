import { Routes } from '@angular/router';

export const appRoutes: Routes = [
    {path: '', loadChildren: './main/main.module#MainModule'},
    {path: 'hub', loadChildren: './hub/hub.module#HubModule'},
    {path: 'battle', loadChildren: './battle/battle.module#BattleModule'},
    {path: '**', redirectTo: ''}
];
