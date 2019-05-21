import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../share';
import { LoadingService } from 'src/app/loading';
import { Router } from '@angular/router';
import { FormGroup } from '@angular/forms';

@Component({
    selector: 'app-profile',
    templateUrl: './profile.component.html',
    styleUrls: ['../main.component.css']
})
export class ProfileComponent implements OnInit {

    private profileForm: FormGroup;

    constructor(private authService: AuthService, private loadingService: LoadingService, private router: Router) {

    }

    ngOnInit() {
        this.profileForm = new FormGroup({ });
    }

    playButtonPress() {

    }

    logOutButtonPress() {

    }
}
