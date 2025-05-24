using System;
using System.Collections.Generic;

namespace CsCodeGenerator
{
    public class FileModel
    {
        public FileModel() { }
        public FileModel(string name)
        {
            Name = name;
        }

        public List<string> UsingDirectives { get; set; } = new List<string>();

        public string FirstLine { get; set; }

        public string LastLine { get; set; }

        public string Namespace { get; set; }

        public string Name { get; set; }

        public string Extension { get; set; } = Util.CsExtension;

        public string FullName => Name + "." + Extension;

        public List<EnumModel> Enums { get; set; } = new List<EnumModel>();

        public List<ClassModel> Classes { get; set; } = new List<ClassModel>();

        public void LoadUsingDirectives(List<string> usingDirectives)
        {
            foreach (var usingDirective in usingDirectives)
            {
                if (UsingDirectives.Contains(usingDirective))
                    continue;
                UsingDirectives.Add(usingDirective);
            }
        }

        public override string ToString()
        {
            string result = string.Empty;

            if (!string.IsNullOrWhiteSpace(FirstLine))
            {
                result += FirstLine + Util.NewLine;
            }

            string usingText = UsingDirectives.Count > 0 ? Util.Using + " " : "";
            result += usingText + String.Join(Util.NewLine + usingText, UsingDirectives);
            result += Util.NewLineDouble + Util.Namespace + " " + Namespace;
            result += Util.NewLine + "{";
            result += String.Join(Util.NewLine, Enums);
            result += (Enums.Count > 0 && Classes.Count > 0) ? Util.NewLine : "";
            result += String.Join(Util.NewLine, Classes);
            result += Util.NewLine + "}";
            result += Util.NewLine;

            if (!string.IsNullOrWhiteSpace(LastLine))
            {
                result += LastLine + Util.NewLine;
            }

            return result;
        }
    }
}
