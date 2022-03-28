export class Spending{
    public id: string;
    public subuserid:string;
    public product:string;
    public productCategory:string;
    public price:number;
    public date:Date;
    constructor(id:string,subuserid:string,product:string,productCategory:string,price:number,date:Date) {
      this.id=id;
      this.subuserid=subuserid;
      this.product=product;
      this.productCategory=productCategory;
      this.price=price;
      this.date=date;
    }
}