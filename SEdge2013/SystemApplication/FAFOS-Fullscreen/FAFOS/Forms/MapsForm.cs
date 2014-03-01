using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using FAFOS.CustomMarkers;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;
using System.Collections;
 

namespace FAFOS
{
    public partial class MapsForm : FAFOS.Background
    {


        // layers
        readonly GMapOverlay top = new GMapOverlay();
        internal readonly GMapOverlay objects = new GMapOverlay("objects");
        internal readonly GMapOverlay routes = new GMapOverlay("routes");
        internal readonly GMapOverlay polygons = new GMapOverlay("polygons");

        // marker
        GMarkerGoogle currentMarker;

        // polygons
        GMapPolygon polygon;

        // etc
        readonly Random rnd = new Random();
        readonly DescendingComparer ComparerIpStatus = new DescendingComparer();
        GMapMarkerRect CurentRectMarker = null;
        string mobileGpsLog = string.Empty;
        bool isMouseDown = false;
        PointLatLng start;
        PointLatLng end;
        int userid;
        WorkOrder[] order;
        ContractService[] service;
        bool prefetch;

        public MapsForm(int id, object orders, object services)
        {
            InitializeComponent();


            //User label
            userid = id;
            setup(userid.ToString(), "FAFOS Day Itinerary");

            //Tables
            DataTable dt = new SalesOrder().getWorkOrders(userid);
            workOrderTable.DataSource = dt;

            DataTable dt2 = new ClientContract().getServices(userid.ToString());
            servicesTable.DataSource = dt2;

            order = (WorkOrder[])orders;
            service = (ContractService[])services;
            prefetch = true;


            //Load the map
            LoadMap();

        }



        private void dgv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ((DataGridView)sender).ClearSelection();
            //Fill in today's existing data
            if (prefetch)
                preload();

        }


        void AddLocation(int order, string place)
        {
            GeoCoderStatusCode status = GeoCoderStatusCode.Unknow;
            PointLatLng? pos = GMapProviders.GoogleMap.GetPoint("Canada, " + place, out status);
            if (pos != null && status == GeoCoderStatusCode.G_GEO_SUCCESS)
            {
                GMarkerGoogle m;
                if (order != 1)
                    m = new GMarkerGoogle(pos.Value, GMarkerGoogleType.green);
                else
                    m = new GMarkerGoogle(pos.Value, GMarkerGoogleType.blue);
                m.ToolTip = new GMapRoundedToolTip(m);
                m.ToolTipText = order + ": " + place;
                if (order != 1)
                    m.ToolTipMode = MarkerTooltipMode.Always;
                else
                    m.ToolTipMode = MarkerTooltipMode.OnMouseOver;

                objects.Markers.Add(m);
                //  objects.Markers.Add(mBorders);
            }
        }


