namespace ProfessionalWebsite.Client.Classes.Mgmt
{
    public sealed class AnimationsTable
    {
        private AnimationsTable() { }
        private static AnimationsTable? instance;
        private static object lockobject = new object();
        public static AnimationsTable Instance
        {
            get
            {
                lock(lockobject)
                {
                    if (instance == null)
                        instance = new AnimationsTable();
                    return instance;
                }
            }
        }
        public List<bool> IsContinuous = new List<bool>()
        {
            false,  // Bombastic
            false,  // Skywalker
            false,  // Kitchen Sink
            false,  // Flipster
            false,  // Asteroid
            false,  // Flip On X
            false,  // FLip On Y
            false,  // Rotate on Z
            false,  // East Is Up
            false,  // West Is Up
            false,  // SloRo
        };
    }
}
