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
using System.Resources;

namespace ADOPets.Web.Common.Helpers
{
    public class PetMedicalSummaryPdfHelper
    {
        public static bool GeneratePDF(string sFilePDF, LoadMedicalRecordModel model)
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
                    var petTypeVar = Resources.Enums.ResourceManager.GetString("PetTypeEnum_" + model.PetType);
                    string PetName = Wording.Pet_Add_PetName + ":" + model.PetName + "                           " + Wording.Pet_Add_PetType + ":" + petTypeVar;

                    //Condition Details
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
                    PdfPCell cell2 = new PdfPCell(new Phrase(Wording.HealthHistory_Index_Conditions));
                    cell2.Colspan = 3;
                    cell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    cell2.VerticalAlignment = Element.ALIGN_CENTER;
                    table2.AddCell(cell2);

                    table2.AddCell(Wording.Condition_Index_Condition);
                    table2.AddCell(Wording.Condition_Index_VisitDate);
                    table2.AddCell(Wording.Condition_Index_EndDate);

                    if (model.Conditions.Count > 0)
                    {
                        foreach (var item in model.Conditions)
                        {
                            if (!string.IsNullOrEmpty(item.Name))
                            {
                                table2.AddCell(item.Name.ToString());
                            }
                            else
                            {
                                table2.AddCell("");
                            }
                            if (!string.IsNullOrEmpty(item.VisitDate.ToString()))
                            {
                                table2.AddCell(item.VisitDate.ToShortDateString());
                            }
                            else
                            {
                                table2.AddCell("");
                            }
                            if (item.EndDate.HasValue)
                            {
                                table2.AddCell(item.EndDate.Value.ToShortDateString());
                            }
                            else
                            {
                                table2.AddCell("");
                            }
                        }
                    }

                    //Surgery Details
                    PdfPTable Surgery = new PdfPTable(4);
                    Surgery.TotalWidth = 400f;
                    //fix the absolute width of the table
                    Surgery.LockedWidth = true;

                    //relative col widths in proportions - 1/3 and 2/3
                    float[] Surgery2 = new float[] { 6f, 6f, 6f, 6f };
                    Surgery.SetWidths(Surgery2);
                    Surgery.HorizontalAlignment = 1;
                    //leave a gap before and after the table
                    Surgery.SpacingBefore = 20f;
                    Surgery.SpacingAfter = 30f;
                    PdfPCell Surgerycell2 = new PdfPCell(new Phrase(Wording.HealthHistory_Index_Surgeries));
                    Surgerycell2.Colspan = 4;
                    Surgerycell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    Surgerycell2.VerticalAlignment = Element.ALIGN_CENTER;
                    Surgery.AddCell(Surgerycell2);

                    Surgery.AddCell(Wording.Surgery_Index_Surgery);
                    Surgery.AddCell(Wording.Surgery_Edit_DateofSurgery);
                    Surgery.AddCell(Wording.Surgery_Index_Reason);
                    Surgery.AddCell(Wording.Surgery_Index_Veterinarian);

                    if (model.Surgeries.Count > 0)
                    {
                        foreach (var item in model.Surgeries)
                        {
                            if (!string.IsNullOrEmpty(item.Name))
                            {
                                Surgery.AddCell(item.Name.ToString());
                            }
                            else
                            {
                                Surgery.AddCell("");
                            }
                            if (item.Date.HasValue)
                            {
                                Surgery.AddCell(item.Date.Value.ToShortDateString());
                            }
                            else
                            {
                                Surgery.AddCell("");
                            }
                            if (!string.IsNullOrEmpty(item.Reason))
                            {
                                Surgery.AddCell(item.Reason);
                            }
                            else
                            {
                                Surgery.AddCell("");
                            }
                            if (!string.IsNullOrEmpty(item.PhysicianName))
                            {
                                Surgery.AddCell(item.PhysicianName);
                            }
                            else
                            {
                                Surgery.AddCell("");
                            }
                        }
                    }

                    //Medication Details
                    PdfPTable Medication = new PdfPTable(4);
                    Medication.TotalWidth = 400f;
                    //fix the absolute width of the table
                    Medication.LockedWidth = true;

