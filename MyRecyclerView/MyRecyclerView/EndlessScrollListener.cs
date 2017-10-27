﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MyRecyclerView
{
    public abstract class EndlessScrollListener : Java.Lang.Object, AbsListView.IOnScrollListener
    {

        // The minimum number of items to have below your current scroll position
        // before loading more.
        private int visibleThreshold = 5;
        // The current offset index of data you have loaded
        private int currentPage = 0;
        // The total number of items in the dataset after the last load
        private int previousTotalItemCount = 0;
        // True if we are still waiting for the last set of data to load.
        private bool loading = true;
        // Sets the starting page index
        private int startingPageIndex = 0;


        public EndlessScrollListener()
        {
        }

        public EndlessScrollListener(int visibleThreshold)
        {
            this.visibleThreshold = visibleThreshold;
        }

        public EndlessScrollListener(int visibleThreshold, int startPage)
        {
            this.visibleThreshold = visibleThreshold;
            this.startingPageIndex = startPage;
            this.currentPage = startPage;
        }

        // Defines the process for actually loading more data based on page
        // Returns true if more data is being loaded; returns false if there is no more data to load.
        public abstract bool onLoadMore(int page, int totalItemsCount);

        public void OnScroll(AbsListView view, int firstVisibleItem, int visibleItemCount, int totalItemCount)
        {
            // If the total item count is zero and the previous isn't, assume the
            // list is invalidated and should be reset back to initial state
            if (totalItemCount < previousTotalItemCount)
            {
                this.currentPage = this.startingPageIndex;
                this.previousTotalItemCount = totalItemCount;
                if (totalItemCount == 0)
                {
                    this.loading = true;
                }
            }

            // If it's still loading, we check to see if the dataset count has
            // changed, if so we conclude it has finished loading and update the current page
            // number and total item count.
            if (loading && (totalItemCount > previousTotalItemCount))
            {
                loading = false;
                previousTotalItemCount = totalItemCount;
                currentPage++;
            }

            // If it isn't currently loading, we check to see if we have breached
            // the visibleThreshold and need to reload more data.
            // If we do need to reload some more data, we execute onLoadMore to fetch the data.
            if (!loading && (firstVisibleItem + visibleItemCount + visibleThreshold) >= totalItemCount)
            {
                loading = onLoadMore(currentPage + 1, totalItemCount);
            }
        }

        public void OnScrollStateChanged(AbsListView view, [GeneratedEnum] ScrollState scrollState)
        {
            // Don't take any action on changed
        }

    }
}



//Notice that this is an abstract class, and that in order to use this, you must extend this base class and define the onLoadMore method to actually retrieve the new data.We can define now an anonymous class within any activity that extends EndlessScrollListener and bind that to the AdapterView.For example:

//public class MainActivity extends Activity
//{
//    @Override
//    protected void onCreate(Bundle savedInstanceState)
//{
//    // ... the usual 
//    ListView lvItems = (ListView)findViewById(R.id.lvItems);
//    // Attach the listener to the AdapterView onCreate
//    lvItems.setOnScrollListener(new EndlessScrollListener() {
//          @Override
//          public boolean onLoadMore(int page, int totalItemsCount)
//    {
//        // Triggered only when new data needs to be appended to the list
//        // Add whatever code is needed to append new items to your AdapterView
//        loadNextDataFromApi(page);
//        // or loadNextDataFromApi(totalItemsCount); 
//        return true; // ONLY if more data is actually being loaded; false otherwise.
//    }
//});
//    }


//   // Append the next page of data into the adapter
//   // This method probably sends out a network request and appends new data items to your adapter. 
//   public void loadNextDataFromApi(int offset)
//{
//    // Send an API request to retrieve appropriate paginated data 
//    //  --> Send the request including an offset value (i.e `page`) as a query parameter.
//    //  --> Deserialize and construct new model objects from the API response
//    //  --> Append the new data objects to the existing set of items inside the array of items
//    //  --> Notify the adapter of the new items made with `notifyDataSetChanged()`
//}
//}

//Now as you scroll, items will be automatically filling in because the onLoadMore method will be triggered once the user crosses the visibleThreshold.This approach works equally well for a GridView and the listener gives access to both the page as well as the totalItemsCount to support both pagination and offset based fetching.
//Implementing with RecyclerView


//We can use a similar approach with the RecyclerView by defining an interface EndlessRecyclerViewScrollListener that requires an onLoadMore() method to be implemented.The LayoutManager, which is responsible in the RecyclerView for rendering where items should be positioned and manages scrolling, provides information about the current scroll position relative to the adapter.For this reason, we need to pass an instance of what LayoutManager is being used to collect the necessary information to ascertain when to load more data.


//Implementing endless pagination for RecyclerView requires the following steps:


//   Copy over the EndlessRecyclerViewScrollListener.java into your application.

