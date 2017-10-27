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

namespace MyRecyclerView.Views
{
    
    public class StudentRecyclerAdapter : SimpleRecycleViewAdapterBase
    {
        private const int VIEW_ITEM = 1;
        private const int VIEW_PROG = 0;

        private Activity _activity;
        private List<Student> _list;



        public StudentRecyclerAdapter(Activity activity, List<Student> list)
        {
            this._activity = activity;
            this._list = list;
            //this._list.CollectionChanged += List_CollectionChanged;
        }

        public StudentRecyclerAdapter(RecyclerView recyclerView, List<Student> list)
        {
            this._list = list;

            //      recyclerView.addOnScrollListener(new RecyclerView.OnScrollListener() {
            //@Override
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

        }

        //private void List_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        //{
        //    _activity.RunOnUiThread(NotifyDataSetChanged);
        //}

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is StudentViewHolder) {

                Student singleStudent = (Student)_list[position];

                ((StudentViewHolder)holder).tvName.Text = (singleStudent.Name);

                ((StudentViewHolder)holder).tvEmailId.Text = (singleStudent.EmailId);

                ((StudentViewHolder)holder).student = singleStudent;

            } else {
                ((ProgressViewHolder)holder).progressBar.Indeterminate = true;
            }

        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            RecyclerView.ViewHolder vh = null;

            if (viewType == VIEW_ITEM)
            {
                View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.recyclerview_item_f, parent, false);

                vh = new StudentViewHolder(view);
            }
            else if (viewType == VIEW_PROG)
            {
                View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.progressbar_more, parent, false);
                vh = new ProgressViewHolder(view);
            }

            return vh;
        }

        public override int ItemCount
        {
            get
            {
                return _list.Count;
            }
        }

        public override int GetItemViewType(int position)
        {
            return _list[position] != null ? VIEW_ITEM : VIEW_PROG;
        }

        //public void setLoaded()
        //{
        //    loading = false;
        //}

        //public void setOnLoadMoreListener(OnLoadMoreListener onLoadMoreListener)
        //{
        //    this.onLoadMoreListener = onLoadMoreListener;
        //}

        //// Clean all elements of the recycler
        //public void Clear()
        //{
        //    _list.Clear();
        //    NotifyDataSetChanged();
        //}

        //// Add a list of items
        //public void addAll(List<Announcement> list)
        //{
        //    list.AddRange(list);
        //    NotifyDataSetChanged();
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //if (_list != null)
                //{
                //    _list.CollectionChanged -= List_CollectionChanged;
                //    _list.Clear();
                //    _list = null;
                //}

                if (_activity != null)
                {
                    _activity = null;
                }
            }

            base.Dispose(disposing);
        }

    }


    public class StudentViewHolder : SimpleViewHolder
    {

        public TextView tvName;

        public TextView tvEmailId;

        public Student student;

        //      public StudentViewHolder(View v)
        //      {
        //          super(v);


        //          v.setOnClickListener(new OnClickListener() {

        //  @Override
        //  public void onClick(View v)
        //          {
        //              Toast.makeText(v.getContext(),
        //                "OnClick :" + student.getName() + " \n " + student.getEmailId(),
        //                Toast.LENGTH_SHORT).show();

        //          }
        //      });
        //}

        public StudentViewHolder(View v) : base(v)
        {
            tvName = (TextView)v.FindViewById(Resource.Id.tvName);

            tvEmailId = (TextView)v.FindViewById(Resource.Id.tvEmailId);
        }

    }
}