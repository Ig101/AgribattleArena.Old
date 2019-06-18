import { Component} from '@angular/core';
import { LoadingService } from 'src/app/loading';
import { Router } from '@angular/router';
import { ProfileService } from 'src/app/share/profile.service';
import { QueueService } from 'src/app/share/queue.service';
import { IExternalWrapper } from 'src/app/share/models';

@Component({
    selector: 'app-play',
    templateUrl: './play.component.html',
    styleUrls: ['../../main.component.css']
})
export class PlayComponent {

    constructor(private loadingService: LoadingService, private router: Router,
                private queueService: QueueService) {

    }

    queueResponseHandler(response: IExternalWrapper<any>, loadingService: LoadingService) {
        if (response.statusCode !== 200) {
            loadingService.loadingError(response.errors[0], 0.5);
        }
    }

    playButtonPress() {
        // this.loadingService.loadingStart('Loading...', 1, {route: '/hub', router: this.router} as IRouteLink);
        if (this.queueService.inQueue) {
            this.queueService.dequeue().subscribe((result) => this.queueResponseHandler(result, this.loadingService));
        } else {
            this.queueService.enqueue().subscribe((result) => this.queueResponseHandler(result, this.loadingService));
        }
    }
}
