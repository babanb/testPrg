using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using OpenPop.Pop3;
using OpenPop.Mime;
using Model;
using Model.Tools;
using Repository.Implementations;
using System.Threading;
using System.Globalization;
using EmailSender;
using System.Data.OleDb;
using System.Data;

namespace Hsbc.ImportUser
{
    class Program
    {
        private static MailSenderHelper mailSender = new MailSenderHelper(ConfigurationManager.AppSettings[Constants.LogoForEmailPath], ConfigurationManager.AppSettings[Constants.SignaturePath]);
        private static string fileName = "", datetimeFileName = "";
        static void Main(string[] args)
        {
            ImportUser();
        }

        private static void ImportUser()
        {
            Pop3Client pop3Client = new Pop3Client();
            pop3Client.Connect("pop.1and1.fr", 995, true);
            pop3Client.Authenticate("hsbc@activ4pets.com", "Jugt_1865", AuthenticationMethod.TryBoth);

            int msgCount = pop3Client.GetMessageCount();

            if (msgCount > 0)
            {
                for (int i = msgCount; i >= 1; i--)
                {
                    OpenPop.Mime.Message message = pop3Client.GetMessage(i);
                    string filePath = "";
                    if (message.Headers.From.Address.ToLower() != "")// == "hsbc@activ4pets.com")
                    {
                        List<MessagePart> attachments = message.FindAllAttachments();

                        if (attachments.Count > 0)
                        {
                            foreach (MessagePart attachment in attachments)
                            {
                                if (attachment.FileName.ToLower().EndsWith(".csv"))
                                {
                                    string newfileName = attachment.FileName;
                                    datetimeFileName = DateTime.Now.Day + "" + DateTime.Now.Ticks + DateTime.Now.Month;

                                    fileName = datetimeFileName + newfileName.Trim();

                                    filePath = ConfigurationManager.AppSettings[Constants.FilePath] + fileName;
                                    if (!Directory.Exists(ConfigurationManager.AppSettings[Constants.FilePath]))
                                    {
                                        Directory.CreateDirectory(ConfigurationManager.AppSettings[Constants.FilePath]);
                                    }

                                    attachment.SaveToFile(new System.IO.FileInfo(filePath));
                                }
                                if (attachment.FileName.ToLower().EndsWith(".xls") || attachment.FileName.ToLower().EndsWith(".xlsx"))
                                {
                                    fileName = GenerateCSVfromXLS(attachment);
                                    filePath = ConfigurationManager.AppSettings[Constants.FilePath] + fileName;
                                }
                            }
                            if (!string.IsNullOrEmpty(filePath))
                            {
                                bool result = mainThread(filePath, datetimeFileName);
                              //  if (result)
                              //  {
                                    pop3Client.DeleteMessage(i);
                                    // Delete the file/email from email account only if the users are imported successfully.
                                    //
                               // }
                            }
                        }
                    }
                    else
                    {
                        string subject = EmailSender.Resources.HsbcMails.NoMailOrAttachmentMailSubject;
                        string messageText = EmailSender.Resources.HsbcMails.NoMailOrAttachmentMailBody;
                        mailSender.SendToSupportNoMailOrAttachmentMail(messageText, subject);
                    }
                }
            }
            else
            {
                string subject = EmailSender.Resources.HsbcMails.NoMailOrAttachmentMailSubject;
                string messageText = EmailSender.Resources.HsbcMails.NoMailOrAttachmentMailBody;
                mailSender.SendToSupportNoMailOrAttachmentMail(messageText, subject);
            }

            pop3Client.Disconnect();
            //Console.ReadLine();
        }

