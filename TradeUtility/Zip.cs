using CrossModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace TradeUtility
{
    public class Zip
    {
        public static void CreateZipFile(List<ResultItemDetail> itemDetails, string zipFilePath)
        {
            Zip.CreateZipFile(zipFilePath, itemDetails.Select(s => s.ServerFilePath).ToList());
        }

        public static void CreateZipFile(string fileName, IEnumerable<string> files)
        {
            // Create and open a new ZIP file
            var zip = ZipFile.Open(fileName, ZipArchiveMode.Create);

            foreach (var file in files)
            {
                // Add the entry for each file
                zip.CreateEntryFromFile(file, Path.GetFileName(file), CompressionLevel.Optimal);
            }

            // Dispose of the object when we are done
            zip.Dispose();
        }
    }
}
