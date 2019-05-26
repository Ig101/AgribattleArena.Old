import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TextboxWrapperComponent } from './textbox-wrapper.component';
import { ButtonWrapperComponent } from './button-wrapper.component';
import { TooltipComponent } from './tooltip/tooltip.component';
import { LoadingService } from 'src/app/loading';
import { ButtonWrapperNoformComponent } from './button-wrapper-noform.component';

@NgModule({
imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule
],
declarations: [
    TextboxWrapperComponent,
    ButtonWrapperComponent,
    ButtonWrapperNoformComponent,
    TooltipComponent
],
exports: [
    TextboxWrapperComponent,
    ButtonWrapperComponent,
    ButtonWrapperNoformComponent,
    FormsModule,
    ReactiveFormsModule
]
})
export class WrappersModule { }
