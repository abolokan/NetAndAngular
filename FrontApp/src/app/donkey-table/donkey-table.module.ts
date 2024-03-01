import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

// material
import {MatIconModule} from '@angular/material/icon';
import {MatLegacyTableModule as MatTableModule} from '@angular/material/legacy-table';
import {MatLegacyButtonModule as MatButtonModule} from '@angular/material/legacy-button';
import {MatLegacyPaginatorModule as MatPaginatorModule} from '@angular/material/legacy-paginator';

// components
import { DonkeySortTableComponent, DonkeyTableComponent } from './components';
import {A11yModule} from '@angular/cdk/a11y';
import {MatSortModule} from '@angular/material/sort';
import {MatLegacyProgressSpinnerModule as MatProgressSpinnerModule} from '@angular/material/legacy-progress-spinner';

@NgModule({
  declarations: [DonkeyTableComponent, DonkeySortTableComponent],
  imports: [
    A11yModule,
    CommonModule,
    MatIconModule,
    MatTableModule,
    MatSortModule,
    MatButtonModule,
    MatPaginatorModule
  ],
  exports: [
    DonkeyTableComponent
  ]
})
export class DonkeyTableModule { }
