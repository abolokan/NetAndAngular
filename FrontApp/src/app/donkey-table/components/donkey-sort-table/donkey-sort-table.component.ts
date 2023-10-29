import { ChangeDetectionStrategy, Component, Input } from '@angular/core';

@Component({
  selector: 'app-donkey-sort-table',
  templateUrl: './donkey-sort-table.component.html',
  styleUrls: ['./donkey-sort-table.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DonkeySortTableComponent {

  @Input() public fieldName = '';
  @Input() public orderField = '';
  @Input() public desc = false;

  public isOrderField(): boolean {
    return this.orderField === this.fieldName;
  }

}
