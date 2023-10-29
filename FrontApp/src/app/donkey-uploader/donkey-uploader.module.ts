import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DonkeyUploaderComponent } from './donkey-uploader/donkey-uploader.component';
import {MatIconModule} from '@angular/material/icon';



@NgModule({
  declarations: [DonkeyUploaderComponent],
  imports: [
    CommonModule,
    MatIconModule
  ],
  exports: [
    DonkeyUploaderComponent
  ]
})
export class DonkeyUploaderModule { }
