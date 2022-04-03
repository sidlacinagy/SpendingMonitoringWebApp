export class HomeQueryData{
    public categories:string[];
    public groupby:string;
    public maxdate: string;
    public minprice: number;
    public mindate: string;
    public maxprice: number;
    public subusersFilter:string[];


	constructor($categories: string[], $groupby: string, $maxdate: string, $minprice: number, $mindate: string, $maxprice: number, $subusersFilter: string[]) {
		this.categories = $categories;
		this.groupby = $groupby;
		this.maxdate = $maxdate;
		this.minprice = $minprice;
		this.mindate = $mindate;
		this.maxprice = $maxprice;
		this.subusersFilter = $subusersFilter;
	}
    
}