                    //relative col widths in proportions - 1/3 and 2/3
                    float[] MedicationWidth = new float[] { 6f, 6f, 6f, 6f };
                    Medication.SetWidths(MedicationWidth);
                    Medication.HorizontalAlignment = 1;
                    //leave a gap before and after the table
                    Medication.SpacingBefore = 20f;
                    Medication.SpacingAfter = 30f;
                    PdfPCell Medicationcell2 = new PdfPCell(new Phrase(Wording.HealthHistory_Index_Medications));
                    Medicationcell2.Colspan = 4;
                    Medicationcell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    Medicationcell2.VerticalAlignment = Element.ALIGN_CENTER;
                    Medication.AddCell(Medicationcell2);

                    Medication.AddCell(Wording.Medication_Index_Medication);
                    Medication.AddCell(Wording.Medication_Index_Duration);
                    Medication.AddCell(Wording.Medication_Index_VisitDate);
                    Medication.AddCell(Wording.Medication_Index_Status);

                    if (model.Medications.Count > 0)
                    {
                        foreach (var item in model.Medications)
                        {
                            if (!string.IsNullOrEmpty(item.Name))
                            {
                                Medication.AddCell(item.Name.ToString());
                            }
                            else
                            {
                                Medication.AddCell("");
                            }
                            if (!string.IsNullOrEmpty(item.Duration.ToString()))
                            {
                                Medication.AddCell(item.Duration.ToString());
                            }
                            else
                            {
                                Medication.AddCell("");
                            }
                            if (!string.IsNullOrEmpty(item.VisitDate.ToShortDateString()))
                            {
                                Medication.AddCell(item.VisitDate.ToShortDateString());
                            }
                            else
                            {
                                Medication.AddCell("");
                            }
                            if (!string.IsNullOrEmpty(item.Status.ToString()))
                            {
                                Medication.AddCell(EnumHelper.GetResourceValueForEnumValue(item.Status));
                            }
                            else
                            {
                                Medication.AddCell("");
                            }
                        }
                    }

                    //Allergy Details
                    PdfPTable Allergy = new PdfPTable(3);
                    Allergy.TotalWidth = 400f;
                    //fix the absolute width of the table
                    Allergy.LockedWidth = true;

                    //relative col widths in proportions - 1/3 and 2/3
                    float[] Allergywidths2 = new float[] { 6f, 6f, 6f };
                    Allergy.SetWidths(Allergywidths2);
                    Allergy.HorizontalAlignment = 1;
                    //leave a gap before and after the table
                    Allergy.SpacingBefore = 20f;
                    Allergy.SpacingAfter = 30f;
                    PdfPCell Allergycell2 = new PdfPCell(new Phrase(Wording.HealthHistory_Index_Allergies));
                    Allergycell2.Colspan = 3;
                    Allergycell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    Allergycell2.VerticalAlignment = Element.ALIGN_CENTER;
                    Allergy.AddCell(Allergycell2);

                    Allergy.AddCell(Wording.Allergy_Add_Allergy);
                    Allergy.AddCell(Wording.Allergy_Add_StartDate);
                    Allergy.AddCell(Wording.Allergy_Add_Reaction);

                    if (model.Allergies.Count > 0)
                    {
                        foreach (var item in model.Allergies)
                        {
                            if (!string.IsNullOrEmpty(item.Allergy))
                            {
                                Allergy.AddCell(item.Allergy.ToString());
                            }
                            else
                            {
                                Allergy.AddCell("");
                            }
                            if (item.StartDate.HasValue)
                            {
                                Allergy.AddCell(item.StartDate.Value.ToShortDateString());
                            }
                            else
                            {
                                Allergy.AddCell("");
                            }
                            if (!string.IsNullOrEmpty(item.Reaction))
                            {
                                Allergy.AddCell(item.Reaction);
                            }
                            else
                            {
                                Allergy.AddCell("");
                            }
                        }
                    }

                    //Immunization Details
                    PdfPTable Immunization = new PdfPTable(3);
                    Immunization.TotalWidth = 400f;
                    //fix the absolute width of the table
                    Immunization.LockedWidth = true;

