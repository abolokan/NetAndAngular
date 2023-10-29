export interface IDonkeyColumn{
  name: string;
  displayed: boolean;
  header?: string;
  controls?: IControl[];
}

export interface IControl{
  name: string;
  icon?: string;
}
