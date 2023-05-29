namespace ProfessionalWebsite.Client.Classes.Mgmt.DataTables
{
    public sealed class AnimationsTable
    {
        public AnimationsTable()
        {
            IsContinuous = new List<bool>()
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

        public List<bool> IsContinuous { get; private set; }
    }
}
