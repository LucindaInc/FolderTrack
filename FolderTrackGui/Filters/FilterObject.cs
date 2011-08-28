using System;
using System.Collections.Generic;
using System.Text;
using FolderTrack.ExclusionRules;

namespace FolderTrackGuiTest1.Filters
{
    /// <summary>
    /// Holds data to help user create filters
    /// </summary>
    public class FilterObject
    {
        public interface FilterObCa
        {
            void Use(bool use, string filter);
            void FilterObList(List<FilterObject> fi);
        }
        public enum FilterObjectMode { SELECT, KEEP, PROFILT };

        public bool Custom;
        public bool ShowCustomText;
        public string filter;
        private bool m_use;
        public string discription;
        public FilterObjectMode mode;
        public List<string> monitor;
        public List<string> filterStrings;
        List<FilterObCa> CallList;
        public GuiInfoMGProperties mgpro;

        public override bool Equals(object obj)
        {
            if (obj is FilterObject == false)
            {
                return false;
            }
            FilterObject other = (FilterObject)obj;

            if (this.filter == null || other.filter == null)
            {
                return false;
            }

            return this.filter.Equals(other.filter);

        }

        

        public void AddFilterObList(List<FilterObject> fi)
        {
            if (CallList != null)
            {
                foreach (FilterObCa c in CallList)
                {
                    c.FilterObList(fi);
                }
            }
        }

        public void AddToCa(FilterObCa ca)
        {
            if (CallList == null)
            {
                CallList = new List<FilterObCa>();
            }

            CallList.Add(ca);
        }

        public bool use
        {
            get
            {
                return m_use;
            }
            set
            {
                m_use = value;
                if (mgpro != null)
                {
                    mgpro.active = value;
                }

                if (CallList != null)
                {
                    foreach (FilterObCa cal in CallList)
                    {
                        cal.Use(m_use, filter);
                    }
                }
            }
        }

    }
}
