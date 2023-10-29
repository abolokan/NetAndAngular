import {ChangeDetectorRef, Component, Input, OnDestroy, OnInit, ViewChild, ViewRef} from '@angular/core';
import {DatePipe} from '@angular/common';

// libs
import {Observable, of, Subject} from 'rxjs';
import {filter, takeUntil} from 'rxjs/operators';
import {MatPaginator, PageEvent} from '@angular/material/paginator';
import {MatTableDataSource} from '@angular/material/table';

// interfaces
import {IFileInfo, IFileInfoFilter} from '../../interfaces';
import {IDonkeyColumn, IDonkeyOrder, IRowAction} from '../../../donkey-table/interfaces';

// models
import {QueryBase} from '../../../models';

// services
import {FileStorageService} from '../../services';

// enums
import {RowAction} from '../../../donkey-table';

const COLUMNS: IDonkeyColumn[] = [
  {  name: 'name', header: 'Name',  displayed: true },
  {  name: 'size', header: 'Size', displayed: true },
  {  name: 'uploadedDate', header: 'Uploaded', displayed: true },
  {  name: 'action', header: '#', displayed: true, controls: [ { name: RowAction.DownLoad, icon: 'cloud_download' }] },
];

@Component({
  selector: 'app-file-storage',
  templateUrl: './file-storage.component.html',
  styleUrls: ['./file-storage.component.scss']
})
export class FileStorageComponent implements OnInit, OnDestroy  {

  @Input() extensionId: number;
  public showNoData: boolean;

  public pageSize = 10;
  public totalCount: number;

  public order: IDonkeyOrder;
  public columns$: Observable<IDonkeyColumn[]>;
  public dataSource: MatTableDataSource<IFileInfo> ;

  @ViewChild(MatPaginator) private _paginator: MatPaginator;

  private _destroy$ = new Subject<boolean>();

  constructor(private _fileStorageService: FileStorageService,
              private _cdr: ChangeDetectorRef,
              private datePipe: DatePipe, ) { }

  public ngOnInit(): void {
    this._listening();
    this._search();
    this.columns$ = of(COLUMNS);
  }

  public ngOnDestroy(): void {
    this._destroy$.next(true);
    this._destroy$.complete();
  }

  public pageChange(event: PageEvent): void {
    this.pageSize = event.pageSize;
    this._search(event.pageIndex);
  }

  public sort(name: string): void {
    this.order = this._sort(this.order, name);
    this._search();
  }

  public onRowAction(event: IRowAction<IFileInfo>): void {
    switch (event.action) {
      case RowAction.DownLoad:
        this.downloadFile(event.data);
        break;
    }
  }

  public downloadFile(file: IFileInfo): void {
    this._fileStorageService.downLoadFile(file.id)
      .pipe(takeUntil(this._destroy$))
      .subscribe((e: Blob) => {
        const url = window.URL.createObjectURL(e);
        const a: any = document.createElement('a');
        document.body.appendChild(a);
        a.style = 'display: none';
        a.href = url;
        a.download = file.name;
        a.click();
        window.URL.revokeObjectURL(url);
      });
  }

  private _search(pageIndex?: number): void {

    const query: QueryBase<IFileInfoFilter, IDonkeyOrder> = new QueryBase<IFileInfoFilter, IDonkeyOrder>(
      {
        withCount: true,
        take: this.pageSize,
        skip: this.pageSize * (pageIndex || 0),
        order: this.order ? [this.order] : null,
        filter: { extensionId: this.extensionId }
      }
    );

    this._fileStorageService.getList(query)
      .pipe(takeUntil(this._destroy$))
      .subscribe({
        next: (response) => {
          this.totalCount = response.count;
          response.items = response.items.map((fileInfo: IFileInfo) => {
            fileInfo.uploadedDate = this.datePipe.transform(fileInfo.uploadedDate, 'dd-MM-YYYY HH:mm');
            return fileInfo;
          });
          this._setDataSource(response.items);
        },
      });
  }
  private _setDataSource(items: IFileInfo[]): void {
    if (this.dataSource) {
      this.dataSource.connect().next(items);
    } else {
      this.dataSource = new MatTableDataSource<IFileInfo>(items);
      this.dataSource.paginator = this._paginator;
    }
    this.showNoData = !this.totalCount;
    this._cgahgeCdr();
  }

  private _sort(order: IDonkeyOrder, field: string): IDonkeyOrder {
    if (!order || order.field !== field) {
      order = {
        field,
        desc: false,
      };
    } else {
      order.desc = !order.desc;
    }
    return order;
  }

  private _cgahgeCdr(): void {
    !(this._cdr as ViewRef).destroyed && this._cdr.detectChanges();
  }

  private _listening(): void {
    this._fileStorageService.refreshTable$
      .pipe(
        filter((extensions: number[]) => extensions.includes(this.extensionId)),
        takeUntil(this._destroy$))
      .subscribe(() => this._search());
  }
}
