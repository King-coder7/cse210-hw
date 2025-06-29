public class Reference
{
    private string _book;
    private int _chapter;
    private int _verseStart;
    private int? _verseEnd;

    public Reference(string book, int chapter, int verseStart, int? verseEnd = null)
    {
        _book = book;
        _chapter = chapter;
        _verseStart = verseStart;
        _verseEnd = verseEnd;
    }

    public override string ToString()
    {
        return _verseEnd == null 
            ? $"{_book} {_chapter}:{_verseStart}" 
            : $"{_book} {_chapter}:{_verseStart}-{_verseEnd}";
    }
}
