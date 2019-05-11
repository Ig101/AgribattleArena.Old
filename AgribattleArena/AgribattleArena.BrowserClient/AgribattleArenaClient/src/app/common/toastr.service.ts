import { InjectionToken} from '@angular/core';

export let TOASTR_TOKEN = new InjectionToken<Toastr>('toastr'); // or Toastr interface is defined

export interface Toastr {
    success(message: string, title?: string);
    info(message: string, title?: string);
    warning(message: string, title?: string);
    error(message: string, title?: string);
}
