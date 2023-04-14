namespace ProfessionalWebsite.Client.Classes.BeeHiveMgmtSystem
{
    public class BeeHiveMgmtSystemController
    {
        public BeeHiveMgmtSystemController()
        {
            theQueen = new QueenBee();
        }
        private QueenBee theQueen;
        public string StatusReport { get { return theQueen.StatusReport; } }
        public float VaultHoney { get { return HoneyVault.Honey; } }
        public float VaultNectar { get { return HoneyVault.Nectar; } }
        public string VaultNotification { get { return HoneyVault.Notifications; } }
        public float Eggs { get { return theQueen.Eggs; } }
        public float UnassignedWorkers { get { return theQueen.UnassignedWorkers; } }
        public int HoneyManufacturers { get { return theQueen.GetWorkerCount(WorkerType.HoneyManufacturer); } }
        public int NectarCollectors { get { return theQueen.GetWorkerCount(WorkerType.NectarCollector); } }
        public int EggNurses { get { return theQueen.GetWorkerCount(WorkerType.EggCare); } }
        public int WorkersTotal { get { return theQueen.GetWorkerCount(); } }
        public void AssignBee(WorkerType workerType) => theQueen.AssignBee(workerType);
        public void WorkTheNextShift() => theQueen.WorkTheNextShift();

        /*public MainWindow()
        {
        /*InitializeComponent();
        // statusReport.Text = theQueen.StatusReport;
        /*theQueen = Resources["queen"] as QueenBee;
            /*
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(1.5);
            timer.Start();
        }*/
        /*
        private void Timer_Tick(object sender, EventArgs e)
        {
            workNextShift_Click(this, new RoutedEventArgs());
        }
        */
        //private QueenBee theQueen = new QueenBee();
        // private DispatcherTimer timer = new DispatcherTimer();
        /*
        private void AssignBee(object sender, RoutedEventArgs e)
        {
            theQueen.AssignBee(jobSelector.Text);
            // statusReport.Text = theQueen.StatusReport;
        }

        private void WorkNextShift(object sender, RoutedEventArgs e)
        {
            theQueen.WorkTheNextShift();
            // statusReport.Text = theQueen.StatusReport;
        }*/
    }
}