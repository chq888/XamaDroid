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

namespace MyRecyclerView
{
    public class Student
    {

        private static long serialVersionUID = 1L;

        public String Name
        {
            get;set;
        }

        public String EmailId
        {
            get; set;
        }

        public Student()
        {

        }
        public Student(String name, String emailId)
        {
            this.Name = name;
            this.EmailId = emailId;
        }

    }
}