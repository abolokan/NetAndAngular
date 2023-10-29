import {ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit} from '@angular/core';

// libs
import {Subject} from 'rxjs';

// services
import {FileStorageService} from '../../services';

// interfaces
import {IFileExtension} from '../../interfaces';

@Component({
  selector: 'app-file-list',
  templateUrl: './file-list.component.html',
  styleUrls: ['./file-list.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FileListComponent implements OnInit, OnDestroy{

  public extensions: IFileExtension[] = [];

  private _destroy$ = new Subject<boolean>();

  constructor(private _fileStorageService: FileStorageService,
              private _cdr: ChangeDetectorRef) { }
  public ngOnInit(): void {
    this._loadExtensions();
    this._fileStorageService.refreshTableList$.subscribe(() => this._loadExtensions());
  }

  public ngOnDestroy(): void {
    this._destroy$.next(true);
    this._destroy$.complete();
  }

  public trackByFn(index: any): void {
    return index;
  }

  private _loadExtensions(): void {
    this._fileStorageService.getExtensions()
      .subscribe((extensions) => {
        this.extensions = extensions;
        this._fileStorageService.actualExtensionList = extensions.map(m => m.id);
        this._cdr.detectChanges();
      });
  }
}
