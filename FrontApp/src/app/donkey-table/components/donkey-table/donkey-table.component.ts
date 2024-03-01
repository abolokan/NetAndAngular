import {ChangeDetectionStrategy, Component, EventEmitter, Input, OnChanges, Output, SimpleChanges, ViewChild} from '@angular/core';

// rxjs
import {from, Observable, of} from 'rxjs';
import {filter, map, toArray} from 'rxjs/operators';

// material
import {MatLegacyTableDataSource as MatTableDataSource} from '@angular/material/legacy-table';
import {MatLegacyPaginator as MatPaginator, LegacyPageEvent as PageEvent} from '@angular/material/legacy-paginator';

// enums
import {RowAction} from '../../row-action.enum';

// interfaces
import {IDonkeyColumn, IDonkeyOrder, IRowAction} from '../../interfaces';

@Component({
  selector: 'app-donkey-table',
  templateUrl: './donkey-table.component.html',
  styleUrls: ['./donkey-table.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DonkeyTableComponent  implements OnChanges {
  @Input() public dataSource: MatTableDataSource<unknown>;
  @Input() public showNoData = false;
  @Input() public noDataText = '';
  @Input() public columns: IDonkeyColumn[] = [];
  @Input() public order: IDonkeyOrder;
  @Input() public totalCount = 0;
  @Input() public pageSize = 10;

  @Output() public changePageEmit: EventEmitter<PageEvent> = new EventEmitter<PageEvent>();
  @Output() public sortEmit: EventEmitter<string> = new EventEmitter<string>();
  @Output() public actionEmit: EventEmitter<IRowAction<any>> = new EventEmitter<IRowAction<any>>();

  @ViewChild(MatPaginator, { static: true }) set paginator(paginator: MatPaginator) {
    if (paginator) {
      this._paginator = paginator;
      this._paginator.pageSize = this.pageSize;
      this._paginator.pageIndex = this._pageIndex;
    }
  }

  public pageSizeCounts$: Observable<number[]> = of([10, 20, 50, 100]);
  public displayedColumns$: Observable<string[]>;

  public _pageIndex = 0;
  private _paginator: MatPaginator;

  constructor() { }

  public ngOnChanges(changes: SimpleChanges): void {
    this._setDisplayedColumns(changes);
    this._setPaginator(changes);
  }

  public trackByFn(index: any): void {
    return index;
  }

  public pageChange(event: PageEvent): void {

    this._pageIndex = event.pageIndex;
    this.changePageEmit.emit(event);
  }

  public emitActionRow<T>(action: RowAction, data: T): void {
    this.actionEmit.emit({ action, data });
  }

  public sort(field: string): void {
    this.sortEmit.emit(field);
  }

  private _setDisplayedColumns(changes: SimpleChanges): void {
    if (changes.columns && changes.columns.currentValue) {
      this.displayedColumns$ = from(this.columns)
        .pipe(
          filter((column) => column.displayed),
          map((v) => v.name),
          toArray(),
        );
    }
  }

  private _setPaginator(changes: SimpleChanges): void {
    if (changes.dataSource && changes.dataSource.currentValue) {
     this.dataSource.paginator = this.paginator;
    }
  }

}