        private static string GenerateCSVfromXLS(MessagePart attachment)
        {
            string newfileName = attachment.FileName.Split('.')[0];
            datetimeFileName = DateTime.Now.Day + "" + DateTime.Now.Ticks + DateTime.Now.Month;
            string targetFileName = datetimeFileName + newfileName.Trim() + ".csv";

            var filePath = ConfigurationManager.AppSettings[Constants.FilePath] + targetFileName;
            var xlsFilePath = ConfigurationManager.AppSettings[Constants.FilePath] + attachment.FileName;
            attachment.SaveToFile(new System.IO.FileInfo(xlsFilePath));

            var strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + xlsFilePath + ";Extended Properties=\"Excel 12.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text\""; ;
            OleDbConnection conn = null;
            StringBuilder wrtr = null;
            OleDbCommand cmd = null;
            OleDbDataAdapter da = null;
            try
            {
                conn = new OleDbConnection(strConn);
                conn.Open();

                DataTable dbSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (dbSchema == null || dbSchema.Rows.Count < 1)
                {
                    throw new Exception("Error: Could not determine the name of the first worksheet.");
                }
                string firstSheetName = dbSchema.Rows[0]["TABLE_NAME"].ToString();


                cmd = new OleDbCommand("SELECT * FROM [" + firstSheetName + "]", conn);
                cmd.CommandType = CommandType.Text;
                wrtr = new StringBuilder();

                da = new OleDbDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    string rowString = "";
                    for (int y = 0; y < dt.Columns.Count; y++)
                    {
                        if (y == (dt.Columns.Count - 1))
                        {
                            rowString += "" + dt.Rows[x][y].ToString();
                        }
                        else
                        {
                            rowString += "" + dt.Rows[x][y].ToString() + ",";
                        }
                    }
                    wrtr.AppendLine(rowString);
                }

                File.WriteAllText(filePath, wrtr.ToString());
                Console.WriteLine();
                Console.WriteLine("Done! Your " + attachment.FileName + " has been converted into " + targetFileName + ".");
                Console.WriteLine();

            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.ToString());
                // Console.ReadLine();
                return "";
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                conn.Dispose();
                cmd.Dispose();
                da.Dispose();
                File.Delete(xlsFilePath);
            }


            return targetFileName;
        }

        private static bool mainThread(string filePath, string datetimeFileName)
        {
            bool flag = false;
            ImportUsers importUser = new ImportUsers();

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            Console.WriteLine("The column names are:");
            ImportUsers.columnIndex.Keys.ToList().ForEach(Console.WriteLine);

            var lines = ImportUsers.GetLines(filePath, datetimeFileName);

            if (lines == null)
            {
                flag = false;
                Console.WriteLine("Error reading csv file.");
                string subject = EmailSender.Resources.HsbcMails.FileFormatIncorrectMailSubject;
                string messageText = EmailSender.Resources.HsbcMails.FileFormatIncorrectMailBody;
                mailSender.SendToSupportFileFormatIncorrectMail(messageText, subject, ConfigurationManager.AppSettings[Constants.FilePath], fileName);
                //  Console.ReadLine();
            }
            else
            {
                var errors = string.Empty;
                var columns = lines.First().Split(new[] { "," }, StringSplitOptions.None).ToList();

                if (ImportUsers.ValidateColumns(columns, ref errors))
                {
                    Console.WriteLine(errors);
                    flag = false;
                    string subject = EmailSender.Resources.HsbcMails.FileFormatIncorrectMailSubject;
                    string messageText = EmailSender.Resources.HsbcMails.FileFormatIncorrectMailBody;
                    mailSender.SendToSupportFileFormatIncorrectMail(messageText, subject, ConfigurationManager.AppSettings[Constants.FilePath], fileName);
                }
                else
                {
                    ImportUsers.columnIndex.Keys.ToList().ForEach(key =>
                    {
                        ImportUsers.columnIndex[key] = columns.FindIndex(c => key.Equals(c, StringComparison.OrdinalIgnoreCase));
                    });
                    using (ImportUsers.uow = new UnitOfWork())
                    {
                        var data = ImportUsers.GetUserInfoFromExcel(lines.ToList());
                        ImportUsers.InsertData(data);

                        Console.WriteLine();
                        string insertedCountMessage = data.Count + " records were inserted successfully.";
                        string locationMessage = "You have in the same folder of your csv, a file named: " + Constants.ErrorFileName + "\nwith the possible lines with errors.";

                        if (data.Count > 0)
                        {
                            flag = true;

                            string subject = EmailSender.Resources.HsbcMails.ImportSuccessMailSubject;
                            string messageText = EmailSender.Resources.HsbcMails.ImportSuccessMailBody;
                            mailSender.SendToSupportImportSuccessMail(messageText, subject, fileName, ConfigurationManager.AppSettings[Constants.FilePath]);
                        }
                        else
                        {
                            flag = false;

                            string subject = EmailSender.Resources.HsbcMails.FileFormatIncorrectMailSubject;
                            string messageText = EmailSender.Resources.HsbcMails.FileFormatIncorrectMailBody;
                            mailSender.SendToSupportFileFormatIncorrectMail(messageText, subject, ConfigurationManager.AppSettings[Constants.FilePath], fileName);
                        }

                        Console.WriteLine(insertedCountMessage);
                        Console.WriteLine(locationMessage);
                    }
                }
            }
            return flag;
        }
    }
}
