using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FAFOS
{
    class ContractService
    {
        int id;
        string service;
    
        string period;
        string nextDate;
        string notes;
        string address;
        string city;
        string province;
        string country;
        bool completed;

        public ContractService(int arg0,string arg,string arg2, string arg3, string arg4, string arg5, string arg6, string arg7,
            string arg8,  bool arg9)
        {
            id = arg0;
            service = arg;
 
            period = arg2;
            nextDate = arg3;
            notes = arg4;
            address = arg5;
            city = arg6;
            province = arg7;
            country = arg8;
            completed = arg9;
        }
        public void clear()
        {
            service = null;

            period = null;
            nextDate = null;
            notes = null;
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
        public string getService()
        {
            return service;
        }

        public string getPeriod()
        {
            return period;
        }
        public string getNextDate()
        {
            return nextDate;
        }
        public string getnotes()
        {
            return notes;
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