                    //relative col widths in proportions - 1/3 and 2/3
                    float[] Immunizationwidths2 = new float[] { 6f, 6f, 6f };
                    Immunization.SetWidths(Immunizationwidths2);
                    Immunization.HorizontalAlignment = 1;
                    //leave a gap before and after the table
                    Immunization.SpacingBefore = 20f;
                    Immunization.SpacingAfter = 30f;
                    PdfPCell Immunizationcell2 = new PdfPCell(new Phrase(Wording.HealthHistory_Index_Immunizations));
                    Immunizationcell2.Colspan = 3;
                    Immunizationcell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    Immunizationcell2.VerticalAlignment = Element.ALIGN_CENTER;
                    Immunization.AddCell(Immunizationcell2);

                    Immunization.AddCell(Wording.Immunization_Index_Immunization);
                    Immunization.AddCell(Wording.Immunization_Index_InjectionDate);
                    Immunization.AddCell(Wording.Immunization_Index_SerialNumber);

                    if (model.Immunizations.Count > 0)
                    {
                        foreach (var item in model.Immunizations)
                        {
                            if (!string.IsNullOrEmpty(item.Immunization))
                            {
                                Immunization.AddCell(item.Immunization.ToString());
                            }
                            else
                            {
                                Immunization.AddCell("");
                            }
                            if (!string.IsNullOrEmpty(item.InjectionDate.ToString()))
                            {
                                Immunization.AddCell(item.InjectionDate.ToShortDateString());
                            }
                            else
                            {
                                Immunization.AddCell("");
                            }
                            if (!string.IsNullOrEmpty(item.SerialNumber))
                            {
                                Immunization.AddCell(item.SerialNumber);
                            }
                            else
                            {
                                Immunization.AddCell("");
                            }
                        }
                    }

                    //FoodPlan Details
                    PdfPTable FoodPlan = new PdfPTable(3);
                    FoodPlan.TotalWidth = 400f;
                    //fix the absolute width of the table
                    FoodPlan.LockedWidth = true;

                    //relative col widths in proportions - 1/3 and 2/3
                    float[] FoodPlanwidths2 = new float[] { 6f, 6f, 6f };
                    FoodPlan.SetWidths(FoodPlanwidths2);
                    FoodPlan.HorizontalAlignment = 1;
                    //leave a gap before and after the table
                    FoodPlan.SpacingBefore = 20f;
                    FoodPlan.SpacingAfter = 30f;
                    PdfPCell FoodPlancell2 = new PdfPCell(new Phrase(Wording.HealthHistory_Index_FoodPlan));
                    FoodPlancell2.Colspan = 3;
                    FoodPlancell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    FoodPlancell2.VerticalAlignment = Element.ALIGN_CENTER;
                    FoodPlan.AddCell(FoodPlancell2);

                    FoodPlan.AddCell(Wording.FoodPlan_Add_FoodName);
                    FoodPlan.AddCell(Wording.FoodPlan_Add_RecomendedFood);
                    FoodPlan.AddCell(Wording.FoodPlan_Add_ForbidenFood);

                    if (model.FoodPlans.Count > 0)
                    {
                        foreach (var item in model.FoodPlans)
                        {
                            if (!string.IsNullOrEmpty(item.FoodName))
                            {
                                FoodPlan.AddCell(item.FoodName.ToString());
                            }
                            else
                            {
                                FoodPlan.AddCell("");
                            }
                            if (!string.IsNullOrEmpty(item.RecomendedFood.ToString()))
                            {
                                FoodPlan.AddCell(item.RecomendedFood.Value.ToString());
                            }
                            else
                            {
                                FoodPlan.AddCell("");
                            }
                            if (!string.IsNullOrEmpty(item.ForbidenFood.ToString()))
                            {
                                FoodPlan.AddCell(item.ForbidenFood.Value.ToString());
                            }
                            else
                            {
                                FoodPlan.AddCell("");
                            }
                        }
                    }

                    //Hospitalization Details
                    PdfPTable Hospitalization = new PdfPTable(4);
                    Hospitalization.TotalWidth = 400f;
                    //fix the absolute width of the table
                    Hospitalization.LockedWidth = true;

