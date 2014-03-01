using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;

namespace FAFOS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new View());
        }
    }
        struct IpStatus
        {
            private string countryName;
            public string CountryName
            {
                get
                {
                    return countryName;
                }
                set
                {
                    countryName = value;
                }
            }

            private int connectionsCount;
            public int ConnectionsCount
            {
                get
                {
                    return connectionsCount;
                }
                set
                {
                    connectionsCount = value;
                }
            }
        }
        class DescendingComparer : IComparer<IpStatus>
        {
            public bool SortOnlyCountryName = false;

            public int Compare(IpStatus x, IpStatus y)
            {
                int r = 0;

                if (!SortOnlyCountryName)
                {
                    r = y.ConnectionsCount.CompareTo(x.ConnectionsCount);
                }

                if (r == 0)
                {
                    return x.CountryName.CompareTo(y.CountryName);
                }
                return r;
            }
        }
    }


