import {Component, ElementRef, EventEmitter, Input, Output, ViewChild} from '@angular/core';

@Component({
  selector: 'app-donkey-uploader',
  templateUrl: './donkey-uploader.component.html',
  styleUrls: ['./donkey-uploader.component.scss']
})
export class DonkeyUploaderComponent {

  @Input() acceptableExtensions: string;
  @Output() uploadFilesEmit: EventEmitter<FileList> = new EventEmitter<FileList>();

  @ViewChild('importFile') private _importFileInput: ElementRef;

  public choseImportFile(): void {
    this._importFileInput.nativeElement.click();
  }

  public uploadFiles(fileList: FileList): void {
    this.uploadFilesEmit.emit(fileList);
    this._importFileInput.nativeElement.value = '';
  }
}