                    //relative col widths in proportions - 1/3 and 2/3
                    float[] HospitalizationWidth = new float[] { 6f, 6f, 6f, 6f };
                    Hospitalization.SetWidths(HospitalizationWidth);
                    Hospitalization.HorizontalAlignment = 1;
                    //leave a gap before and after the table
                    Hospitalization.SpacingBefore = 20f;
                    Hospitalization.SpacingAfter = 30f;
                    PdfPCell Hospitalizationcell2 = new PdfPCell(new Phrase(Wording.HealthHistory_Index_Hospitalizations));
                    Hospitalizationcell2.Colspan = 4;
                    Hospitalizationcell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    Hospitalizationcell2.VerticalAlignment = Element.ALIGN_CENTER;
                    Hospitalization.AddCell(Hospitalizationcell2);

                    Hospitalization.AddCell(Wording.Hospitalization_Index_HospitalName);
                    Hospitalization.AddCell(Wording.Hospitalization_Index_Reason);
                    Hospitalization.AddCell(Wording.Hospitalization_Index_AdmissionDate);
                    Hospitalization.AddCell(Wording.Hospitalization_Index_Urgent);

                    if (model.Hospitalizations.Count > 0)
                    {
                        foreach (var item in model.Hospitalizations)
                        {
                            if (!string.IsNullOrEmpty(item.HospitalName))
                            {
                                Hospitalization.AddCell(item.HospitalName);
                            }
                            else
                            {
                                Hospitalization.AddCell("");
                            }
                            if (!string.IsNullOrEmpty(item.Reason))
                            {
                                Hospitalization.AddCell(item.Reason);
                            }
                            else
                            {
                                Hospitalization.AddCell("");
                            }
                            if (item.AdmissionDate.HasValue)
                            {
                                Hospitalization.AddCell(item.AdmissionDate.Value.ToShortDateString());
                            }
                            else
                            {
                                Hospitalization.AddCell("");
                            }
                            if (!string.IsNullOrEmpty(item.UrgentCareVisit.ToString()))
                            {
                                Hospitalization.AddCell(item.UrgentCareVisit.ToString());
                            }
                            else
                            {
                                Hospitalization.AddCell("");
                            }
                        }
                    }

                    //Consultation Details
                    PdfPTable Consultation = new PdfPTable(3);
                    Consultation.TotalWidth = 400f;
                    //fix the absolute width of the table
                    Consultation.LockedWidth = true;

                    //relative col widths in proportions - 1/3 and 2/3
                    float[] Consultationwidths2 = new float[] { 6f, 6f, 6f };
                    Consultation.SetWidths(Consultationwidths2);
                    Consultation.HorizontalAlignment = 1;
                    //leave a gap before and after the table
                    Consultation.SpacingBefore = 20f;
                    Consultation.SpacingAfter = 30f;
                    PdfPCell Consultationcell2 = new PdfPCell(new Phrase(Wording.HealthHistory_Index_Consultations));
                    Consultationcell2.Colspan = 3;
                    Consultationcell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    Consultationcell2.VerticalAlignment = Element.ALIGN_CENTER;
                    Consultation.AddCell(Consultationcell2);

                    Consultation.AddCell(Wording.Consultation_Add_ConsultationName);
                    Consultation.AddCell(Wording.Consultation_Add_VisitDate);
                    Consultation.AddCell(Wording.Consultation_Add_Reason);

                    if (model.Consultations.Count > 0)
                    {
                        foreach (var item in model.Consultations)
                        {
                            if (!string.IsNullOrEmpty(item.Name))
                            {
                                Consultation.AddCell(item.Name.ToString());
                            }
                            else
                            {
                                Consultation.AddCell("");
                            }
                            if (!string.IsNullOrEmpty(item.VisitDate.ToString()))
                            {
                                Consultation.AddCell(item.VisitDate.ToShortDateString());
                            }
                            else
                            {
                                Consultation.AddCell("");
                            }
                            if (!string.IsNullOrEmpty(item.Reason))
                            {
                                Consultation.AddCell(item.Reason);
                            }
                            else
                            {
                                Consultation.AddCell("");
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
                    document.Add(table2);
                    document.Add(Surgery);
                    document.Add(Medication);
                    document.Add(Allergy);
                    document.Add(Immunization);
                    document.Add(FoodPlan);
                    document.Add(Hospitalization);
                    document.Add(Consultation);
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