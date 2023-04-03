using dnGREP.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProgramStudio.DeployCheckToolkits.Web
{
    public class WebSearchCriteria
    {
        public WebSearchCriteria()
        {
            Operation = GrepOperation.Search; //  vm.CurrentGrepOperation;
            UseFileSizeFilter = FileSizeFilter.No; //  vm.UseFileSizeFilter;
            SizeFrom = 0; //  vm.SizeFrom;
            SizeTo = 100; //  vm.SizeTo;
            UseFileDateFilter = FileDateFilter.All; //  vm.UseFileDateFilter;
            TypeOfTimeRangeFilter = FileTimeRange.All; //  vm.TypeOfTimeRangeFilter;
            StartDate = null; //  vm.StartDate;
            EndDate = null; //  vm.EndDate;
            HoursFrom = 0; //  vm.HoursFrom;
            HoursTo = 8; //  vm.HoursTo;
            TypeOfFileSearch = FileSearchType.Asterisk; //  vm.TypeOfFileSearch;
            FilePattern = "*.*"; //  vm.FilePattern;
            FilePatternIgnore = "*.png;*.jpg"; //  vm.FilePatternIgnore;
            UseGitIgnore = true; //  vm.UseGitignore;
            IncludeArchive = true; //  vm.IncludeArchive;
            IncludeBinary = false; //  vm.IncludeBinary;
            IncludeHidden = true; //  vm.IncludeHidden;
            IncludeSubfolder = true; //  vm.IncludeSubfolder;
            MaxSubfolderDepth = -1; //  vm.MaxSubfolderDepth;
            FollowSymlinks = false; //  vm.FollowSymlinks;
            CodePage = -1; //  vm.CodePage;
            TypeOfSearch = SearchType.PlainText; //  vm.TypeOfSearch;
            SearchFor = "qa"; //  vm.SearchFor;
            ReplaceWith = null; //  vm.ReplaceWith;
        }

        public void AddSearchFiles(IEnumerable<string> files)
        {
            SearchInFiles = files;
        }

        public void AddReplaceFiles(IEnumerable<ReplaceDef> files)
        {
            ReplaceFiles = files;
        }

        public GrepOperation Operation { get; set; }
        public FileSizeFilter UseFileSizeFilter { get; set; }
        public int SizeFrom { get; set; }
        public int SizeTo { get; set; }
        public FileDateFilter UseFileDateFilter { get; set; }
        public FileTimeRange TypeOfTimeRangeFilter { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int HoursFrom { get; set; }
        public int HoursTo { get; set; }
        public FileSearchType TypeOfFileSearch { get; set; }
        public string FilePattern { get; set; }
        public string FilePatternIgnore { get; set; }
        public bool UseGitIgnore { get; set; }
        public bool IncludeArchive { get; set; }
        public bool IncludeBinary { get; set; }
        public bool IncludeHidden { get; set; }
        public bool IncludeSubfolder { get; set; }
        public int MaxSubfolderDepth { get; set; }
        public bool FollowSymlinks { get; set; }
        public int CodePage { get; set; }
        public SearchType TypeOfSearch { get; set; }
        public string SearchFor { get; set; }
        public string ReplaceWith { get; set; }

        [XmlIgnore]
        public IEnumerable<string> SearchInFiles { get; private set; }
        [XmlIgnore]
        public IEnumerable<ReplaceDef> ReplaceFiles { get; private set; }

        public static List<WebSearchCriteria> DeserializeFromXML(string xml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<WebSearchCriteria>));
            List<WebSearchCriteria> result = new List<WebSearchCriteria>();

            if (!string.IsNullOrEmpty(xml))
            {
                using (var xmlReader = new StringReader(xml))
                {
                    result = (List<WebSearchCriteria>)xmlSerializer.Deserialize(xmlReader);
                }
            }
            return result;
        }

        public static string SerializeToXML(List<WebSearchCriteria> items)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<WebSearchCriteria>));

            using (var xmlWriter = new StringWriter())
            {
                xmlSerializer.Serialize(xmlWriter, items);
                return xmlWriter.ToString();
            }
        }
    }
}
