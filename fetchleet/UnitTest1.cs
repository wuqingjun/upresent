using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WatiN.Core;
using WatiN.Core.Native.Chrome;

namespace fetchleet
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (var browser = new IE("google.com"))
            {
                browser.TextField(Find.ByName("q")).TypeText("Hello Watin");
            }

        }

        [TestMethod]
        public void LoginLeetCode()
        {
            using (var browser = new IE("https://leetcode.com"))
            {

                var toHtml = new IE("http://hilite.me/");

                browser.Link(Find.ByClass("btn btn-default")).Click();
                browser.TextField(Find.ById("id_login")).TypeText("qjwu9908@hotmail.com");
                browser.TextField(Find.ById("id_password")).TypeText("Test123");
                browser.Button(Find.ByText("Sign In")).Click();
                var table = browser.Table(Find.ById("problemList"));
                var body = table.TableBodies[0];
                foreach (var row in body.OwnTableRows)
                {
                    if ("ac" == row.Elements[1].ClassName)
                    {
                        var cell = row.Elements[3] as TableCell;
                        cell.Links[0].Click();

                        browser.Link(Find.ByText("My Submissions")).Click();
                        browser.WaitForComplete();

                        var tableResult = browser.Table(Find.ById("result_testcases"));
                        var resultBody = tableResult.TableBodies[0];

                        string solutionCode = "";

                        solutionCode = solutionCode.Replace("\\r\\n\\r\\n", "\\r\\n");

                        foreach (var resultRow in resultBody.OwnTableRows)
                        {
                            if (resultRow.Elements[3].Text.Contains("Accepted"))
                            {
                                var acceptedCell = resultRow.Elements[3] as TableCell;
                                acceptedCell.Links[0].Click();

                                var div = browser.Div(Find.ByClass("ace_content"));
                                solutionCode = div.Text;
                                break;
                            }
                        }

                        toHtml.WaitForComplete(5);

                        toHtml.TextField(Find.ByName("code")).TypeText(solutionCode);

                        toHtml.SelectList(Find.ByName("lexer")).SelectByValue("cpp");

                        toHtml.Button(Find.ByValue("Highlight!")).Click();

                        browser.Back();
                    }
                }
            }
        }

    }
}
