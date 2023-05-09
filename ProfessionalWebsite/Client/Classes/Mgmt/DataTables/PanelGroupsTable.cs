﻿namespace ProfessionalWebsite.Client.Classes.Mgmt
{
    public sealed class PanelGroupsTable
    {
        private PanelGroupsTable() { }
        private static PanelGroupsTable? instance;
        private static object instanceLock = new object();
        public static PanelGroupsTable Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                        instance = new PanelGroupsTable();
                    return instance;
                }
            }
        }
        public List<PanelGroup> PanelGroups { get; set; } = new List<PanelGroup>()
        {
            new PanelGroup(0, 4),  // [0] NavMgmt panels
        };
    }
}