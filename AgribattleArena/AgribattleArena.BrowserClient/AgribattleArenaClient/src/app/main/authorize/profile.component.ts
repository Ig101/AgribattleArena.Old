import { Component } from '@angular/core';
import { AuthService } from '../../share';
import { LoadingService } from 'src/app/loading';
import { Router } from '@angular/router';

@Component({
    selector: 'app-profile',
    templateUrl: './profile.component.html',
    styleUrls: ['../main.component.css']
})
export class ProfileComponent {

    constructor(private authService: AuthService, private loadingService: LoadingService, private router: Router) {

    }

    playButtonPress() {

    }

    logOutButtonPress() {

    }
}
