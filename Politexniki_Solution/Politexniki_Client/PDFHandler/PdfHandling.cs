using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Politexniki_Client.Model;
using System.Collections.ObjectModel;

namespace Politexniki_Client.PDFHandler
{
    public class PdfHandling
    {
        private string _messageStatus;

        #region Handling Civil Engineer

        public string ExportAllCivilsInPDF(ObservableCollection<ModelView.CivilModelView> listCivilEngineer)
        {
            try
            {
                System.IO.FileStream fs = new FileStream(@"C:\Politexniki\PDF Export\" + "CivilEngineers_1" + ".pdf", FileMode.Create);

                Document document = new Document(PageSize.A4, 25, 25, 30, 1);
                PdfWriter writer = PdfWriter.GetInstance(document, fs);

                // Open the document to enable you to write to the document
                document.Open();

                // Makes it possible to add text to a specific place in the document using 
                // a X & Y placement syntax.
                PdfContentByte cb = writer.DirectContent;
                // Add a footer template to the document
                cb.AddTemplate(PdfFooter(cb), 30, 1);

                // Add a logo to the invoice
                iTextSharp.text.Image png = iTextSharp.text.Image.GetInstance(@"C:\Users\akech\source\Workspaces\Politexniki\BuildProcessTemplates\Politexniki\Politexniki_Client\Icons\plotexniki_logo.png");
                png.ScaleAbsolute(200, 55);
                png.SetAbsolutePosition(40, 750);
                cb.AddImage(png);

                // First we must activate writing
                cb.BeginText();

                // First we write out the header information

                // Start with the invoice type header
                writeText(cb, "Λίστα Μηχανικών", 350, 800, f_cb, 14);
                // HEader details; invoice number, invoice date, due date and customer Id
                writeText(cb, "Ποσότητα Μηχανικών", 350, 780, f_cb, 10);
                writeText(cb, listCivilEngineer.Count.ToString(), 480, 780, f_cn, 10);
                writeText(cb, "Ημερομηνία", 350, 760, f_cb, 10);
                writeText(cb, DateTime.Now.ToShortDateString(), 480, 760, f_cn, 10);
                cb.EndText();

                // Delivery address details
                int left_margin = 40;
                int top_margin = 720;

                // Separate the header from the rows with a line
                // Draw a line by setting the line width and position
                cb.SetLineWidth(0f);
                cb.MoveTo(40, 750);
                cb.LineTo(560, 750);
                cb.Stroke();
                // Don't forget to call the BeginText() method when done doing graphics!
                cb.BeginText();

                // Before we write the lines, it's good to assign a "last position to write"
                // variable to validate against if we need to make a page break while outputting.
                // Change it to 510 to write to test a page break; the fourth line on a new page
                int lastwriteposition = 100;

                // Loop thru the rows in the rows table
                // Start by writing out the line headers
                top_margin = 730;
                left_margin = 40;
                // Line headers
                writeText(cb, "'Ονομα και Επίθετο", left_margin, top_margin, f_cb, 10);
                writeText(cb, "Ειδικότητα", left_margin + 150, top_margin, f_cb, 10);
                //cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "Qty", left_margin + 415, top_margin, 0);
                writeText(cb, "Τηλέφωνο", left_margin + 320, top_margin, f_cb, 10);
                //cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "Price", left_margin + 495, top_margin, 0);
                //writeText(cb, "Curr", left_margin + 500, top_margin, f_cb, 10);

                // First item line position starts here
                top_margin = 700;

                // Loop thru the table of items and set the linespacing to 12 points.
                // Note that we use the -= operator, the coordinates goes from the bottom of the page!
                foreach (ModelView.CivilModelView engineer in listCivilEngineer)
                {
                    writeText(cb, engineer.CivilFirstName + " " + engineer.CivilLastName, left_margin, top_margin, f_cn, 10);
                    writeText(cb, engineer.Speciality, left_margin + 150, top_margin, f_cn, 10);
                    //cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, drItem["invoicedQuantity"].ToString(), left_margin + 415, top_margin, 0);
                    writeText(cb, engineer.CivilTele, left_margin + 320, top_margin, f_cn, 10);
                    //cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, drItem["price"].ToString(), left_margin + 495, top_margin, 0);
                    //writeText(cb, drItem["currency"].ToString(), left_margin + 500, top_margin, f_cn, 10);

                    // This is the line spacing, if you change the font size, you might want to change this as well.
                    top_margin -= 12;

                    // Implement a page break function, checking if the write position has reached the lastwriteposition
                    if (top_margin <= lastwriteposition)
                    {
                        // We need to end the writing before we change the page
                        cb.EndText();
                        // Make the page break
                        document.NewPage();
                        // Start the writing again
                        cb.BeginText();
                        // Assign the new write location on page two!
                        // Here you might want to implement a new header function for the new page
                        top_margin = 780;
                    }
                }

                // Okay, write out the totals table
                // Here you might want to do some page break scenarios, as well:
                // Example:
                // Calculate how many rows you are about to print and see if they fit before the lastwriteposition, 
                // then decide how to do; write some on first page, then the rest on second page or perhaps all the 
                // total lines after the page break.
                // We are not doing this here, we just write them out 80 points below the last writed item row

                //top_margin -= 80;
                //left_margin = 350;
                // End the writing of text
                cb.EndText();
                // Close the document, the writer and the filestream!
                document.Close();
                writer.Close();
                fs.Close();

                _messageStatus = "Η εξαγωγή ολοκληρώθηκε.";
            }
            catch (Exception e)
            {
                _messageStatus = e.Message;
            }

            return _messageStatus;
        }

        public string ExportSelectedCivilInPDF(ObservableCollection<ModelView.CivilModelView> listCivilEngineer)
        {
            try
            {
                System.IO.FileStream fs = new FileStream(@"C:\Politexniki\PDF Export\" + listCivilEngineer[0].CivilFirstName.ToString().ToUpper() + " " + listCivilEngineer[0].CivilLastName.ToString().ToUpper() + ".pdf", FileMode.Create);

                Document document = new Document(PageSize.A4, 25, 25, 30, 1);
                PdfWriter writer = PdfWriter.GetInstance(document, fs);

                // Open the document to enable you to write to the document
                document.Open();

                // Makes it possible to add text to a specific place in the document using 
                // a X & Y placement syntax.
                PdfContentByte cb = writer.DirectContent;
                // Add a footer template to the document
                cb.AddTemplate(PdfFooter(cb), 30, -5);

                // Add a logo to the invoice
                iTextSharp.text.Image png = iTextSharp.text.Image.GetInstance(@"C:\Users\akech\source\Workspaces\Politexniki\BuildProcessTemplates\Politexniki\Politexniki_Client\Icons\plotexniki_logo.png");
                png.ScaleAbsolute(200, 55);
                png.SetAbsolutePosition(40, 750);
                cb.AddImage(png);

                // First we must activate writing
                cb.BeginText();

                // First we write out the header information

                // Start with the invoice type header
                writeText(cb, "Λίστα Μηχανικών", 350, 800, f_cb, 14);
                // HEader details; invoice number, invoice date, due date and customer Id
                writeText(cb, "Ποσότητα Μηχανικών", 350, 780, f_cb, 10);
                writeText(cb, listCivilEngineer.Count.ToString(), 480, 780, f_cn, 10);
                writeText(cb, "Ημερομηνία", 350, 760, f_cb, 10);
                writeText(cb, DateTime.Now.ToShortDateString(), 480, 760, f_cn, 10);
                cb.EndText();

                // Delivery address details
                int left_margin = 40;
                int top_margin = 720;

                // Separate the header from the rows with a line
                // Draw a line by setting the line width and position
                cb.SetLineWidth(0f);
                cb.MoveTo(40, 750);
                cb.LineTo(560, 750);
                cb.Stroke();
                // Don't forget to call the BeginText() method when done doing graphics!
                cb.BeginText();

                // Before we write the lines, it's good to assign a "last position to write"
                // variable to validate against if we need to make a page break while outputting.
                // Change it to 510 to write to test a page break; the fourth line on a new page
                int lastwriteposition = 100;

                // Loop thru the rows in the rows table
                // Start by writing out the line headers
                top_margin = 730;
                left_margin = 190;
                // Line headers
                writeText(cb, "'Ονομα :", left_margin, top_margin, f_cb, 10);
                writeText(cb, "Επίθετο :", left_margin, top_margin - 20, f_cb, 10);
                writeText(cb, "Ειδικότητα :", left_margin , top_margin - 40, f_cb, 10);
                writeText(cb, "Αρ. Μητρώου ΤΕΕ :", left_margin, top_margin - 60, f_cb, 10);
                writeText(cb, "ΑΦΜ :", left_margin, top_margin - 80, f_cb, 10);
                writeText(cb, "ΔΟΥ :", left_margin, top_margin - 100, f_cb, 10);
                writeText(cb, "Τηλέφωνο :", left_margin, top_margin - 120, f_cb, 10);
                writeText(cb, "Τηλέφωνο :", left_margin, top_margin - 140, f_cb, 10);
                writeText(cb, "Αριθ. δελτ. ταυτότητας :", left_margin, top_margin - 160, f_cb, 10);
                writeText(cb, "Νομός :", left_margin, top_margin - 180, f_cb, 10);
                writeText(cb, "Δήμος :", left_margin, top_margin - 200, f_cb, 10);
                writeText(cb, "Τόπος κατοικίας :", left_margin, top_margin - 220, f_cb, 10);
                writeText(cb, "Οδός :", left_margin, top_margin - 240, f_cb, 10);
                writeText(cb, "Αριθμός :", left_margin, top_margin - 260, f_cb, 10);
                writeText(cb, "Τ.Κ. :", left_margin, top_margin - 280, f_cb, 10);

                // First item line position starts here
                top_margin = 730;
                left_margin = 360;
                // Loop thru the table of items and set the linespacing to 12 points.
                // Note that we use the -= operator, the coordinates goes from the bottom of the page!
                foreach (ModelView.CivilModelView engineer in listCivilEngineer)
                {
                    writeText(cb, engineer.CivilFirstName , left_margin, top_margin, f_cn, 10);
                    writeText(cb, engineer.CivilLastName, left_margin, top_margin - 20, f_cn, 10);
                    writeText(cb, engineer.Speciality, left_margin, top_margin - 40, f_cn, 10);
                    writeText(cb, engineer.CivilTele, left_margin, top_margin - 60, f_cn, 10);
                    writeText(cb, engineer.NumberTEE, left_margin, top_margin - 80, f_cn, 10);
                    writeText(cb, engineer.CivilAFM, left_margin, top_margin - 100, f_cn, 10);
                    writeText(cb, engineer.CivilDOY, left_margin, top_margin - 120, f_cn, 10);
                    writeText(cb, engineer.CivilTele, left_margin, top_margin - 140, f_cn, 10);
                    writeText(cb, engineer.CivilNumberID, left_margin, top_margin - 160, f_cn, 10);
                    writeText(cb, engineer.Nomos, left_margin, top_margin - 180, f_cn, 10);
                    writeText(cb, engineer.CivilMunicipality, left_margin, top_margin - 200, f_cn, 10);
                    writeText(cb, engineer.PlaceOfHouse, left_margin, top_margin - 220, f_cn, 10);
                    writeText(cb, engineer.CivilAddress, left_margin, top_margin - 240, f_cn, 10);
                    writeText(cb, engineer.CivilNumber, left_margin, top_margin - 260, f_cn, 10);
                    writeText(cb, engineer.CivilPostCode, left_margin, top_margin - 280, f_cn, 10);



                    // This is the line spacing, if you change the font size, you might want to change this as well.
                    top_margin -= 12;

                    // Implement a page break function, checking if the write position has reached the lastwriteposition
                    if (top_margin <= lastwriteposition)
                    {
                        // We need to end the writing before we change the page
                        cb.EndText();
                        // Make the page break
                        document.NewPage();
                        // Start the writing again
                        cb.BeginText();
                        // Assign the new write location on page two!
                        // Here you might want to implement a new header function for the new page
                        top_margin = 780;
                    }
                }

                // End the writing of text
                cb.EndText();
                // Close the document, the writer and the filestream!
                document.Close();
                writer.Close();
                fs.Close();

                _messageStatus = "Η εξαγωγή ολοκληρώθηκε.";
            }
            catch (Exception e)
            {
                _messageStatus = e.Message;
            }

            return _messageStatus;
        }

        #endregion

        #region Customer Pdf

        public string ExportAllCustomersInPDF(ObservableCollection<ModelView.CustomerModelView> listCustomer)
        {
            try
            {
                System.IO.FileStream fs = new FileStream(@"C:\Politexniki\PDF Export\" + "Customers_1" + ".pdf", FileMode.Create);

                Document document = new Document(PageSize.A4, 25, 25, 30, 1);
                PdfWriter writer = PdfWriter.GetInstance(document, fs);

                // Open the document to enable you to write to the document
                document.Open();

                // Makes it possible to add text to a specific place in the document using 
                // a X & Y placement syntax.
                PdfContentByte cb = writer.DirectContent;
                // Add a footer template to the document
                cb.AddTemplate(PdfFooter(cb), 30, 1);

                // Add a logo to the invoice
                iTextSharp.text.Image png = iTextSharp.text.Image.GetInstance(@"C:\Users\akech\source\Workspaces\Politexniki\BuildProcessTemplates\Politexniki\Politexniki_Client\Icons\plotexniki_logo.png");
                png.ScaleAbsolute(200, 55);
                png.SetAbsolutePosition(40, 750);
                cb.AddImage(png);

                // First we must activate writing
                cb.BeginText();

                // First we write out the header information

                // Start with the invoice type header
                writeText(cb, "Λίστα Πελατών", 350, 800, f_cb, 14);
                // HEader details; invoice number, invoice date, due date and customer Id
                writeText(cb, "Ποσότητα Πελατών", 350, 780, f_cb, 10);
                writeText(cb, listCustomer.Count.ToString(), 480, 780, f_cn, 10);
                writeText(cb, "Ημερομηνία", 350, 760, f_cb, 10);
                writeText(cb, DateTime.Now.ToShortDateString(), 480, 760, f_cn, 10);
                cb.EndText();

                // Delivery address details
                int left_margin = 40;
                int top_margin = 720;

                // Separate the header from the rows with a line
                // Draw a line by setting the line width and position
                cb.SetLineWidth(0f);
                cb.MoveTo(40, 750);
                cb.LineTo(560, 750);
                cb.Stroke();
                // Don't forget to call the BeginText() method when done doing graphics!
                cb.BeginText();

                // Before we write the lines, it's good to assign a "last position to write"
                // variable to validate against if we need to make a page break while outputting.
                // Change it to 510 to write to test a page break; the fourth line on a new page
                int lastwriteposition = 100;

                // Loop thru the rows in the rows table
                // Start by writing out the line headers
                top_margin = 730;
                left_margin = 40;
                // Line headers
                writeText(cb, "'Ονομα και Επίθετο", left_margin, top_margin, f_cb, 10);
                writeText(cb, "Διεύθηνση", left_margin + 150, top_margin, f_cb, 10);
                //cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "Qty", left_margin + 415, top_margin, 0);
                writeText(cb, "Τηλέφωνο", left_margin + 320, top_margin, f_cb, 10);
                //cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "Price", left_margin + 495, top_margin, 0);
                //writeText(cb, "Curr", left_margin + 500, top_margin, f_cb, 10);

                // First item line position starts here
                top_margin = 700;

                // Loop thru the table of items and set the linespacing to 12 points.
                // Note that we use the -= operator, the coordinates goes from the bottom of the page!
                foreach (ModelView.CustomerModelView customer in listCustomer)
                {
                    writeText(cb, customer.FirstName + " " + customer.LastName, left_margin, top_margin, f_cn, 10);
                    writeText(cb, customer.Address, left_margin + 150, top_margin, f_cn, 10);
                    //cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, drItem["invoicedQuantity"].ToString(), left_margin + 415, top_margin, 0);
                    writeText(cb, customer.Telephone, left_margin + 320, top_margin, f_cn, 10);
                    //cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, drItem["price"].ToString(), left_margin + 495, top_margin, 0);
                    //writeText(cb, drItem["currency"].ToString(), left_margin + 500, top_margin, f_cn, 10);

                    // This is the line spacing, if you change the font size, you might want to change this as well.
                    top_margin -= 12;

                    // Implement a page break function, checking if the write position has reached the lastwriteposition
                    if (top_margin <= lastwriteposition)
                    {
                        // We need to end the writing before we change the page
                        cb.EndText();
                        // Make the page break
                        document.NewPage();
                        // Start the writing again
                        cb.BeginText();
                        // Assign the new write location on page two!
                        // Here you might want to implement a new header function for the new page
                        top_margin = 780;
                    }
                }

                // Okay, write out the totals table
                // Here you might want to do some page break scenarios, as well:
                // Example:
                // Calculate how many rows you are about to print and see if they fit before the lastwriteposition, 
                // then decide how to do; write some on first page, then the rest on second page or perhaps all the 
                // total lines after the page break.
                // We are not doing this here, we just write them out 80 points below the last writed item row

                //top_margin -= 80;
                //left_margin = 350;
                // End the writing of text
                cb.EndText();
                // Close the document, the writer and the filestream!
                document.Close();
                writer.Close();
                fs.Close();

                _messageStatus = "Η εξαγωγή ολοκληρώθηκε.";
            }
            catch (Exception e)
            {
                _messageStatus = e.Message;
            }

            return _messageStatus;
        }

        public string ExportSelectedCustomerInPDF(ObservableCollection<ModelView.CivilModelView> listCivilEngineer)
        {
            try
            {
                System.IO.FileStream fs = new FileStream(@"C:\Politexniki\PDF Export\" + listCivilEngineer[0].CivilFirstName.ToString().ToUpper() + " " + listCivilEngineer[0].CivilLastName.ToString().ToUpper() + ".pdf", FileMode.Create);

                Document document = new Document(PageSize.A4, 25, 25, 30, 1);
                PdfWriter writer = PdfWriter.GetInstance(document, fs);

                // Open the document to enable you to write to the document
                document.Open();

                // Makes it possible to add text to a specific place in the document using 
                // a X & Y placement syntax.
                PdfContentByte cb = writer.DirectContent;
                // Add a footer template to the document
                cb.AddTemplate(PdfFooter(cb), 30, -5);

                // Add a logo to the invoice
                iTextSharp.text.Image png = iTextSharp.text.Image.GetInstance(@"C:\Users\akech\source\Workspaces\Politexniki\BuildProcessTemplates\Politexniki\Politexniki_Client\Icons\plotexniki_logo.png");
                png.ScaleAbsolute(200, 55);
                png.SetAbsolutePosition(40, 750);
                cb.AddImage(png);

                // First we must activate writing
                cb.BeginText();

                // First we write out the header information

                // Start with the invoice type header
                writeText(cb, "Λίστα Μηχανικών", 350, 800, f_cb, 14);
                // HEader details; invoice number, invoice date, due date and customer Id
                writeText(cb, "Ποσότητα Μηχανικών", 350, 780, f_cb, 10);
                writeText(cb, listCivilEngineer.Count.ToString(), 480, 780, f_cn, 10);
                writeText(cb, "Ημερομηνία", 350, 760, f_cb, 10);
                writeText(cb, DateTime.Now.ToShortDateString(), 480, 760, f_cn, 10);
                cb.EndText();

                // Delivery address details
                int left_margin = 40;
                int top_margin = 720;

                // Separate the header from the rows with a line
                // Draw a line by setting the line width and position
                cb.SetLineWidth(0f);
                cb.MoveTo(40, 750);
                cb.LineTo(560, 750);
                cb.Stroke();
                // Don't forget to call the BeginText() method when done doing graphics!
                cb.BeginText();

                // Before we write the lines, it's good to assign a "last position to write"
                // variable to validate against if we need to make a page break while outputting.
                // Change it to 510 to write to test a page break; the fourth line on a new page
                int lastwriteposition = 100;

                // Loop thru the rows in the rows table
                // Start by writing out the line headers
                top_margin = 730;
                left_margin = 190;
                // Line headers
                writeText(cb, "'Ονομα :", left_margin, top_margin, f_cb, 10);
                writeText(cb, "Επίθετο :", left_margin, top_margin - 20, f_cb, 10);
                writeText(cb, "Ειδικότητα :", left_margin, top_margin - 40, f_cb, 10);
                writeText(cb, "Αρ. Μητρώου ΤΕΕ :", left_margin, top_margin - 60, f_cb, 10);
                writeText(cb, "ΑΦΜ :", left_margin, top_margin - 80, f_cb, 10);
                writeText(cb, "ΔΟΥ :", left_margin, top_margin - 100, f_cb, 10);
                writeText(cb, "Τηλέφωνο :", left_margin, top_margin - 120, f_cb, 10);
                writeText(cb, "Τηλέφωνο :", left_margin, top_margin - 140, f_cb, 10);
                writeText(cb, "Αριθ. δελτ. ταυτότητας :", left_margin, top_margin - 160, f_cb, 10);
                writeText(cb, "Νομός :", left_margin, top_margin - 180, f_cb, 10);
                writeText(cb, "Δήμος :", left_margin, top_margin - 200, f_cb, 10);
                writeText(cb, "Τόπος κατοικίας :", left_margin, top_margin - 220, f_cb, 10);
                writeText(cb, "Οδός :", left_margin, top_margin - 240, f_cb, 10);
                writeText(cb, "Αριθμός :", left_margin, top_margin - 260, f_cb, 10);
                writeText(cb, "Τ.Κ. :", left_margin, top_margin - 280, f_cb, 10);

                // First item line position starts here
                top_margin = 730;
                left_margin = 360;
                // Loop thru the table of items and set the linespacing to 12 points.
                // Note that we use the -= operator, the coordinates goes from the bottom of the page!
                foreach (ModelView.CivilModelView engineer in listCivilEngineer)
                {
                    writeText(cb, engineer.CivilFirstName, left_margin, top_margin, f_cn, 10);
                    writeText(cb, engineer.CivilLastName, left_margin, top_margin - 20, f_cn, 10);
                    writeText(cb, engineer.Speciality, left_margin, top_margin - 40, f_cn, 10);
                    writeText(cb, engineer.CivilTele, left_margin, top_margin - 60, f_cn, 10);
                    writeText(cb, engineer.NumberTEE, left_margin, top_margin - 80, f_cn, 10);
                    writeText(cb, engineer.CivilAFM, left_margin, top_margin - 100, f_cn, 10);
                    writeText(cb, engineer.CivilDOY, left_margin, top_margin - 120, f_cn, 10);
                    writeText(cb, engineer.CivilTele, left_margin, top_margin - 140, f_cn, 10);
                    writeText(cb, engineer.CivilNumberID, left_margin, top_margin - 160, f_cn, 10);
                    writeText(cb, engineer.Nomos, left_margin, top_margin - 180, f_cn, 10);
                    writeText(cb, engineer.CivilMunicipality, left_margin, top_margin - 200, f_cn, 10);
                    writeText(cb, engineer.PlaceOfHouse, left_margin, top_margin - 220, f_cn, 10);
                    writeText(cb, engineer.CivilAddress, left_margin, top_margin - 240, f_cn, 10);
                    writeText(cb, engineer.CivilNumber, left_margin, top_margin - 260, f_cn, 10);
                    writeText(cb, engineer.CivilPostCode, left_margin, top_margin - 280, f_cn, 10);



                    // This is the line spacing, if you change the font size, you might want to change this as well.
                    top_margin -= 12;

                    // Implement a page break function, checking if the write position has reached the lastwriteposition
                    if (top_margin <= lastwriteposition)
                    {
                        // We need to end the writing before we change the page
                        cb.EndText();
                        // Make the page break
                        document.NewPage();
                        // Start the writing again
                        cb.BeginText();
                        // Assign the new write location on page two!
                        // Here you might want to implement a new header function for the new page
                        top_margin = 780;
                    }
                }

                // End the writing of text
                cb.EndText();
                // Close the document, the writer and the filestream!
                document.Close();
                writer.Close();
                fs.Close();

                _messageStatus = "Η εξαγωγή ολοκληρώθηκε.";
            }
            catch (Exception e)
            {
                _messageStatus = e.Message;
            }

            return _messageStatus;
        }

        #endregion

        #region PDF Helper

        /// <summary>
        /// Properties in order to write the Greek letters
        /// </summary>
        BaseFont f_cb = BaseFont.CreateFont("c:\\windows\\fonts\\calibrib.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        BaseFont f_cn = BaseFont.CreateFont("c:\\windows\\fonts\\calibri.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

        // This is the method writing text to the content byte
        private void writeText(PdfContentByte cb, string Text, int X, int Y, BaseFont font, int Size)
        {
            cb.SetFontAndSize(font, Size);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Text, X, Y, 0);
        }

        private PdfTemplate PdfFooter(PdfContentByte cb)
        {
            // Create the template and assign height
            PdfTemplate tmpFooter = cb.CreateTemplate(580, 70);
            // Move to the bottom left corner of the template
            tmpFooter.MoveTo(1, 1);
            // Place the footer content
            tmpFooter.Stroke();
            // Begin writing the footer
            tmpFooter.BeginText();
            // Set the font and size
            tmpFooter.SetFontAndSize(f_cn, 8);
            // Write out details from the payee table
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Politexniki Ltd", 0, 53, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Thessaloniki", 0, 45, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Menemeni", 0, 37, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "20", 0, 29, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "56123" + " " + "Thessaloniki" + " " + "Greece", 0, 21, 0);
            // Bold text for ther headers
            tmpFooter.SetFontAndSize(f_cb, 8);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Phone", 215, 53, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Mail", 215, 45, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Web", 215, 37, 0);
            //tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Legal info", 400, 53, 0);
            // Regular text for infomation fields
            tmpFooter.SetFontAndSize(f_cn, 8);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "456155616", 265, 53, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "politexniki@gmail.com", 265, 45, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "www.politexniki.gr", 265, 37, 0);
            // End text
            tmpFooter.EndText();
            // Stamp a line above the page footer
            cb.SetLineWidth(0f);
            cb.MoveTo(30, 60);
            cb.LineTo(560, 60);
            cb.Stroke();
            // Return the footer template
            return tmpFooter;
        }

        #endregion

    }
}
