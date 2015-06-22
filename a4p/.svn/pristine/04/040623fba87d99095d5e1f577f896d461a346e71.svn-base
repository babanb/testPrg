using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;
using System.Web;
using ADOPets.Web.ViewModels.SMO;
using ADOPets.Web.Resources;

namespace ADOPets.Web.Common.Helpers
{
    public class PDFHelper
    {
        //Generate PDF Template for SMO
        public static bool GeneratePDF(string sFilePDF, DetailsViewModel model)
        {
            bool bRet = false;
            try
            {
                if (System.IO.File.Exists(sFilePDF))
                {
                    System.IO.File.Delete(sFilePDF);
                }

                // step 1: creation of a document-object
                Document document = new Document(PageSize.A4);
                document.SetMargins(1F, 1F, 50F, 50F);
                try
                {
                    // step 2:
                    // we create a writer that listens to the document
                    // and directs a PDF-stream to a file
                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(sFilePDF, FileMode.Create));

                    // step 3: we open the document
                    document.Open();

                    // step 4: we create a table and add it to the document
                    #region PDF Report and Logo
                    Table table = new Table(1);

                    #region PdfPTable settings
                    table.BorderWidth = 0;
                    table.Padding = 2;
                    table.TableFitsPage = true;
                    table.Spacing = 2;
                    table.Alignment = Element.ALIGN_CENTER;
                    #endregion

                    BaseFont f_cn = BaseFont.CreateFont("c:\\windows\\fonts\\calibri.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

                    Font calibriBoldGray = new Font(f_cn, 36, Font.BOLD, new Color(127, 127, 127));
                    Font calibriNormalGray = new Font(f_cn, 36, Font.NORMAL, new Color(127, 127, 127));

                    #region Header info pcell


                    Cell cell = new Cell();

                    cell = new Cell();
                    cell.BorderWidth = 0;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.AddElement(new Phrase(Wording.Smo_Add_Confidential, calibriBoldGray));
                    table.AddCell(cell);

                    cell = new Cell();
                    cell.BorderWidth = 0;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new Cell();
                    cell.BorderWidth = 0;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.AddElement(new Phrase(Wording.Smo_Add_SecondMedicalOpinionReport, calibriBoldGray));
                    table.AddCell(cell);

                    cell = new Cell();
                    cell.BorderWidth = 0;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.AddElement(new Phrase(Wording.Smo_Add_from + ":", calibriBoldGray));
                    table.AddCell(cell);

                    cell = new Cell();
                    cell.BorderWidth = 0;
                    cell.Add(new Chunk("\n"));
                    table.AddCell(cell);

                    cell = new Cell();
                    cell.BorderWidth = 0;
                    cell.Add(new Chunk("\n"));
                    table.AddCell(cell);

                    cell = new Cell();
                    cell.Add(new Chunk("\n"));
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    #endregion

                    #region Activ4Pets Logo
                    var logo = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/Content/images/reportLogo.png"));
                    cell = new Cell(logo);
                    cell.BorderWidth = 0;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    table.AddCell(cell);

                    cell = new Cell();
                    cell.Add(new Chunk("\n"));
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    #endregion

                    #region Date
                    cell = new Cell();
                    cell.BorderWidth = 0;
                    cell.Add(new Chunk("\n"));
                    table.AddCell(cell);

                    cell = new Cell();
                    cell.BorderWidth = 0;
                    cell.Add(new Chunk("\n"));
                    table.AddCell(cell);

                    Font calibribold = new Font(f_cn, 12, Font.BOLD);
                    Font calibrinormal = new Font(f_cn, 12, Font.NORMAL);
                    cell = new Cell();
                    cell.BorderWidth = 0;
                    Phrase phrase = new Phrase();
                    phrase.Add(
                        new Chunk(Wording.Smo_Add_Date + ": ", calibribold)
                    );
                    phrase.Add(new Chunk(DateTime.Now.ToShortDateString(), calibrinormal));
                    cell.AddElement(phrase);
                    table.AddCell(cell);



                    #endregion

                    #region SMO Info
                    cell = new Cell();
                    cell.BorderWidth = 0;
                    cell.AddElement(new Phrase(Wording.Smo_Add_SMOID + ": " + model.SMOId, calibribold));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cell);

                    cell = new Cell();
                    cell.BorderWidth = 0;
                    cell.AddElement(new Phrase(Wording.Smo_Add_Dear + " " + model.OwnerName, calibrinormal));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cell);

                    cell = new Cell();
                    cell.BorderWidth = 0;
                    cell.AddElement(new Phrase(Wording.Smo_Add_ReportContent, calibrinormal));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cell);


