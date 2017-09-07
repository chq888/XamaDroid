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
    public class SimpleViewHolder : RecyclerView.ViewHolder
    {

        protected Action<RecyclerClickEventArgs> clickListener;
        protected Action<RecyclerClickEventArgs> longClickListener;

        public SimpleViewHolder(View itemView, Action<RecyclerClickEventArgs> clickListener = null, Action<RecyclerClickEventArgs> longClickListener = null) : base(itemView)
        {
            if (clickListener != null)
            {
                this.clickListener = clickListener;
                itemView.Click += ItemView_Click;
            }

            if (longClickListener != null)
            {
                this.longClickListener = longClickListener;
                itemView.LongClick += ItemView_LongClick;
            }
        }

        private void ItemView_LongClick(object sender, View.LongClickEventArgs e)
        {
            longClickListener(new RecyclerClickEventArgs { View = this.ItemView, Position = AdapterPosition });
        }

        private void ItemView_Click(object sender, EventArgs e)
        {
            clickListener(new RecyclerClickEventArgs { View = this.ItemView, Position = AdapterPosition });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.ItemView != null)
                {
                    this.clickListener = null;
                    this.longClickListener = null;
                    this.ItemView.Click -= ItemView_Click;
                    this.ItemView.LongClick -= ItemView_LongClick;
                    this.ItemView = null;
                }
            }

            base.Dispose(disposing);
        }

    }
}