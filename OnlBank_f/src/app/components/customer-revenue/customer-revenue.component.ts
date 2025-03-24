import { Component, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
// import { Chart } from 'chart.js';

interface DataItem {
  id: number;
  username: string;
  accname: string;
  amount: number;
  trantype: string;
  date: string;
}

@Component({
  selector: 'app-customer-revenue',
  templateUrl: './customer-revenue.component.html',
  styleUrl: './customer-revenue.component.css'
})
export class CustomerRevenueComponent {
  public data: DataItem[] = [{"id":1,"username":"msofe0","accname":"Minnnie Sofe","amount":45976017.818,"trantype":"Teal","date":"12/19/2023"},
    {"id":2,"username":"easplen1","accname":"Earle Asplen","amount":7044654.099,"trantype":"Pink","date":"1/9/2024"},
    {"id":3,"username":"gcuseck2","accname":"Gayler Cuseck","amount":75487521.559,"trantype":"Indigo","date":"9/25/2023"},
    {"id":4,"username":"mfawltey3","accname":"Michel Fawltey","amount":21722135.824,"trantype":"Mauv","date":"10/17/2023"},
    {"id":5,"username":"tpariss4","accname":"Theo Pariss","amount":8978481.719,"trantype":"Red","date":"4/13/2024"},
    {"id":6,"username":"lpirdue5","accname":"Lynn Pirdue","amount":52550861.374,"trantype":"Khaki","date":"11/24/2023"},
    {"id":7,"username":"imargery6","accname":"Issiah Margery","amount":4720739.783,"trantype":"Yellow","date":"3/15/2024"},
    {"id":8,"username":"bsetchfield7","accname":"Binnie Setchfield","amount":32023126.445,"trantype":"Blue","date":"2/12/2024"},
    {"id":9,"username":"drolfe8","accname":"Dewey Rolfe","amount":74357001.786,"trantype":"Teal","date":"1/23/2024"},
    {"id":10,"username":"dkelsow9","accname":"Dorey Kelsow","amount":37563897.022,"trantype":"Goldenrod","date":"8/24/2023"},
    {"id":11,"username":"tsabbana","accname":"Teena Sabban","amount":75335935.305,"trantype":"Maroon","date":"8/23/2023"},
    {"id":12,"username":"ohartilb","accname":"Olenolin Hartil","amount":60734628.402,"trantype":"Mauv","date":"1/5/2024"},
    {"id":13,"username":"oyashnovc","accname":"Orelia Yashnov","amount":40116413.299,"trantype":"Violet","date":"3/5/2024"},
    {"id":14,"username":"moloinnd","accname":"Merwyn O'Loinn","amount":15856395.547,"trantype":"Teal","date":"9/10/2023"},
    {"id":15,"username":"hsecombee","accname":"Hermann Secombe","amount":83280012.185,"trantype":"Indigo","date":"7/11/2023"},
    {"id":16,"username":"tfoleyf","accname":"Tedda Foley","amount":26138829.754,"trantype":"Fuscia","date":"10/11/2023"},
    {"id":17,"username":"sillsleyg","accname":"Shane Illsley","amount":61832217.773,"trantype":"Blue","date":"5/26/2024"},
    {"id":18,"username":"losmanth","accname":"Louisa Osmant","amount":74756114.802,"trantype":"Goldenrod","date":"8/21/2023"},
    {"id":19,"username":"agodmani","accname":"Ambur Godman","amount":91061411.078,"trantype":"Red","date":"6/19/2024"},
    {"id":20,"username":"gjeremaesj","accname":"Gwenore Jeremaes","amount":9054376.346,"trantype":"Pink","date":"6/3/2024"},
    {"id":21,"username":"rbishk","accname":"Row Bish","amount":76183944.339,"trantype":"Mauv","date":"9/22/2023"},
    {"id":22,"username":"mkindonl","accname":"Muire Kindon","amount":52031485.519,"trantype":"Crimson","date":"1/29/2024"},
    {"id":23,"username":"tchillcotm","accname":"Trev Chillcot","amount":8222771.657,"trantype":"Fuscia","date":"4/28/2024"},
    {"id":24,"username":"cdaughtreyn","accname":"Corey Daughtrey","amount":73845581.624,"trantype":"Pink","date":"6/28/2024"},
    {"id":25,"username":"kphilipo","accname":"Kizzie Philip","amount":90765491.746,"trantype":"Red","date":"11/7/2023"},
    {"id":26,"username":"jcoenp","accname":"Jessie Coen","amount":64377170.244,"trantype":"Purple","date":"3/21/2024"},
    {"id":27,"username":"sblackallerq","accname":"Sylas Blackaller","amount":28582225.547,"trantype":"Red","date":"7/11/2023"},
    {"id":28,"username":"amcdonnellr","accname":"Ambrosius McDonnell","amount":74591706.429,"trantype":"Indigo","date":"4/20/2024"},
    {"id":29,"username":"rcorpss","accname":"Rosemary Corps","amount":12419986.381,"trantype":"Purple","date":"5/13/2024"},
    {"id":30,"username":"rgibbardt","accname":"Rice Gibbard","amount":41693838.918,"trantype":"Indigo","date":"5/26/2024"}]

    public paginatedData: DataItem[] = [];
    public selectedRow: DataItem | null = null;

    @ViewChild(MatPaginator) paginator!: MatPaginator;

    ngOnInit(): void {
      this.OnPageChange;
    }

    OnPageChange (event: PageEvent){
      const startIndex = event.pageIndex * event.pageSize;
      let endIndex = startIndex + event.pageSize;
      if(endIndex > this.data.length){
        endIndex = this.data.length;
      }
      this.paginatedData = this.data.slice(startIndex, endIndex);
    }
    updatePaginatedData(startIndex: number, endIndex: number) {
      this.paginatedData = this.data.slice(startIndex, endIndex);
    }

    selectRow(row: DataItem) {
      this.selectedRow = row;
      // this.updateCharts();
    }
    private initializeCharts() {
      const ctxArea = (document.getElementById('myAreaChart') as HTMLCanvasElement).getContext('2d');
      const ctxBar = (document.getElementById('myBarChart') as HTMLCanvasElement).getContext('2d');
      const ctxPie = (document.getElementById('myPieChart') as HTMLCanvasElement).getContext('2d');
  
      // if (ctxArea) {
      //   new Chart(ctxArea, {
      //     type: 'line',
      //     data: {
      //       labels: this.data.map(item => item.date),
      //       datasets: [{
      //         label: 'Amount',
      //         data: this.data.map(item => item.amount),
      //         backgroundColor: 'rgba(78, 115, 223, 0.05)',
      //         borderColor: 'rgba(78, 115, 223, 1)',
      //       }]
      //     }
      //   });
      }
  
      // if (ctxBar) {
      //   new Chart(ctxBar, {
      //     type: 'bar',
      //     data: {
      //       labels: this.data.map(item => item.date),
      //       datasets: [{
      //         label: 'Amount',
      //         data: this.data.map(item => item.amount),
      //         backgroundColor: 'rgba(78, 115, 223, 1)',
      //       }]
      //     }
      //   });
      // }
  
      // if (ctxPie) {
      //   new Chart(ctxPie, {
      //     type: 'doughnut',
      //     data: {
      //       labels: this.data.map(item => item.trantype),
      //       datasets: [{
      //         data: this.data.map(item => item.amount),
      //         backgroundColor: ['#4e73df', '#1cc88a', '#36b9cc', '#f6c23e', '#e74a3b', '#858796', '#5a5c69'],
      //       }]
      //     }
      //   });
      // }
    }
  
    // private updateCharts() {
    //   // Update the charts with the selected row's data if needed
    // }
// }
