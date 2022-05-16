using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;

namespace DiplomDokumentooborot.Forms.Staff
{
    internal class WordHelper
    {
        private FileInfo _fileInfo;
        public WordHelper(string FileName)
        {
            
            if (File.Exists(FileName))
            {
                _fileInfo = new FileInfo(FileName);
            }
            else
            {
                throw new ArgumentException("Файл не найден");
            }
        }

        internal void Process(Dictionary<string, string> items)
        {
            Word.Application app = null;

            
            try
            {
                app = new Word.Application();
                object file = _fileInfo.FullName;
                object missing = Type.Missing;
                app.Documents.Open(file);

                foreach (var item in items)
                {
                    Word.Find find = app.Selection.Find;
                    find.Text = item.Key;
                    find.Replacement.Text = item.Value;

                    Object wrap = Word.WdFindWrap.wdFindContinue;
                    Object replace = Word.WdReplace.wdReplaceAll;

                    find.Execute(FindText: Type.Missing,
                        MatchCase: false,
                        MatchWholeWord: false,
                        MatchWildcards: false,
                        MatchSoundsLike: missing,
                        MatchAllWordForms: false,
                        Forward: true,
                        Wrap: wrap,
                        Format: false,
                        ReplaceWith: missing, Replace: replace);
                }
                object newFileName = Path.Combine(_fileInfo.DirectoryName, DateTime.Now.ToString("yyyyMMdd HHmmss") + _fileInfo.Name);
                app.ActiveDocument.SaveAs2(newFileName);
                
                
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
