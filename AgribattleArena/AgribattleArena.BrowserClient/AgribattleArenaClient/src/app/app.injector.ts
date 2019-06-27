import { Injector } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app.module';

export let appInjector: Injector;

platformBrowserDynamic().bootstrapModule(AppModule).then(
(componentRef) => {
  appInjector = componentRef.injector;
});
