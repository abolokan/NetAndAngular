import { NgModule } from '@angular/core';
import {CommonModule, DatePipe} from '@angular/common';
import {HttpClientModule} from '@angular/common/http';
import {DonkeyTableModule} from '../donkey-table';
import {DonkeyUploaderModule} from '../donkey-uploader';

// services
import {FileStorageService} from './services';

// routing
import {FileStorageRouting} from './file-storage.routing';

// components
import { FileStorageComponent, FileListComponent, FileUploaderComponent } from './components';

@NgModule({
  declarations: [
    FileStorageComponent,
    FileListComponent,
    FileUploaderComponent
  ],
  imports: [
    FileStorageRouting,
    CommonModule,
    HttpClientModule,
    DonkeyTableModule,
    DonkeyUploaderModule
  ],
  providers: [
    FileStorageService,
    DatePipe
  ]
})
export class FileStorageModule { }