        private void LoadMap()
        {
            // set cache mode only if no internet avaible
            if (!DesignMode)
            {
                // set cache mode only if no internet avaible
                if (!PingNetwork("www.google.ca"))
        //        if (!Stuff.PingNetwork("www.google.ca"))
                {
                    MainMap.Manager.Mode = AccessMode.CacheOnly;
                    MessageBox.Show("No internet connection available, going to CacheOnly mode.", "FAFOS Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                String[] startingAddress = new Franchisee().getAddress(userid);

                // config map         
                MainMap.MapProvider = GMapProviders.BingMap; //.GoogleMap;
                GeoCoderStatusCode status = GeoCoderStatusCode.Unknow;
      //          PointLatLng? p = GMapProviders.GoogleMap.GetPoint(startingAddress[0] + ", " + startingAddress[2] + ", " + startingAddress[1], out status);
                PointLatLng? p = GMapProviders.GoogleMap.GetPoint("817 Silversmith Street, London, Ontario, N6H 5T4, Canada", out status);
                if (p != null && status == GeoCoderStatusCode.G_GEO_SUCCESS)
                {
                    MainMap.Position = p.Value;  
                }
                else
                {
                    MainMap.Position =  new PointLatLng(42.98252, -81.25397);
                }

                MainMap.MinZoom = 3;
                MainMap.MaxZoom = 20;
                MainMap.Zoom = 13;

                // map events
                {

                    MainMap.OnTileLoadStart += new TileLoadStart(MainMap_OnTileLoadStart);


                    MainMap.OnMarkerClick += new MarkerClick(MainMap_OnMarkerClick);
                    MainMap.OnMarkerEnter += new MarkerEnter(MainMap_OnMarkerEnter);
                    MainMap.OnMarkerLeave += new MarkerLeave(MainMap_OnMarkerLeave);

                    MainMap.OnPolygonEnter += new PolygonEnter(MainMap_OnPolygonEnter);
                    MainMap.OnPolygonLeave += new PolygonLeave(MainMap_OnPolygonLeave);

                    MainMap.OnRouteEnter += new RouteEnter(MainMap_OnRouteEnter);
                    MainMap.OnRouteLeave += new RouteLeave(MainMap_OnRouteLeave);

                    MainMap.Manager.OnTileCacheComplete += new TileCacheComplete(OnTileCacheComplete);
                    //   MainMap.Manager.OnTileCacheProgress += new TileCacheProgress(OnTileCacheProgress);
                }
                MainMap.MouseMove += new MouseEventHandler(MainMap_MouseMove);
                MainMap.MouseDown += new MouseEventHandler(MainMap_MouseDown);
                MainMap.MouseUp += new MouseEventHandler(MainMap_MouseUp);

                MainMap.DragButton = MouseButtons.Left;

                // add custom layers  
                {
                    MainMap.Overlays.Add(routes);
                    MainMap.Overlays.Add(polygons);
                    MainMap.Overlays.Add(objects);
                    MainMap.Overlays.Add(top);
                }

                //Set Operational Region
                /*       PointLatLng? p2 = GMapProviders.GoogleMap.GetPoint(startingAddress[2]+", "+startingAddress[0], out status);
                       GMapMarkerCircle circle = new GMapMarkerCircle(p2.Value);
                       circle.Radius = 15000;
                       circle.Stroke=new Pen(Color.Red);
                       circle.Fill = Brushes.Transparent;//new Brush(Color.Red); //Color.Transparent;
                       top.Markers.Add(circle);
                      */
                // set current marker
                currentMarker = new GMarkerGoogle(MainMap.Position, GMarkerGoogleType.red_pushpin);
                currentMarker.IsHitTestVisible = false;
                top.Markers.Add(currentMarker);

            }

        }

        #region -- map events --

        void OnTileCacheComplete()
        {
            // Debug.WriteLine("OnTileCacheComplete");
            long size = 0;
            int db = 0;
            try
            {
                DirectoryInfo di = new DirectoryInfo(MainMap.CacheLocation);
                var dbs = di.GetFiles("*.gmdb", SearchOption.AllDirectories);
                foreach (var d in dbs)
                {
                    size += d.Length;
                    db++;
                }
            }
            catch
            {
            }


        }



        void MainMap_OnMarkerLeave(GMapMarker item)
        {
            if (item is GMapMarkerRect)
            {
                CurentRectMarker = null;

                GMapMarkerRect rc = item as GMapMarkerRect;
                rc.Pen.Color = Color.Blue;

                // Debug.WriteLine("OnMarkerLeave: " + item.Position);
            }
        }

        void MainMap_OnMarkerEnter(GMapMarker item)
        {
            if (item is GMapMarkerRect)
            {
                GMapMarkerRect rc = item as GMapMarkerRect;
                rc.Pen.Color = Color.Red;

                CurentRectMarker = rc;

                // Debug.WriteLine("OnMarkerEnter: " + item.Position);
            }
        }

        GMapPolygon currentPolygon = null;
        void MainMap_OnPolygonLeave(GMapPolygon item)
        {
            currentPolygon = null;
            item.Stroke.Color = Color.MidnightBlue;
            // Debug.WriteLine("OnPolygonLeave: " + item.Name);
        }

        void MainMap_OnPolygonEnter(GMapPolygon item)
        {
            currentPolygon = item;
            item.Stroke.Color = Color.Red;
            //  Debug.WriteLine("OnPolygonEnter: " + item.Name);
        }

        GMapRoute currentRoute = null;
        void MainMap_OnRouteLeave(GMapRoute item)
        {
            currentRoute = null;
            item.Stroke.Color = Color.MidnightBlue;
            //  Debug.WriteLine("OnRouteLeave: " + item.Name);
        }

        void MainMap_OnRouteEnter(GMapRoute item)
        {
            currentRoute = item;
            item.Stroke.Color = Color.Red;
            //Debug.WriteLine("OnRouteEnter: " + item.Name);
        }



        void MainMap_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }
        }



