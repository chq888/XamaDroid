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
    public class RecyclerClickEventArgs : EventArgs
    {

        public View View
        {
            get; set;
        }
        public int Position
        {
            get; set;
        }

    }


    public class SimpleRecycleViewAdapterBase : RecyclerView.Adapter
    {

        public event EventHandler<RecyclerClickEventArgs> ItemClick;
        public event EventHandler<RecyclerClickEventArgs> ItemLongClick;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            throw new NotImplementedException();
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            throw new NotImplementedException();
        }

        public override int ItemCount
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        protected void OnClick(RecyclerClickEventArgs args)
        {
            ItemClick?.Invoke(this, args);
        }

        protected void OnLongClick(RecyclerClickEventArgs args)
        {
            ItemLongClick?.Invoke(this, args);
        }

        public void ReleaseEvent()
        {
            if (ItemClick != null)
            {
                // release event
                Delegate handler = ItemClick.GetInvocationList().FirstOrDefault();
                if (handler != null)
                {
                    ItemClick -= (EventHandler<RecyclerClickEventArgs>)handler;
                    handler = null;
                }

                ItemClick = null;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ReleaseEvent();

                if (ItemLongClick != null)
                {
                    // release event
                    Delegate handler = ItemLongClick.GetInvocationList().FirstOrDefault();
                    if (handler != null)
                    {
                        ItemLongClick -= (EventHandler<RecyclerClickEventArgs>)handler;
                        handler = null;
                    }

                    ItemLongClick = null;
                }
            }

            base.Dispose(disposing);
        }

    }
}