using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WatiN.Core;
using WatiN;

namespace GrabLeet
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

                        foreach(var resultRow in resultBody.OwnTableRows)
                        {
                            if (resultRow.Elements[3].Text == "Accepted")
                            {
                                
                            }
                        }

                        browser.Back();
                    }
                }
            }

        }
    }
}
