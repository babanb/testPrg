using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;
using System.Web;
using ADOPets.Web.ViewModels.Econsultation;
using ADOPets.Web.Resources;

namespace ADOPets.Web.Common.Helpers
{
    public class ECPDFHelper
    {
        //Generate PDF Template for Econsultation Rx Form
        public static bool GeneratePDF(string sFilePDF, SubmitReportModel model)
        {
            bool bRet = false;
            try
            {
                //string path = "";
                string filename1 = HttpContext.Current.Server.MapPath("~/Content/PDF/Summary_Template_new.pdf");

                string filename2 = sFilePDF;

                if (System.IO.File.Exists(filename2))
                {
                    System.IO.File.Delete(filename2);
                }

                //create PdfReader object to read from the existing document
                PdfReader reader = new PdfReader(filename1);
                //select three pages from the original document
                reader.SelectPages("1-1");

                //create PdfStamper object to write to get the pages from reader 
                PdfStamper stamper = new PdfStamper(reader, new FileStream(filename2, FileMode.Create));
                try
                {
                    // PdfContentByte from stamper to add content to the pages over the original content
                    PdfContentByte pbover = stamper.GetOverContent(1);
                                     
                    //add content to the page using ColumnText
                    //ParamsPName = "x=300; y=612; width=160; alignment=left; size=14; color=&H585656"
                    ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(model.ActionBeginDate.ToString()), 330, 780, 0);

                    string durationsText = model.Durations.Value.ToString() + " Minutes";

                    ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(durationsText), 260, 760, 0);

                    ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(model.EcId.ToString()), 240, 740, 0);

                    ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(model.OwnerFirstName), 180, 655, 0);
                    
                    ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(model.OwnerLastName), 180, 630, 0);

                    ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(model.OwnerEmail), 180, 605, 0);

                    ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(model.PetName), 180, 510, 0);
                    string PetType  = EnumHelper.GetResourceValueForEnumValue(model.PetType);
                    ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(PetType), 180, 487, 0);
                    //ParamsPDOB = "x=488; y=612; width=160; alignment=left; size=14; color=&H585656"
                    //add content to the page using ColumnText
                    ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(model.PetBreadType), 180, 470, 0);
                    // PdfContentByte from stamper to add content to the pages under the original content

                    //ParamsPDate = "x=130; y=554; width=160; alignment=left; size=14; color=&H585656"
                    //add content to the page using ColumnText
                    ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(model.PetDOB.Value.ToShortDateString()), 180, 440, 0);

                    //ParamsPeriod = "x=385; y=554; width=160; alignment=left; size=14; color=&H585656"


                    //ParamsDiag = "x=50; y=475; width=500; alignment=left; size=14; color=&H585656"                    
                    var TextArr = model.Diagnosis.ToString().Split('\n');
                    int x = 50;
                    int y = 362;
                    for (int i = 0; i < TextArr.Length && i < 7; i++)
                    {
                        ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(TextArr[i]), x, y, 0);
                        y = y - 15;
                    }

                    //ParamsTreatment = "x=50; y=270; width=500; alignment=left; size=14; color=&H585656"
                    var TextArr1 = model.Treatment.ToString().Split('\n');
                    int x1 = 50;
                    int y1 = 210;
                    for (int i = 0; i < TextArr.Length && i < 9; i++)
                    {
                        ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(TextArr1[i]), x1, y1, 0);
                        y1 = y1 - 15;
                    }

                    string VetName = model.VetFirstName + " " + model.VetLastName;
                    //ParamsDName = "x=36; y=80; width=200; alignment=left; size=14; color=&H585656"
                    ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(VetName), 30, 28, 0);

                    // PdfContentByte from stamper to add content to the pages under the original content
                    PdfContentByte pbunder = stamper.GetUnderContent(1);

                    //close the stamper
                    stamper.Close();

                    bRet = true;
                }
                catch (DocumentException de)
                {
                    stamper.Close();
                    throw de;
                }
                catch (IOException ioe)
                {
                    stamper.Close();
                    throw ioe;
                }

                // step 5: we close the document
                stamper.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRet;
        }
    }
}