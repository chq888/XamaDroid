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

        private LinearLayoutManager linearLayoutManager;
        private int lastVisibleItem, totalItemCount;
        private bool loading;
        bool isMoreDataAvailable = true;

        //// The minimum amount of items to have below your current scroll position before loading more.
        private int visibleThreshold = 5;

        public RecyclerViewOnScrollListener(LinearLayoutManager layoutManager)
        {
            linearLayoutManager = layoutManager;
        }

        public void SetMoreDataAvailable(bool moreDataAvailable)
        {
            isMoreDataAvailable = moreDataAvailable;
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


        public void SetLoaded()
        {
            loading = false;
        }


        public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
        {
            base.OnScrolled(recyclerView, dx, dy);

            //var visibleItemCount = recyclerView.ChildCount;
            //var totalItemCount = recyclerView.GetAdapter().ItemCount;
            //var pastVisiblesItems = LayoutManager.FindFirstVisibleItemPosition();

            //if ((visibleItemCount + pastVisiblesItems) >= totalItemCount)
            //{
            //    LoadMoreEvent(this, null);
            //    loading = true;
            //}



            totalItemCount = linearLayoutManager.ItemCount;
            lastVisibleItem = linearLayoutManager.FindLastVisibleItemPosition();

            //if (position >= getItemCount() - 1 && isMoreDataAvailable && !isLoading && loadMoreListener != null)
            //{
            //    isLoading = true;
            //    loadMoreListener.onLoadMore();
            //}

            if (LoadMoreEvent != null && isMoreDataAvailable && !loading && totalItemCount <= (lastVisibleItem + visibleThreshold))
            {
                LoadMoreEvent(this, null);

                loading = true;
            }

        }
    }
}