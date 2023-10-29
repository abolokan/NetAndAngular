import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

// libs
import { forkJoin, Observable, of, Subject } from 'rxjs';

// interfaces
import {IFileExtension, IFileInfo, IFileInfoFilter} from '../interfaces';
import {IRequestOptions,  IQueryResult} from '../../interfaces';
import {IDonkeyOrder} from '../../donkey-table/interfaces';

// models
import {QueryBase} from '../../models';

// environment
import {environment} from '../../../environments/environment';

@Injectable()
export class FileStorageService {

  public acceptableExtension = '.doc,.docx,.xls,.xlsx,.pdf,.tiff,.tif,.png,.jpg,.jpeg,.zip,.rar';

  public actualExtensionList: number[] = [];
  public refreshTableList$: Subject<void> = new Subject<void>();
  public refreshTable$: Subject<number[]> = new Subject<number[]>();

  private _entity = 'files';

  constructor(private _http: HttpClient) { }

  public getList(query: QueryBase<IFileInfoFilter, IDonkeyOrder>): Observable<IQueryResult<IFileInfo>>{
    const url = `${environment.apiUri}${this._entity}`;
    return this._http.post<IQueryResult<IFileInfo>>(url, JSON.stringify(query), this._getDefaultOptions());
  }

  public uploadFileList(files: FileList): Observable<number[]> {
    if (files && files.length > 0) {
      const url = `${environment.apiUri}${this._entity}/upload`;
      const observables: Observable<number>[] = [];

      Array.from(files).forEach((file) => {
        const formData: FormData = new FormData();
        formData.append(file.name, file);
        observables.push(this.uploadItem<number>(url, formData));
      });

      return forkJoin(observables);
    }

    return of([]);
    }

  public uploadItem<T>(url: string, formData: FormData): Observable<T> {
    const headers = new HttpHeaders();
    headers.append('Content-Type', 'multipart/form-data');
    headers.append('Accept', 'application/json');
    return this._http.post<T>(url, formData, {
      responseType: 'json',
      headers,
    });
  }

  public downLoadFile(fileId: number): Observable<Blob> {
    const url = `${environment.apiUri}${this._entity}/download/${fileId}`;
    return this._http.get(url, { responseType: 'blob' });
  }

  public correctFiles(files: FileList): boolean {
    if (!files) {
      return false;
    }
    const acceptableExtensions = this.acceptableExtensions();
    return Array.from(files).every(file => acceptableExtensions.findIndex((ext) => file.name.endsWith(ext) === true) !== -1);
  }

  public checkFilesSize(files: FileList): boolean {
    if (!files) {
      return false;
    }
    return Array.from(files).every(file => file.size > 0);
  }

  public acceptableExtensions(): string[] { return this.acceptableExtension.split(',')};

  public getExtensions(): Observable<IFileExtension[]>{
    const url = `${environment.apiUri}${this._entity}/extensions`;
    return this._http.get<IFileExtension[]>(url);
  }

  private _getDefaultOptions(options?: IRequestOptions): IRequestOptions {
    options = options || {};
    options.headers = options.headers || new HttpHeaders({ 'Content-Type': 'application/json' });
    options.withCredentials = true;
    return options;
  }
}
