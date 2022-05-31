//using para la imagen
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
namespace KUNAK.VMS.API.Interfaces
{
    public interface IReport
    {
        //Stream FromImageUrlToStream(string imgUrl);
        Drawing DrawingManager(string relationshipId, string name, Int64Value cxVal, Int64Value cyVal, string impPosition);
        Document GenerateMainDocumentPart();
        Footer GeneratePageFooterPart(string FooterText);
        Header GeneratePageHeaderPart(string HeaderText);
        Settings GenerateDocumentSettingsPart();
        void AddImageToCell(TableCell cell, string relationshipId);
    }
}
