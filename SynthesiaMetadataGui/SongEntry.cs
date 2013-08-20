using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Synthesia
{
    public class SongEntry
    {
        public string UniqueId { get; set; }

        public string Title { get; set; }
        public string Subtitle { get; set; }

        public string BackgroundImage { get; set; }

        public string Composer { get; set; }
        public string Arranger { get; set; }
        public string Copyright { get; set; }
        public string License { get; set; }
        public string MadeFamousBy { get; set; }

        public string FingerHints { get; set; }
        public string HandParts { get; set; }

        private int? m_rating;
        public int? Rating
        {
            get { return m_rating; }
            set { m_rating = (value.HasValue) ? Math.Max(0, Math.Min(100, value.Value)) : value; }
        }

        private int? m_difficulty;
        public int? Difficulty
        {
            get { return m_difficulty; }
            set { m_difficulty = (value.HasValue) ? Math.Max(0, Math.Min(100, value.Value)) : value; }
        }

        /// <summary>
        /// Returns a copy of the list.  Use AddTag() and RemoveTag() to make changes.
        /// </summary>
        public List<string> Tags { get { return m_tags.ToList(); } }
        private List<string> m_tags = new List<string>();

        public void ClearAllTags() { m_tags.Clear(); }
        public void RemoveTag(string tag) { m_tags.RemoveAll(t => t.ToLower() == tag.ToLower()); }

        public void AddTag(string tag)
        {
            // Disallow (case-insensitive) duplicates
            if ((from t in m_tags where t.ToLower() == tag.ToLower() select t).Any()) return;

            if (tag.Contains(';')) throw new InvalidOperationException("Tags cannot contain semi-colons.");

            m_tags.Add(tag);
        }

        /// <summary>
        /// Returns a copy of the dictionary.  Use AddBookmark() and RemoveBookmark() to make changes.
        /// </summary>
        public Dictionary<int, string> Bookmarks { get { return m_bookmarks.ToDictionary(p => p.Key, p => p.Value); } }
        private Dictionary<int, string> m_bookmarks = new Dictionary<int, string>();

        public void ClearAllBookmarks() { m_bookmarks.Clear(); }
        public void RemoveBookmark(int measure) { m_bookmarks.Remove(measure); }

        public void AddBookmark(int measure, string description = "")
        {
            string d = description ?? "";

            if (measure < 1) throw new InvalidOperationException("Bookmarks must have a positive integer measure number.");
            if (d.Contains(';')) throw new InvalidOperationException("Bookmark descriptions cannot contain semi-colons.");

            // NOTE: We overwrite duplicates silently, so no need to check for them
            m_bookmarks[measure] = d;
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Title)) return "(Unknown Song)";

            if (string.IsNullOrEmpty(Subtitle)) return Title;
            return string.Format("{0}, {1}", Title, Subtitle);
        }
    }
}
