using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using para la imagen
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using A = DocumentFormat.OpenXml.Drawing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;
using System.Net;
using KUNAK.VMS.API.Interfaces;

namespace KUNAK.VMS.API.Methods
{
    public class Report :IReport
    {
        public Report()
        {

        }
        //public Stream FromImageUrlToStream(string imgUrl)
        //{
        //    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(imgUrl);
        //    req.UseDefaultCredentials = true;
        //    req.PreAuthenticate = true;
        //    req.Credentials = CredentialCache.DefaultCredentials;
        //    HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
        //    return resp.GetResponseStream();
        //}

        // To insert the picture
        public Drawing DrawingManager(string relationshipId, string name, Int64Value cxVal, Int64Value cyVal, string impPosition)
        {
            string haPosition = impPosition;
            if (string.IsNullOrEmpty(haPosition))
            {
                haPosition = "center";
            }
            // Define the reference of the image.
            DW.Anchor anchor = new();
            anchor.Append(new DW.SimplePosition() { X = 0L, Y = 0L });
            anchor.Append(
                new DW.HorizontalPosition(
                    new DW.HorizontalAlignment(haPosition)
                )
                {
                    RelativeFrom =
                      DW.HorizontalRelativePositionValues.Margin
                }
            );
            anchor.Append(
                new DW.VerticalPosition(
                    new DW.PositionOffset("0")
                )
                {
                    RelativeFrom =
                    DW.VerticalRelativePositionValues.Paragraph
                }
            );
            anchor.Append(
                new DW.Extent()
                {
                    Cx = cxVal,
                    Cy = cyVal
                }
            );
            anchor.Append(
                new DW.EffectExtent()
                {
                    LeftEdge = 0L,
                    TopEdge = 0L,
                    RightEdge = 0L,
                    BottomEdge = 0L
                }
            );
            if (!string.IsNullOrEmpty(impPosition))
            {
                anchor.Append(new DW.WrapSquare() { WrapText = DW.WrapTextValues.BothSides });
            }
            else
            {
                anchor.Append(new DW.WrapTopBottom());
            }
            anchor.Append(
                new DW.DocProperties()
                {
                    Id = (UInt32Value)1U,
                    Name = name
                }
            );
            anchor.Append(
                new DW.NonVisualGraphicFrameDrawingProperties(
                      new A.GraphicFrameLocks() { NoChangeAspect = true })
            );
            anchor.Append(
                new A.Graphic(
                      new A.GraphicData(
                        new PIC.Picture(

                          new PIC.NonVisualPictureProperties(
                            new PIC.NonVisualDrawingProperties()
                            {
                                Id = (UInt32Value)0U,
                                Name = name + ".jpg"
                            },
                            new PIC.NonVisualPictureDrawingProperties()),

                            new PIC.BlipFill(
                                new A.Blip(
                                    new A.BlipExtensionList(
                                        new A.BlipExtension()
                                        {
                                            Uri =
                                            "{28A0092B-C50C-407E-A947-70E740481C1C}"
                                        })
                                )
                                {
                                    Embed = relationshipId,
                                    CompressionState =
                                    A.BlipCompressionValues.Print
                                },
                                new A.Stretch(
                                    new A.FillRectangle())),

                          new PIC.ShapeProperties(

                            new A.Transform2D(
                              new A.Offset() { X = 0L, Y = 0L },

                              new A.Extents()
                              {
                                  Cx = cxVal,
                                  Cy = cyVal
                              }),

                            new A.PresetGeometry(
                              new A.AdjustValueList()
                            )
                            { Preset = A.ShapeTypeValues.Rectangle }
                          )
                        )
                  )
                      { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" })
            );

            anchor.DistanceFromTop = (UInt32Value)0U;
            anchor.DistanceFromBottom = (UInt32Value)0U;
            anchor.DistanceFromLeft = (UInt32Value)114300U;
            anchor.DistanceFromRight = (UInt32Value)114300U;
            anchor.SimplePos = false;
            anchor.RelativeHeight = (UInt32Value)251658240U;
            anchor.BehindDoc = true;
            anchor.Locked = false;
            anchor.LayoutInCell = true;
            anchor.AllowOverlap = true;

            Drawing element = new();
            element.Append(anchor);

            return element;
        }

        public void AddImageToCell(TableCell cell, string relationshipId)
        {
            var element =
              new Drawing(
                new DW.Inline(
                  new DW.Extent() { Cx = 990000L, Cy = 792000L },
                  new DW.EffectExtent()
                  {
                      LeftEdge = 0L,
                      TopEdge = 0L,
                      RightEdge = 0L,
                      BottomEdge = 0L
                  },
                  new DW.DocProperties()
                  {
                      Id = (UInt32Value)1U,
                      Name = "Picture 1"
                  },
                  new DW.NonVisualGraphicFrameDrawingProperties(
                      new A.GraphicFrameLocks() { NoChangeAspect = true }),
                  new A.Graphic(
                    new A.GraphicData(
                      new PIC.Picture(
                        new PIC.NonVisualPictureProperties(
                          new PIC.NonVisualDrawingProperties()
                          {
                              Id = (UInt32Value)0U,
                              Name = "New Bitmap Image.jpg"
                          },
                          new PIC.NonVisualPictureDrawingProperties()),
                        new PIC.BlipFill(
                          new A.Blip(
                            new A.BlipExtensionList(
                              new A.BlipExtension()
                              {
                                  Uri = "{28A0092B-C50C-407E-A947-70E740481C1C}"
                              })
                           )
                          {
                              Embed = relationshipId,
                              CompressionState =
                              A.BlipCompressionValues.Print
                          },
                          new A.Stretch(
                            new A.FillRectangle())),
                          new PIC.ShapeProperties(
                            new A.Transform2D(
                              new A.Offset() { X = 0L, Y = 0L },
                              new A.Extents() { Cx = 990000L, Cy = 792000L }),
                            new A.PresetGeometry(
                              new A.AdjustValueList()
                            )
                            { Preset = A.ShapeTypeValues.Rectangle }))
                    )
                    { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" })
                )
                {
                    DistanceFromTop = (UInt32Value)0U,
                    DistanceFromBottom = (UInt32Value)0U,
                    DistanceFromLeft = (UInt32Value)0U,
                    DistanceFromRight = (UInt32Value)0U
                });

            cell.Append(new Paragraph(new Run(element)));
        }
        //HEADER AND FOOTER
        public Document GenerateMainDocumentPart()
        {
            var element =
                new Document(
                    new Body(
                        new Paragraph(
                            new Run(
                                new Text("Page 1 content"))
                        ),
                        new Paragraph(
                            new Run(
                                new Break() { Type = BreakValues.Page })
                        ),
                        new Paragraph(
                            new Run(
                                new LastRenderedPageBreak(),
                                new Text("Page 2 content"))
                        ),
                        new Paragraph(
                            new Run(
                                new Break() { Type = BreakValues.Page })
                        ),
                        new Paragraph(
                            new Run(
                                new LastRenderedPageBreak(),
                                new Text("Page 3 content"))
                        ),
                        new Paragraph(
                            new Run(
                                new Break() { Type = BreakValues.Page })
                        ),
                        new Paragraph(
                            new Run(
                                new LastRenderedPageBreak(),
                                new Text("Page 4 content"))
                        ),
                        new Paragraph(
                            new Run(
                                new Break() { Type = BreakValues.Page })
                        ),
                        new Paragraph(
                            new Run(
                                new LastRenderedPageBreak(),
                                new Text("Page 5 content"))
                        ),
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
                        )));

            return element;
        }

        public Footer GeneratePageFooterPart(string FooterText)
        {
            var element =
                new Footer(
                    new Paragraph(
                        new ParagraphProperties(
                            new ParagraphStyleId() { Val = "Footer" }),
                        new Run(
                            new Text(FooterText))
                    ));

            return element;
        }

        public Header GeneratePageHeaderPart(string HeaderText)
        {
            var element =
                new Header(
                    new Paragraph(
                        new ParagraphProperties(
                            new ParagraphStyleId() { Val = "Header" }),
                        new Run(
                            new Text(HeaderText))
                    ));

            return element;
        }

        public Settings GenerateDocumentSettingsPart()
        {
            var element =
                new Settings(new EvenAndOddHeaders());

            return element;
        }
        //--------------
    }
}
