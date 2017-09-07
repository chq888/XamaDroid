using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Views;
using MyRecyclerView.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyRecyclerView.Fragments
{
    public class Fragment1 : Fragment
    {

        StudentRecyclerAdapter mAdapter;
        private List<Student> studentList = new List<Student>();

      
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public static Fragment1 NewInstance()
        {
            var frag1 = new Fragment1 { Arguments = new Bundle() };
            return frag1;
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            var v = inflater.Inflate(Resource.Layout.fragment1, null);
            RecyclerView mRecyclerView = (RecyclerView)v.FindViewById(Resource.Id.my_recycler_view);

            loadData();  // in this method, Create a list of items.

            mRecyclerView.HasFixedSize = true;
            var layoutManager = new LinearLayoutManager(Activity);

            var onScrollListener = new RecyclerViewOnScrollListener(layoutManager);
            onScrollListener.LoadMoreEvent += async (object sender, EventArgs e) =>
            {
                //add null , so the adapter will check view_type and show progress bar at bottom
                studentList.Add(null);
                mAdapter.NotifyItemInserted(studentList.Count - 1);

                await Task.Run(() =>
                {
                    // remove progress item
                    studentList.RemoveAt(studentList.Count - 1);
                    mAdapter.NotifyItemRemoved(studentList.Count);
                    //add items one by one
                    int start = studentList.Count;
                    int end = start + 20;

                    for (int i = start + 1; i <= end; i++)
                    {
                        studentList.Add(new Student("Student " + i, "AndroidStudent" + i + "@gmail.com"));
                        mAdapter.NotifyItemInserted(studentList.Count);
                    }

                    //mAdapter.setLoaded();
                    //or you can add all at once but do not forget to call mAdapter.notifyDataSetChanged();

                });
                //Load more stuff here
            };

            mRecyclerView.AddOnScrollListener(onScrollListener);

            mRecyclerView.SetLayoutManager(layoutManager);


            mAdapter = new StudentRecyclerAdapter(mRecyclerView, studentList);
            mRecyclerView.SetAdapter(mAdapter);

            if (studentList.Count == 0)
            {
                mRecyclerView.Visibility = ViewStates.Gone;
                //tvEmptyView.setVisibility(View.VISIBLE);
            }
            else
            {
                mRecyclerView.Visibility = ViewStates.Visible;
                //tvEmptyView.setVisibility(View.GONE);
            }


    //        mAdapter.setOnLoadMoreListener(new OnLoadMoreListener() {
    //        @Override
    //        public void onLoadMore()
    //        {
    //            //add null , so the adapter will check view_type and show progress bar at bottom
    //            studentList.add(null);
    //            mAdapter.notifyItemInserted(studentList.size() - 1);

    //            handler.postDelayed(new Runnable() {
    //                @Override
    //                public void run()
    //            {
    //                //   remove progress item
    //                studentList.remove(studentList.size() - 1);
    //                mAdapter.notifyItemRemoved(studentList.size());
    //                //add items one by one
    //                int start = studentList.size();
    //                int end = start + 20;

    //                for (int i = start + 1; i <= end; i++)
    //                {
    //                    studentList.add(new Student("Student " + i, "AndroidStudent" + i + "@gmail.com"));
    //                    mAdapter.notifyItemInserted(studentList.size());
    //                }
    //                mAdapter.setLoaded();
    //                //or you can add all at once but do not forget to call mAdapter.notifyDataSetChanged();
    //            }
    //        }, 2000);

    //    }
    //});



            return v;
        }

        // load initial data
        private void loadData()
        {

            for (int i = 1; i <= 20; i++)
            {
                studentList.Add(new Student("Student " + i, "androidstudent" + i + "@gmail.com"));

            }


        }

        //public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        //{
        //    var view = base.OnCreateView(inflater, container, savedInstanceState);

        //    var recyclerView = view.FindViewById<RecyclerView>(Resource.Id.my_recycler_view);
        //    if (recyclerView != null)
        //    {
        //        recyclerView.HasFixedSize = true;

        //        var layoutManager = new LinearLayoutManager(Activity);

        //        var onScrollListener = new RecyclerViewOnScrollListener(layoutManager);
        //        onScrollListener.LoadMoreEvent += (object sender, EventArgs e) => {
        //            //Load more stuff here
        //        };

        //        recyclerView.AddOnScrollListener(onScrollListener);

        //        recyclerView.SetLayoutManager(layoutManager);
        //    }
        //    return view;
        //}


    }
}