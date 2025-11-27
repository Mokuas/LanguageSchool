using System;
using System.Collections.Generic;

namespace LanguageSchoolBYT.Models
{
    public class Material
    {
        private static List<Material> _extent = new();
        public static IReadOnlyList<Material> Extent => _extent.AsReadOnly();

        private static void AddToExtent(Material m) => _extent.Add(m);

        // ATTRIBUTES
        private string _title;
        private string _type;
        private string _editionNote;

        public string Title
        {
            get => _title;
            set => _title = value;
        }

        public string Type
        {
            get => _type;
            set => _type = value;
        }

        public string EditionNote
        {
            get => _editionNote;
            set => _editionNote = value;
        }

        // CONSTRUCTORS
        public Material() => AddToExtent(this);

        public Material(string title, string type, string editionNote)
        {
            Title = title;
            Type = type;
            EditionNote = editionNote;

            AddToExtent(this);
        }
    }
}
