export class QueryBase<TFilter, TOrder> {
  public take?: number;
  public skip?: number;
  public filter: TFilter;
  public order?: TOrder[];
  public withCount = true;

  constructor(obj?: Partial<QueryBase<TFilter, TOrder>>) {
    Object.assign(this, obj);
  }
}
