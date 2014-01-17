﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cfglib;

namespace cfgtest
{
    [TestClass]
    public class LoadFromExcelTests
    {
        Repos repos = new Repos();

        [TestMethod]
        public void OpenExcelFile()
        {
            HorizonFile file = repos.HorizonFile("1_14_Horizon.xls");
            int i = file.Load();
            Assert.AreEqual(4150, i, "Line Items not loaded correctly");


        }
    }
}