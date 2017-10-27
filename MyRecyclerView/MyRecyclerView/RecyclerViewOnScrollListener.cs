using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;

namespace MyRecyclerView
{
    public class RecyclerViewOnScrollListener : RecyclerView.OnScrollListener
    {

        public delegate void LoadMoreEventHandler(object sender, EventArgs e);
        public event LoadMoreEventHandler LoadMoreEvent;

        private LinearLayoutManager layoutManager;
        private int lastVisibleItem, totalItemCount;
        private bool loading;
        private int visibleThreshold = 5;


        public RecyclerViewOnScrollListener(LinearLayoutManager layoutManager)
        {
            this.layoutManager = layoutManager;
        }

        //public void onScrolled(RecyclerView recyclerView,
        //        int dx, int dy)
        //      {
        //          super.onScrolled(recyclerView, dx, dy);

        //          totalItemCount = linearLayoutManager.getItemCount();
        //          lastVisibleItem = linearLayoutManager
        //            .findLastVisibleItemPosition();
        //          if (!loading
        //            && totalItemCount <= (lastVisibleItem + visibleThreshold))
        //          {
        //              // End has been reached
        //              // Do something
        //              if (onLoadMoreListener != null)
        //              {
        //                  onLoadMoreListener.onLoadMore();
        //              }
        //              loading = true;
        //          }
        //      }
        //  });

        public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
        {
            base.OnScrolled(recyclerView, dx, dy);

            var visibleItemCount = recyclerView.ChildCount;
            var totalItemCount = recyclerView.GetAdapter().ItemCount;
            var pastVisiblesItems = layoutManager.FindFirstVisibleItemPosition();

            if ((visibleItemCount + pastVisiblesItems) >= totalItemCount)
            {
                LoadMoreEvent(this, null);
                loading = true;
            }

            //totalItemCount = LayoutManager.ItemCount;
            //lastVisibleItem = LayoutManager.FindLastVisibleItemPosition();
            //if (!loading && totalItemCount <= (lastVisibleItem + visibleThreshold))
            //{
            //    // End has been reached
            //    // Do something
            //    if (LoadMoreEvent != null)
            //    {
            //        LoadMoreEvent(this, null);
            //    }

            //    loading = true;
            //}
        }

    }
}