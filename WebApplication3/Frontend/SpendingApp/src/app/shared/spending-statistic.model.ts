export class SpendingStatistic{
    public key: any;
    public sum:number;
    public count:number;
    constructor(key:any,sum:number,count:number) {
      this.key=key;
      this.sum=sum;
      this.count=count;
    }
}