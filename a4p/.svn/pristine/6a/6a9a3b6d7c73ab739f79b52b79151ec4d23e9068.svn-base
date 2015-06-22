using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;
using System.Web;
using ADOPets.Web.ViewModels.Pet;
using ADOPets.Web.Resources;
using System.Net;
using System.Collections.Generic;
using iTextSharp.text.html.simpleparser;

namespace ADOPets.Web.Common.Helpers
{
    public class PetSummaryPdfHelper
    {
        public static bool GeneratePDF(string sFilePDF, LoadReportModel model)
        {
            bool bRet = false;
            try
            {
                string filename2 = sFilePDF;

                if (System.IO.File.Exists(filename2))
                {
                    System.IO.File.Delete(filename2);
                }

                try
                {
                    System.IO.FileStream fs = new FileStream(filename2, FileMode.Create);

                    Document document = new Document(PageSize.A4, 25, 25, 30, 30);

                    PdfWriter writer = PdfWriter.GetInstance(document, fs);

                    // Open the document to enable you to write to the document

                    PdfPTable table = new PdfPTable(4);
                    table.TotalWidth = 400f;
                    //fix the absolute width of the table
                    table.LockedWidth = true;
 
                    //relative col widths in proportions - 1/3 and 2/3
                    float[] widths = new float[] { 6f, 6f, 6f, 6f };
                    table.SetWidths(widths);
                    table.HorizontalAlignment = 1;
                    //leave a gap before and after the table
                    table.SpacingBefore = 20f;
                    table.SpacingAfter = 30f;
                    var petTypeVar = Resources.Enums.ResourceManager.GetString("PetTypeEnum_" + model.PetType);
                    string PetName = Wording.Pet_Add_PetName + ":" + model.PetName + "                           " + Wording.Pet_Add_PetType + ":" + petTypeVar;
                    PdfPCell cell = new PdfPCell(new Phrase(Wording.Pet_Add_PetDetail));
                    cell.Colspan = 4;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    cell.VerticalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    table.AddCell(Wording.Pet_Card_Breed);
                    if (!string.IsNullOrEmpty(model.Breed))
                    {
                        table.AddCell(model.Breed.ToString());
                    }
                    else
                    {
                        table.AddCell("");
                    }
                    table.AddCell(Wording.Pet_Card_BirthDate);
                    if (!string.IsNullOrEmpty(model.BirthDate.ToString()))
                    {
                        table.AddCell(model.BirthDate.ToShortDateString());
                    }
                    else
                    {
                        table.AddCell("");
                    }
                    table.AddCell(Wording.Pet_Card_SecondaryBreed);
                    if (!string.IsNullOrEmpty(model.SecondaryBreed))
                    {
                        table.AddCell(model.SecondaryBreed.ToString());
                    }
                    else
                    {
                        table.AddCell("");
                    }
                    table.AddCell(Wording.Pet_Card_PlaceOfBirth);
                    if (!string.IsNullOrEmpty(model.PlaceOfBirth))
                    {
                        table.AddCell(model.PlaceOfBirth.ToString());
                    }
                    else
                    {
                        table.AddCell("");
                    }

                    table.AddCell(Wording.Pet_Card_Pedigree);
                    if (!string.IsNullOrEmpty(model.Pedigree))
                    {
                        table.AddCell(model.Pedigree.ToString());
                    }
                    else
                    {
                        table.AddCell("");
                    }
                    table.AddCell(Wording.Pet_Card_CountryOfBirth);
                    if (!string.IsNullOrEmpty(model.CountryOfBirth.ToString()))
                    {
                        table.AddCell(model.CountryOfBirth.Value.ToString());
                    }
                    else
                    {
                        table.AddCell("");
                    }
                    table.AddCell(Wording.Pet_Card_BloodGroupType);
                    if (!string.IsNullOrEmpty(model.BloodGroupType.ToString()))
                    {
                        table.AddCell(EnumHelper.GetResourceValueForEnumValue(model.BloodGroupType));
                    }
                    else
                    {
                        table.AddCell("");
                    }
                    table.AddCell(Wording.Pet_Card_StateOfBirth);
                    if (!string.IsNullOrEmpty(model.StateOfBirth.ToString()))
                    {
                        table.AddCell(EnumHelper.GetResourceValueForEnumValue(model.StateOfBirth));
                    }
                    else
                    {
                        table.AddCell("");
                    }

                    table.AddCell(Wording.Pet_Card_ChipNumber);
                    if (!string.IsNullOrEmpty(model.ChipNumber))
                    {
                        table.AddCell(model.ChipNumber.ToString());
                    }
                    else
                    {
                        table.AddCell("");
                    }
                    table.AddCell(Wording.Pet_Card_Zip);
                    if (!string.IsNullOrEmpty(model.Zip))
                    {
                        table.AddCell(model.Zip.ToString());
                    }
                    else
                    {
                        table.AddCell("");
                    }
                    table.AddCell(Wording.Pet_Card_TattooNumber);
                    if (!string.IsNullOrEmpty(model.TattooNumber))
                    {
                        table.AddCell(model.TattooNumber.ToString());
                    }
                    else
                    {
                        table.AddCell("");
                    }
                    table.AddCell(Wording.Pet_Card_HairType);
                    if (!string.IsNullOrEmpty(model.HairTypeOther))
                    {
                        table.AddCell(model.HairTypeOther.ToString());
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(model.HairType.ToString()))
                        {
                            table.AddCell(EnumHelper.GetResourceValueForEnumValue(model.HairType));
                        }
                        else
                        {
                            table.AddCell("");
                        }
                    }

                    table.AddCell(Wording.Pet_Card_Gender);
                    if (!string.IsNullOrEmpty(model.Gender.ToString()))
                    {
                        table.AddCell(EnumHelper.GetResourceValueForEnumValue(model.Gender));
                    }
                    else
                    {
                        table.AddCell("");
                    }
                    table.AddCell(Wording.Pet_Card_Color);
                    if (!string.IsNullOrEmpty(model.ColorOther))
                    {
                        table.AddCell(model.ColorOther.ToString());
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(model.Color.ToString()))
                        {
                            table.AddCell(EnumHelper.GetResourceValueForEnumValue(model.Color));
                        }
                        else
                        {
                            table.AddCell("");
                        }
                    }

                    table.AddCell(Wording.Pet_Card_AdoptionDate);
                    if (!string.IsNullOrEmpty(model.AdoptionDate.ToString()))
                    {
                        table.AddCell(model.AdoptionDate.Value.ToShortDateString());
                    }
                    else
                    {
                        table.AddCell("");
                    }
                    table.AddCell(Wording.Pet_Card_IsSterile);
                    if (model.IsSterile == true)
                    {
                        table.AddCell(Wording.Pet_Card_IsSterileYes);
                    }
                    else
                    {
                        if (model.IsSterile == false)
                        {
                            table.AddCell(Wording.Pet_Card_IsSterileNo);
                        }
                        else
                        {
                            table.AddCell(Wording.Pet_Card_IsSterileUnknown);
                        }
                    }
                    
                    //Adoption Details
                    PdfPTable table1 = new PdfPTable(4);
                    table1.TotalWidth = 400f;
                    //fix the absolute width of the table
                    table1.LockedWidth = true;

                    //relative col widths in proportions - 1/3 and 2/3
                    float[] widths1 = new float[] { 6f, 6f, 6f, 6f };
                    table1.SetWidths(widths1);
                    table1.HorizontalAlignment = 1;
                    //leave a gap before and after the table
                    table1.SpacingBefore = 20f;
                    table1.SpacingAfter = 30f;
                    PdfPCell cell1 = new PdfPCell(new Phrase("Adoption Details"));
                    cell1.Colspan = 4;
                    cell1.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    cell1.VerticalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(cell1);

                    table1.AddCell(Wording.Pet_Card_FarmerName);
                    if (!string.IsNullOrEmpty(model.FarmerName))
                    {
                        table1.AddCell(model.FarmerName.ToString());
                    }
                    else
                    {
                        table1.AddCell("");
                    }
                    table1.AddCell(Wording.Pet_Card_FarmerHomePhone);
                    if (!string.IsNullOrEmpty(model.FarmerHomePhone))
                    {
                        table1.AddCell(model.FarmerHomePhone);
                    }
                    else
                    {
                        table1.AddCell("");
                    }
                    table1.AddCell(Wording.Pet_Card_FarmerAddress1);
                    if (!string.IsNullOrEmpty(model.FarmerAddress1))
                    {
                        table1.AddCell(model.FarmerAddress1.ToString());
                    }
                    else
                    {
                        table1.AddCell("");
                    }
                    table1.AddCell(Wording.Pet_Card_FarmerOfficePhone);
                    if (!string.IsNullOrEmpty(model.FarmerOfficePhone))
                    {
                        table1.AddCell(model.FarmerOfficePhone.ToString());
                    }
                    else
                    {
                        table1.AddCell("");
                    }

                    table1.AddCell(Wording.Pet_Card_FarmerAddress2);
                    if (!string.IsNullOrEmpty(model.FarmerAddress2))
                    {
                        table1.AddCell(model.FarmerAddress2.ToString());
                    }
                    else
                    {
                        table1.AddCell("");
                    }
                    table1.AddCell(Wording.Pet_Card_FarmerCellPhone);
                    if (!string.IsNullOrEmpty(model.FarmerCellPhone))
                    {
                        table1.AddCell(model.FarmerCellPhone);
                    }
                    else
                    {
                        table1.AddCell("");
                    }
                    table1.AddCell(Wording.Pet_Card_FarmerCountry);
                    if (!string.IsNullOrEmpty(model.FarmerCountry.ToString()))
                    {
                        table1.AddCell(EnumHelper.GetResourceValueForEnumValue(model.FarmerCountry));
                    }
                    else
                    {
                        table1.AddCell("");
                    }
                    table1.AddCell(Wording.Pet_Card_FarmerFax);
                    if (!string.IsNullOrEmpty(model.FarmerFax))
                    {
                        table1.AddCell(model.FarmerFax.ToString());                        
                    }
                    else
                    {
                        table1.AddCell("");
                    }

                    table1.AddCell(Wording.Pet_Card_FarmerState);
                    if (!string.IsNullOrEmpty(model.FarmerState.ToString()))
                    {
                        table1.AddCell(EnumHelper.GetResourceValueForEnumValue(model.FarmerState));
                    }
                    else
                    {
                        table1.AddCell("");
                    }
                    table1.AddCell(Wording.Pet_Card_FarmerCity);
                    if (!string.IsNullOrEmpty(model.FarmerCity))
                    {
                        table1.AddCell(model.FarmerCity.ToString());
                    }
                    else
                    {
                        table1.AddCell("");
                    }

                    table1.AddCell(Wording.Pet_Card_FarmerZip);
                    if (!string.IsNullOrEmpty(model.FarmerZip))
                    {
                        table1.AddCell(model.FarmerZip.ToString());
                    }
                    else
                    {
                        table1.AddCell("");
                    }

                    table1.AddCell("");
                    table1.AddCell("");

                    //Adoption Details
                    PdfPTable table2 = new PdfPTable(3);
                    table2.TotalWidth = 400f;
                    //fix the absolute width of the table
                    table2.LockedWidth = true;

                    //relative col widths in proportions - 1/3 and 2/3
                    float[] widths2 = new float[] { 6f, 6f, 6f };
                    table2.SetWidths(widths2);
                    table2.HorizontalAlignment = 1;
                    //leave a gap before and after the table
                    table2.SpacingBefore = 20f;
                    table2.SpacingAfter = 30f;
                    PdfPCell cell2 = new PdfPCell(new Phrase("Insurance"));
                    cell2.Colspan = 3;
                    cell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    cell2.VerticalAlignment = Element.ALIGN_CENTER;
                    table2.AddCell(cell2);

                    table2.AddCell(Wording.Insurance_Edit_Name);
                    table2.AddCell(Wording.Insurance_Edit_AccountNumber);
                    table2.AddCell(Wording.Insurance_Delete_EndDate);

                    if (model.Insurances.Count > 0)
                    {
                        foreach (var item in model.Insurances)
                        {
                            if (!string.IsNullOrEmpty(item.Name))
                            {
                                table2.AddCell(item.Name.ToString());
                            }
                            else
                            {
                                table2.AddCell("");
                            }
                            if (!string.IsNullOrEmpty(item.AccountNumber))
                            {
                                table2.AddCell(item.AccountNumber.ToString());
                            }
                            else
                            {
                                table2.AddCell("");
                            }
                            if (!string.IsNullOrEmpty(item.EndDate.ToString()))
                            {
                                table2.AddCell(item.EndDate.Value.ToShortDateString());
                            }
                            else
                            {
                                table2.AddCell("");
                            }
                        }
                    }

                    // Add a simple and wellknown phrase to the document in a flow layout manner
                    //Image img = Image.GetInstance(HttpContext.Current.Server.MapPath("~/Content/images/A4PLogo.jpg"));
                    Image img = Image.GetInstance(HttpContext.Current.Server.MapPath("~" + DomainHelper.GetLogoPath()));
                    img.ScaleToFit(140f, 120f);
                    img.Border = Rectangle.NO_BORDER;
                    img.Alignment = Element.ALIGN_CENTER;

                    Paragraph p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;
                    Phrase p2 = new Phrase();
                    p2.Add(PetName);
                    p.Add(p2);

                    document.Open();
                    document.Add(img);
                    //document.Add(new Paragraph(PetName));
                    document.Add(p);
                    document.Add(table);
                    //document.Add(new Paragraph("Adoption Details"));
                    document.Add(table1);
                    document.Add(table2);
                    //document.Add(new Paragraph("Hello World!"));

                    //// Close the document

                    document.Close();
                    // Close the writer instance

                    writer.Close();
                    // Always close open filehandles explicity
                    fs.Close();
                    
                    
                }
                catch (DocumentException de)
                {
                    
                    throw de;
                }
                catch (IOException ioe)
                {
                    
                    throw ioe;
                }                

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return bRet;
        }
    }
}