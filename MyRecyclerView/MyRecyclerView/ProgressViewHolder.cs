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
    public class ProgressViewHolder : RecyclerView.ViewHolder
    {

        public ProgressBar progressBar;

        public ProgressViewHolder(View v) : base(v)
        {
            progressBar = (ProgressBar)v.FindViewById(Resource.Id.progressbar_more);
        }

    }
}