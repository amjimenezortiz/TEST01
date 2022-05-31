using ClosedXML.Excel;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using HtmlToOpenXml;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using KUNAK.VMS.API.Interfaces;
using KUNAK.VMS.CORE.Exceptions;
using KUNAK.VMS.CORE.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net;

namespace KUNAK.GAP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ReportController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IBlobManagement _blobManagement;
        private readonly IReport _report;
        private readonly IConfiguration _configuration;
        private readonly IValidateUserPermissions _validationUserPermissions;
        public ReportController(ICompanyService companyService,IBlobManagement blobManagement, IReport report, 
            IConfiguration configuration, IValidateUserPermissions validateUserPermissions)
        {
            _companyService = companyService;
            _blobManagement = blobManagement;
            _report = report;
            _configuration = configuration;
            _validationUserPermissions = validateUserPermissions;
        }

        [HttpGet("Word")]
        public ActionResult Word()
        {
            try
            {
                //obtenemos todos los datos que se van a necesitar para lelnar el informe
                var companyG = _companyService.GetCompany(2);
                var kunak = _companyService.GetByRucSync("20546970826");
                //----------------------------------------------------

                MemoryStream ms = new();
                WordprocessingDocument word = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document, true);

                // Add a main document part. 
                MainDocumentPart mainPart = word.AddMainDocumentPart();

                // Create the document structure and add some text.
                mainPart.Document = new DocumentFormat.OpenXml.Wordprocessing.Document();
                Body body = mainPart.Document.AppendChild(new Body());

                //new code to support orientation and margins
                SectionProperties sectProp = new();
                //DocumentFormat.OpenXml.Wordprocessing.PageSize pageSize = new DocumentFormat.OpenXml.Wordprocessing.PageSize() { Width = 16838U, Height = 11906U, Orient = PageOrientationValues.Landscape };
                PageMargin pageMargin = new() { Top = 2300, Right = 720, Bottom = 720, Left = 720 };

                //sectProp.Append(pageSize);
                sectProp.Append(pageMargin);




                //START BODY

                //HEADER
                var tableHeader = new DocumentFormat.OpenXml.Wordprocessing.Table();
                var tableHEWidth = new TableWidth() { Width = "5000", Type = TableWidthUnitValues.Pct };
                var borderHEColor = "000000";

                var tableHEProperties = new TableProperties();
                var tableHEBorders = new TableBorders();

                var topBorderHE = new TopBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Thick),
                    Color = borderHEColor
                };

                var bottomBorderHE = new BottomBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Thick),
                    Color = borderHEColor
                };

                var rightBorderHE = new RightBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Thick),
                    Color = borderHEColor
                };

                var leftBorderHE = new LeftBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Thick),
                    Color = borderHEColor
                };

                var insideHorizontalBorderHE = new InsideHorizontalBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Thick),
                    Color = borderHEColor
                };

                var insideVerticalBorderHE = new InsideVerticalBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Thick),
                    Color = borderHEColor
                };

                tableHEBorders.AppendChild(topBorderHE);
                tableHEBorders.AppendChild(bottomBorderHE);
                tableHEBorders.AppendChild(rightBorderHE);
                tableHEBorders.AppendChild(leftBorderHE);
                tableHEBorders.AppendChild(insideHorizontalBorderHE);
                tableHEBorders.AppendChild(insideVerticalBorderHE);

                tableHEProperties.Append(tableHEWidth);
                tableHEProperties.AppendChild(tableHEBorders);

                tableHeader.AppendChild(tableHEProperties);

                //contenido de la tabla
                var row1H = new TableRow();
                var row1HProperties = new TableRowProperties();
                var row1HHeight = new TableRowHeight() { Val = (UInt32Value)210U, HeightType = HeightRuleValues.Exact };
                row1HProperties.Append(row1HHeight);
                row1H.Append(row1HProperties);
                //---------------------
                ////imagen
                //DocumentFormat.OpenXml.Wordprocessing.Paragraph pImgHeader = new();
                //ImagePart imagePartHeader = mainPart.AddImagePart(ImagePartType.Icon);
                //string imgPathHeader = _blobManagement.GetLink(kunak.Ruc, kunak.Logo);
                //var reqHeader = WebRequest.Create(imgPathHeader);
                //var respHeader = reqHeader.GetResponse();
                //imagePartHeader.FeedData(respHeader.GetResponseStream());

                //// 1500000 and 1092000 are img width and height
                //Run rImgHeader = new(_report.DrawingManager(mainPart.GetIdOfPart(imagePartHeader), "PictureName", 500000, 500000, string.Empty));
                //pImgHeader.Append(rImgHeader);
                //---
                var cell11H = new TableCell
                {
                    TableCellProperties = new TableCellProperties()
                };
                cell11H.TableCellProperties.VerticalMerge = new VerticalMerge { Val = MergedCellValues.Restart };
                cell11H.TableCellProperties.TableCellVerticalAlignment = new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Center };
                var paragraph11H = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                var paragraphProperties11H = new ParagraphProperties
                {
                    Justification = new Justification() { Val = JustificationValues.Center }
                };
                paragraph11H.Append(paragraphProperties11H);
                var run11H = new Run();
                var runProperties11H = new RunProperties
                {
                    FontSize = new FontSize() { Val = "16" },
                    // Always add properties first
                    Bold = new Bold()
                };
                run11H.Append(runProperties11H);
                run11H.Append(new DocumentFormat.OpenXml.Wordprocessing.Text("RISK MANAGEMENT"));
                paragraph11H.Append(run11H);
                //------------
                cell11H.Append(paragraph11H);
                //_report.AddImageToCell(cell11H, mainPart.GetIdOfPart(imagePartHeader));
                row1H.Append(cell11H);
                //-----------
                cell11H = new TableCell
                {
                    TableCellProperties = new TableCellProperties()
                };
                cell11H.TableCellProperties.TableCellVerticalAlignment = new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Center };
                cell11H.TableCellProperties.VerticalMerge = new VerticalMerge { Val = MergedCellValues.Restart };
                paragraph11H = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                paragraphProperties11H = new ParagraphProperties
                {
                    Justification = new Justification() { Val = JustificationValues.Center }
                };
                paragraph11H.Append(paragraphProperties11H);
                run11H = new Run();
                runProperties11H = new RunProperties
                {
                    FontSize = new FontSize() { Val = "16" },
                    //Bold = new Bold()
                };
                run11H.Append(runProperties11H);
                run11H.Append(new DocumentFormat.OpenXml.Wordprocessing.Text("Servicio de Ethical Hacking – EG 2021"));
                paragraph11H.Append(run11H);
                //--
                var paragraph12H = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                var paragraphProperties12H = new ParagraphProperties
                {
                    Justification = new Justification() { Val = JustificationValues.Center }
                };
                paragraph12H.Append(paragraphProperties12H);
                var run12H = new Run();
                var runProperties12H = new RunProperties
                {
                    FontSize = new FontSize() { Val = "16" },
                    //Bold = new Bold()
                };
                run12H.Append(runProperties12H);
                run12H.Append(new DocumentFormat.OpenXml.Wordprocessing.Text("Servicio de Ethical Hacking – EG 2021"));
                paragraph12H.Append(run12H);
                //--
                //var paragraph12H = new DocumentFormat.OpenXml.Wordprocessing.Paragraph(new Run(new DocumentFormat.OpenXml.Wordprocessing.Text("Servicio de Ethical Hacking – EG 2021")));
                cell11H.Append(paragraph11H, paragraph12H);
                row1H.Append(cell11H);
                //---------------
                cell11H = new TableCell
                {
                    TableCellProperties = new TableCellProperties()
                };
                cell11H.TableCellProperties.TableCellVerticalAlignment = new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Center };
                paragraph11H = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                paragraphProperties11H = new ParagraphProperties
                {
                    Justification = new Justification() { Val = JustificationValues.Center }
                };
                paragraph11H.Append(paragraphProperties11H);
                run11H = new Run();
                runProperties11H = new RunProperties
                {
                    FontSize = new FontSize() { Val = "18" }
                };
                run11H.Append(runProperties11H);
                run11H.Append(new DocumentFormat.OpenXml.Wordprocessing.Text("Código:"));
                paragraph11H.Append(run11H);
                cell11H.Append(paragraph11H);
                row1H.Append(cell11H);
                //------------
                cell11H = new TableCell
                {
                    TableCellProperties = new TableCellProperties()
                };
                cell11H.TableCellProperties.TableCellVerticalAlignment = new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Center };
                paragraph11H = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                paragraphProperties11H = new ParagraphProperties
                {
                    Justification = new Justification() { Val = JustificationValues.Center }
                };
                paragraph11H.Append(paragraphProperties11H);
                run11H = new Run();
                runProperties11H = new RunProperties
                {
                    FontSize = new FontSize() { Val = "18" }
                };
                run11H.Append(runProperties11H);
                run11H.Append(new DocumentFormat.OpenXml.Wordprocessing.Text("EH-ONPE-PT"));
                paragraph11H.Append(run11H);
                cell11H.Append(paragraph11H);
                row1H.Append(cell11H);
                //------------
                tableHeader.Append(row1H);
                //------------
                row1H = new TableRow();
                row1HProperties = new TableRowProperties();
                row1HHeight = new TableRowHeight() { Val = (UInt32Value)210U, HeightType = HeightRuleValues.Exact };
                row1HProperties.Append(row1HHeight);
                row1H.Append(row1HProperties);
                //---------------------
                cell11H = new TableCell
                {
                    TableCellProperties = new TableCellProperties()
                };
                cell11H.TableCellProperties.VerticalMerge = new VerticalMerge { Val = MergedCellValues.Continue };
                paragraph11H = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                run11H = new Run();
                run11H.Append(new DocumentFormat.OpenXml.Wordprocessing.Text(""));
                paragraph11H.Append(run11H);
                cell11H.Append(paragraph11H);
                row1H.Append(cell11H);
                //-----------
                cell11H = new TableCell
                {
                    TableCellProperties = new TableCellProperties()
                };
                cell11H.TableCellProperties.VerticalMerge = new VerticalMerge { Val = MergedCellValues.Continue };
                paragraph11H = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                run11H = new Run();
                run11H.Append(new DocumentFormat.OpenXml.Wordprocessing.Text(""));
                paragraph11H.Append(run11H);
                cell11H.Append(paragraph11H);
                row1H.Append(cell11H);
                //---------------
                cell11H = new TableCell
                {
                    TableCellProperties = new TableCellProperties()
                };
                cell11H.TableCellProperties.TableCellVerticalAlignment = new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Center };
                paragraph11H = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                paragraphProperties11H = new ParagraphProperties
                {
                    Justification = new Justification() { Val = JustificationValues.Center }
                };
                paragraph11H.Append(paragraphProperties11H);
                run11H = new Run();
                runProperties11H = new RunProperties
                {
                    FontSize = new FontSize() { Val = "18" },
                    //Bold = new Bold()
                };
                run11H.Append(runProperties11H);
                run11H.Append(new DocumentFormat.OpenXml.Wordprocessing.Text("Versión:"));
                paragraph11H.Append(run11H);
                cell11H.Append(paragraph11H);
                row1H.Append(cell11H);
                //------------
                cell11H = new TableCell
                {
                    TableCellProperties = new TableCellProperties()
                };
                cell11H.TableCellProperties.TableCellVerticalAlignment = new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Center };
                paragraph11H = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                paragraphProperties11H = new ParagraphProperties
                {
                    Justification = new Justification() { Val = JustificationValues.Center }
                };
                paragraph11H.Append(paragraphProperties11H);
                run11H = new Run();
                runProperties11H = new RunProperties
                {
                    FontSize = new FontSize() { Val = "18" },
                    //Bold = new Bold()
                };
                run11H.Append(runProperties11H);
                run11H.Append(new DocumentFormat.OpenXml.Wordprocessing.Text("2.0"));
                paragraph11H.Append(run11H);
                cell11H.Append(paragraph11H);
                row1H.Append(cell11H);
                //------------
                tableHeader.Append(row1H);
                //------------
                row1H = new TableRow();
                row1HProperties = new TableRowProperties();
                row1HHeight = new TableRowHeight() { Val = (UInt32Value)210U, HeightType = HeightRuleValues.Exact };
                row1HProperties.Append(row1HHeight);
                row1H.Append(row1HProperties);
                //---------------------
                cell11H = new TableCell
                {
                    TableCellProperties = new TableCellProperties()
                };
                cell11H.TableCellProperties.VerticalMerge = new VerticalMerge { Val = MergedCellValues.Continue };
                paragraph11H = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                run11H = new Run();
                run11H.Append(new DocumentFormat.OpenXml.Wordprocessing.Text(""));
                paragraph11H.Append(run11H);
                cell11H.Append(paragraph11H);
                row1H.Append(cell11H);
                //-----------
                cell11H = new TableCell
                {
                    TableCellProperties = new TableCellProperties()
                };
                cell11H.TableCellProperties.TableCellVerticalAlignment = new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Center };
                cell11H.TableCellProperties.VerticalMerge = new VerticalMerge { Val = MergedCellValues.Restart };
                paragraph11H = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                paragraphProperties11H = new ParagraphProperties
                {
                    Justification = new Justification() { Val = JustificationValues.Center }
                };
                paragraph11H.Append(paragraphProperties11H);
                run11H = new Run();
                runProperties11H = new RunProperties
                {
                    FontSize = new FontSize() { Val = "18" },
                    //Bold = new Bold()
                };
                run11H.Append(runProperties11H);
                run11H.Append(new DocumentFormat.OpenXml.Wordprocessing.Text("Plan de Trabajo"));
                paragraph11H.Append(run11H);
                cell11H.Append(paragraph11H);
                row1H.Append(cell11H);
                //---------------
                cell11H = new TableCell
                {
                    TableCellProperties = new TableCellProperties()
                };
                cell11H.TableCellProperties.TableCellVerticalAlignment = new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Center };
                paragraph11H = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                paragraphProperties11H = new ParagraphProperties
                {
                    Justification = new Justification() { Val = JustificationValues.Center }
                };
                paragraph11H.Append(paragraphProperties11H);
                run11H = new Run();
                runProperties11H = new RunProperties
                {
                    FontSize = new FontSize() { Val = "18" },
                    //Bold = new Bold()
                };
                run11H.Append(runProperties11H);
                run11H.Append(new DocumentFormat.OpenXml.Wordprocessing.Text("Fecha:"));
                paragraph11H.Append(run11H);
                cell11H.Append(paragraph11H);
                row1H.Append(cell11H);
                //------------
                cell11H = new TableCell
                {
                    TableCellProperties = new TableCellProperties()
                };
                cell11H.TableCellProperties.TableCellVerticalAlignment = new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Center };
                paragraph11H = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                paragraphProperties11H = new ParagraphProperties
                {
                    Justification = new Justification() { Val = JustificationValues.Center }
                };
                paragraph11H.Append(paragraphProperties11H);
                run11H = new Run();
                runProperties11H = new RunProperties
                {
                    FontSize = new FontSize() { Val = "18" },
                    //Bold = new Bold()
                };
                run11H.Append(runProperties11H);
                run11H.Append(new DocumentFormat.OpenXml.Wordprocessing.Text(DateTime.Now.ToString("d")));
                paragraph11H.Append(run11H);
                cell11H.Append(paragraph11H);
                row1H.Append(cell11H);
                //------------
                tableHeader.Append(row1H);
                //------------
                row1H = new TableRow();
                row1HProperties = new TableRowProperties();
                row1HHeight = new TableRowHeight() { Val = (UInt32Value)210U, HeightType = HeightRuleValues.Exact };
                row1HProperties.Append(row1HHeight);
                row1H.Append(row1HProperties);
                //---------------------
                cell11H = new TableCell
                {
                    TableCellProperties = new TableCellProperties()
                };
                cell11H.TableCellProperties.VerticalMerge = new VerticalMerge { Val = MergedCellValues.Continue };
                paragraph11H = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                run11H = new Run();
                run11H.Append(new DocumentFormat.OpenXml.Wordprocessing.Text(""));
                paragraph11H.Append(run11H);
                cell11H.Append(paragraph11H);
                row1H.Append(cell11H);
                //-----------
                cell11H = new TableCell
                {
                    TableCellProperties = new TableCellProperties()
                };
                cell11H.TableCellProperties.VerticalMerge = new VerticalMerge { Val = MergedCellValues.Continue };
                paragraph11H = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                run11H = new Run();
                run11H.Append(new DocumentFormat.OpenXml.Wordprocessing.Text(""));
                paragraph11H.Append(run11H);
                cell11H.Append(paragraph11H);
                row1H.Append(cell11H);
                //---------------
                cell11H = new TableCell
                {
                    TableCellProperties = new TableCellProperties()
                };
                cell11H.TableCellProperties.TableCellVerticalAlignment = new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Center };
                paragraph11H = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                paragraphProperties11H = new ParagraphProperties
                {
                    Justification = new Justification() { Val = JustificationValues.Center }
                };
                paragraph11H.Append(paragraphProperties11H);
                run11H = new Run();
                runProperties11H = new RunProperties
                {
                    FontSize = new FontSize() { Val = "18" },
                    //Bold = new Bold()
                };
                run11H.Append(runProperties11H);
                run11H.Append(new DocumentFormat.OpenXml.Wordprocessing.Text("Página:"));
                paragraph11H.Append(run11H);
                cell11H.Append(paragraph11H);
                row1H.Append(cell11H);
                //------------

                cell11H = new TableCell
                {
                    TableCellProperties = new TableCellProperties()
                };
                cell11H.TableCellProperties.TableCellVerticalAlignment = new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Center };
                paragraph11H = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                paragraphProperties11H = new ParagraphProperties
                {
                    Justification = new Justification() { Val = JustificationValues.Center }
                };
                paragraph11H.Append(paragraphProperties11H);
                run11H = new Run();
                runProperties11H = new RunProperties
                {
                    FontSize = new FontSize() { Val = "18" },
                    //Bold = new Bold()
                };
                run11H.Append(runProperties11H);
                run11H.Append(new DocumentFormat.OpenXml.Wordprocessing.Text("1"));
                paragraph11H.Append(run11H);
                cell11H.Append(paragraph11H);
                row1H.Append(cell11H);
                //------------
                tableHeader.Append(row1H);
                //------------

                //FOOTER
                //table footer
                var tableFooter = new DocumentFormat.OpenXml.Wordprocessing.Table();
                var tableFWidth = new TableWidth() { Width = "5000", Type = TableWidthUnitValues.Pct };
                var borderFColor = "000000";

                var tableFProperties = new TableProperties();
                var tableFBorders = new TableBorders();

                var topBorderF = new TopBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Thick),
                    Color = borderFColor
                };

                var bottomBorderF = new BottomBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Thick),
                    Color = borderFColor
                };

                var rightBorderF = new RightBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Thick),
                    Color = borderFColor
                };

                var leftBorderF = new LeftBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Thick),
                    Color = borderFColor
                };

                var insideHorizontalBorderF = new InsideHorizontalBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Thick),
                    Color = borderFColor
                };

                var insideVerticalBorderF = new InsideVerticalBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Thick),
                    Color = borderFColor
                };

                tableFBorders.AppendChild(topBorderF);
                tableFBorders.AppendChild(bottomBorderF);
                tableFBorders.AppendChild(rightBorderF);
                tableFBorders.AppendChild(leftBorderF);
                tableFBorders.AppendChild(insideHorizontalBorderF);
                tableFBorders.AppendChild(insideVerticalBorderF);

                tableFProperties.Append(tableFWidth);
                tableFProperties.AppendChild(tableFBorders);

                tableFooter.AppendChild(tableFProperties);

                //contenido de la tabla
                var row1F = new TableRow();
                var cell11F = new TableCell();
                //var cell11PropertiesF = new TableCellProperties();
                //var verticalMerge=new VerticalMerge();
                //verticalMerge.Val = VerticalMergeRevisionValues.Continue;
                var paragraph11F = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                var paragraphProperties11F = new ParagraphProperties
                {
                    Justification = new Justification() { Val = JustificationValues.Center }
                };
                paragraph11F.Append(paragraphProperties11F);
                var run11F = new Run();
                var runProperties11F = new RunProperties
                {
                    Bold = new Bold()
                };
                run11F.Append(runProperties11F);
                run11F.Append(new DocumentFormat.OpenXml.Wordprocessing.Text("RISK MANAGEMENT"));
                paragraph11F.Append(run11F);

                cell11F.Append(paragraph11F);
                row1F.Append(cell11F);

                tableFooter.Append(row1F);

                //-------------
                body.Append(
                        new SectionProperties(
                            new HeaderReference()
                            {
                                Type = HeaderFooterValues.First,
                                Id = "rId2"
                            },
                            new FooterReference()
                            {
                                Type = HeaderFooterValues.First,
                                Id = "rId3"
                            },
                            new HeaderReference()
                            {
                                Type = HeaderFooterValues.Even,
                                Id = "rId4"
                            },
                            new FooterReference()
                            {
                                Type = HeaderFooterValues.Even,
                                Id = "rId5"
                            },
                            new HeaderReference()
                            {
                                Type = HeaderFooterValues.Default,
                                Id = "rId6"
                            },
                            new FooterReference()
                            {
                                Type = HeaderFooterValues.Default,
                                Id = "rId7"
                            },
                            new PageMargin()
                            {
                                Top = 1440,
                                Right = (UInt32Value)1440UL,
                                Bottom = 1440,
                                Left = (UInt32Value)1440UL,
                                Header = (UInt32Value)720UL,
                                Footer = (UInt32Value)720UL,
                                Gutter = (UInt32Value)0UL
                            },
                            new TitlePage()
                        ));

                var header = new Header();
                header.Append(tableHeader);
                var footer = new Footer();
                footer.Append(tableFooter);

                var documentSettingsPart =
                    mainPart.AddNewPart
                    <DocumentSettingsPart>("rId1");

                _report.GenerateDocumentSettingsPart().Save(documentSettingsPart);

                var firstPageHeaderPart =
                    mainPart.AddNewPart<HeaderPart>("rId2");

                _report.GeneratePageHeaderPart(
                    "").Save(firstPageHeaderPart);

                var firstPageFooterPart =
                    mainPart.AddNewPart<FooterPart>("rId3");

                _report.GeneratePageFooterPart(
                    "").Save(firstPageFooterPart);

                var evenPageHeaderPart =
                    mainPart.AddNewPart<HeaderPart>("rId4");

                header.Save(evenPageHeaderPart);

                var evenPageFooterPart =
                    mainPart.AddNewPart<FooterPart>("rId5");

                footer.Save(evenPageFooterPart);

                var oddPageheaderPart =
                    mainPart.AddNewPart<HeaderPart>("rId6");

                header.Save(oddPageheaderPart);

                var oddPageFooterPart =
                    mainPart.AddNewPart<FooterPart>("rId7");

                footer.Save(oddPageFooterPart);
                //END FOOTER



                //CARÁTULA
                // imagen
                DocumentFormat.OpenXml.Wordprocessing.Paragraph pImg = new();
                ImagePart imagePart = mainPart.AddImagePart(ImagePartType.Jpeg);
                string imgPath = _blobManagement.GetLink(kunak.Ruc, kunak.Logo);
                var req = WebRequest.Create(imgPath);
                var resp = req.GetResponse();
                imagePart.FeedData(resp.GetResponseStream());

                // 1500000 and 1092000 are img width and height
                Run rImg = new(_report.DrawingManager(mainPart.GetIdOfPart(imagePart), "PictureName", 3059125, 1853470 , string.Empty));
                pImg.Append(rImg);
                body.Append(pImg);
                //----------
                DocumentFormat.OpenXml.Wordprocessing.Paragraph title = new();
                ParagraphProperties titleP = new();
                Justification titleJ = new() { Val = JustificationValues.Center };
                titleP.SpacingBetweenLines = new SpacingBetweenLines()
                {
                    Before = "100"
                };
                titleP.Append(titleJ);
                title.Append(titleP);
                Run titleR = new();
                RunProperties titleRP = new();
                titleRP.FontSize = new FontSize() { Val = "56" };
                //titleRP.Color = new DocumentFormat.OpenXml.Wordprocessing.Color() { Val = "FF0000" };
                titleRP.Bold = new Bold();
                // Always add properties first
                titleR.Append(titleRP);
                DocumentFormat.OpenXml.Wordprocessing.Text titleT = new("Cyber Security")
                {
                    Space = SpaceProcessingModeValues.Preserve
                };
                titleR.Append(titleT);
                title.Append(titleR);
                body.Append(title);

                DocumentFormat.OpenXml.Wordprocessing.Paragraph nameClient = new();
                ParagraphProperties nameClientP = new();
                Justification nameClienteJ = new() { Val = JustificationValues.Center };
                nameClientP.SpacingBetweenLines = new SpacingBetweenLines()
                {
                    Before = "500"
                };
                nameClientP.Append(nameClienteJ);
                nameClient.Append(nameClientP);
                Run nameClientR = new();
                RunProperties nameClientRP = new();
                nameClientRP.FontSize = new FontSize() { Val = "30" };
                nameClientRP.Bold = new Bold();
                // Always add properties first
                nameClientR.Append(nameClientRP);
                DocumentFormat.OpenXml.Wordprocessing.Text nameClientT = new(companyG.TradeName)
                {
                    Space = SpaceProcessingModeValues.Preserve
                };
                nameClientR.Append(nameClientT);
                nameClient.Append(nameClientR);
                body.Append(nameClient);
                // imagen
                DocumentFormat.OpenXml.Wordprocessing.Paragraph pImgClient = new();
                ImagePart imagePartClient = mainPart.AddImagePart(ImagePartType.Jpeg);
                //string imgPathClient = _blobManagement.GetLink(companyG.Ruc, companyG.Logo);
                string imgPathClient = "C:/Users/USER/Pictures/Screenshots/2.jpg";
                var reqClient = WebRequest.Create(imgPathClient);
                var respClient = reqClient.GetResponse();
                imagePartClient.FeedData(respClient.GetResponseStream());

                // 1500000 and 1092000 are img width and height
                Run rImgClient = new(_report.DrawingManager(mainPart.GetIdOfPart(imagePartClient), "PictureName", 1500000, 1500000, string.Empty));
                pImgClient.Append(rImgClient);
                body.Append(pImgClient);
                //----------
                DocumentFormat.OpenXml.Wordprocessing.Paragraph subtitle = new();
                ParagraphProperties subtitleP = new();
                Justification subtitleJ = new() { Val = JustificationValues.Center };
                subtitleP.SpacingBetweenLines = new SpacingBetweenLines()
                {
                    Before = "0"
                };
                subtitleP.Append(subtitleJ);
                subtitle.Append(subtitleP);
                Run subtitleR = new();
                RunProperties subtitleRP = new();
                subtitleRP.FontSize = new FontSize() { Val = "52" };
                subtitleRP.Bold = new Bold();
                subtitleRP.Underline = new DocumentFormat.OpenXml.Wordprocessing.Underline() { Val = DocumentFormat.OpenXml.Wordprocessing.UnderlineValues.Single };
                // Always add properties first
                subtitleR.Append(subtitleRP);
                DocumentFormat.OpenXml.Wordprocessing.Text subtitleT = new("PLAN DE TRABAJO")
                {
                    Space = SpaceProcessingModeValues.Preserve
                };
                subtitleR.Append(subtitleT);
                subtitle.Append(subtitleR);
                body.Append(subtitle);
                body.Append(new DocumentFormat.OpenXml.Wordprocessing.Paragraph(new Run(new Break() { Type = BreakValues.Page })));
                //FIN CARÁTULA

                //HISTORIAL
                DocumentFormat.OpenXml.Wordprocessing.Paragraph record = new();
                ParagraphProperties recordP = new();
                Justification recordJ = new() { Val = JustificationValues.Center };
                recordP.SpacingBetweenLines = new SpacingBetweenLines()
                {
                    Before = "0"
                };
                recordP.Append(recordJ);
                record.Append(recordP);
                Run recordR = new();
                RunProperties recordRP = new();
                recordRP.FontSize = new FontSize() { Val = "30" };
                recordRP.Bold = new Bold();
                // Always add properties first
                recordR.Append(recordRP);
                DocumentFormat.OpenXml.Wordprocessing.Text recordT = new("HISTORIAL DE VERSIONES")
                {
                    Space = SpaceProcessingModeValues.Preserve
                };
                recordR.Append(recordT);
                record.Append(recordR);
                body.Append(record);

                //tabla de historial
                var tableH = new DocumentFormat.OpenXml.Wordprocessing.Table();
                var tableHWidth = new TableWidth() { Width = "5000", Type = TableWidthUnitValues.Pct };
                var borderHColor = "000000";

                var tableHProperties = new TableProperties();
                var tableHBorders = new TableBorders();

                var topBorder = new TopBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Thick),
                    Color = borderHColor
                };

                var bottomBorder = new BottomBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Thick),
                    Color = borderHColor
                };

                var rightBorder = new RightBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Thick),
                    Color = borderHColor
                };

                var leftBorder = new LeftBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Thick),
                    Color = borderHColor
                };

                var insideHorizontalBorder = new InsideHorizontalBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Thick),
                    Color = borderHColor
                };

                var insideVerticalBorder = new InsideVerticalBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Thick),
                    Color = borderHColor
                };

                tableHBorders.AppendChild(topBorder);
                tableHBorders.AppendChild(bottomBorder);
                tableHBorders.AppendChild(rightBorder);
                tableHBorders.AppendChild(leftBorder);
                tableHBorders.AppendChild(insideHorizontalBorder);
                tableHBorders.AppendChild(insideVerticalBorder);

                tableHProperties.Append(tableHWidth);
                tableHProperties.AppendChild(tableHBorders);

                tableH.AppendChild(tableHProperties);

                //contenido de la tabla
                var row1 = new TableRow();

                var cell11 = new TableCell();
                var cell11Properties = new TableCellProperties();
                var shading11 = new Shading()
                {
                    Color = "auto",
                    Fill = "E5E8E8",
                    Val = ShadingPatternValues.Clear
                };
                cell11Properties.Append(shading11);
                var paragraph11 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                var paragraphProperties11 = new ParagraphProperties
                {
                    Justification = new Justification() { Val = JustificationValues.Center }
                };
                paragraph11.Append(paragraphProperties11);
                var run11 = new Run();
                var runProperties11 = new RunProperties
                {
                    Bold = new Bold()
                };
                run11.Append(runProperties11);
                run11.Append(new DocumentFormat.OpenXml.Wordprocessing.Text("Fecha"));
                paragraph11.Append(run11);
                cell11.Append(cell11Properties);
                cell11.Append(paragraph11);
                row1.Append(cell11);

                var cell12 = new TableCell();
                var cell12Properties = new TableCellProperties();
                var shading12 = new Shading()
                {
                    Color = "auto",
                    Fill = "E5E8E8",
                    Val = ShadingPatternValues.Clear
                };
                cell12Properties.Append(shading12);
                var paragraph12 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                var paragraphProperties12 = new ParagraphProperties
                {
                    Justification = new Justification() { Val = JustificationValues.Center }
                };
                paragraph12.Append(paragraphProperties12);
                var run12 = new Run();
                var runProperties12 = new RunProperties
                {
                    Bold = new Bold()
                };
                run12.Append(runProperties12);
                run12.Append(new DocumentFormat.OpenXml.Wordprocessing.Text("Versión"));
                paragraph12.Append(run12);
                cell12.Append(cell12Properties);
                cell12.Append(paragraph12);
                row1.Append(cell12);


                var cell13 = new TableCell();
                var cell13Properties = new TableCellProperties();
                var shading13 = new Shading()
                {
                    Color = "auto",
                    Fill = "E5E8E8",
                    Val = ShadingPatternValues.Clear
                };
                cell13Properties.Append(shading13);
                var paragraph13 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                var paragraphProperties13 = new ParagraphProperties
                {
                    Justification = new Justification() { Val = JustificationValues.Center }
                };
                paragraph13.Append(paragraphProperties13);
                var run13 = new Run();
                var runProperties13 = new RunProperties
                {
                    Bold = new Bold()
                };
                run13.Append(runProperties13);
                run13.Append(new DocumentFormat.OpenXml.Wordprocessing.Text("Descripción"));
                paragraph13.Append(run13);
                cell13.Append(cell13Properties);
                cell13.Append(paragraph13);
                row1.Append(cell13);


                var cell14 = new TableCell();
                var cell14Properties = new TableCellProperties();
                var shading14 = new Shading()
                {
                    Color = "auto",
                    Fill = "E5E8E8",
                    Val = ShadingPatternValues.Clear
                };
                cell14Properties.Append(shading14);
                var paragraph14 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                var paragraphProperties14 = new ParagraphProperties
                {
                    Justification = new Justification() { Val = JustificationValues.Center }
                };
                paragraph14.Append(paragraphProperties14);
                var run14 = new Run();
                var runProperties14 = new RunProperties
                {
                    Bold = new Bold()
                };
                run14.Append(runProperties14);
                run14.Append(new DocumentFormat.OpenXml.Wordprocessing.Text("Autor"));
                paragraph14.Append(run14);
                cell14.Append(cell14Properties);
                cell14.Append(paragraph14);
                row1.Append(cell14);
                tableH.Append(row1);


                row1 = new TableRow();

                cell11 = new TableCell();
                paragraph11 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                paragraphProperties11 = new ParagraphProperties
                {
                    Justification = new Justification() { Val = JustificationValues.Center }
                };
                paragraph11.Append(paragraphProperties11);
                run11 = new Run();
                run11.Append(new DocumentFormat.OpenXml.Wordprocessing.Text(DateTime.Now.ToString("d")));
                paragraph11.Append(run11);
                cell11.Append(paragraph11);
                row1.Append(cell11);

                cell12 = new TableCell();
                paragraph12 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                paragraphProperties12 = new ParagraphProperties
                {
                    Justification = new Justification() { Val = JustificationValues.Center }
                };
                paragraph12.Append(paragraphProperties12);
                run12 = new Run();
                run12.Append(new DocumentFormat.OpenXml.Wordprocessing.Text("1.0"));
                paragraph12.Append(run12);
                cell12.Append(paragraph12);
                row1.Append(cell12);


                cell13 = new TableCell();
                paragraph13 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                paragraphProperties13 = new ParagraphProperties
                {
                    Justification = new Justification() { Val = JustificationValues.Center }
                };
                paragraph13.Append(paragraphProperties13);
                run13 = new Run();
                run13.Append(new DocumentFormat.OpenXml.Wordprocessing.Text("Elaboración del documento"));
                paragraph13.Append(run13);
                cell13.Append(paragraph13);
                row1.Append(cell13);


                cell14 = new TableCell();

                paragraph14 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                paragraphProperties14 = new ParagraphProperties
                {
                    Justification = new Justification() { Val = JustificationValues.Center }
                };
                paragraph14.Append(paragraphProperties14);
                run14 = new Run();
                run14.Append(new DocumentFormat.OpenXml.Wordprocessing.Text("gap.Responsable"));
                paragraph14.Append(run14);
                cell14.Append(paragraph14);
                row1.Append(cell14);

                tableH.Append(row1);

                body.Append(tableH);
                body.Append(new DocumentFormat.OpenXml.Wordprocessing.Paragraph(new Run(new Break() { Type = BreakValues.Page })));
                //FIN HISTORIAL 

                // EJEMPLO DE CONVERTYIR HTML A WORD-----------------------------
                HtmlConverter converter = new HtmlConverter(mainPart);
                var html = "<ul>\r\n" + "<li>Item1</li>\r\n" +"<li>Item2</li>\r\n" +"</ul>";
                var paragraphs = converter.Parse(html);
                for (int i = 0; i < paragraphs.Count; i++)
                {
                    body.Append(paragraphs[i]);
                }
                //-------------------------------------------------------------
                ////COMPAÑÍA
                //DocumentFormat.OpenXml.Wordprocessing.Paragraph company = new();
                //Run companyR = new();
                //RunProperties companyRP = new();
                //companyRP.FontSize = new FontSize() { Val = "25" };
                //companyRP.Bold = new Bold();
                //companyR.Append(companyRP);
                //DocumentFormat.OpenXml.Wordprocessing.Text companyT = new("COMPAÑÍA: ")
                //{
                //    Space = SpaceProcessingModeValues.Preserve
                //};
                //companyR.Append(companyT);
                //company.Append(companyR);

                //Run companyR2 = new();
                //RunProperties companyRP2 = new();
                //companyRP2.FontSize = new FontSize() { Val = "25" };
                //companyR2.Append(companyRP2);
                //DocumentFormat.OpenXml.Wordprocessing.Text companyT2 = new(companyG.TradeName)
                //{
                //    Space = SpaceProcessingModeValues.Preserve
                //};
                //companyR2.Append(companyT2);
                //company.Append(companyR2);
                //body.Append(company);
                ////FIN COMPAÑÍA

                ////MODELO DE MADUREZ
                //DocumentFormat.OpenXml.Wordprocessing.Paragraph madurityModel = new();
                //Run madurityModelR = new();
                //RunProperties madurityModelRP = new();
                //madurityModelRP.FontSize = new FontSize() { Val = "25" };
                //madurityModelRP.Bold = new Bold();
                //madurityModelR.Append(madurityModelRP);
                //DocumentFormat.OpenXml.Wordprocessing.Text madurityModelT = new("MODELO DE MADUREZ: ")
                //{
                //    Space = SpaceProcessingModeValues.Preserve
                //};
                //madurityModelR.Append(madurityModelT);
                //madurityModel.Append(madurityModelR);

                //Run madurityModelR2 = new();
                //RunProperties madurityModelRP2 = new();
                //madurityModelRP2.FontSize = new FontSize() { Val = "25" };
                //madurityModelR2.Append(madurityModelRP2);
                //DocumentFormat.OpenXml.Wordprocessing.Text madurityModelT2 = new(madurityModelG.Name)
                //{
                //    Space = SpaceProcessingModeValues.Preserve
                //};
                //madurityModelR2.Append(madurityModelT2);
                //madurityModel.Append(madurityModelR2);
                //body.Append(madurityModel);
                ////FIN MODELO DE MADUREZ

                ////tabla de modelo de madurez
                //var tableMM = new DocumentFormat.OpenXml.Wordprocessing.Table();
                //var tableMMWidth = new TableWidth() { Width = "5000", Type = TableWidthUnitValues.Pct };
                //var borderMMColor = "000000";

                //var tableMMProperties = new TableProperties();
                //var tableMMBorders = new TableBorders();

                //var topBorderMM = new TopBorder
                //{
                //    Val = new EnumValue<BorderValues>(BorderValues.Thick),
                //    Color = borderHColor
                //};

                //var bottomBorderMM = new BottomBorder
                //{
                //    Val = new EnumValue<BorderValues>(BorderValues.Thick),
                //    Color = borderMMColor
                //};

                //var rightBorderMM = new RightBorder
                //{
                //    Val = new EnumValue<BorderValues>(BorderValues.Thick),
                //    Color = borderMMColor
                //};

                //var leftBorderMM = new LeftBorder
                //{
                //    Val = new EnumValue<BorderValues>(BorderValues.Thick),
                //    Color = borderMMColor
                //};

                //var insideHorizontalBorderMM = new InsideHorizontalBorder
                //{
                //    Val = new EnumValue<BorderValues>(BorderValues.Thick),
                //    Color = borderMMColor
                //};

                //var insideVerticalBorderMM = new InsideVerticalBorder
                //{
                //    Val = new EnumValue<BorderValues>(BorderValues.Thick),
                //    Color = borderMMColor
                //};

                //tableMMBorders.AppendChild(topBorderMM);
                //tableMMBorders.AppendChild(bottomBorderMM);
                //tableMMBorders.AppendChild(rightBorderMM);
                //tableMMBorders.AppendChild(leftBorderMM);
                //tableMMBorders.AppendChild(insideHorizontalBorderMM);
                //tableMMBorders.AppendChild(insideVerticalBorderMM);

                //tableMMProperties.Append(tableMMWidth);
                //tableMMProperties.AppendChild(tableMMBorders);

                //tableMM.AppendChild(tableMMProperties);

                ////contenido de la tabla
                //row1 = new TableRow();

                //cell11 = new TableCell();
                //cell11Properties = new TableCellProperties();

                //shading11 = new Shading()
                //{
                //    Color = "auto",
                //    Fill = "5DADE2",
                //    Val = ShadingPatternValues.Clear
                //};
                //cell11Properties.Append(shading11);
                //paragraph11 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //paragraphProperties11 = new ParagraphProperties
                //{
                //    Justification = new Justification() { Val = JustificationValues.Center }
                //};
                //paragraph11.Append(paragraphProperties11);
                //run11 = new Run();
                //runProperties11 = new RunProperties
                //{
                //    Bold = new Bold()
                //};
                //run11.Append(runProperties11);
                //run11.Append(new DocumentFormat.OpenXml.Wordprocessing.Text("Nivel"));
                //paragraph11.Append(run11);
                //cell11.Append(cell11Properties);
                //cell11.Append(paragraph11);
                //row1.Append(cell11);


                //cell12 = new TableCell();
                //cell12Properties = new TableCellProperties();
                //shading12 = new Shading()
                //{
                //    Color = "auto",
                //    Fill = "5DADE2",
                //    Val = ShadingPatternValues.Clear
                //};
                //cell12Properties.Append(shading12);
                //paragraph12 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //paragraphProperties12 = new ParagraphProperties
                //{
                //    Justification = new Justification() { Val = JustificationValues.Center }
                //};
                //paragraph12.Append(paragraphProperties12);
                //run12 = new Run();
                //runProperties12 = new RunProperties
                //{
                //    Bold = new Bold()
                //};
                //run12.Append(runProperties12);
                //run12.Append(new DocumentFormat.OpenXml.Wordprocessing.Text("Descripción"));
                //paragraph12.Append(run12);
                //cell12.Append(cell12Properties);
                //cell12.Append(paragraph12);
                //row1.Append(cell12);

                //tableMM.Append(row1);

                ////foreach
                //foreach (var madurityLevel in madurityModelG.MadurityLevels)
                //{
                //    row1 = new TableRow();

                //    cell11 = new TableCell();
                //    paragraph11 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //    paragraphProperties11 = new ParagraphProperties
                //    {
                //        Justification = new Justification() { Val = JustificationValues.Left }
                //    };
                //    paragraph11.Append(paragraphProperties11);
                //    run11 = new Run();
                //    run11.Append(new DocumentFormat.OpenXml.Wordprocessing.Text(madurityLevel.Name));
                //    paragraph11.Append(run11);
                //    cell11.Append(paragraph11);
                //    row1.Append(cell11);

                //    cell12 = new TableCell();
                //    paragraph12 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //    paragraphProperties12 = new ParagraphProperties
                //    {
                //        Justification = new Justification() { Val = JustificationValues.Left }
                //    };
                //    paragraph12.Append(paragraphProperties12);
                //    run12 = new Run();
                //    run12.Append(new DocumentFormat.OpenXml.Wordprocessing.Text(madurityLevel.Description));
                //    paragraph12.Append(run12);
                //    cell12.Append(paragraph12);
                //    row1.Append(cell12);
                //    tableMM.Append(row1);
                //}

                //body.Append(tableMM);
                ////-----------------------------

                ////fin tabla de modelo de madurez
                //body.Append(new DocumentFormat.OpenXml.Wordprocessing.Paragraph(new Run(new DocumentFormat.OpenXml.Wordprocessing.Text("\n"))));
                ////MARCO DE TRABAJO
                //DocumentFormat.OpenXml.Wordprocessing.Paragraph framework = new();
                //Run frameworkR = new();
                //RunProperties frameworkRP = new();
                //frameworkRP.FontSize = new FontSize() { Val = "25" };
                //frameworkRP.Bold = new Bold();
                //frameworkR.Append(frameworkRP);
                //DocumentFormat.OpenXml.Wordprocessing.Text frameworkT = new("MARCO DE TRABAJO: ")
                //{
                //    Space = SpaceProcessingModeValues.Preserve
                //};
                //frameworkR.Append(frameworkT);
                //framework.Append(frameworkR);

                //Run frameworkR2 = new();
                //RunProperties frameworkRP2 = new();
                //frameworkRP2.FontSize = new FontSize() { Val = "25" };
                //frameworkR2.Append(frameworkRP2);
                //DocumentFormat.OpenXml.Wordprocessing.Text frameworkT2 = new(frameworkG.Name)
                //{
                //    Space = SpaceProcessingModeValues.Preserve
                //};
                //frameworkR2.Append(frameworkT2);
                //framework.Append(frameworkR2);
                //body.Append(framework);
                ////versión del marco de trabajo
                //DocumentFormat.OpenXml.Wordprocessing.Paragraph version = new();
                //Run versionR = new();
                //RunProperties versionRP = new();
                //versionRP.FontSize = new FontSize() { Val = "25" };
                //versionRP.Bold = new Bold();
                //versionR.Append(versionRP);
                //DocumentFormat.OpenXml.Wordprocessing.Text versionT = new("    - Versión: ")
                //{
                //    Space = SpaceProcessingModeValues.Preserve
                //};
                //versionR.Append(versionT);
                //version.Append(versionR);

                //Run versionR2 = new();
                //RunProperties versionRP2 = new();
                //versionRP2.FontSize = new FontSize() { Val = "25" };
                //versionR2.Append(versionRP2);
                //DocumentFormat.OpenXml.Wordprocessing.Text versionT2 = new(frameworkG.Version)
                //{
                //    Space = SpaceProcessingModeValues.Preserve
                //};
                //versionR2.Append(versionT2);
                //version.Append(versionR2);
                //body.Append(version);
                ////-----

                ////FIN MARCO DE TRABAJO 

                ////EVALUACIÓN DE CONTROLES
                //DocumentFormat.OpenXml.Wordprocessing.Paragraph evaluation = new();
                //Run evaluationR = new();
                //RunProperties evaluationRP = new();
                //evaluationRP.FontSize = new FontSize() { Val = "25" };
                //evaluationRP.Bold = new Bold();
                //evaluationR.Append(evaluationRP);
                //DocumentFormat.OpenXml.Wordprocessing.Text evaluationT = new("EVALUACIÓN DE CONTROLES: ")
                //{
                //    Space = SpaceProcessingModeValues.Preserve
                //};
                //evaluationR.Append(evaluationT);
                //evaluation.Append(evaluationR);

                //Run evaluationR2 = new();
                //RunProperties evaluationRP2 = new();
                //evaluationRP2.FontSize = new FontSize() { Val = "25" };
                //evaluationR2.Append(evaluationRP2);
                //DocumentFormat.OpenXml.Wordprocessing.Text evaluationT2 = new(gap.Name)
                //{
                //    Space = SpaceProcessingModeValues.Preserve
                //};
                //evaluationR2.Append(evaluationT2);
                //evaluation.Append(evaluationR2);
                //body.Append(evaluation);

                ////lista
                //SpacingBetweenLines sblUl = new() { After = "0" };  // Get rid of space between bullets  
                //Indentation iUl = new() { Left = "5", Hanging = "360" };  // correct indentation  
                //NumberingProperties npUl = new(
                //    new NumberingLevelReference() { Val = 1 },
                //    new NumberingId() { Val = 2 }
                //);
                //ParagraphProperties ppUnordered = new(npUl, sblUl, iUl);
                //ppUnordered.ParagraphStyleId = new ParagraphStyleId() { Val = "Heading1" };


                ////foreach
                //foreach (var frameworkFirstlevel in gap.IdFrameworkNavigation.FrameworkFirstlevels)
                //{
                //    body.Append(new DocumentFormat.OpenXml.Wordprocessing.Paragraph(new Run(new DocumentFormat.OpenXml.Wordprocessing.Text("\n"))));
                //    DocumentFormat.OpenXml.Wordprocessing.Paragraph function = new();
                //    function.ParagraphProperties = new ParagraphProperties(ppUnordered.OuterXml);
                //    Run functionR = new();
                //    RunProperties functionRP = new();
                //    functionRP.FontSize = new FontSize() { Val = "25" };
                //    functionRP.Bold = new Bold();
                //    functionR.Append(functionRP);
                //    DocumentFormat.OpenXml.Wordprocessing.Text functionT = new("    Función: ")
                //    {
                //        Space = SpaceProcessingModeValues.Preserve
                //    };
                //    functionR.Append(functionT);
                //    function.Append(functionR);

                //    Run functionR2 = new();
                //    RunProperties functionRP2 = new();
                //    functionRP2.FontSize = new FontSize() { Val = "25" };
                //    functionR2.Append(functionRP2);
                //    DocumentFormat.OpenXml.Wordprocessing.Text functionT2 = new(frameworkFirstlevel.Name)
                //    {
                //        Space = SpaceProcessingModeValues.Preserve
                //    };
                //    functionR2.Append(functionT2);
                //    function.Append(functionR2);
                //    body.Append(function);
                //    foreach (var frameworkSecondlevel in frameworkFirstlevel.FrameworkSecondlevels)
                //    {
                //        body.Append(new DocumentFormat.OpenXml.Wordprocessing.Paragraph(new Run(new DocumentFormat.OpenXml.Wordprocessing.Text("\n"))));
                //        DocumentFormat.OpenXml.Wordprocessing.Paragraph category = new();
                //        Run categoryR = new();
                //        RunProperties categoryRP = new();
                //        categoryRP.FontSize = new FontSize() { Val = "25" };
                //        categoryRP.Bold = new Bold();
                //        categoryR.Append(categoryRP);
                //        DocumentFormat.OpenXml.Wordprocessing.Text categoryT = new("    - Categoría: ")
                //        {
                //            Space = SpaceProcessingModeValues.Preserve
                //        };
                //        categoryR.Append(categoryT);
                //        category.Append(categoryR);

                //        Run categoryR2 = new();
                //        RunProperties categoryRP2 = new();
                //        categoryRP2.FontSize = new FontSize() { Val = "25" };
                //        categoryR2.Append(categoryRP2);
                //        DocumentFormat.OpenXml.Wordprocessing.Text categoryT2 = new(frameworkSecondlevel.Name)
                //        {
                //            Space = SpaceProcessingModeValues.Preserve
                //        };
                //        categoryR2.Append(categoryT2);
                //        category.Append(categoryR2);
                //        body.Append(category);
                //        //-- tabla de las evaluaciones
                //        //tabla de modelo de madurez
                //        var tableC = new DocumentFormat.OpenXml.Wordprocessing.Table();
                //        //var tablehorizontal = new TableJustification() { Val=TableRowAlignmentValues.Center};
                //        var tableCWidth = new TableWidth() { Width = "5000", Type = TableWidthUnitValues.Pct };
                //        var borderCColor = "000000";

                //        var tableCProperties = new TableProperties();
                //        var tableCBorders = new TableBorders();

                //        var topBorderC = new TopBorder
                //        {
                //            Val = new EnumValue<BorderValues>(BorderValues.Thick),
                //            Color = borderCColor
                //        };

                //        var bottomBorderC = new BottomBorder
                //        {
                //            Val = new EnumValue<BorderValues>(BorderValues.Thick),
                //            Color = borderCColor
                //        };

                //        var rightBorderC = new RightBorder
                //        {
                //            Val = new EnumValue<BorderValues>(BorderValues.Thick),
                //            Color = borderCColor
                //        };

                //        var leftBorderC = new LeftBorder
                //        {
                //            Val = new EnumValue<BorderValues>(BorderValues.Thick),
                //            Color = borderCColor
                //        };

                //        var insideHorizontalBorderC = new InsideHorizontalBorder
                //        {
                //            Val = new EnumValue<BorderValues>(BorderValues.Thick),
                //            Color = borderCColor
                //        };

                //        var insideVerticalBorderC = new InsideVerticalBorder
                //        {
                //            Val = new EnumValue<BorderValues>(BorderValues.Thick),
                //            Color = borderCColor
                //        };

                //        tableCBorders.AppendChild(topBorderC);
                //        tableCBorders.AppendChild(bottomBorderC);
                //        tableCBorders.AppendChild(rightBorderC);
                //        tableCBorders.AppendChild(leftBorderC);
                //        tableCBorders.AppendChild(insideHorizontalBorderC);
                //        tableCBorders.AppendChild(insideVerticalBorderC);

                //        tableCProperties.Append(tableCWidth);
                //        tableCProperties.AppendChild(tableCBorders);

                //        tableC.AppendChild(tableCProperties);

                //        row1 = new TableRow();

                //        cell11 = new TableCell();
                //        cell11Properties = new TableCellProperties(new TableCellWidth()
                //        {
                //            Type = TableWidthUnitValues.Dxa,
                //            Width = "1500",
                //        });

                //        shading11 = new Shading()
                //        {
                //            Color = "auto",
                //            Fill = "5DADE2",
                //            Val = ShadingPatternValues.Clear
                //        };
                //        cell11Properties.Append(shading11);
                //        paragraph11 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //        paragraphProperties11 = new ParagraphProperties
                //        {
                //            Justification = new Justification() { Val = JustificationValues.Center }
                //        };
                //        paragraph11.Append(paragraphProperties11);
                //        run11 = new Run();
                //        runProperties11 = new RunProperties
                //        {
                //            Bold = new Bold()
                //        };
                //        run11.Append(runProperties11);
                //        run11.Append(new DocumentFormat.OpenXml.Wordprocessing.Text("Subcategoria"));
                //        paragraph11.Append(run11);
                //        cell11.Append(cell11Properties);
                //        cell11.Append(paragraph11);
                //        row1.Append(cell11);


                //        cell12 = new TableCell();
                //        cell12Properties = new TableCellProperties();
                //        shading12 = new Shading()
                //        {
                //            Color = "auto",
                //            Fill = "5DADE2",
                //            Val = ShadingPatternValues.Clear
                //        };
                //        cell12Properties.Append(shading12);
                //        paragraph12 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //        paragraphProperties12 = new ParagraphProperties
                //        {
                //            Justification = new Justification() { Val = JustificationValues.Center }
                //        };
                //        paragraph12.Append(paragraphProperties12);
                //        run12 = new Run();
                //        runProperties12 = new RunProperties
                //        {
                //            Bold = new Bold()
                //        };
                //        run12.Append(runProperties12);
                //        run12.Append(new DocumentFormat.OpenXml.Wordprocessing.Text("Descripción"));
                //        paragraph12.Append(run12);
                //        cell12.Append(cell12Properties);
                //        cell12.Append(paragraph12);
                //        row1.Append(cell12);

                //        cell13 = new TableCell();
                //        cell13Properties = new TableCellProperties();
                //        shading13 = new Shading()
                //        {
                //            Color = "auto",
                //            Fill = "5DADE2",
                //            Val = ShadingPatternValues.Clear
                //        };
                //        cell13Properties.Append(shading13);
                //        paragraph13 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //        paragraphProperties13 = new ParagraphProperties
                //        {
                //            Justification = new Justification() { Val = JustificationValues.Center }
                //        };
                //        paragraph13.Append(paragraphProperties13);
                //        run13 = new Run();
                //        runProperties13 = new RunProperties
                //        {
                //            Bold = new Bold()
                //        };
                //        run13.Append(runProperties13);
                //        run13.Append(new DocumentFormat.OpenXml.Wordprocessing.Text("Situación actual"));
                //        paragraph13.Append(run13);
                //        cell13.Append(cell13Properties);
                //        cell13.Append(paragraph13);
                //        row1.Append(cell13);


                //        cell14 = new TableCell();
                //        cell14Properties = new TableCellProperties(new TableCellWidth()
                //        {
                //            Type = TableWidthUnitValues.Dxa,
                //            Width = "500",
                //        });
                //        shading14 = new Shading()
                //        {
                //            Color = "auto",
                //            Fill = "5DADE2",
                //            Val = ShadingPatternValues.Clear
                //        };
                //        cell14Properties.Append(shading14);
                //        paragraph14 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //        paragraphProperties14 = new ParagraphProperties
                //        {
                //            Justification = new Justification() { Val = JustificationValues.Center }
                //        };
                //        paragraph14.Append(paragraphProperties14);
                //        run14 = new Run();
                //        runProperties14 = new RunProperties
                //        {
                //            Bold = new Bold()
                //        };
                //        run14.Append(runProperties14);
                //        run14.Append(new DocumentFormat.OpenXml.Wordprocessing.Text("Gap"));
                //        paragraph14.Append(run14);
                //        cell14.Append(cell14Properties);
                //        cell14.Append(paragraph14);
                //        row1.Append(cell14);

                //        cell14 = new TableCell();
                //        cell14Properties = new TableCellProperties();
                //        shading14 = new Shading()
                //        {
                //            Color = "auto",
                //            Fill = "5DADE2",
                //            Val = ShadingPatternValues.Clear
                //        };
                //        cell14Properties.Append(shading14);
                //        paragraph14 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //        paragraphProperties14 = new ParagraphProperties
                //        {
                //            Justification = new Justification() { Val = JustificationValues.Center }
                //        };
                //        paragraph14.Append(paragraphProperties14);
                //        run14 = new Run();
                //        runProperties14 = new RunProperties
                //        {
                //            Bold = new Bold()
                //        };
                //        run14.Append(runProperties14);
                //        run14.Append(new DocumentFormat.OpenXml.Wordprocessing.Text("Valor actual"));
                //        paragraph14.Append(run14);
                //        cell14.Append(cell14Properties);
                //        cell14.Append(paragraph14);
                //        row1.Append(cell14);

                //        cell14 = new TableCell();
                //        cell14Properties = new TableCellProperties();
                //        shading14 = new Shading()
                //        {
                //            Color = "auto",
                //            Fill = "5DADE2",
                //            Val = ShadingPatternValues.Clear
                //        };
                //        cell14Properties.Append(shading14);
                //        paragraph14 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //        paragraphProperties14 = new ParagraphProperties
                //        {
                //            Justification = new Justification() { Val = JustificationValues.Center }
                //        };
                //        paragraph14.Append(paragraphProperties14);
                //        run14 = new Run();
                //        runProperties14 = new RunProperties
                //        {
                //            Bold = new Bold()
                //        };
                //        run14.Append(runProperties14);
                //        run14.Append(new DocumentFormat.OpenXml.Wordprocessing.Text("Valor objetivo"));
                //        paragraph14.Append(run14);
                //        cell14.Append(cell14Properties);
                //        cell14.Append(paragraph14);
                //        row1.Append(cell14);
                //        tableC.Append(row1);
                //        foreach (var frameworkThirdlevel in frameworkSecondlevel.FrameworkThirdlevels)
                //        {
                //            var gapControlResult = _gapControlResultService.GetGapControlResult(frameworkThirdlevel.Idthirdlevel, gap.IdGapAnalysis);
                //            if (gapControlResult.Apply == false)
                //            {
                //                row1 = new TableRow();

                //                cell11 = new TableCell();
                //                paragraph11 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //                paragraphProperties11 = new ParagraphProperties
                //                {
                //                    Justification = new Justification() { Val = JustificationValues.Left }
                //                };
                //                paragraph11.Append(paragraphProperties11);
                //                run11 = new Run();
                //                run11.Append(new DocumentFormat.OpenXml.Wordprocessing.Text(frameworkThirdlevel.Name));
                //                paragraph11.Append(run11);
                //                cell11.Append(paragraph11);
                //                row1.Append(cell11);

                //                cell11 = new TableCell();
                //                paragraph11 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //                paragraphProperties11 = new ParagraphProperties
                //                {
                //                    Justification = new Justification() { Val = JustificationValues.Left }
                //                };
                //                paragraph11.Append(paragraphProperties11);
                //                run11 = new Run();
                //                run11.Append(new DocumentFormat.OpenXml.Wordprocessing.Text(frameworkThirdlevel.Description));
                //                paragraph11.Append(run11);
                //                cell11.Append(paragraph11);
                //                row1.Append(cell11);

                //                cell11 = new TableCell();
                //                paragraph11 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //                paragraphProperties11 = new ParagraphProperties
                //                {
                //                    Justification = new Justification() { Val = JustificationValues.Left }
                //                };
                //                paragraph11.Append(paragraphProperties11);
                //                run11 = new Run();
                //                run11.Append(new DocumentFormat.OpenXml.Wordprocessing.Text("NO APLICA"));
                //                paragraph11.Append(run11);
                //                cell11.Append(paragraph11);
                //                row1.Append(cell11);

                //                cell11 = new TableCell();
                //                paragraph11 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //                paragraphProperties11 = new ParagraphProperties
                //                {
                //                    Justification = new Justification() { Val = JustificationValues.Left }
                //                };
                //                paragraph11.Append(paragraphProperties11);
                //                run11 = new Run();
                //                run11.Append(new DocumentFormat.OpenXml.Wordprocessing.Text("NO APLICA"));
                //                paragraph11.Append(run11);
                //                cell11.Append(paragraph11);
                //                row1.Append(cell11);

                //                cell11 = new TableCell();
                //                paragraph11 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //                paragraphProperties11 = new ParagraphProperties
                //                {
                //                    Justification = new Justification() { Val = JustificationValues.Center }
                //                };
                //                paragraph11.Append(paragraphProperties11);
                //                run11 = new Run();
                //                run11.Append(new DocumentFormat.OpenXml.Wordprocessing.Text("NO APLICA"));
                //                paragraph11.Append(run11);
                //                cell11.Append(paragraph11);
                //                row1.Append(cell11);

                //                cell11 = new TableCell();
                //                paragraph11 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //                paragraphProperties11 = new ParagraphProperties
                //                {
                //                    Justification = new Justification() { Val = JustificationValues.Center }
                //                };
                //                paragraph11.Append(paragraphProperties11);
                //                run11 = new Run();
                //                run11.Append(new DocumentFormat.OpenXml.Wordprocessing.Text("NO APLICA"));
                //                paragraph11.Append(run11);
                //                cell11.Append(paragraph11);
                //                row1.Append(cell11);
                //                tableC.Append(row1);
                //            }
                //            else
                //            {
                //                if (gapControlResult.Status == false)
                //                {
                //                    row1 = new TableRow();

                //                    cell11 = new TableCell();
                //                    paragraph11 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //                    paragraphProperties11 = new ParagraphProperties
                //                    {
                //                        Justification = new Justification() { Val = JustificationValues.Left }
                //                    };
                //                    paragraph11.Append(paragraphProperties11);
                //                    run11 = new Run();
                //                    run11.Append(new DocumentFormat.OpenXml.Wordprocessing.Text(frameworkThirdlevel.Name));
                //                    paragraph11.Append(run11);
                //                    cell11.Append(paragraph11);
                //                    row1.Append(cell11);

                //                    cell11 = new TableCell();
                //                    paragraph11 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //                    paragraphProperties11 = new ParagraphProperties
                //                    {
                //                        Justification = new Justification() { Val = JustificationValues.Left }
                //                    };
                //                    paragraph11.Append(paragraphProperties11);
                //                    run11 = new Run();
                //                    run11.Append(new DocumentFormat.OpenXml.Wordprocessing.Text(frameworkThirdlevel.Description));
                //                    paragraph11.Append(run11);
                //                    cell11.Append(paragraph11);
                //                    row1.Append(cell11);

                //                    cell11 = new TableCell();
                //                    paragraph11 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //                    paragraphProperties11 = new ParagraphProperties
                //                    {
                //                        Justification = new Justification() { Val = JustificationValues.Left }
                //                    };
                //                    paragraph11.Append(paragraphProperties11);
                //                    run11 = new Run();
                //                    run11.Append(new DocumentFormat.OpenXml.Wordprocessing.Text(" "));
                //                    paragraph11.Append(run11);
                //                    cell11.Append(paragraph11);
                //                    row1.Append(cell11);

                //                    cell11 = new TableCell();
                //                    paragraph11 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //                    paragraphProperties11 = new ParagraphProperties
                //                    {
                //                        Justification = new Justification() { Val = JustificationValues.Left }
                //                    };
                //                    paragraph11.Append(paragraphProperties11);
                //                    run11 = new Run();
                //                    run11.Append(new DocumentFormat.OpenXml.Wordprocessing.Text(" "));
                //                    paragraph11.Append(run11);
                //                    cell11.Append(paragraph11);
                //                    row1.Append(cell11);

                //                    cell11 = new TableCell();
                //                    paragraph11 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //                    paragraphProperties11 = new ParagraphProperties
                //                    {
                //                        Justification = new Justification() { Val = JustificationValues.Center }
                //                    };
                //                    paragraph11.Append(paragraphProperties11);
                //                    run11 = new Run();
                //                    run11.Append(new DocumentFormat.OpenXml.Wordprocessing.Text(" "));
                //                    paragraph11.Append(run11);
                //                    cell11.Append(paragraph11);
                //                    row1.Append(cell11);

                //                    cell11 = new TableCell();
                //                    paragraph11 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //                    paragraphProperties11 = new ParagraphProperties
                //                    {
                //                        Justification = new Justification() { Val = JustificationValues.Center }
                //                    };
                //                    paragraph11.Append(paragraphProperties11);
                //                    run11 = new Run();
                //                    run11.Append(new DocumentFormat.OpenXml.Wordprocessing.Text(" "));
                //                    paragraph11.Append(run11);
                //                    cell11.Append(paragraph11);
                //                    row1.Append(cell11);
                //                    tableC.Append(row1);
                //                }
                //                else
                //                {
                //                    row1 = new TableRow();

                //                    cell11 = new TableCell();
                //                    paragraph11 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //                    paragraphProperties11 = new ParagraphProperties
                //                    {
                //                        Justification = new Justification() { Val = JustificationValues.Left }
                //                    };
                //                    paragraph11.Append(paragraphProperties11);
                //                    run11 = new Run();
                //                    run11.Append(new DocumentFormat.OpenXml.Wordprocessing.Text(frameworkThirdlevel.Name));
                //                    paragraph11.Append(run11);
                //                    cell11.Append(paragraph11);
                //                    row1.Append(cell11);

                //                    cell11 = new TableCell();
                //                    paragraph11 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //                    paragraphProperties11 = new ParagraphProperties
                //                    {
                //                        Justification = new Justification() { Val = JustificationValues.Left }
                //                    };
                //                    paragraph11.Append(paragraphProperties11);
                //                    run11 = new Run();
                //                    run11.Append(new DocumentFormat.OpenXml.Wordprocessing.Text(frameworkThirdlevel.Description));
                //                    paragraph11.Append(run11);
                //                    cell11.Append(paragraph11);
                //                    row1.Append(cell11);

                //                    cell11 = new TableCell();
                //                    paragraph11 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //                    paragraphProperties11 = new ParagraphProperties
                //                    {
                //                        Justification = new Justification() { Val = JustificationValues.Left }
                //                    };
                //                    paragraph11.Append(paragraphProperties11);
                //                    run11 = new Run();
                //                    run11.Append(new DocumentFormat.OpenXml.Wordprocessing.Text(gapControlResult.Description));
                //                    paragraph11.Append(run11);
                //                    cell11.Append(paragraph11);
                //                    row1.Append(cell11);

                //                    cell11 = new TableCell();
                //                    paragraph11 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //                    paragraphProperties11 = new ParagraphProperties
                //                    {
                //                        Justification = new Justification() { Val = JustificationValues.Left }
                //                    };
                //                    paragraph11.Append(paragraphProperties11);
                //                    run11 = new Run();
                //                    run11.Append(new DocumentFormat.OpenXml.Wordprocessing.Text(gapControlResult.Gap));
                //                    paragraph11.Append(run11);
                //                    cell11.Append(paragraph11);
                //                    row1.Append(cell11);

                //                    cell11 = new TableCell();
                //                    paragraph11 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //                    paragraphProperties11 = new ParagraphProperties
                //                    {
                //                        Justification = new Justification() { Val = JustificationValues.Center }
                //                    };
                //                    paragraph11.Append(paragraphProperties11);
                //                    run11 = new Run();
                //                    run11.Append(new DocumentFormat.OpenXml.Wordprocessing.Text(gapControlResult.MadurityLevelValue.ToString()));
                //                    paragraph11.Append(run11);
                //                    cell11.Append(paragraph11);
                //                    row1.Append(cell11);

                //                    cell11 = new TableCell();
                //                    paragraph11 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //                    paragraphProperties11 = new ParagraphProperties
                //                    {
                //                        Justification = new Justification() { Val = JustificationValues.Center }
                //                    };
                //                    paragraph11.Append(paragraphProperties11);
                //                    run11 = new Run();
                //                    run11.Append(new DocumentFormat.OpenXml.Wordprocessing.Text(gapControlResult.MadurityLevelObjectiveValue.ToString()));
                //                    paragraph11.Append(run11);
                //                    cell11.Append(paragraph11);
                //                    row1.Append(cell11);
                //                    tableC.Append(row1);
                //                }
                //            }

                //        }
                //        body.Append(tableC);
                //    }
                //}
                ////--------------------------------------
                ////FIN DE EVALUACIÓN DE CONTROLES
                //body.Append(new DocumentFormat.OpenXml.Wordprocessing.Paragraph(new Run(new Break() { Type = BreakValues.Page })));
                ////RESUMEN DE RESULTADOS
                //DocumentFormat.OpenXml.Wordprocessing.Paragraph resumeC = new();
                //Run resumeCR = new();
                //RunProperties resumeCRP = new();
                //resumeCRP.FontSize = new FontSize() { Val = "25" };
                //resumeCRP.Bold = new Bold();
                //resumeCR.Append(resumeCRP);
                //DocumentFormat.OpenXml.Wordprocessing.Text resumeCT = new("RESUMEN DE RESULTADOS: ")
                //{
                //    Space = SpaceProcessingModeValues.Preserve
                //};
                //resumeCR.Append(resumeCT);
                //resumeC.Append(resumeCR);
                //body.Append(resumeC);
                ////categorias
                //DocumentFormat.OpenXml.Wordprocessing.Paragraph resumeCategory = new();
                //Run resumeCategoryR = new();
                //RunProperties resumeCategoryRP = new();
                //resumeCategoryRP.FontSize = new FontSize() { Val = "25" };
                //resumeCategoryRP.Bold = new Bold();
                //resumeCategoryR.Append(resumeCategoryRP);
                //DocumentFormat.OpenXml.Wordprocessing.Text resumeCategoryT = new("    - Categorías: ")
                //{
                //    Space = SpaceProcessingModeValues.Preserve
                //};
                //resumeCategoryR.Append(resumeCategoryT);
                //resumeCategory.Append(resumeCategoryR);
                //body.Append(resumeCategory);

                ////tabla resumen de categorias
                //var tableRC = new DocumentFormat.OpenXml.Wordprocessing.Table();
                //var tableRCWidth = new TableWidth() { Width = "3000", Type = TableWidthUnitValues.Pct };
                //TableProperties tablep = new() { };
                //Justification JustificationTable = new() { Val = JustificationValues.Center };
                //tablep.Append(JustificationTable);
                //tableRC.Append(tablep);

                //var borderRCColor = "000000";

                //var tableRCProperties = new TableProperties();
                //var tableRCBorders = new TableBorders();

                //var topBorderRC = new TopBorder
                //{
                //    Val = new EnumValue<BorderValues>(BorderValues.Thick),
                //    Color = borderRCColor
                //};

                //var bottomBorderRC = new BottomBorder
                //{
                //    Val = new EnumValue<BorderValues>(BorderValues.Thick),
                //    Color = borderRCColor
                //};

                //var rightBorderRC = new RightBorder
                //{
                //    Val = new EnumValue<BorderValues>(BorderValues.Thick),
                //    Color = borderRCColor
                //};

                //var leftBorderRC = new LeftBorder
                //{
                //    Val = new EnumValue<BorderValues>(BorderValues.Thick),
                //    Color = borderRCColor
                //};

                //var insideHorizontalBorderRC = new InsideHorizontalBorder
                //{
                //    Val = new EnumValue<BorderValues>(BorderValues.Thick),
                //    Color = borderRCColor
                //};

                //var insideVerticalBorderRC = new InsideVerticalBorder
                //{
                //    Val = new EnumValue<BorderValues>(BorderValues.Thick),
                //    Color = borderRCColor
                //};

                //tableRCBorders.AppendChild(topBorderRC);
                //tableRCBorders.AppendChild(bottomBorderRC);
                //tableRCBorders.AppendChild(rightBorderRC);
                //tableRCBorders.AppendChild(leftBorderRC);
                //tableRCBorders.AppendChild(insideHorizontalBorderRC);
                //tableRCBorders.AppendChild(insideVerticalBorderRC);

                //tableRCProperties.Append(tableRCWidth);
                //tableRCProperties.AppendChild(tableRCBorders);

                //tableRC.AppendChild(tableRCProperties);

                ////contenido de la tabla
                //row1 = new TableRow();

                //cell11 = new TableCell();
                //cell11Properties = new TableCellProperties(new TableCellWidth()
                //{
                //    Type = TableWidthUnitValues.Dxa,
                //    Width = "2500",
                //});

                //shading11 = new Shading()
                //{
                //    Color = "auto",
                //    Fill = "5DADE2",
                //    Val = ShadingPatternValues.Clear
                //};
                //cell11Properties.Append(shading11);
                //paragraph11 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //paragraphProperties11 = new ParagraphProperties
                //{
                //    Justification = new Justification() { Val = JustificationValues.Center }
                //};
                //paragraph11.Append(paragraphProperties11);
                //run11 = new Run();
                //runProperties11 = new RunProperties
                //{
                //    Bold = new Bold()
                //};
                //run11.Append(runProperties11);
                //run11.Append(new DocumentFormat.OpenXml.Wordprocessing.Text("Categoría"));
                //paragraph11.Append(run11);
                //cell11.Append(cell11Properties);
                //cell11.Append(paragraph11);
                //row1.Append(cell11);


                //cell12 = new TableCell();
                //cell12Properties = new TableCellProperties();
                //shading12 = new Shading()
                //{
                //    Color = "auto",
                //    Fill = "5DADE2",
                //    Val = ShadingPatternValues.Clear
                //};
                //cell12Properties.Append(shading12);
                //paragraph12 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //paragraphProperties12 = new ParagraphProperties
                //{
                //    Justification = new Justification() { Val = JustificationValues.Center }
                //};
                //paragraph12.Append(paragraphProperties12);
                //run12 = new Run();
                //runProperties12 = new RunProperties
                //{
                //    Bold = new Bold()
                //};
                //run12.Append(runProperties12);
                //run12.Append(new DocumentFormat.OpenXml.Wordprocessing.Text("Promedio del valor actual"));
                //paragraph12.Append(run12);
                //cell12.Append(cell12Properties);
                //cell12.Append(paragraph12);
                //row1.Append(cell12);

                //tableRC.Append(row1);

                ////foreach
                //double sumaTotal = 0;
                //var tamañoTotal = 0;
                //foreach (var firstLevel in gap.IdFrameworkNavigation.FrameworkFirstlevels)
                //{
                //    double sumafirst = 0;
                //    var tamaño = 0;
                //    foreach (var secondlevel in firstLevel.FrameworkSecondlevels)
                //    {
                //        double suma = 0;
                //        foreach (var thirdlevel in secondlevel.FrameworkThirdlevels)
                //        {
                //            var gapControlResult = _gapControlResultService.GetGapControlResult(thirdlevel.Idthirdlevel, gap.IdGapAnalysis);
                //            if (gapControlResult.Apply == true && gapControlResult.Status == true)//debería ser respecto a solo los evaluados?
                //            {
                //                suma += gapControlResult.MadurityLevelValue;
                //                tamaño++;
                //            }
                //        }
                //        sumafirst += +suma;
                //    }
                //    if (tamaño > 0)
                //    {
                //        sumafirst /= tamaño;
                //    }
                //    sumaTotal += sumafirst;
                //    tamañoTotal++;
                //    row1 = new TableRow();

                //    cell11 = new TableCell();
                //    paragraph11 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //    paragraphProperties11 = new ParagraphProperties
                //    {
                //        Justification = new Justification() { Val = JustificationValues.Left }
                //    };
                //    paragraph11.Append(paragraphProperties11);
                //    run11 = new Run();
                //    run11.Append(new DocumentFormat.OpenXml.Wordprocessing.Text(firstLevel.Name));
                //    paragraph11.Append(run11);
                //    cell11.Append(paragraph11);
                //    row1.Append(cell11);

                //    cell12 = new TableCell();
                //    paragraph12 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //    paragraphProperties12 = new ParagraphProperties
                //    {
                //        Justification = new Justification() { Val = JustificationValues.Center }
                //    };
                //    paragraph12.Append(paragraphProperties12);
                //    run12 = new Run();
                //    run12.Append(new DocumentFormat.OpenXml.Wordprocessing.Text(Math.Round(sumafirst, 2).ToString()));
                //    paragraph12.Append(run12);
                //    cell12.Append(paragraph12);
                //    row1.Append(cell12);
                //    tableRC.Append(row1);
                //    //tableC.AddCell(new PdfPCell(new Phrase(firstLevel.Name)) { HorizontalAlignment = Element.ALIGN_LEFT });
                //    //tableC.AddCell(new PdfPCell(new Phrase(Math.Round(sumafirst, 2).ToString())) { HorizontalAlignment = Element.ALIGN_CENTER });
                //}
                ////--------------------
                //row1 = new TableRow();

                //cell11 = new TableCell();
                //paragraph11 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //paragraphProperties11 = new ParagraphProperties
                //{
                //    Justification = new Justification() { Val = JustificationValues.Left }
                //};
                //paragraph11.Append(paragraphProperties11);
                //run11 = new Run();
                //runProperties11 = new RunProperties
                //{
                //    Bold = new Bold()
                //};
                //run11.Append(runProperties11);
                //run11.Append(new DocumentFormat.OpenXml.Wordprocessing.Text("Promedio"));
                //paragraph11.Append(run11);
                //cell11.Append(paragraph11);
                //row1.Append(cell11);

                //cell12 = new TableCell();
                //paragraph12 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //paragraphProperties12 = new ParagraphProperties
                //{
                //    Justification = new Justification() { Val = JustificationValues.Center }
                //};
                //paragraph12.Append(paragraphProperties12);
                //run12 = new Run();
                //run12.Append(new DocumentFormat.OpenXml.Wordprocessing.Text(Math.Round(sumaTotal / tamañoTotal, 2).ToString()));
                //paragraph12.Append(run12);
                //cell12.Append(paragraph12);
                //row1.Append(cell12);
                //tableRC.Append(row1);

                //body.Append(tableRC);
                ////fin tabla de resumen de categorias
                //body.Append(new DocumentFormat.OpenXml.Wordprocessing.Paragraph(new Run(new DocumentFormat.OpenXml.Wordprocessing.Text("\n"))));

                ////--
                ////Subcategorias
                //DocumentFormat.OpenXml.Wordprocessing.Paragraph resumeSubcategory = new();
                //Run resumeSubcategoryR = new();
                //RunProperties resumeSubcategoryRP = new();
                //resumeSubcategoryRP.FontSize = new FontSize() { Val = "25" };
                //resumeSubcategoryRP.Bold = new Bold();
                //resumeSubcategoryR.Append(resumeSubcategoryRP);
                //DocumentFormat.OpenXml.Wordprocessing.Text resumeSubcategoryT = new("    - Subcategorías: ")
                //{
                //    Space = SpaceProcessingModeValues.Preserve
                //};
                //resumeSubcategoryR.Append(resumeSubcategoryT);
                //resumeSubcategory.Append(resumeSubcategoryR);
                //body.Append(resumeSubcategory);

                ////tabla resumen de subcategorias
                //var tableRS = new DocumentFormat.OpenXml.Wordprocessing.Table();
                //var tableRSWidth = new TableWidth() { Width = "3000", Type = TableWidthUnitValues.Pct };


                //TableProperties tablepRS = new() { };
                //Justification JustificationTableRS = new() { Val = JustificationValues.Center };
                //tablepRS.Append(JustificationTableRS);
                //tableRS.Append(tablepRS);


                //var borderRSColor = "000000";

                //var tableRSProperties = new TableProperties();
                //var tableRSBorders = new TableBorders();

                //var topBorderRS = new TopBorder
                //{
                //    Val = new EnumValue<BorderValues>(BorderValues.Thick),
                //    Color = borderRSColor
                //};

                //var bottomBorderRS = new BottomBorder
                //{
                //    Val = new EnumValue<BorderValues>(BorderValues.Thick),
                //    Color = borderRSColor
                //};

                //var rightBorderRS = new RightBorder
                //{
                //    Val = new EnumValue<BorderValues>(BorderValues.Thick),
                //    Color = borderRSColor
                //};

                //var leftBorderRS = new LeftBorder
                //{
                //    Val = new EnumValue<BorderValues>(BorderValues.Thick),
                //    Color = borderRSColor
                //};

                //var insideHorizontalBorderRS = new InsideHorizontalBorder
                //{
                //    Val = new EnumValue<BorderValues>(BorderValues.Thick),
                //    Color = borderRSColor
                //};

                //var insideVerticalBorderRS = new InsideVerticalBorder
                //{
                //    Val = new EnumValue<BorderValues>(BorderValues.Thick),
                //    Color = borderRSColor
                //};

                //tableRSBorders.AppendChild(topBorderRS);
                //tableRSBorders.AppendChild(bottomBorderRS);
                //tableRSBorders.AppendChild(rightBorderRS);
                //tableRSBorders.AppendChild(leftBorderRS);
                //tableRSBorders.AppendChild(insideHorizontalBorderRS);
                //tableRSBorders.AppendChild(insideVerticalBorderRS);

                //tableRSProperties.Append(tableRSWidth);
                //tableRSProperties.AppendChild(tableRSBorders);

                //tableRS.AppendChild(tableRSProperties);

                ////contenido de la tabla
                //row1 = new TableRow();

                //cell11 = new TableCell();
                //cell11Properties = new TableCellProperties();

                //shading11 = new Shading()
                //{
                //    Color = "auto",
                //    Fill = "5DADE2",
                //    Val = ShadingPatternValues.Clear
                //};
                //cell11Properties.Append(shading11);
                //paragraph11 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //paragraphProperties11 = new ParagraphProperties
                //{
                //    Justification = new Justification() { Val = JustificationValues.Center }
                //};
                //paragraph11.Append(paragraphProperties11);
                //run11 = new Run();
                //runProperties11 = new RunProperties
                //{
                //    Bold = new Bold()
                //};
                //run11.Append(runProperties11);
                //run11.Append(new DocumentFormat.OpenXml.Wordprocessing.Text("Subcategoría"));
                //paragraph11.Append(run11);
                //cell11.Append(cell11Properties);
                //cell11.Append(paragraph11);
                //row1.Append(cell11);


                //cell12 = new TableCell();
                //cell12Properties = new TableCellProperties();
                //shading12 = new Shading()
                //{
                //    Color = "auto",
                //    Fill = "5DADE2",
                //    Val = ShadingPatternValues.Clear
                //};
                //cell12Properties.Append(shading12);
                //paragraph12 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //paragraphProperties12 = new ParagraphProperties
                //{
                //    Justification = new Justification() { Val = JustificationValues.Center }
                //};
                //paragraph12.Append(paragraphProperties12);
                //run12 = new Run();
                //runProperties12 = new RunProperties
                //{
                //    Bold = new Bold()
                //};
                //run12.Append(runProperties12);
                //run12.Append(new DocumentFormat.OpenXml.Wordprocessing.Text("Promedio del valor actual"));
                //paragraph12.Append(run12);
                //cell12.Append(cell12Properties);
                //cell12.Append(paragraph12);
                //row1.Append(cell12);

                //tableRS.Append(row1);

                ////foreach
                //sumaTotal = 0;
                //tamañoTotal = 0;
                //foreach (var firstLevel in gap.IdFrameworkNavigation.FrameworkFirstlevels)
                //{
                //    foreach (var secondlevel in firstLevel.FrameworkSecondlevels)
                //    {
                //        double suma = 0;
                //        int tamaño = 0;

                //        foreach (var thirdlevel in secondlevel.FrameworkThirdlevels)
                //        {
                //            var gapControlResult = _gapControlResultService.GetGapControlResult(thirdlevel.Idthirdlevel, gap.IdGapAnalysis);
                //            if (gapControlResult.Apply == true && gapControlResult.Status == true)
                //            {
                //                suma += gapControlResult.MadurityLevelValue;
                //                tamaño++;
                //            }
                //        }
                //        if (tamaño > 0)
                //        {
                //            suma /= tamaño;
                //        }
                //        sumaTotal += suma;
                //        tamañoTotal++;
                //        row1 = new TableRow();

                //        cell11 = new TableCell();
                //        paragraph11 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //        paragraphProperties11 = new ParagraphProperties
                //        {
                //            Justification = new Justification() { Val = JustificationValues.Left }
                //        };
                //        paragraph11.Append(paragraphProperties11);
                //        run11 = new Run();
                //        run11.Append(new DocumentFormat.OpenXml.Wordprocessing.Text(secondlevel.Name));
                //        paragraph11.Append(run11);
                //        cell11.Append(paragraph11);
                //        row1.Append(cell11);

                //        cell12 = new TableCell();
                //        paragraph12 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //        paragraphProperties12 = new ParagraphProperties
                //        {
                //            Justification = new Justification() { Val = JustificationValues.Center }
                //        };
                //        paragraph12.Append(paragraphProperties12);
                //        run12 = new Run();
                //        run12.Append(new DocumentFormat.OpenXml.Wordprocessing.Text(Math.Round(suma, 2).ToString()));
                //        paragraph12.Append(run12);
                //        cell12.Append(paragraph12);
                //        row1.Append(cell12);
                //        tableRS.Append(row1);
                //        //tableS.AddCell(new Cell().SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT).Add(new iText.Layout.Element.Paragraph(secondlevel.Name)));
                //        //tableS.AddCell(new Cell().SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).Add(new iText.Layout.Element.Paragraph((Math.Round(suma, 2).ToString()))));


                //    }
                //}
                //row1 = new TableRow();

                //cell11 = new TableCell();
                //paragraph11 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //paragraphProperties11 = new ParagraphProperties
                //{
                //    Justification = new Justification() { Val = JustificationValues.Left }
                //};
                //paragraph11.Append(paragraphProperties11);
                //run11 = new Run();
                //runProperties11 = new RunProperties
                //{
                //    Bold = new Bold()
                //};
                //run11.Append(runProperties11);
                //run11.Append(new DocumentFormat.OpenXml.Wordprocessing.Text("Promedio"));
                //paragraph11.Append(run11);
                //cell11.Append(paragraph11);
                //row1.Append(cell11);

                //cell12 = new TableCell();
                //paragraph12 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                //paragraphProperties12 = new ParagraphProperties
                //{
                //    Justification = new Justification() { Val = JustificationValues.Center }
                //};
                //paragraph12.Append(paragraphProperties12);
                //run12 = new Run();
                //run12.Append(new DocumentFormat.OpenXml.Wordprocessing.Text(Math.Round(sumaTotal / tamañoTotal, 2).ToString()));
                //paragraph12.Append(run12);
                //cell12.Append(paragraph12);
                //row1.Append(cell12);
                //tableRS.Append(row1);
                ////tableS.AddCell(new Cell().SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT).Add(new iText.Layout.Element.Paragraph("Promedio").SetBold()));
                ////tableS.AddCell(new Cell().SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).Add(new iText.Layout.Element.Paragraph((Math.Round(sumaTotal / tamañoTotal, 2).ToString()))));
                ////--

                //body.Append(tableRS);
                ////FIN RESUMEN DE RESULTADOS
                body.Append(sectProp);
                word.Close();
                byte[] bytesStream = ms.ToArray();
                ms = new MemoryStream();
                ms.Write(bytesStream, 0, bytesStream.Length);
                ms.Position = 0;
                return File(ms, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "1. "+companyG.TradeName+" - EH EG 2021 - Plan de Trabajo v2.0"+".docx");




                //var token = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Remove(0, 7));
                //if (_validationUserPermissions.RolePermissionValidation(token, _configuration["Permissions:DW_Pdf"].ToString())
                //    || _validationUserPermissions.UserPermissionValidation(token, _configuration["Permissions:DW_Pdf"].ToString()))
                //{

                //}
                //else
                //{
                //    //no se puede retornar un msj
                //    return BadRequest("No tiene permiso para realizar esta operación");
                //}
            }
            catch (Exception e)
            {
                return BadRequest(e);
                //throw new BusinessException("Token no ingresado");

            }
        }
    }

    public class HeaderEventHandler : IEventHandler
    {
        //private readonly IGapAnalyzeService _gapAnalyzeService;
        private readonly ICompanyService _companyService;
        private readonly IBlobManagement _blobManagement;
        public int IdGapAnalyze { get; set; }
        public HeaderEventHandler(/*IGapAnalyzeService gapAnalyzeService, */ICompanyService companyService, IBlobManagement blobManagement)
        {
            //_gapAnalyzeService = gapAnalyzeService;
            _companyService = companyService;
            _blobManagement = blobManagement;
        }
        public void HandleEvent(Event @event)
        {
            PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
            PdfDocument pdfDoc = docEvent.GetDocument();
            PdfPage page = docEvent.GetPage();

            PdfCanvas canvas = new(page.NewContentStreamBefore(), page.GetResources(), pdfDoc);
            Rectangle rootArea = new(35, page.GetPageSize().GetTop() - 100, page.GetPageSize().GetRight() - 70, 90);
            Canvas canvasEncabezado = new(canvas, rootArea, true);
            canvasEncabezado.Add(GetTable(docEvent));
        }
        public iText.Layout.Element.Table GetTable(PdfDocumentEvent docEvent)
        {
            //var gap = _gapAnalyzeService.GetGapAnalyzeEvaluation(IdGapAnalyze);
            var company = _companyService.GetCompany(2).TradeName;
            var kunakG = _companyService.GetByRucSync("20546970826");
            float[] cellWidth = { 14f, 56f, 16f, 16f };
            iText.Layout.Element.Table tabFot = new iText.Layout.Element.Table(UnitValue.CreatePercentArray(cellWidth)).UseAllAvailableWidth();


            String route = _blobManagement.GetLink(kunakG.Ruc, kunakG.Logo);
            Image kunak = new Image(ImageDataFactory
            .Create(route))
            .SetHorizontalAlignment(HorizontalAlignment.CENTER);
            kunak.ScaleToFit(50, 50);

            PdfPage page = docEvent.GetPage();
            var pageNum = docEvent.GetDocument().GetPageNumber(page);

            if (pageNum > 1)
            {
                tabFot.AddCell(new Cell(4, 1).Add(kunak).SetVerticalAlignment(VerticalAlignment.MIDDLE));
                tabFot.AddCell(new Cell(2, 1).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new iText.Layout.Element.Paragraph("Análisis de brechas")
                    .SetFontSize(8)
                    .SetBold()
                    ).SetHeight(11));
                tabFot.AddCell(new Cell().Add(new iText.Layout.Element.Paragraph("Código:").SetFontSize(8)).SetHeight(11));
                tabFot.AddCell(new Cell().SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new iText.Layout.Element.Paragraph("GAP-" + company).SetFontSize(8)).SetHeight(11));
                tabFot.AddCell(new Cell().Add(new iText.Layout.Element.Paragraph("Versión:").SetFontSize(8)).SetHeight(11));
                tabFot.AddCell(new Cell().SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new iText.Layout.Element.Paragraph("1.0").SetFontSize(8)).SetHeight(11));
                tabFot.AddCell(new Cell(2, 1).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new iText.Layout.Element.Paragraph("gap.Name")
                    .SetFontSize(8)
                    ).SetHeight(11));
                tabFot.AddCell(new Cell().Add(new iText.Layout.Element.Paragraph("Clasificación:").SetFontSize(8)).SetHeight(11));
                tabFot.AddCell(new Cell().SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new iText.Layout.Element.Paragraph("Uso interno").SetFontSize(8)).SetHeight(11));
                tabFot.AddCell(new Cell().Add(new iText.Layout.Element.Paragraph("Página:").SetFontSize(8)).SetHeight(11));
                tabFot.AddCell(new Cell().SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new iText.Layout.Element.Paragraph(pageNum.ToString()).SetFontSize(8)).SetHeight(11));
            }

            return tabFot;
        }
    }

    public class FooterEventHandler : IEventHandler
    {

        public FooterEventHandler()
        {

        }
        public void HandleEvent(Event @event)
        {
            PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
            PdfDocument pdfDoc = docEvent.GetDocument();
            PdfPage page = docEvent.GetPage();

            PdfCanvas canvas = new(page.NewContentStreamBefore(), page.GetResources(), pdfDoc);
            Rectangle rootArea = new(36, 20, page.GetPageSize().GetRight() - 70, 10);
            Canvas canvasEncabezado = new(canvas, rootArea, true);
            canvasEncabezado.Add(GetTable(docEvent));
        }
        public iText.Layout.Element.Table GetTable(PdfDocumentEvent docEvent)
        {
            float[] cellWidth = { 100f };
            iText.Layout.Element.Table tabFot = new iText.Layout.Element.Table(UnitValue.CreatePercentArray(cellWidth)).UseAllAvailableWidth();

            PdfPage page = docEvent.GetPage();
            var pageNum = docEvent.GetDocument().GetPageNumber(page);

            if (pageNum > 1)
            {
                tabFot.AddCell(new Cell().SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                   .SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new iText.Layout.Element.Paragraph("RISK MANAGEMENT")
                   //.SetFontSize(8)
                   )
                   //.SetHeight(11)
                   );
            }
            return tabFot;
        }
    }


}
