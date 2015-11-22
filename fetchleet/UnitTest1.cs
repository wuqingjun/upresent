using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WatiN.Core;
using WatiN.Core.Native.Chrome;

namespace fetchleet
{
    [TestClass]
    public class UnitTest1
    {
        private const string Directory = @"C:\Users\ek2zqun\Source\Repos\upresent\fetchleet\";

        [TestMethod]
        public void TestMethod1()
        {
            using (var browser = new IE("google.com"))
            {
                browser.TextField(Find.ByName("q")).TypeText("Hello Watin");
            }
        }

        [TestMethod]
        public void FetchLeetCode()
        {
            using (var browser = new IE("https://leetcode.com"))
            {

                using (StreamReader sr = new StreamReader(Directory + @"template\solution.html"))
                {
                    string solutionTemplate = sr.ReadToEnd();

                    using (StreamReader questionReader = new StreamReader(Directory + @"template\question.html"))
                    {
                        string questionTemplate = questionReader.ReadToEnd();

                        var toHtml = new IE("http://hilite.me/");

                        browser.Link(Find.ByClass("btn btn-default")).Click();
                        browser.TextField(Find.ById("id_login")).SetAttributeValue("value", "qjwu9908@hotmail.com"); //TypeText("qjwu9908@hotmail.com");
                        browser.TextField(Find.ById("id_password")).SetAttributeValue("value", "Test123"); //.TypeText("Test123");
                        browser.Button(Find.ByText("Sign In")).Click();

                        for (int i = 115; i < 300; ++i)
                        {
                            var table = browser.Table(Find.ById("problemList"));
                            var body = table.TableBodies[0];
                            foreach (var row in body.OwnTableRows)
                            {
                                if (row.Elements != null && row.Elements.Count > 0 && "ac" == row.Elements[1].ClassName)
                                {
                                    var cell2 = row.Elements[2] as TableCell;
                                    string questionIndex = cell2.Text;

                                    if (Int32.Parse(questionIndex) == i)
                                    {
                                        var cell = row.Elements[3] as TableCell;
                                        string questionName = cell.Text;
                                        cell.Links[0].Click();

                                        var questionDiv = browser.Div(Find.ByClass("question-content"));

                                        string questionContent = "";

                                        for (int j = 0; j < questionDiv.Paras.Count; ++j)
                                        {
                                            if (!string.IsNullOrEmpty(questionDiv.Paras[j].Text) &&
                                                questionDiv.Paras[j].Text.StartsWith("<b>Notes:</b>"))
                                            {
                                                break;
                                            }
                                            questionContent += "<p>" + questionDiv.Paras[j].Text + "</p>";
                                        }

                                        //browser.Links.SelectMany(l => l.Text == "My Submissions");
                                        browser.Links.Filter(l => l.Text == "My Submissions").Where(l => l.Url != "https://leetcode.com/submissions/").First().Click();
                                        //browser.Link(Find.ByText("My Submissions") ).  .Click();
                                        browser.WaitForComplete();

                                        var tableResult = browser.Table(Find.ById("result_testcases"));
                                        var resultBody = tableResult.TableBodies[0];

                                        string solutionCode = "";

                                        foreach (var resultRow in resultBody.OwnTableRows)
                                        {
                                            if (resultRow.Elements[3].Text.Contains("Accepted"))
                                            {
                                                var acceptedCell = resultRow.Elements[3] as TableCell;
                                                acceptedCell.Links[0].Click();

                                                var div = browser.Div(Find.ByClass("ace_content"));
                                                solutionCode = div.Text;
                                                solutionCode = solutionCode.Replace("\r\n\r\n", "\r\n");
                                                break;
                                            }
                                        }

                                        toHtml.TextField(Find.ById("divstyles")).SetAttributeValue("value", "padding:.1em .3em;");

                                        toHtml.TextField(Find.ByName("code")).SetAttributeValue("value", solutionCode);

                                        toHtml.SelectList(Find.ByName("lexer")).SelectByValue("cpp");

                                        toHtml.Button(Find.ByValue("Highlight!")).Click();

                                        toHtml.WaitForComplete(3);

                                        string questionhtml = questionTemplate.Replace("<%QuestionName%>", questionName).Replace("<%QuestionContent%>", questionContent);
                                        string solutonhtml = solutionTemplate.Replace("<%QuestionName%>", questionName).Replace("<%SolutionTemplate%>", toHtml.TextField(Find.ById("html")).Text);
                                        solutonhtml = solutonhtml.Replace("margin: 0; line-height: 125%", "margin: 0; line-height: 125%; white-space: pre-wrap;");

                                        string solutionFilename = Directory + "output\\" + questionIndex + ".solution.html";
                                        string questionFileName = Directory + "output\\" + questionIndex + ".question.html";

                                        System.IO.File.WriteAllText(solutionFilename, solutonhtml);
                                        System.IO.File.WriteAllText(questionFileName, questionhtml);

                                        browser.GoTo("https://leetcode.com/problemset/algorithms/");
                                    }
                                }
                            }     
                        
                        }

                    }
                }
            }
        }
    }
}
