using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;
using System.Web;
using ADOPets.Web.ViewModels.HealthMeasureTracker;
using ADOPets.Web.Resources;
using System.Net;
using System.Collections.Generic;
using iTextSharp.text.html.simpleparser;

namespace ADOPets.Web.Common.Helpers
{
    public class PetMedicalHealthMeasurePdf
    {
        public static bool GeneratePDF(string sFilePDF, HealthMeasureTrackerViewModel model, string PetName, string PetType)
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

                    string strPetName = Wording.Pet_Add_PetName + ":" + PetName + "                           " + Wording.Pet_Add_PetType + ":" + PetType;

                    //Vitals-Height Details
                    PdfPTable Height = new PdfPTable(2);
                    Height.TotalWidth = 400f;
                    Height.LockedWidth = true;

                    float[] widths2 = new float[] { 6f, 6f };
                    Height.SetWidths(widths2);
                    Height.HorizontalAlignment = 1;
                    Height.SpacingBefore = 20f;
                    Height.SpacingAfter = 30f;
                    PdfPCell cell2 = new PdfPCell(new Phrase(Wording.Height_Add_Height));
                    cell2.Colspan = 2;
                    cell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    cell2.VerticalAlignment = Element.ALIGN_CENTER;
                    Height.AddCell(cell2);

                    Height.AddCell(Wording.Height_Add_Date);
                    Height.AddCell(Wording.Height_Add_Height);                    

                    if (model.Vitals.Height.Count > 0)
                    {
                        foreach (var item in model.Vitals.Height)
                        {
                            if (!string.IsNullOrEmpty(item.Date.ToString()))
                            {
                                Height.AddCell(item.Date.ToShortDateString());
                            }
                            else
                            {
                                Height.AddCell("");
                            }
                            if (!string.IsNullOrEmpty(item.Height.ToString()))
                            {
                                Height.AddCell(item.Height.ToString());
                            }
                            else
                            {
                                Height.AddCell("");
                            }                            
                        }
                    }

                    //Vitals-Weight Details
                    PdfPTable Weight = new PdfPTable(2);
                    Weight.TotalWidth = 400f;
                    Weight.LockedWidth = true;

                    float[] Weightwidths2 = new float[] { 6f, 6f };
                    Weight.SetWidths(Weightwidths2);
                    Weight.HorizontalAlignment = 1;
                    Weight.SpacingBefore = 20f;
                    Weight.SpacingAfter = 30f;
                    PdfPCell Weightcell2 = new PdfPCell(new Phrase(Wording.Vitals_Index_Weight));
                    Weightcell2.Colspan = 2;
                    Weightcell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    Weightcell2.VerticalAlignment = Element.ALIGN_CENTER;
                    Weight.AddCell(Weightcell2);

                    Weight.AddCell(Wording.Weight_Add_Date);
                    Weight.AddCell(Wording.Weight_Add_Weight);

                    if (model.Vitals.Weight.Count > 0)
                    {
                        foreach (var item in model.Vitals.Weight)
                        {
                            if (!string.IsNullOrEmpty(item.Date.ToString()))
                            {
                                Weight.AddCell(item.Date.ToShortDateString());
                            }
                            else
                            {
                                Weight.AddCell("");
                            }
                            if (!string.IsNullOrEmpty(item.Weight.ToString()))
                            {
                                Weight.AddCell(item.Weight.ToString());
                            }
                            else
                            {
                                Weight.AddCell("");
                            }
                        }
                    }

                    //Vitals-Temperature Details                    
                    PdfPTable Temperature = new PdfPTable(2);
                    Temperature.TotalWidth = 400f;
                    Temperature.LockedWidth = true;

                    float[] Temperaturewidths2 = new float[] { 6f, 6f };
                    Temperature.SetWidths(Temperaturewidths2);
                    Temperature.HorizontalAlignment = 1;
                    Temperature.SpacingBefore = 20f;
                    Temperature.SpacingAfter = 30f;
                    PdfPCell Temperaturecell2 = new PdfPCell(new Phrase(Wording.Vitals_Index_Temperature));
                    Temperaturecell2.Colspan = 2;
                    Temperaturecell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    Temperaturecell2.VerticalAlignment = Element.ALIGN_CENTER;
                    Temperature.AddCell(Temperaturecell2);

                    Temperature.AddCell(Wording.Temperature_Add_Date);
                    Temperature.AddCell(Wording.Temperature_Add_Temperature);

