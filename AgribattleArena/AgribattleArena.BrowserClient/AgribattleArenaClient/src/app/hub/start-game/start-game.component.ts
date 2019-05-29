import { Component, OnInit, AfterViewInit } from '@angular/core';
import { QueueService } from 'src/app/share/queue.service';
@Component({
    selector: 'app-start-game',
    templateUrl: './start-game.component.html',
    styleUrls: ['../hub.component.css']
})
export class StartGameComponent implements OnInit {

    // TODO Pipe to transform queue time

    constructor(private queueService: QueueService) { }

    ngOnInit() {

    }

}
