using System;
using System.Collections;
using System.ComponentModel;
using Aspose.Words;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aspose.Words.Reporting;
using System.IO;
using System.Configuration;
using System.Diagnostics;

namespace BKLeadsOnline.Domain
{
    public class TOS
    {
        public Dealer TOSDealer { get; set; }
        public string FileName { get; set; }

        public byte[] getTOSDocument()
        {

            try
            {
                FileStream _fileStream = new FileStream(FileName, FileMode.Open);

                try
                {


                    // Load the stream into a new document object.
                    Aspose.Words.Document _asposeDoc = new Document(_fileStream);
                    _asposeDoc.MailMerge.CleanupOptions = MailMergeCleanupOptions.RemoveUnusedFields;

                    _asposeDoc.MailMerge.Execute(
                        new string[] { "DealerName", "PriceString", "AcknowledgedOnTOSDateString", "AcknowledgedByTOSName" },
                        new object[] { TOSDealer.DealerName, TOSDealer.PriceString, TOSDealer.AcknowledgedOnTOSDateString, TOSDealer.AcknowledgedByTOSName });

                    //send back a converted PDF
                    MemoryStream outStream = new MemoryStream();
                    _asposeDoc.Save(outStream, Aspose.Words.SaveFormat.Pdf);
                    return outStream.ToArray();
                }
                catch
                {
                    _fileStream.Close();
                    return null;
                }
                finally
                {
                    _fileStream.Close();
                }
            
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