                    #endregion

                    #region Pet Info
                    cell = new Cell();
                    cell.BorderWidth = 0;
                    cell.AddElement(new Phrase(Wording.Smo_Add_PetInformation + ":", calibribold));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cell);

                    cell = new Cell();
                    cell.BorderWidth = 0;
                    cell.AddElement(new Phrase(ADOPets.Web.Resources.Wording.Pet_Add_PetName + ":" + model.SMOREquest.Pet.Name, calibrinormal));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cell);

                    cell = new Cell();
                    cell.BorderWidth = 0;
                    var petTypeVar = Resources.Enums.ResourceManager.GetString("PetTypeEnum_" + model.SMOREquest.Pet.PetTypeId);

                    cell.AddElement(new Phrase(ADOPets.Web.Resources.Wording.Pet_Add_PetType + ":" + petTypeVar, calibrinormal));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cell);

                    cell = new Cell();
                    cell.BorderWidth = 0;
                    cell.AddElement(new Phrase(ADOPets.Web.Resources.Wording.Pet_Add_Breed + ":" + model.SMOREquest.Pet.CustomBreed, calibrinormal));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cell);

                    cell = new Cell();
                    cell.BorderWidth = 0;
                    cell.AddElement(new Phrase(ADOPets.Web.Resources.Wording.Pet_Add_BirthDate + ":" + model.SMOREquest.Pet.BirthDate.ToShortDateString(), calibrinormal));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cell);
                    #endregion

                    #region Owner Info
                    cell = new Cell();
                    cell.BorderWidth = 0;
                    cell.AddElement(new Phrase(Wording.Smo_Add_OwnerInformation + ":", calibribold));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cell);

                    cell = new Cell();
                    cell.BorderWidth = 0;
                    cell.AddElement(new Phrase(Wording.Smo_Add_FirstName + ":" + model.SMOREquest.User.FirstName, calibrinormal));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cell);

                    cell = new Cell();
                    cell.BorderWidth = 0;
                    cell.AddElement(new Phrase(Wording.Smo_Add_LastName + ":" + model.SMOREquest.User.LastName, calibrinormal));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cell);

                    cell = new Cell();
                    cell.BorderWidth = 0;
                    cell.AddElement(new Phrase(Wording.Smo_Add_Email + ":" + model.SMOREquest.User.Email, calibrinormal));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cell);

                    #endregion

                    #region Request Info
                    cell = new Cell();
                    cell.BorderWidth = 0;
                    cell.AddElement(new Phrase(Wording.Smo_Add_ReasonForRequest + ":", calibribold));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cell);

                    cell = new Cell();
                    cell.BorderWidth = 0;
                    cell.AddElement(new Phrase(model.SMOREquest.RequestReason, calibrinormal));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cell);
                    #endregion

                    #region Medical Info
                    cell = new Cell();
                    cell.BorderWidth = 0;
                    cell.AddElement(new Phrase(Wording.Smo_Add_MedicalHistory + ":", calibribold));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cell);

                    cell = new Cell();
                    cell.BorderWidth = 0;
                    cell.AddElement(new Phrase(model.SMOREquest.MedicalHistoryComment, calibrinormal));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cell);
                    #endregion

                    #region SMOConclusion
                    cell = new Cell();
                    cell.BorderWidth = 0;
                    cell.AddElement(new Phrase(Wording.Smo_Add_SMOConclusions + ":", calibribold));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cell);

                    cell = new Cell();
                    cell.BorderWidth = 0;
                    cell.AddElement(new Phrase(model.SMOREquest.VetComment, calibrinormal));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cell);
                    #endregion

                    #region Additional Info
                    cell = new Cell();
                    cell.BorderWidth = 0;
                    cell.AddElement(new Phrase(Wording.Smo_Add_AdditionalInformation + ":", calibribold));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cell);

                    cell = new Cell();
                    cell.BorderWidth = 0;
                    cell.AddElement(new Phrase(model.SMOREquest.AdditionalInformation, calibrinormal));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cell);
                    #endregion

                    #region Conclusion
                    // commented by nutan as per Marie's email
                    //cell = new Cell();
                    //cell.BorderWidth = 0;
                    //cell.AddElement(new Phrase(Wording.Smo_Add_Conclusion + ":", calibribold));
                    //cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    ////cell.BorderColorBottom = new Color(228, 239, 250);
                    ////cell.BorderWidthBottom = 1f;
                    //table.AddCell(cell);
                    #endregion

                    #region ConclusionText
                    cell = new Cell();
                    cell.BorderWidth = 0;
                    cell.AddElement(new Phrase(Wording.Smo_Add_ConclusionText, calibrinormal));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    // cell.BorderColorBottom = new Color(228, 239, 250);
                    // cell.BorderWidthBottom = 1f;
                    table.AddCell(cell);
                    #endregion

                    #region Sincerely
                    cell = new Cell();
                    cell.BorderWidth = 0;
                    cell.AddElement(new Phrase(Wording.Smo_Add_Sincerely + ",", calibrinormal));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    //cell.BorderColorBottom = new Color(228, 239, 250);
                    //cell.BorderWidthBottom = 1f;
                    table.AddCell(cell);
                    #endregion

                    #region VD Info

                    cell = new Cell();
                    cell.BorderWidth = 0;
                    Phrase phrasevet = new Phrase();
                    phrasevet.Add(
                        new Chunk(model.VetName, calibribold)
                    );
                    // phrasevet.Add(new Chunk(model.VetBio, calibrinormal)); commented by nutan as per maries email
                    cell.AddElement(phrasevet);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cell);

                    #endregion
                    foreach (var item in model.lstExpertRel)
                    {
                        #region VE Info

                        cell = new Cell();
                        cell.BorderWidth = 0;
                        phrasevet = new Phrase();
                        if (item.ExpertResponse != null)
                        {
                            if (item.User != null)
                            {
                                phrasevet.Add(
                                    new Chunk(item.User.FirstName + " " + item.User.LastName + "-", calibribold)
                                );
                            }
                            if (item.expertBioData != null)
                            {
                                phrasevet.Add(new Chunk(item.expertBioData.Information, calibrinormal));
                            }
                            cell.AddElement(phrasevet);

                            table.AddCell(cell);
                        }
                        #endregion
                    }

                    cell = new Cell();
                    cell.BorderWidth = 0;
                    cell.Add(new Chunk("\n"));
                    table.AddCell(cell);

                    cell = new Cell();
                    cell.BorderWidth = 0;
                    cell.Add(new Chunk("\n"));
                    table.AddCell(cell);

                    cell = new Cell();
                    cell.BorderWidth = 0;
                    cell.Add(new Chunk("\n"));
                    table.AddCell(cell);

                    #region phone and email
                    Font calibriboldgrey = new Font(f_cn, 12, Font.BOLD, new Color(127, 127, 127));
                    cell = new Cell();
                    cell.BorderWidth = 0;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.AddElement(new Phrase(Wording.Smo_Add_ActivePhone, calibriboldgrey));
                    table.AddCell(cell);

                    cell = new Cell();
                    cell.BorderWidth = 0;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.AddElement(new Phrase(Wording.Smo_Add_ActiveEmail, calibriboldgrey));
                    table.AddCell(cell);
                    #endregion



                    document.Add(table);
                    #endregion


                    bRet = true;
                }
                catch (DocumentException de)
                {
                    document.Close();
                    throw de;
                }
                catch (IOException ioe)
                {
                    document.Close();
                    throw ioe;
                }

                // step 5: we close the document
                document.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRet;
        }
    }
}