                    if (model.Vitals.Temperature.Count > 0)
                    {
                        foreach (var item in model.Vitals.Temperature)
                        {
                            if (!string.IsNullOrEmpty(item.Date.ToString()))
                            {
                                Temperature.AddCell(item.Date.ToShortDateString());
                            }
                            else
                            {
                                Temperature.AddCell("");
                            }
                            if (!string.IsNullOrEmpty(item.Temperature.ToString()))
                            {
                                Temperature.AddCell(item.Temperature.ToString());
                            }
                            else
                            {
                                Temperature.AddCell("");
                            }
                        }
                    }

                    //Vitals-Pulse Details                    
                    PdfPTable Pulse = new PdfPTable(2);
                    Pulse.TotalWidth = 400f;
                    Pulse.LockedWidth = true;

                    float[] Pulsewidths2 = new float[] { 6f, 6f };
                    Pulse.SetWidths(Pulsewidths2);
                    Pulse.HorizontalAlignment = 1;
                    Pulse.SpacingBefore = 20f;
                    Pulse.SpacingAfter = 30f;
                    PdfPCell Pulsecell2 = new PdfPCell(new Phrase(Wording.Vitals_Index_Pulse));
                    Pulsecell2.Colspan = 2;
                    Pulsecell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    Pulsecell2.VerticalAlignment = Element.ALIGN_CENTER;
                    Pulse.AddCell(Pulsecell2);

                    Pulse.AddCell(Wording.Temperature_Add_Date);
                    Pulse.AddCell(Wording.Temperature_Add_Temperature);

                    if (model.Vitals.Pulse.Count > 0)
                    {
                        foreach (var item in model.Vitals.Pulse)
                        {
                            if (!string.IsNullOrEmpty(item.Date.ToString()))
                            {
                                Pulse.AddCell(item.Date.ToShortDateString());
                            }
                            else
                            {
                                Pulse.AddCell("");
                            }
                            if (!string.IsNullOrEmpty(item.Pulse.ToString()))
                            {
                                Pulse.AddCell(item.Pulse.ToString());
                            }
                            else
                            {
                                Pulse.AddCell("");
                            }
                        }
                    }

                    //CBG Details                    
                    PdfPTable CBG = new PdfPTable(2);
                    CBG.TotalWidth = 400f;
                    CBG.LockedWidth = true;

                    CBG.SetWidths(Pulsewidths2);
                    CBG.HorizontalAlignment = 1;
                    CBG.SpacingBefore = 20f;
                    CBG.SpacingAfter = 30f;
                    PdfPCell CBGcell2 = new PdfPCell(new Phrase(Wording.HealthMeasureTracker_Index_CBG));
                    CBGcell2.Colspan = 2;
                    CBGcell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    CBGcell2.VerticalAlignment = Element.ALIGN_CENTER;
                    CBG.AddCell(CBGcell2);

                    CBG.AddCell(Wording.CBG_Add_Date);
                    CBG.AddCell(Wording.CBG_Add_CBG);

                    if (model.Cbg.ToString().Length > 0)
                    {
                        foreach (var item in model.Cbg)
                        {
                            if (!string.IsNullOrEmpty(item.MeasureDate.ToString()))
                            {
                                CBG.AddCell(item.MeasureDate.ToShortDateString());
                            }
                            else
                            {
                                CBG.AddCell("");
                            }
                            if (!string.IsNullOrEmpty(item.CBGValue.ToString()))
                            {
                                CBG.AddCell(item.CBGValue.ToString());
                            }
                            else
                            {
                                CBG.AddCell("");
                            }
                        }
                    }

                    //Hemoglobin Details                    
                    PdfPTable Hemoglobin = new PdfPTable(2);
                    Hemoglobin.TotalWidth = 400f;
                    Hemoglobin.LockedWidth = true;

                    Hemoglobin.SetWidths(Pulsewidths2);
                    Hemoglobin.HorizontalAlignment = 1;
                    Hemoglobin.SpacingBefore = 20f;
                    Hemoglobin.SpacingAfter = 30f;
                    PdfPCell Hemoglobincell2 = new PdfPCell(new Phrase(Wording.HealthMeasureTracker_Index_Hemoglobin));
                    Hemoglobincell2.Colspan = 2;
                    Hemoglobincell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    Hemoglobincell2.VerticalAlignment = Element.ALIGN_CENTER;
                    Hemoglobin.AddCell(Hemoglobincell2);

                    Hemoglobin.AddCell(Wording.Hemoglobin_Add_Date);
                    Hemoglobin.AddCell(Wording.Hemoglobin_Add_Hemoglobin);

