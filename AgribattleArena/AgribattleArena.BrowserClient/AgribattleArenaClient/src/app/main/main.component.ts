import { Component, OnInit, AfterViewInit } from '@angular/core';
import { LoadingService } from '../loading';

@Component({
    selector: 'app-main',
    templateUrl: './main.component.html',
    styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit, AfterViewInit {

    constructor(private loadingService: LoadingService) {

    }

    ngOnInit() {
    }

    ngAfterViewInit() {
        setTimeout(() => {
            this.loadingService.loadingEnd();
        }, 10);
    }
}
