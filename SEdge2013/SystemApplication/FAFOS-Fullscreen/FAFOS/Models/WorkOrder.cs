using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FAFOS
{
    class WorkOrder
    {
        int id;
        string dateIssued;
        string address;
        string city;
        string province;
        string country;
        bool completed;
        public WorkOrder(int arg, string arg1, string arg2,string arg3,string arg4,string arg5,bool arg6)
        {
            id = arg;
            dateIssued = arg1;
            address = arg2;
            city = arg3;
            province = arg4;
            country = arg5;
            completed = arg6;
        }
        public void clear()
        {
            id = 0;
            dateIssued = null;
            address = null;
            city = null;
            province = null;
            country = null;
            completed = false;
        }
        public int getID()
        {
            return id;
        }
        public string getdate()
        {
            return dateIssued;
        }
        public string getaddress()
        {
            return address;
        }
        public string getcity()
        {
            return city;
        }
        public string getprovince()
        {
            return province;
        }
        public string getcountry()
        {
            return country;
        }
        public bool getCompleted()
        {
            return completed;
        }

    }
}
