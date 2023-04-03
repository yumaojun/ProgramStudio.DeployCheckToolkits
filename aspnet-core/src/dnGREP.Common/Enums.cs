using System;

namespace dnGREP.Common
{
    public enum SearchType
    {
        PlainText,
        Regex,
        XPath,
        Soundex,
        Hex
    }

    public enum FileSearchType
    {
        Asterisk,
        Regex,
        Everything
    }

    public enum FileSizeFilter
    {
        None,
        Yes,
        No
    }

    [Flags]
    public enum GrepSearchOption
    {
        None = 0,
        CaseSensitive = 1,
        Multiline = 2,
        SingleLine = 4,
        WholeWord = 8,
        StopAfterFirstMatch = 16,
        BooleanOperators = 32
    }

    public enum GrepOperation
    {
        Search,
        SearchInResults,
        Replace,
        None
    }

    public enum FileDateFilter
    {
        None = 0,
        All = 0,
        Modified,
        Created
    }

    public enum FileTimeRange
    {
        None = 0,
        All = 0,
        Dates,
        Hours
    }

    public enum OverwriteFile
    {
        Yes,
        No,
        Prompt
    }

    public enum SortType
    {
        FileNameOnly,
        FileTypeAndName,
        FileNameDepthFirst,
        FileNameBreadthFirst,
        Size,
        Date,
        MatchCount,
    }

    public enum ReportMode 
    { 
        FullLine, 
        Matches,
        Groups
    }

    public enum UniqueScope
    { 
        PerFile,
        Global
    }

    public enum PdfNumberType
    { 
        LineNumber,
        PageNumber
    }

}
