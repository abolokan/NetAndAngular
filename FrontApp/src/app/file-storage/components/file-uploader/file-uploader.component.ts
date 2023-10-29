import { Component, OnDestroy } from '@angular/core';

// libs
import {takeUntil} from 'rxjs/operators';
import {Subject} from 'rxjs';

// services
import {FileStorageService} from '../../services';

@Component({
  selector: 'app-file-uploader',
  templateUrl: './file-uploader.component.html',
  styleUrls: ['./file-uploader.component.scss']
})
export class FileUploaderComponent implements OnDestroy {

  public acceptableExtensions = this._fileStorageService.acceptableExtension;

  private _destroy$ = new Subject<boolean>();

  constructor(private _fileStorageService: FileStorageService) { }

  public ngOnDestroy(): void {
    this._destroy$.next(true);
    this._destroy$.complete();
  }

  public uploadFiles(files: FileList): void {
    if (!this._fileStorageService.correctFiles(files)) {
      alert(`Invalid file format. Acceptable formats are: ${this.acceptableExtensions}.`);
      return;
    }

    if (!this._fileStorageService.checkFilesSize(files)) {
      alert('File cannot be empty');
      return;
    }

    this._fileStorageService.uploadFileList(files)
      .pipe(takeUntil(this._destroy$))
      .subscribe((extensionIds: number[]) => {
        if (this._fileStorageService.actualExtensionList.some(extId => extensionIds.includes(extId)))
        {
          this._fileStorageService.refreshTable$.next(extensionIds);
        }
        else
        {
          this._fileStorageService.refreshTableList$.next();
        }
        this._fileStorageService.actualExtensionList = [...new Set([...this._fileStorageService.actualExtensionList, ...extensionIds])];
      });
  }

}
