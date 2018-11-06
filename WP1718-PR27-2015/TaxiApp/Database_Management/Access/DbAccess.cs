using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxiApp.Database_Management.Access
{
    public class DbAccess
    {
        private static DbAccess _instance;
        private static readonly object _syncLock = new object();
        public static DbAccess Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new DbAccess();
                        }
                    }
                }
                return _instance;
            }
        }

        private DbAccess()
        {
            AdminDbAccess = new AdminDbAccess();
            CustomerDbAccess = new CustomerDbAccess();
            DriverDbAccess = new DriverDbAccess();
            TaxiDriveDbAccess = new TaxiDriveDbAccess();
            VehicleDbAccess = new VehicleDbAccess();
            CommentDbAccess = new CommentDbAccess();
            LocationDbAccess = new LocationDbAccess();
        }

        public AdminDbAccess AdminDbAccess { get; private set; }
        public CustomerDbAccess CustomerDbAccess { get; private set; }
        public DriverDbAccess DriverDbAccess { get; private set; }
        public TaxiDriveDbAccess TaxiDriveDbAccess { get; private set; }
        public VehicleDbAccess VehicleDbAccess { get; private set; }
        public CommentDbAccess CommentDbAccess { get; private set; }
        public LocationDbAccess LocationDbAccess { get; private set; }
    }
}