//   Call addOnScrollListener(...) on a RecyclerView to enable endless pagination.Pass in an instance of EndlessRecyclerViewScrollListener and implement the onLoadMore which fires whenever a new page needs to be loaded to fill up the list.
//  Inside the aforementioned onLoadMore method, load additional items into the adapter either by sending out a network request or by loading from another source.

//To start handling the scroll events for steps 2 and 3, we need to use the addOnScrollListener() method in our Activity or Fragment and pass in the instance of the EndlessRecyclerViewScrollListener with the layout manager as shown below:

//public class MainActivity extends Activity
//{
//    // Store a member variable for the listener
//    private EndlessRecyclerViewScrollListener scrollListener;

//@Override
//    protected void onCreate(Bundle savedInstanceState)
//{
//    // Configure the RecyclerView
//    RecyclerView rvItems = (RecyclerView)findViewById(R.id.rvContacts);
//    LinearLayoutManager linearLayoutManager = new LinearLayoutManager(this);
//    rvItems.setLayoutManager(linearLayoutManager);
//    // Retain an instance so that you can call `resetState()` for fresh searches
//    scrollListener = new EndlessRecyclerViewScrollListener(linearLayoutManager) {
//           @Override
//           public void onLoadMore(int page, int totalItemsCount, RecyclerView view)
//    {
//        // Triggered only when new data needs to be appended to the list
//        // Add whatever code is needed to append new items to the bottom of the list
//        loadNextDataFromApi(page);
//    }
//};
//// Adds the scroll listener to RecyclerView
//rvItems.addOnScrollListener(scrollListener);
//  }

//  // Append the next page of data into the adapter
//  // This method probably sends out a network request and appends new data items to your adapter. 
//  public void loadNextDataFromApi(int offset)
//{
//    // Send an API request to retrieve appropriate paginated data 
//    //  --> Send the request including an offset value (i.e `page`) as a query parameter.
//    //  --> Deserialize and construct new model objects from the API response
//    //  --> Append the new data objects to the existing set of items inside the array of items
//    //  --> Notify the adapter of the new items made with `notifyItemRangeInserted()`
//}
//}

//Resetting the Endless Scroll State

//When you intend to perform a new search, make sure to clear the existing contents from the list and notify the adapter the contents have changed as soon as possible.Make sure also to reset the state of the EndlessRecyclerViewScrollListener with the resetState method:

//// 1. First, clear the array of data
//listOfItems.clear();
//// 2. Notify the adapter of the update
//recyclerAdapterOfItems.notifyDataSetChanged(); // or notifyItemRangeRemoved
//// 3. Reset endless scroll listener when performing a new search
//scrollListener.resetState();

//You can refer to this code sample for usage and this code sample for the full endless scroll source code.

//All of the code needed is already incorporated in the EndlessRecyclerViewScrollListener.java code snippet above. However, if you wish to understand how the endless scrolling is calculated, the detailed explanation is available here.
//Troubleshooting

//If you are running into problems, please carefully consider the following suggestions:

//    For the ListView, make sure to setup the setOnScrollListener listener in the onCreate method of the Activity or onCreateView in a Fragment and not much later otherwise you may encounter unexpected issues.

//    In order for the pagination system to continue working reliably, you should make sure to clear the adapter of items (or notify adapter after clearing the array) before appending new items to the list.For RecyclerView, it is highly recommended to make more granular updates when notifying the adapter.See this video talk for more context.

//    In order for this pagination system to trigger, keep in mind that as loadNextDataFromApi is called, new data needs to be appended to the existing data source.In other words, only clear items from the list when on the initial "page". Subsequent "pages" of data should be appended to the existing data.

//    If you see Cannot call this method in a scroll callback.Scroll callbacks might be run during a measure & layout pass where you cannot change the RecyclerView data., you need to do the following inside your onLoadMore() method as outlined in this Stack Overflow article to delay the adapter update:

//    // Delay before notifying the adapter since the scroll listeners 
//    // can be called while RecyclerView data cannot be changed.
//    view.post(new Runnable()
//{
//    @Override
//        public void run()
//    {
//        // Notify adapter with appropriate notify methods
//        adapter.notifyItemRangeInserted(curSize, allContacts.size() - 1);
//    }
//});

//Displaying Progress with Custom Adapter

//To display the last row as a ProgressBar indicating that the ListView is loading data, we do the trick in the Adapter.Having defined two types of views in getItemViewType(int position), we can display the last row differently from a normal data row. It can be a ProgressBar or some text to indicate that the ListView has reached the last row by comparing the size of data List to the number of items on the server side. See this gist for sample code.
//References

//    http://benjii.me/2010/08/endless-scrolling-listview-in-android/
//    http://stackoverflow.com/questions/1080811/android-endless-list
//    http://stackoverflow.com/questions/12583419/android-listview-automatically-load-more-data-when-scroll-to-the-bottom
