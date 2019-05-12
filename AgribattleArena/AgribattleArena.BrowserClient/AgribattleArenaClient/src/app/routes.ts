import { Routes } from '@angular/router';

export const appRoutes: Routes = [
    {path: '', loadChildren: './hub/hub.module#HubModule'},
    {path: 'battle', loadChildren: './battle/battle.module#BattleModule'},
    {path: '**', redirectTo: ''}
];
