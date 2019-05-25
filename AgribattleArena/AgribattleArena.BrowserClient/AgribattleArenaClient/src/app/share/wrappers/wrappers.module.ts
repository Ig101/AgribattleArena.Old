import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TextboxWrapperComponent } from './textbox-wrapper.component';
import { ButtonWrapperComponent } from './button-wrapper.component';
import { TooltipComponent } from './tooltip/tooltip.component';
import { LoadingService } from 'src/app/loading';

@NgModule({
imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule
],
declarations: [
    TextboxWrapperComponent,
    ButtonWrapperComponent,
    TooltipComponent
],
exports: [
    TextboxWrapperComponent,
    ButtonWrapperComponent,
    FormsModule,
    ReactiveFormsModule
]
})
export class WrappersModule { }