        void MainMap_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = true;

                if (currentMarker.IsVisible)
                {
                    currentMarker.Position = MainMap.FromLocalToLatLng(e.X, e.Y);

                    var px = MainMap.MapProvider.Projection.FromLatLngToPixel(currentMarker.Position.Lat, currentMarker.Position.Lng, (int)MainMap.Zoom);
                    var tile = MainMap.MapProvider.Projection.FromPixelToTileXY(px);

                    //  Debug.WriteLine("MouseDown: geo: " + currentMarker.Position + " | px: " + px + " | tile: " + tile);
                }
            }
        }

        // move current marker with left holding
        void MainMap_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && isMouseDown)
            {
                if (CurentRectMarker == null)
                {
                    if (currentMarker.IsVisible)
                    {
                        currentMarker.Position = MainMap.FromLocalToLatLng(e.X, e.Y);
                    }
                }
                else // move rect marker
                {
                    PointLatLng pnew = MainMap.FromLocalToLatLng(e.X, e.Y);

                    int? pIndex = (int?)CurentRectMarker.Tag;
                    if (pIndex.HasValue)
                    {
                        if (pIndex < polygon.Points.Count)
                        {
                            polygon.Points[pIndex.Value] = pnew;
                            MainMap.UpdatePolygonLocalPosition(polygon);
                        }
                    }

                    if (currentMarker.IsVisible)
                    {
                        currentMarker.Position = pnew;
                    }
                    CurentRectMarker.Position = pnew;

                    if (CurentRectMarker.InnerMarker != null)
                    {
                        CurentRectMarker.InnerMarker.Position = pnew;
                    }
                }

                MainMap.Refresh(); // force instant invalidation
            }
        }

        // click on some marker
        void MainMap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (item is GMapMarkerRect)
                {
                    GeoCoderStatusCode status;
                    var pos = GMapProviders.GoogleMap.GetPlacemark(item.Position, out status);
                    if (status == GeoCoderStatusCode.G_GEO_SUCCESS && pos != null)
                    {
                        GMapMarkerRect v = item as GMapMarkerRect;
                        {
                            v.ToolTipText = pos.Value.Address;
                        }
                        MainMap.Invalidate(false);
                    }
                }
                else
                {

                }
            }
        }

        // loader start loading tiles
        void MainMap_OnTileLoadStart()
        {
            MethodInvoker m = delegate()
            {

            };
            try
            {
                BeginInvoke(m);
            }
            catch
            {
            }
        }


        // center markers on start
        private void MainForm_Load(object sender, EventArgs e)
        {

            Activate();
            TopMost = true;
            TopMost = false;

        }
        #endregion

        #region -- ui events --





        // reload map
        private void button1b_Click(object sender, EventArgs e)
        {
            MainMap.ReloadMap();
        }





        // key-up events
        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            int offset = -22;

            if (e.KeyCode == Keys.Left)
            {
                MainMap.Offset(-offset, 0);
            }
            else if (e.KeyCode == Keys.Right)
            {
                MainMap.Offset(offset, 0);
            }
            else if (e.KeyCode == Keys.Up)
            {
                MainMap.Offset(0, -offset);
            }
            else if (e.KeyCode == Keys.Down)
            {
                MainMap.Offset(0, offset);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                if (currentPolygon != null)
                {
                    polygons.Polygons.Remove(currentPolygon);
                    currentPolygon = null;
                }

                if (currentRoute != null)
                {
                    routes.Routes.Remove(currentRoute);
                    currentRoute = null;
                }

                if (CurentRectMarker != null)
                {
                    objects.Markers.Remove(CurentRectMarker);

                    if (CurentRectMarker.InnerMarker != null)
                    {
                        objects.Markers.Remove(CurentRectMarker.InnerMarker);
                    }
                    CurentRectMarker = null;

                    //RegeneratePolygon();
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                MainMap.Bearing = 0;


            }
        }

        // key-press events
        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (MainMap.Focused)
            {
                if (e.KeyChar == '+')
                {
                    MainMap.Zoom = ((int)MainMap.Zoom) + 1;
                }
                else if (e.KeyChar == '-')
                {
                    MainMap.Zoom = ((int)(MainMap.Zoom + 0.99)) - 1;
                }
                else if (e.KeyChar == 'a')
                {
                    MainMap.Bearing--;
                }
                else if (e.KeyChar == 'z')
                {
                    MainMap.Bearing++;
                }
            }
        }

        private void buttonZoomUp_Click(object sender, EventArgs e)
        {
            MainMap.Zoom = ((int)MainMap.Zoom) + 1;
        }

        private void buttonZoomDown_Click(object sender, EventArgs e)
        {
            MainMap.Zoom = ((int)(MainMap.Zoom + 0.99)) - 1;
        }



        // load gpx file
        private void button16_Click(object sender, EventArgs e)
        {
            using (FileDialog dlg = new OpenFileDialog())
            {
                dlg.CheckPathExists = true;
                dlg.CheckFileExists = false;
                dlg.AddExtension = true;
                dlg.DefaultExt = "gpx";
                dlg.ValidateNames = true;
                dlg.Title = "GMap.NET: open gpx log";
                dlg.Filter = "gpx files (*.gpx)|*.gpx";
                dlg.FilterIndex = 1;
                dlg.RestoreDirectory = true;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string gpx = File.ReadAllText(dlg.FileName);

                        gpxType r = MainMap.Manager.DeserializeGPX(gpx);
                        if (r != null)
                        {
                            if (r.trk.Length > 0)
                            {
                                foreach (var trk in r.trk)
                                {
                                    List<PointLatLng> points = new List<PointLatLng>();

                                    foreach (var seg in trk.trkseg)
                                    {
                                        foreach (var p in seg.trkpt)
                                        {
                                            points.Add(new PointLatLng((double)p.lat, (double)p.lon));
                                        }
                                    }

                                    GMapRoute rt = new GMapRoute(points, string.Empty);
                                    {
                                        rt.Stroke = new Pen(Color.FromArgb(144, Color.Red));
                                        rt.Stroke.Width = 5;
                                        rt.Stroke.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
                                    }
                                    routes.Routes.Add(rt);
                                }

                                MainMap.ZoomAndCenterRoutes(null);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //  Debug.WriteLine("GPX import: " + ex.ToString());
                        MessageBox.Show("Error importing gpx: " + ex.Message, "GMap.NET", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }


        // open disk cache location
        private void button17_Click(object sender, EventArgs e)
        {
            try
            {
                string argument = "/select, \"" + MainMap.CacheLocation + "TileDBv5\"";
                System.Diagnostics.Process.Start("explorer.exe", argument);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to open: " + ex.Message, "GMap.NET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void generate_btn_Click(object sender, EventArgs e)
        {
            MainMap.Visible = true;
            int order = 1;
            routes.Routes.Clear();
            objects.Markers.Clear();
            String[] startingAddress = new Franchisee().getAddress(userid);

            PointLatLng? pos;
            GeoCoderStatusCode status = GeoCoderStatusCode.Unknow;
            {
                pos = GMapProviders.GoogleMap.GetPoint(startingAddress[2] + ", " + startingAddress[0], out status);
                if (pos != null && status == GeoCoderStatusCode.G_GEO_SUCCESS)
                {
                    currentMarker.Position = pos.Value;
                }
            }
            AddLocation(order++, startingAddress[0]);
            List<PointLatLng> myWaypoints = new List<PointLatLng>();

            if (workOrderTable.SelectedRows.Count > 0)
            {
                String[] address = new String[workOrderTable.SelectedRows.Count];
                String[] location = new String[workOrderTable.SelectedRows.Count];
                String[] country = new String[workOrderTable.SelectedRows.Count];
                for (int i = 0; i < workOrderTable.SelectedRows.Count; i++)
                {

                    country[i] = workOrderTable.SelectedRows[i].Cells[6].Value.ToString();
                    address[i] = workOrderTable.SelectedRows[i].Cells[3].Value.ToString();
//                    location[i] = workOrderTable.SelectedRows[i].Cells[6].Value.ToString() + ", " + workOrderTable.SelectedRows[i].Cells[4].Value.ToString();
                    location[i] = workOrderTable.SelectedRows[i].Cells[3].Value.ToString() + ", "
                        + workOrderTable.SelectedRows[i].Cells[4].Value.ToString() + ", "
                        + workOrderTable.SelectedRows[i].Cells[5].Value.ToString() + ", "
                        + workOrderTable.SelectedRows[i].Cells[6].Value.ToString() ;

                }

                for (int i = 0; i < workOrderTable.SelectedRows.Count; i++)
                {
  MessageBox.Show(location[i].ToString());
                    PointLatLng? pos1 = GMapProviders.GoogleMap.GetPoint(location[i].ToString(), out status);
                    if (pos1 != null && status == GeoCoderStatusCode.G_GEO_SUCCESS)
                    {
                        myWaypoints.Add(pos1.Value);
                        AddLocation(order++, location[i]);
                        currentMarker.Position = pos1.Value;
                    }
                    else
                    {
                        myWaypoints.Add(new PointLatLng(42.98252, -81.25397));
                        AddLocation(order++, location[i]);
                        currentMarker.Position = new PointLatLng(42.98252, -81.25397);
                    }
                    
                    

                }

            }
            if (servicesTable.SelectedRows.Count > 0)
            {
                String[] address = new String[servicesTable.SelectedRows.Count];
                String[] location = new String[servicesTable.SelectedRows.Count];
                String[] country = new String[servicesTable.SelectedRows.Count];
                for (int i = 0; i < servicesTable.SelectedRows.Count; i++)
                {

                    country[i] = servicesTable.SelectedRows[i].Cells[8].Value.ToString();
                    address[i] = servicesTable.SelectedRows[i].Cells[5].Value.ToString();
                    location[i] = servicesTable.SelectedRows[i].Cells[8].Value.ToString() + ", " + servicesTable.SelectedRows[i].Cells[6].Value.ToString();

                }

                for (int i = 0; i < servicesTable.SelectedRows.Count; i++)
                {
                    PointLatLng? pos1 = GMapProviders.GoogleMap.GetPoint(country[i] + ", " + address[i], out status);
                    myWaypoints.Add(pos1.Value);
                    AddLocation(order++, address[i]);
                    currentMarker.Position = pos1.Value;
                }
            }
            GMapRoute rte = new GMapRoute("name");

            GDirections _dir;
            DirectionsStatusCode _code = GMapProviders.GoogleMap.GetDirections(out _dir, pos.Value, myWaypoints, false, false, false, false, true, true);
            if (_code == DirectionsStatusCode.OK)
            {
                foreach (GDirectionStep _step in _dir.Steps)
                {
                    rte.Points.AddRange(_step.Points);
                }
            }

            routes.Routes.Add(rte);
        }

        private void preload()
        {
            if (order != null)
            {
                for (int i = 0; i < order.Length; i++)
                {
                    if (order[i] != null)
                    {
                        for (int j = 0; j < workOrderTable.Rows.Count; j++)
                        {
                            if (workOrderTable.Rows[j].Cells[1].Value.ToString() == order[i].getID().ToString())
                            {
                                workOrderTable.Rows[j].Selected = true;
                                if (order[i].getCompleted())
                                    workOrderTable.Rows[j].Cells[0].Value = true;
                            }
                        }
                    }
                }
            }

            if (service != null)
            {
                for (int i = 0; i < service.Length; i++)
                {
                    if (service[i] != null)
                    {
                        /* for (int j = 0; j < servicesTable.Rows.Count; j++)
                         {
                             if (new ClientContract().getServiceID(userid.ToString(), j) == service[i].getID())
                             {*/
                        servicesTable.Rows[service[i].getID()].Selected = true;
                        if (service[i].getCompleted())
                            servicesTable.Rows[service[i].getID()].Cells[0].Value = true;
                        /*   }
                       }*/
                    }
                }
            }

        }

        private void saveRoute_Click(object sender, EventArgs e)
        {
            prefetch = false;
            for (int i = 0; i < workOrderTable.SelectedRows.Count; i++)
            {
                int check = 0;
                DataGridViewCheckBoxCell ch = (DataGridViewCheckBoxCell)workOrderTable.SelectedRows[i].Cells[0];
                if (ch.Value != null)
                {
                    if ((bool)ch.Value)
                    {
                        check = 1;
                    }

                }
                if (check == 1)
                    new SalesOrder().setDone(workOrderTable.SelectedRows[i].Cells[1].Value.ToString());
            }

            for (int i = 0; i < servicesTable.SelectedRows.Count; i++)
            {
                int check = 0;
                DataGridViewCheckBoxCell ch = (DataGridViewCheckBoxCell)servicesTable.SelectedRows[i].Cells[0];
                if (ch.Value != null)
                {
                    if ((bool)ch.Value)
                    {
                        check = 1;
                    }

                }
                Itinerary it = new Itinerary();

                if (check == 1)
                    it.complete(new ClientContract().getServiceID(userid.ToString(),
                        servicesTable.SelectedRows[i].Index), servicesTable.SelectedRows[i].Cells[3].Value.ToString());

            }
            this.Close();

        }

        public bool PingNetwork(string hostNameOrAddress)
        {
            bool pingStatus = false;

            using (System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping())
            {
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 120;

                try
                {
                    System.Net.NetworkInformation.PingReply reply = p.Send(hostNameOrAddress, timeout, buffer);
                    pingStatus = (reply.Status == System.Net.NetworkInformation.IPStatus.Success);
                }
                catch (Exception)
                {
                    pingStatus = false;
                }
            }

            return pingStatus;
        }

        private void MapsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (prefetch)
            {
                for (int i = 0; i < order.Length; i++)
                {
                    if (order[i] != null)
                        order[i].clear();
                }
                for (int i = 0; i < workOrderTable.SelectedRows.Count; i++)
                {
                    bool check = false;
                    DataGridViewCheckBoxCell ch = (DataGridViewCheckBoxCell)workOrderTable.SelectedRows[i].Cells[0];
                    if (ch.Value != null)
                    {
                        if ((bool)ch.Value)
                        {
                            check = true;
                        }

                    }

                    order[i] = new WorkOrder(Convert.ToInt32(workOrderTable.SelectedRows[i].Cells[1].Value),
                        workOrderTable.SelectedRows[i].Cells[2].Value.ToString(), workOrderTable.SelectedRows[i].Cells[3].Value.ToString(),
                        workOrderTable.SelectedRows[i].Cells[4].Value.ToString(), workOrderTable.SelectedRows[i].Cells[5].Value.ToString(),
                        workOrderTable.SelectedRows[i].Cells[6].Value.ToString(), check);
                }

                for (int i = 0; i < service.Length; i++)
                {
                    if (service[i] != null)
                        service[i].clear();
                }
                for (int i = 0; i < servicesTable.SelectedRows.Count; i++)
                {
                    bool check = false;
                    DataGridViewCheckBoxCell ch = (DataGridViewCheckBoxCell)servicesTable.SelectedRows[i].Cells[0];
                    if (ch.Value != null)
                    {
                        if ((bool)ch.Value)
                        {
                            check = true;
                        }

                    }
                    service[i] = new ContractService(servicesTable.SelectedRows[i].Index, servicesTable.SelectedRows[i].Cells[1].Value.ToString(),
                        servicesTable.SelectedRows[i].Cells[2].Value.ToString(), servicesTable.SelectedRows[i].Cells[3].Value.ToString(),
                        servicesTable.SelectedRows[i].Cells[4].Value.ToString(), servicesTable.SelectedRows[i].Cells[5].Value.ToString(),
                        servicesTable.SelectedRows[i].Cells[6].Value.ToString(), servicesTable.SelectedRows[i].Cells[7].Value.ToString(),
                        servicesTable.SelectedRows[i].Cells[8].Value.ToString(),
                        check);
                }


            }

        }

    }
}
