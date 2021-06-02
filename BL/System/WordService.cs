using DAL;
using DAL.Dto;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using word = Microsoft.Office.Interop.Word;

namespace BL
{
    public class WordService
    {
        public string Message { get; private set; }
        public void GenerateWord(OrderDto orderDto, ClientDto clientDto, ServiceDto serviceDto, string path)
        {
            Dictionary<string, string> stubs = new Dictionary<string, string>();
            Dictionary<string, string[]> stubsColl = new Dictionary<string, string[]>();


            stubs.Add("[Номер_договора]", orderDto.Id.ToString());
            stubs.Add("[День]", orderDto.CreationDate.ToString("dd"));
            stubs.Add("[Месяц]", orderDto.CreationDate.ToString("MMMM"));
            stubs.Add("[Год]", orderDto.CreationDate.ToString("yyyy"));
            stubs.Add("[организация]", clientDto.CompanyName);
            stubs.Add("[лицо]", clientDto.Name);
            stubs.Add("[Общая_стоимость]", orderDto.FullCost.ToString());


            ShowDocument(stubs, path);

        }
        
        public void ShowWord()
        {
            if(_word != null)
                _word.Visible = true;
        }

        word.Application _word;

        private bool ShowDocument(Dictionary<string, string> stubs, string path, Dictionary<string, string[]> stubsCollection = null)
        {
            _word = new word.Application();
            try
            {
                _word.Visible = false;


                var newDoc = _word.Documents.Add();
                newDoc.Content.InsertFile(path);
                //newDoc.Content.Font.Name = "Times New Roman";
                //Console.WriteLine(newDoc.Content.PageSetup.LeftMargin);
                //newDoc.Content.PageSetup.LeftMargin = 56.7f;
                //newDoc.Content.PageSetup.RightMargin = 56.7f;

                foreach (var stub in stubs)
                {
                    ReplaceWordStub(stub.Key, stub.Value, newDoc);
                }

                if (stubsCollection != null)
                {
                    foreach (var stub in stubsCollection)
                    {
                        for (int i = 0; i < stub.Value.Length; i++)
                        {
                            StringBuilder sb = new StringBuilder();
                            if (i < stub.Value.Length - 1)
                            {
                                sb.AppendLine(stub.Value[i]);
                                sb.Append($"{stub.Key}");
                            }
                            else
                            {
                                sb.Append(stub.Value[i]);
                            }

                            ReplaceWordStub(stub.Key, sb.ToString(), newDoc);
                            sb.Clear();
                        }
                    }
                }

                _word.Visible = true;
            }
            catch (Exception ex)
            {
                _word.Quit();
                Message = ex.Message;
                return false;
            }
            return true;
        }
        private void ReplaceWordStub(string stub, string value, word.Document document)
        {
            var range = document.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stub, ReplaceWith: value);
        }
    }
}
