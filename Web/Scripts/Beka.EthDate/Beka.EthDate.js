var reference_date=new Date(11,7,28,1,0,0,0);
reference_date.setFullYear(11);
reference_date.setDate(28);
reference_date.setMonth(7);
//reference_date.setTime(-61802182800000);

//reference_date=

var reference_date_eth={year:4,month:1,date:1};
var month_name_amh=["mes","tik","hidar","tasa","tir","yekatit","Megabit","Miyaza","Ginbot","Sene","hamle","Nehase","pagume"];
var day_name_amh=["seno","Maks","rob","hamus","arb","kidame","ehud"];
var day_name_amh_short=["se","Ma","ro","ha","ar","kd","eh"];
var day = 1000 * 60 * 60 * 24;

month_name_amh=['መስከረም', 'ጥቅምት', 'ኅዳር', 'ታህሣሥ', 'ጥር', 'የካቲት','መጋቢት', 'ሚያዝያ', 'ግንቦት', 'ሰኔ', 'ሐምሌ', 'ነሐሴ', 'ጳጉሜ'];
month_name_amh_short= ['መስከረም', 'ጥቅምት', 'ኅዳር', 'ታህሣሥ', 'ጥር', 'የካቲት','መጋቢት', 'ሚያዝያ', 'ግንቦት', 'ሰኔ', 'ሐምሌ', 'ነሐሴ', 'ጳጉሜ'];
day_name_amh = ['ሰኞ', 'ማክሰኞ', 'ረቡዕ', 'ሓሙስ', 'ዓርብ', 'ቅዳሜ', 'እሑድ'];
day_name_amh_short = ['ሰኞ', 'ማክ', 'ረቡ', 'ሐሙ', 'ዓር', 'ቅዳ', 'እሑ'];

//Date.prototype.mGetDay=Date.prototype.getDay
Date.prototype.mGetDay = function() {
return (this.getDay() + 6) %7;
}
function EthDate(year,month,date)
{
	this.parse=function(txt)
	{
		var arr=txt.split("/");
	
		this.year=arr[2]/1;
		this.month=arr[1]/1;
		this.date=arr[0]/1;
        this.month=this.month>13?13:this.month;
        
        if(this.month==13 && this.date>6)
        {
            this.date=5;
        }
        else if(this.date>30)
        {
            this.date=30;
        }
		return this;
	}
	this.fromGregStr = function (input_date) {
	    if(!input_date)
        {
            input_date=new Date().toLocaleDateString();
        }
        var g = new Date();
        
	    if (input_date) {
	         g = new Date(Date.parse(input_date));
	    }
	        /* if (input_date) {
	        var arr = input_date.split("/");
	        g.setFullYear(arr[2]/1);
	        g.setMonth(arr[0]-1);
	        g.setDate(arr[1]-1);
	     //   g = new Date(arr[2], arr[0], arr[1]);
	    }*/
	    this.fromGreg(g);
	    return this;
	}
this.fromGreg = function (input_date) {
        input_date.setHours(0,0,0,0);
	    var elaspsed_date = Math.floor(1 + (input_date.getTime() - reference_date.getTime()) /day) ;
       // console.log("fromGreg",elaspsed_date)
	    var lipyear = Math.floor((elaspsed_date + 1) / (365 * 4 + 1));
	    var elapsedYear = Math.floor((elaspsed_date - lipyear) / 365);
	    lipyear = Math.floor((elaspsed_date) / (365 * 4 + 1))
	    var date_in_year = (elaspsed_date - lipyear - 365 * elapsedYear);
	    var month = Math.floor(date_in_year / 30);
	    var date = Math.floor(date_in_year % 30);



	    this.year = elapsedYear + reference_date_eth.year;
	    this.month = month + 1;
	    this.date = date + 1;
	    return this;


	}
	this.toGreg = function () {
	    //var reference_date = new Date(11, 7, 28, 0, 0, 0, 0);
	   // reference_date.setFullYear(11);
	    var date_diff = Math.floor((this.year - reference_date_eth.year) * 365.25) + Math.floor((this.month - reference_date_eth.month) * 30) + this.date - reference_date_eth.date;
	    var elapsed_ms = Math.floor(date_diff) * 1000 * 60 * 60 * 24;
	    var total_ms = reference_date.getTime()/1 + elapsed_ms/1
	    var new_time = new Date(total_ms);
	   // new_time.setTime(total_ms);
	    return new_time;
	    
	}
	this.year=year?year:2000;
	this.month=month?month:1;
	this.date=date?date:1;
	if(!year)
	{
	       
		this.fromGreg(new Date());
        //console.log("No Year",this.date);
	}
	this.toString=function(){return this.date + "/" + this.month + "/" + this.year}
	
			
}
