using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;

//namespace EWdispatchAllocationList
//{
//    public partial class EWdispatchview : Page
//    {
//        public EWdispatchview()
//        {
//            InitializeComponent();
//        }

//        // Executes when the user navigates to this page.
//        protected override void OnNavigatedTo(NavigationEventArgs e)
//        {
//        }

//    }
//}
//using System;


namespace GridDemo {
    public partial class Grouping {
        public Grouping() {
            InitializeComponent();
            
        }
        void viewsListBox_SelectionChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
        }
  void GroupByCountryThenCity() {
            grid.ClearGrouping();
   grid.GroupBy("Country");
   grid.GroupBy("City");
  }

  private void GroupByCountryThenCityThenOrderDate() {
            grid.ClearGrouping();
            grid.GroupBy("Country");
            grid.GroupBy("City");
            grid.GroupBy("OrderDate");
  }

  private void GroupByCityThenOrderDate() {
            grid.ClearGrouping();
            grid.GroupBy("City");
            grid.GroupBy("OrderDate");
  }
        private void ClearGrouping() {
            grid.ClearGrouping();
        }
        private void groupList_SelectionChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
   if(grid == null) return;
   switch(groupList.SelectedIndex) {
    case 0: GroupByCountryThenCity(); break;
    case 1: GroupByCountryThenCityThenOrderDate(); break;
    case 2: GroupByCityThenOrderDate(); break;
                case 3: ClearGrouping(); break;
   }
  }
    }
}
