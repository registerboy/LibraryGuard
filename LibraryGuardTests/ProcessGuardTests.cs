using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryGuard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryGuard.Tests
{
    [TestClass()]
    public class ProcessGuardTests
    {
        [TestMethod()]
        public void isRunningTest()
        {
            string fileName = @"C:\Program Files\LibraryGuard\Testing.exe";
            string[] fileNames = { fileName };
            ProcessGuard processGuard = new ProcessGuard(fileNames);
            bool actual = processGuard.isRunning(fileName);
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void runDeadProcessTest()
        {
            string fileName = @"C:\LibraryLock.exe";
            string[] fileNames = { fileName };
            ProcessGuard processGuard = new ProcessGuard(fileNames);
            processGuard.runDeadProcess();
            bool actual = processGuard.isRunning(fileName);
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void getProcessNamesTest()
        {
            string fileName = @"C:\Program Files\LibraryGuard\Testing.exe";
            string[] fileNames = { fileName };

            ProcessGuard pGuard = new ProcessGuard(fileNames);
            string actual = pGuard.getProcessNames(fileNames)[0];
            string expected = "Testing";
            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void getRunningProcessFileNamesTest()
        {
            string fileName = @"C:\Program Files\LibraryGuard\Testing.exe";
            string[] fileNames = { fileName };

            ProcessGuard pGuard = new ProcessGuard(fileNames);
            string actual = pGuard.getRunningProcessFileNames()[0];
            string expected = fileName;
            Assert.AreEqual(expected, actual);

        }
    }
}