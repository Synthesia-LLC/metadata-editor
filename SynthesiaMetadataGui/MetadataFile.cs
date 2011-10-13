using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Xml;

namespace Synthesia
{
    /// <summary>Non-destructive Synthesia metadata XML reader/writer</summary>
    /// <remarks>Custom XML structures in existing metadata files will be preserved as best as possible.</remarks>
    public class MetadataFile
    {
        XDocument m_document;

        /// <summary>
        /// Starts a new, empty metadata XML file
        /// </summary>
        public MetadataFile()
        {
            m_document = new XDocument(new XElement("SynthesiaMetadata", (new XAttribute("Version", "1"))));
        }

        public MetadataFile(Stream input)
        {
            using (var reader = new StreamReader(input))
                m_document = XDocument.Load(reader, LoadOptions.None);

            XElement top = m_document.Root;
            if (top == null || top.Name != "SynthesiaMetadata") throw new InvalidOperationException("Stream does not contain a valid Synthesia metadata file.");
            
            if (top.AttributeOrDefault("Version") != "1") throw new InvalidOperationException("Unknown Synthesia metadata version.  A newer version of this editor may be available.");
        }

        public void Save(Stream output)
        {
            using (StreamWriter writer = new StreamWriter(output))
                m_document.Save(writer, SaveOptions.None);
        }

        public void RemoveSong(string uniqueId)
        {
            if (uniqueId == null) return;

            XElement songs = m_document.Root.Element("Songs");
            if (songs == null) return;

            foreach (XElement s in songs.Elements("Song"))
            {
                if (s.AttributeOrDefault("UniqueId") != uniqueId) continue;

                s.Remove();
                break;
            }
        }

        public void AddSong(XElement songs, SongEntry entry)
        {
            XElement element = (from e in songs.Elements("Song") where e.AttributeOrDefault("UniqueId") == entry.UniqueId select e).FirstOrDefault();
            if (element == null) songs.Add(element = new XElement("Song"));

            element.SetAttributeValue("UniqueId", entry.UniqueId);
            element.SetAttributeValue("Title", entry.Title);
            element.SetAttributeValue("Subtitle", entry.Subtitle);

            element.SetAttributeValue("Composer", entry.Composer);
            element.SetAttributeValue("Arranger", entry.Arranger);
            element.SetAttributeValue("Copyright", entry.Copyright);
            element.SetAttributeValue("License", entry.License);

            element.SetAttributeValue("Rating", entry.Rating);
            element.SetAttributeValue("Difficulty", entry.Difficulty);

            element.SetAttributeValue("FingerHints", entry.FingerHints);
            element.SetAttributeValue("Tags", string.Join(";", entry.Tags.ToArray()));
        }

        public void AddSong(SongEntry entry)
        {
            XElement songs = m_document.Root.Element("Songs");
            if (songs == null) m_document.Root.Add(songs = new XElement("Songs"));

            AddSong(songs, entry);
        }

        public IEnumerable<SongEntry> Songs
        {
            get
            {
                XElement songs = m_document.Root.Element("Songs");
                if (songs == null) yield break;

                foreach (XElement s in songs.Elements("Song"))
                {
                    SongEntry entry = new SongEntry();

                    entry.UniqueId = s.AttributeOrDefault("UniqueId");
                    entry.Title = s.AttributeOrDefault("Title");
                    entry.Subtitle = s.AttributeOrDefault("Subtitle");

                    entry.Composer = s.AttributeOrDefault("Composer");
                    entry.Arranger = s.AttributeOrDefault("Arranger");
                    entry.Copyright = s.AttributeOrDefault("Copyright");
                    entry.License = s.AttributeOrDefault("License");

                    entry.FingerHints = s.AttributeOrDefault("FingerHints");

                    int rating;
                    if (int.TryParse(s.AttributeOrDefault("Rating"), out rating)) entry.Rating = rating;

                    int difficulty;
                    if (int.TryParse(s.AttributeOrDefault("Difficulty"), out difficulty)) entry.Difficulty = difficulty;

                    string tags = s.AttributeOrDefault("Tags");
                    if (tags != null)
                    {
                        entry.ClearAllTags();
                        foreach (var t in tags.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
                            entry.AddTag(t);
                    }

                    yield return entry;
                }
            }

            set
            {
                XElement songs = m_document.Root.Element("Songs");
                if (songs == null)  m_document.Add(songs = new XElement("Songs"));

                foreach (SongEntry entry in value)
                    AddSong(songs, entry);
            }

        }

    }
}