                    if (model.Hmglbin.ToString().Length > 0)
                    {
                        foreach (var item in model.Hmglbin)
                        {
                            if (!string.IsNullOrEmpty(item.MeasureDate.ToString()))
                            {
                                Hemoglobin.AddCell(item.MeasureDate.ToShortDateString());
                            }
                            else
                            {
                                Hemoglobin.AddCell("");
                            }
                            if (!string.IsNullOrEmpty(item.HemoglobinValue.ToString()))
                            {
                                Hemoglobin.AddCell(item.HemoglobinValue.ToString());
                            }
                            else
                            {
                                Hemoglobin.AddCell("");
                            }
                        }
                    }

                    //Hemogram Details                    
                    PdfPTable Hemogram = new PdfPTable(2);
                    Hemogram.TotalWidth = 400f;
                    Hemogram.LockedWidth = true;

                    Hemogram.SetWidths(Pulsewidths2);
                    Hemogram.HorizontalAlignment = 1;
                    Hemogram.SpacingBefore = 20f;
                    Hemogram.SpacingAfter = 30f;
                    PdfPCell Hemogramcell2 = new PdfPCell(new Phrase(Wording.HealthMeasureTracker_Index_Hemogram));
                    Hemogramcell2.Colspan = 2;
                    Hemogramcell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    Hemogramcell2.VerticalAlignment = Element.ALIGN_CENTER;
                    Hemogram.AddCell(Hemogramcell2);

                    Hemogram.AddCell(Wording.Hemogram_Add_Date);
                    Hemogram.AddCell(Wording.Hemogram_Add_Hemogram);

                    if (model.Hmgram.ToString().Length > 0)
                    {
                        foreach (var item in model.Hmgram)
                        {
                            if (!string.IsNullOrEmpty(item.MeasureDate.ToString()))
                            {
                                Hemogram.AddCell(item.MeasureDate.ToShortDateString());
                            }
                            else
                            {
                                Hemogram.AddCell("");
                            }
                            if (!string.IsNullOrEmpty(item.HemogramValue.ToString()))
                            {
                                Hemogram.AddCell(item.HemogramValue.ToString());
                            }
                            else
                            {
                                Hemogram.AddCell("");
                            }
                        }
                    }

                    //SerumElectrolytes Details                    
                    PdfPTable Electrolyte = new PdfPTable(2);
                    Electrolyte.TotalWidth = 400f;
                    Electrolyte.LockedWidth = true;

                    Electrolyte.SetWidths(Pulsewidths2);
                    Electrolyte.HorizontalAlignment = 1;
                    Electrolyte.SpacingBefore = 20f;
                    Electrolyte.SpacingAfter = 30f;
                    PdfPCell Electrolytecell2 = new PdfPCell(new Phrase(Wording.HealthMeasureTracker_Index_SerumElectrolytes));
                    Electrolytecell2.Colspan = 2;
                    Electrolytecell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    Electrolytecell2.VerticalAlignment = Element.ALIGN_CENTER;
                    Electrolyte.AddCell(Electrolytecell2);

                    Electrolyte.AddCell(Wording.SerumElectrolyte_Add_Date);
                    Electrolyte.AddCell(Wording.SerumElectrolyte_Add_SerumElectrolyte);

                    if (model.SerumElectro.ToString().Length > 0)
                    {
                        foreach (var item in model.SerumElectro)
                        {
                            if (!string.IsNullOrEmpty(item.MeasureDate.ToString()))
                            {
                                Electrolyte.AddCell(item.MeasureDate.ToShortDateString());
                            }
                            else
                            {
                                Electrolyte.AddCell("");
                            }
                            if (!string.IsNullOrEmpty(item.SerumElectrolyteValue.ToString()))
                            {
                                Electrolyte.AddCell(item.SerumElectrolyteValue.ToString());
                            }
                            else
                            {
                                Electrolyte.AddCell("");
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
                    p2.Add(new Phrase(strPetName));
                    p.Add(p2);

                    Paragraph p1 = new Paragraph();
                    p1.Alignment = Element.ALIGN_CENTER;
                    Phrase p3 = new Phrase();
                    p3.Add(new Phrase(Wording.MedicalRecord_Index_HealthMeasureTracker));
                    p1.Add(p3);

                    Paragraph p4 = new Paragraph();
                    p4.Alignment = Element.ALIGN_CENTER;
                    Phrase p41 = new Phrase();
                    p41.Add(new Phrase(""));
                    p4.Add(p41);

                    document.Open();
                    document.Add(img);
                    //document.Add(new Paragraph(PetName));
                    document.Add(p);
                    document.Add(p4);
                    document.Add(p1);
                    document.Add(Height);
                    document.Add(Weight);
                    document.Add(Temperature);
                    document.Add(Pulse);
                    document.Add(CBG);
                    document.Add(Hemoglobin);
                    document.Add(Hemogram);
                    document.Add(Electrolyte);
                    
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