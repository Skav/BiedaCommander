using System.Collections;
using System.Windows.Forms;


namespace BiedaCommander
{
    internal class ListViewColumnSorter : IComparer
    {
        private int columnToSort;
        private SortOrder OrderOfSort;
        private CaseInsensitiveComparer ObjectCompare;

        public ListViewColumnSorter()
        {
            columnToSort = 0;
            OrderOfSort = SortOrder.None;
            ObjectCompare = new CaseInsensitiveComparer();
        }

        public int Compare(object x, object y)
        {
            int compareResult;
            ListViewItem listviewX, listviewY;

            listviewX = (ListViewItem)x;
            listviewY = (ListViewItem)y;

            compareResult = ObjectCompare.Compare(listviewX.SubItems[columnToSort].Text, listviewY.SubItems[columnToSort].Text);

            if (OrderOfSort == SortOrder.Ascending)
            {
                return compareResult;
            }
            else if(OrderOfSort == SortOrder.Descending)
            {
                return (-compareResult);
            }
            else
            {
                return 0;
            }
        }

        public int SortColumn
        {
            set { columnToSort = value; }
            get { return columnToSort; }
        }
        public SortOrder Order
        {
            set { OrderOfSort= value; }
            get { return OrderOfSort; }
        }
    }
}
