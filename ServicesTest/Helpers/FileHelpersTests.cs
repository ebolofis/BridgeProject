using Hit.Services.Helpers.Classes.Classes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HitServicesTest   //  <----<<< IMPORTANT: All tests must have that namespace
{
    /// <summary>
    /// Test file helpers
    /// </summary>
    [TestFixture]
    public class FileHelpersTests
    {
        [Test, Order(20)]
        public void EnumerateFiles()
        {
            FileHelpers fh = new FileHelpers();

            List<string> files = fh.EnumerateFiles(@"\\sisifos\ftp\HitServices\Implementations\Singular\FuelExports", "SMexp-*.exp");
            Assert.IsTrue(files.Count==2);
        }


        [Test, Order(30)]
        public void ReadFileLines()
        {
            FileHelpers fh = new FileHelpers();
            List<string> lines = fh.ReadFileLines(@"\\sisifos\ftp\HitServices\Implementations\Singular\FuelExports\SMexp-20180517-1720.exp", ";");
            Assert.IsTrue(lines.Count == 166);
        }


    }
}
